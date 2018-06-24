
using System;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking;
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

    #region Composition
    [Import(ConsumerCompositionSettings.ViewModelContract, typeof(ConsumerViewModel))]
    internal ConsumerViewModel ViewModel
    {
      get; set;
    }
    /// <summary>
    /// Sets the producer configuration factory.
    /// </summary>
    /// <value>The producer configuration factory.</value>
    [Import(ConsumerCompositionSettings.ConfigurationFactoryContract, typeof(IConfigurationFactory))]
    public IConfigurationFactory ProducerConfigurationFactory
    {
      set { ConfigurationFactory = value; }
    }
    /// <summary>
    /// Sets the producer encoding factory.
    /// </summary>
    /// <value>The producer encoding factory.</value>
    [Import(typeof(IEncodingFactory))]
    public IEncodingFactory ProducerEncodingFactory
    {
      set { EncodingFactory = value; }
    }
    /// <summary>
    /// Sets the producer binding factory.
    /// </summary>
    /// <value>The producer binding factory.</value>
    [Import(ConsumerCompositionSettings.BindingFactoryContract, typeof(IBindingFactory))]
    public IBindingFactory ProducerBindingFactory
    {
      set { BindingFactory = value; }
    }
    /// <summary>
    /// Sets the producer message handler factory.
    /// </summary>
    /// <value>An instance of the <see cref="IMessageHandlerFactory"/> The producer message handler factory.</value>
    [Import(typeof(IMessageHandlerFactory))]
    public IMessageHandlerFactory ProducerMessageHandlerFactory
    {
      set { MessageHandlerFactory = value; }
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
        ViewModel.ChangeProducerCommand(Restart);
        Start();
        ViewModel.ConsumerErrorMessage = "Running";
        ReferenceApplicationEventSource.Log.Initialization($" consumer engine and starting receiving data acomplished");
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.LogException(_ex);
        ViewModel.ConsumerErrorMessage = "ERROR";
        Dispose();
      }
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(LoggerManagementSetup), disposing);
      base.Dispose(disposing);
      if (!disposing || m_Disposed)
        return;
    }
    #endregion

    #region private
    private bool m_Disposed = false;
    private void Restart()
    {
      ViewModel.Trace("Entering Restart");
      Start();
    }
    #endregion

  }
}


