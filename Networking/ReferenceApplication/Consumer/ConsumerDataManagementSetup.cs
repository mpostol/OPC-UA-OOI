
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using UAOOI.Networking.ReferenceApplication.Diagnostic;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.ReferenceApplication.Consumer
{
  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  internal sealed class ConsumerDataManagementSetup : DataManagementSetup
  {

    #region Composition
    [Import(typeof(IConsumerViewModel))]
    internal IConsumerViewModel ViewModel
    {
      get; set;
    }
    [Import(typeof(IMessageHandlerFactory))]
    public IMessageHandlerFactory ProducerMessageHandlerFactory
    {
      set { MessageHandlerFactory = value; }
    }
    #endregion

    #region API
    internal void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(ConsumerDataManagementSetup)}.{nameof(Setup)} starting");
        ViewModel.ConsumerUpdateConfiguration = new RestartCommand(Restart); //TODO Remove reference of ConsumerDataManagementSetup System.Windows  #239
        ConfigurationFactory = new ConsumerConfigurationFactory();
        MainWindowModel _model = new MainWindowModel() { ViewModelBindingFactory = ViewModel };
        BindingFactory = _model;
        EncodingFactory = _model;
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
    protected override void Dispose(bool disposing)
    {
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(ConsumerDataManagementSetup), disposing);
      base.Dispose(disposing);
      if (!disposing || m_Disposed)
        return;
    }
    #endregion

    #region private
    private class RestartCommand : ICommand
    {
      public RestartCommand(Action restart)
      {
        m_restart = restart;
      }
      /// <summary>
      /// Occurs when changes occur that affect whether or not the command should execute.
      /// </summary>
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        m_restart();
      }
      private Action m_restart;
    }
    private bool m_Disposed = false;
    private void Restart()
    {
      ViewModel.Trace("Entering Restart");
      Start();
    }
    #endregion

  }
}


