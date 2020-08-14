//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.ReferenceApplication.Core;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  /// <summary>
  /// Class AzureGatewayDataManagementSetup - represents a data producer in the Reference Application. It is responsible to compose all parts making up a producer
  /// This class cannot be inherited.
  /// Implements the <see cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
  /// Implements the <see cref="UAOOI.Networking.ReferenceApplication.Core.IProducerDataManagementSetup" />
  /// </summary>
  /// <seealso cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
  /// <seealso cref="UAOOI.Networking.ReferenceApplication.Core.IProducerDataManagementSetup" />
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
      string _configurationFileName = _serviceLocator.GetInstance<string>(CompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<ProducerViewModel>();
      ConfigurationFactory = new PartConfigurationFactory(_configurationFileName);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      BindingFactory = new PartBindingFactory();
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
    }

    #endregion Composition

    #region IProducerDataManagementSetup

    /// <summary>
    /// Setups this instance.
    /// </summary>
    public void Setup()
    {
      try
      {
        //ReferenceApplicationEventSource.Log.Initialization($"{nameof(SimulatorDataManagementSetup)}.{nameof(Setup)} starting");
        m_ViewModel.ChangeProducerCommand(() => { m_ViewModel.ProducerErrorMessage = "Restarted"; });
        Start();
        m_ViewModel.ProducerErrorMessage = "Running";
        //ReferenceApplicationEventSource.Log.Initialization($" Setup of the producer engine has been accomplished and it starts sending data.");
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
    }

    #endregion IDisposable

    #region private

    /// <summary>
    /// Gets or sets the view model to be used for diagnostic purpose..
    /// </summary>
    /// <value>The view model.</value>
    private ProducerViewModel m_ViewModel;

    /// <summary>
    /// Gets a value indicating whether this <see cref="LoggerManagementSetup"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    private bool m_disposed = false;

    private Action<bool> m_onDispose = disposing => { };

    #endregion private

    #region Unit tests instrumentation

    [Conditional("DEBUG")]
    internal void DisposeCheck(Action<bool> onDispose)
    {
      m_onDispose = onDispose;
    }

    #endregion Unit tests instrumentation

    //TODO Implement DataManagementSetup #450
    private class PartConfigurationFactory : IConfigurationFactory
    {
      private readonly string _configurationFileName;

      public PartConfigurationFactory(string configurationFileName)
      {
        _configurationFileName = configurationFileName;
      }

      #region IConfigurationFactory

      public event EventHandler<EventArgs> OnAssociationConfigurationChange;

      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

      public ConfigurationData GetConfiguration()
      {
        throw new NotImplementedException();
      }

      #endregion IConfigurationFactory
    }

    private class PartBindingFactory : IBindingFactory
    {
      #region IBindingFactory

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        throw new NotImplementedException();
      }

      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        throw new NotImplementedException();
      }

      #endregion IBindingFactory
    }
  }
}