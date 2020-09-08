//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UAOOI.Networking.Core;
using UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection;
using UAOOI.Networking.ReferenceApplication.Core;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  /// <summary>
  /// Class AzureGatewayDataManagementSetup - represents a data producer in the Reference Application. It is responsible to compose all parts making up a producer
  /// This class cannot be inherited.
  /// Implements the <see cref="DataManagementSetup" />
  /// Implements the <see cref="IProducerDataManagementSetup" />
  /// </summary>
  /// <seealso cref="DataManagementSetup" />
  /// <seealso cref="IProducerDataManagementSetup" />
  [Export(typeof(IProducerDataManagementSetup))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public sealed class PartDataManagementSetup : DataManagementSetup, IProducerDataManagementSetup
  {
    #region Composition

    /// <summary>
    /// Initializes a new instance of the <see cref="PartDataManagementSetup"/> class.
    /// </summary>
    public PartDataManagementSetup()
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      //string _configurationFileName = _serviceLocator.GetInstance<string>(CompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<ProducerViewModel>();
      ConfigurationFactory = new PartConfigurationFactory(ConfigurationFilePath);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      PartBindingFactory pbf = new PartBindingFactory();
      _DTOProvider = pbf;
      BindingFactory = pbf;
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
    }

    internal static string ConfigurationFilePath { get; set; } = @"ConfigurationDataConsumer.BoilersSet.xml";

    #endregion Composition

    #region IProducerDataManagementSetup

    private readonly ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();
    private CancellationTokenSource _tokenSource = new CancellationTokenSource();
    private CancellationToken token;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    public void Setup()
    {
      try
      {
        //ReferenceApplicationEventSource.Log.Initialization($"{nameof(SimulatorDataManagementSetup)}.{nameof(Setup)} starting");
        token = _tokenSource.Token;
        m_ViewModel.ChangeProducerCommand(() => { m_ViewModel.ProducerErrorMessage = "Restarted"; });
        Start();
        StartAzureCommunication();
      }
      catch (Exception _ex)
      {
        //ReferenceApplicationEventSource.Log.LogException(_ex);
        m_ViewModel.ProducerErrorMessage = "ERROR";
        Dispose();
      }
    }

    #endregion IProducerDataManagementSetup

    #region IDisposable

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      m_onDispose(disposing);
      base.Dispose(disposing);
      if (!disposing || m_disposed)
        return;
      m_disposed = true;
      _tokenSource.Cancel();
      try
      {
        Task.WhenAll(_tasks.ToArray()).Wait();
      }
      catch (OperationCanceledException)
      {
        Console.WriteLine($"\n{nameof(OperationCanceledException)} thrown\n"); //TODO Replace by Log
      }
      finally
      {
        _tokenSource.Dispose();
      }
    }

    #endregion IDisposable

    #region private

    /// <summary>
    /// Gets or sets the view model to be used for diagnostic purpose..
    /// </summary>
    /// <value>The view model.</value>
    private ProducerViewModel m_ViewModel;

    /// <summary>
    /// Gets a value indicating whether this <see cref="PartDataManagementSetup"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    private bool m_disposed = false;

    private readonly IDTOProvider _DTOProvider = null;

    private Action<bool> m_onDispose = disposing => { };

    private void StartAzureCommunication()
    {
      List<CommunicationContext> azureComunicationContextList = new List<CommunicationContext>();
      TaskFactory taskFactory = Task.Factory;
      foreach (string repository in _DTOProvider)
      {
        CommunicationContext communicationContext = new CommunicationContext(_DTOProvider, repository, null, null);
        azureComunicationContextList.Add(communicationContext);
        Task newCommunicatinTask = taskFactory.StartNew(() => communicationContext.Run(token), token);
        _tasks.Add(newCommunicatinTask);
      }
      m_ViewModel.ProducerErrorMessage = "Running";
      //ReferenceApplicationEventSource.Log.Initialization($" Setup of the producer engine has been accomplished and it starts sending data.");
    }

    #endregion private

    #region Unit tests instrumentation

    [Conditional("DEBUG")]
    internal void DisposeCheck(Action<bool> onDispose)
    {
      m_onDispose = onDispose;
    }

    #endregion Unit tests instrumentation
  }
}