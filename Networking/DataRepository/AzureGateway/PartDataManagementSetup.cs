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
  /// Class AzureGatewayDataManagementSetup.
  /// Implements the <see cref="DataManagementSetup" />
  /// </summary>
  [Export(typeof(IProducerDataManagementSetup))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class PartDataManagementSetup : DataManagementSetup, IProducerDataManagementSetup
  {

    #region Composition
    /// <seealso cref="DataManagementSetup" />
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

    #region API
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
    #endregion API

    #region IDisposable
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      m_onDispose(disposing);
      base.Dispose(disposing);
      if (disposing)
        ;
    }
    #endregion IDisposable

    #region private
    /// <summary>
    /// Gets or sets the view model to be used for diagnostic purpose..
    /// </summary>
    /// <value>The view model.</value>
    private ProducerViewModel m_ViewModel;

    private Action<bool> m_onDispose = disposing => { };

    #endregion private

    #region Unit tests instrumentation

    [Conditional("DEBUG")]
    internal void DisposeCheck(Action<bool> onDispose)
    {
      m_onDispose = onDispose;
    }
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
      #endregion
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
      #endregion
    }
    #endregion Unit tests instrumentation
  }
}