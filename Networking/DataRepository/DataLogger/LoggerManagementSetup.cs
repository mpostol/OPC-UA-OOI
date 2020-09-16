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
  /// Class <see cref="LoggerManagementSetup" /> - custom implementation of the <seealso cref="DataManagementSetup" />
  /// This class cannot be inherited.
  /// Implements the <see cref="DataManagementSetup" />
  /// </summary>
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
      _logger.EnteringMethodPart();
      // get external parts to compose
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      string _ConsumerConfigurationFileName = _serviceLocator.GetInstance<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);
      _ViewModel = _serviceLocator.GetInstance<ConsumerViewModel>(ConsumerCompositionSettings.ViewModelContract);
      _logger.Composed(nameof(_ViewModel), _ViewModel.GetType().FullName);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      _logger.Composed(nameof(EncodingFactory), EncodingFactory.GetType().FullName);
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
      _logger.Composed(nameof(MessageHandlerFactory), MessageHandlerFactory.GetType().FullName);
      // setup local functionality
      ConfigurationFactory = new ConsumerConfigurationFactory(_ConsumerConfigurationFileName);
      _logger.Composed(nameof(ConfigurationFactory), ConfigurationFactory.GetType().FullName);
      BindingFactory = new PartIBindingFactory(_ViewModel);
      _logger.Composed(nameof(BindingFactory), BindingFactory.GetType().FullName);
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
        _logger.EnteringMethodPart();
        _ViewModel.ChangeProducerCommand(Restart);
        Start();
        _ViewModel.ConsumerErrorMessage = "Running";
        _logger.PartInitializationCompleted();
      }
      catch (Exception _ex)
      {
        _logger.LogException(nameof(LoggerManagementSetup), _ex);
        _ViewModel.ConsumerErrorMessage = "ERROR";
        throw;
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
      _logger.DisposingObject(nameof(LoggerManagementSetup));
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
    private ConsumerViewModel _ViewModel;

    private readonly DataLoggerEventSource _logger = DataLoggerEventSource.Log();

    /// <summary>
    /// Gets a value indicating whether this <see cref="LoggerManagementSetup"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    private bool m_disposed = false;

    private Action<bool> m_onDispose = disposing => { };

    private void Restart()
    {
      _logger.EnteringMethodPart();
      _ViewModel.Trace("Entering Restart");
      Start();
    }

    #endregion private

    #region Unit tests instrumentation

    [Conditional("DEBUG")]
    internal void DisposeCheck(Action<bool> onDispose)
    {
      _logger.EnteringMethodPart();
      m_onDispose = onDispose;
    }

    #endregion Unit tests instrumentation
  }
}