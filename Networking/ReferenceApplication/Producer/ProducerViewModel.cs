
using GalaSoft.MvvmLight;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace UAOOI.Networking.ReferenceApplication.Producer
{

  /// <summary>
  /// class SimulatorViewModel - defines a ViewModel part to be used by the producer to expose diagnostic information on the UI.
  /// </summary>
  /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
  [Export(ProducerCompositionSettings.ProducerViewModelContract)]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class SimulatorViewModel : ViewModelBase
  {

    #region API
    /// <summary>
    /// Initializes a new instance of the <see cref="SimulatorViewModel"/> class.
    /// </summary>
    public SimulatorViewModel()
    {
      ProducerRestartCommand = new RestartCommand(() => { });
    }
    /// <summary>
    /// Gets or sets the producer error message.
    /// </summary>
    /// <value>The producer error message.</value>
    public string ProducerErrorMessage
    {
      get
      {
        return b_ProducerErrorMessage;
      }
      set
      {
        b_ProducerErrorMessage = value;
        RaisePropertyChanged<string>("ProducerErrorMessage", b_ProducerErrorMessage, value);
      }
    }
    /// <summary>
    /// Gets or sets the producer restart command. While assigned by 
    /// </summary>
    /// <value>The producer restart command.</value>
    public ICommand ProducerRestartCommand
    {
      get
      {
        return b_ProducerRestartCommand;
      }
      set
      {
        b_ProducerRestartCommand = value;
        RaisePropertyChanged<ICommand>("ProducerRestartCommand", b_ProducerRestartCommand, value);
      }
    }

    internal void ChangeProducerRestartCommand(Action action)
    {
      ProducerRestartCommand = new RestartCommand(action);
    }
    #endregion

    #region private
    private class RestartCommand : ICommand
    {
      public RestartCommand(Action restart)
      {
        m_restart = restart;
      }
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
    private string b_ProducerErrorMessage;
    private ICommand b_ProducerRestartCommand;
    #endregion

  }
}