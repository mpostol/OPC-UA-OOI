
using CommonServiceLocator;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using UAOOI.Networking.ReferenceApplication.Core.Diagnostic;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.DataLogger
{

  /// <summary>
  /// Class ConsumerDataManagementSetup - custom implementation of the <seealso cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
  /// This class cannot be inherited.
  /// </summary>
  /// <seealso cref="UAOOI.Networking.SemanticData.DataManagementSetup" />
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
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      string _ConsumerConfigurationFileName = _serviceLocator.GetInstance<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<ConsumerViewModel>(ConsumerCompositionSettings.ViewModelContract);
      ConfigurationFactory = new ConsumerConfigurationFactory(_ConsumerConfigurationFileName);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      BindingFactory = new DataConsumer(m_ViewModel);
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
    }
    #endregion

    #region API
    /// <summary>
    /// Setups this instance.
    /// </summary>
    public void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(LoggerManagementSetup)}.{nameof(Setup)} starting");
        m_ViewModel.ChangeProducerCommand(Restart);
        Start();
        m_ViewModel.ConsumerErrorMessage = "Running";
        ReferenceApplicationEventSource.Log.Initialization($" consumer engine and starting receiving data acomplished");
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.LogException(_ex);
        m_ViewModel.ConsumerErrorMessage = "ERROR";
        Dispose();
      }
    }
    /// <summary>
    /// Gets a value indicating whether this <see cref="LoggerManagementSetup"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    public bool Disposed { get; private set; } = false;
    #endregion

    #region IDisposable
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(LoggerManagementSetup), disposing);
      m_onDispose(disposing);
      base.Dispose(disposing);
      if (!disposing || Disposed)
        return;
      Disposed = true;
    }
    #endregion

    #region private
    private ConsumerViewModel m_ViewModel;
    private Action<bool> m_onDispose = disposing => { };
    private void Restart()
    {
      m_ViewModel.Trace("Entering Restart");
      Start();
    }
    #endregion

    #region Unit tests instrumentation
    [Conditional("DEBUG")]
    internal void DisposeCheck(Action<bool> onDispose)
    {
      m_onDispose = onDispose;
    }
    #endregion

  }
}


