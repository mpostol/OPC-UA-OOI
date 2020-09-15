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
using UAOOI.Networking.Core;
using UAOOI.Networking.DataRepository.DataLogger.Diagnostic;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.DataRepository.DataLogger
{
  /// <summary>
  /// Class ConsumerDataManagementSetup - custom implementation of the <seealso cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
  /// This class cannot be inherited.
  /// Implements the <see cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
  /// </summary>
  /// <seealso cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
  /// <seealso cref="DataManagementSetup" />
  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public sealed class LoggerManagementSetup : DataManagementSetup
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggerManagementSetup"/> class.
    /// </summary>
    public LoggerManagementSetup()
    {
      // get external parts to compose
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      string _ConsumerConfigurationFileName = _serviceLocator.GetInstance<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<ConsumerViewModel>(ConsumerCompositionSettings.ViewModelContract);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
      // setup local functionality
      ConfigurationFactory = new ConsumerConfigurationFactory(_ConsumerConfigurationFileName);
      BindingFactory = new PartIBindingFactory(m_ViewModel);
    }

    #endregion constructor

    #region API

    /// <summary>
    /// Setups this instance.
    /// </summary>
    public void Setup()
    {
      try
      {
        _logger.Initialization($"{nameof(LoggerManagementSetup)}.{nameof(Setup)} starting");
        m_ViewModel.ChangeProducerCommand(Restart);
        Start();
        m_ViewModel.ConsumerErrorMessage = "Running";
        _logger.Initialization($" consumer engine and starting receiving data accomplished");
      }
      catch (Exception _ex)
      {
        _logger.LogException(_ex);
        m_ViewModel.ConsumerErrorMessage = "ERROR";
        Dispose();
      }
    }

    private readonly DataLoggerEventSource _logger = DataLoggerEventSource.Log();

    #endregion API

    #region IDisposable

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      _logger.EnteringDispose(nameof(LoggerManagementSetup), disposing);
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
    private ConsumerViewModel m_ViewModel;

    /// <summary>
    /// Gets a value indicating whether this <see cref="LoggerManagementSetup"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    private bool m_disposed = false;

    private Action<bool> m_onDispose = disposing => { };

    private void Restart()
    {
      m_ViewModel.Trace("Entering Restart");
      Start();
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