
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
  internal sealed class ConsumerDataManagementSetup : DataManagementSetup, IDisposable
  {

    #region Composition
    [Import(typeof(IConsumerViewModel))]
    internal IConsumerViewModel ViewModel
    {
      get; set;
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
      ViewModel.Trace("Entering Dispose");
      foreach (IDisposable _2Dispose in m_ToDispose)
        _2Dispose.Dispose();
      m_ToDispose.Clear();
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
        MainWindowModel _model = new MainWindowModel() { ViewModelBindingFactory = ViewModel };
        BindingFactory = _model;
        EncodingFactory = _model;
        MessageHandlerFactory = new MessageHandlerFactory(x => m_ToDispose.Add(x), ViewModel.Trace);
        ViewModel.Trace("Initialize consumer engine.");
        Initialize();
        ViewModel.Trace("On start receiving UDP frames.");
        Run();
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
    private List<IDisposable> m_ToDispose = new List<IDisposable>();
    private void Restart()
    {
      ViewModel.Trace("Entering Restart");
      Dispose();
      Setup();
    }
    #endregion

  }
}
