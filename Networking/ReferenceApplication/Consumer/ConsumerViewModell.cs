
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UAOOI.Networking.ReferenceApplication.Consumer
{
  internal class ConsumerViewModel : ViewModelBase
  {
    public ConsumerViewModel()
    {
      b_ConsumerLog = new ObservableCollection<string>();
    }
    #region ViewModel
    /// <summary>
    /// Gets or sets the consumer received bytes.
    /// </summary>
    /// <value>The consumer received bytes.</value>
    public int ConsumerReceivedBytes
    {
      get
      {
        return b_ConsumerBytesReceived;
      }
      set
      {
        b_ConsumerBytesReceived = value;
        RaisePropertyChanged<int>("ConsumerReceivedBytes", b_ConsumerBytesReceived, value);
      }
    }
    /// <summary>
    /// Gets or sets the number of consumer received frames .
    /// </summary>
    /// <value>The consumer frames received.</value>
    public int ConsumerFramesReceived
    {
      get
      {
        return b_ConsumerFramesReceived;
      }
      set
      {
        b_ConsumerFramesReceived = value;
        RaisePropertyChanged<int>("ConsumerFramesReceived", b_ConsumerFramesReceived, value);
      }
    }
    /// <summary>
    /// Gets or sets the consumer update configuration command.
    /// </summary>
    /// <value>The consumer update configuration <see cref="ICommand" />.</value>
    public ICommand ConsumerUpdateConfiguration //TODO Remove reference of ConsumerDataManagementSetup System.Windows  #239
    {
      get
      {
        return b_ConsumerUpdateConfiguration;
      }
      set
      {
        b_ConsumerUpdateConfiguration = value;
        RaisePropertyChanged<ICommand>("ConsumerUpdateConfiguration", b_ConsumerUpdateConfiguration, value);
      }
    }
    /// <summary>
    /// Gets or sets the last consumer error message.
    /// </summary>
    /// <value>The consumer error message.</value>
    public string ConsumerErrorMessage
    {
      get
      {
        return b_ConsumerErrorMessage;
      }
      set
      {
        b_ConsumerErrorMessage = value;
        RaisePropertyChanged<string>("ConsumerErrorMessage", b_ConsumerErrorMessage, value);
      }
    }
    /// <summary>
    /// Add the message to the <see cref="MainWindowViewModel.ConsumerLog"/>.
    /// </summary>
    /// <param name="message">The message to be added to the log <see cref="MainWindowViewModel.ConsumerLog"/>.</param>
    public void Trace(string message)
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.RunAsync((() => ConsumerLog.Insert(0, message)));
    }
    public ObservableCollection<string> ConsumerLog
    {
      get
      {
        return b_ConsumerLog;
      }
      set
      {
        b_ConsumerLog = value;
        RaisePropertyChanged<ObservableCollection<string>>("ConsumerLog", b_ConsumerLog, value);
      }
    }

    #endregion

    #region API
    internal void ChangeProducerCommand(Action action)
    {
      ConsumerUpdateConfiguration = new RelayCommand(action);
    }
    #endregion

    #region private
    private int b_ConsumerBytesReceived;
    private int b_ConsumerFramesReceived;
    private ICommand b_ConsumerUpdateConfiguration;
    private string b_ConsumerErrorMessage;
    private ObservableCollection<string> b_ConsumerLog;
    #endregion

  }

}