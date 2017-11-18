
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.UDPMessageHandler;

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
    #endregion

    #region API
    internal void Setup()
    {
      try
      {
        ViewModel.Trace("Entering Setup");
        ViewModel.ConsumerUpdateConfiguration = new RestartCommand(Restart);
        ConfigurationFactory = new ConsumerConfigurationFactory();
        MessageHandlerFactory = new MessageHandlerFactory(ViewModel.Trace);
        MainWindowModel _model = new MainWindowModel() { ViewModelBindingFactory = ViewModel };
        BindingFactory = _model;
        EncodingFactory = _model;
        ViewModel.Trace("Initialize consumer engine and start receiving UDP frames.");
        Start();
        ViewModel.ConsumerErrorMessage = "Running";
      }
      catch (Exception ex)
      {
        string _errorMessage = $"Error: {ex.Message}";
        ViewModel.Trace(_errorMessage);
        ViewModel.ConsumerErrorMessage = _errorMessage;
        Dispose();
      }
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
    private void Restart()
    {
      ViewModel.Trace("Entering Restart");
      Start();
    }
    #endregion

  }
}
