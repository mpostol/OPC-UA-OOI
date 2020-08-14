//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using UAOOI.Networking.Core;
using UAOOI.Networking.ReferenceApplication.Core;
using UAOOI.Networking.ReferenceApplication.Core.Diagnostic;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.Simulator.Boiler
{
  /// <summary>
  /// Class SimulatorDataManagementSetup represents a data producer in the Reference Application. It is responsible to compose all parts making up a producer.
  /// </summary>
  [Export(typeof(IProducerDataManagementSetup))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public sealed class SimulatorDataManagementSetup : DataManagementSetup, IProducerDataManagementSetup
  {
    #region Composition

    /// <summary>
    /// Initializes a new instance of the <see cref="SimulatorDataManagementSetup"/> class.
    /// </summary>
    public SimulatorDataManagementSetup()
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      string _configurationFileName = _serviceLocator.GetInstance<string>(CompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<ProducerViewModel>();
      ConfigurationFactory = new ProducerConfigurationFactory(_configurationFileName);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      BindingFactory = m_DataGenerator = new DataGenerator();
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
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(SimulatorDataManagementSetup)}.{nameof(Setup)} starting");
        m_ViewModel.ChangeProducerCommand(() => { m_ViewModel.ProducerErrorMessage = "Restarted"; });
        Start();
        m_ViewModel.ProducerErrorMessage = "Running";
        ReferenceApplicationEventSource.Log.Initialization($" Setup of the producer engine has been accomplished and it starts sending data.");
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.LogException(_ex);
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
      if (!disposing || m_disposed)
        return;
      m_disposed = true;
      m_DataGenerator.Dispose();
    }

    #endregion IDisposable

    #region private

    /// <summary>
    /// Gets or sets the view model to be used for diagnostic purpose..
    /// </summary>
    /// <value>The view model.</value>
    private ProducerViewModel m_ViewModel;

    private DataGenerator m_DataGenerator = null;

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
  }
}