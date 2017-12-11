
using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;
using System.Reactive;
using System.Windows.Input;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.Diagnostics;
//using UAOOI.Networking.UDPMessageHandler;

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
        MainWindowModel _model = new MainWindowModel() { ViewModelBindingFactory = ViewModel };
        BindingFactory = _model;
        EncodingFactory = _model;
        ViewModel.Trace("Initialize consumer engine and start receiving UDP frames.");
        INetworkingEventSourceProvider _sourceProvider = MessageHandlerFactory as INetworkingEventSourceProvider;
        if (_sourceProvider != null)
        {
          m_Subscription = SinkExtensions.CreateSink(x => ViewModel.Trace(x.FormattedMessage));
          m_Subscription.Sink.EnableEvents(_sourceProvider.GetPartEventSource(), EventLevel.LogAlways, Keywords.All);
          m_FileSubscription = SinkExtensions.CreateSink(_sourceProvider.GetPartEventSource(), "ReferenceApplication.log");
          //m_FileSubscription.Sink.EnableEvents(_sourceProvider.GetPartEventSource(), EventLevel.LogAlways, Keywords.All);
        }
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
    private SinkSubscription<ObservableEventListener> m_Subscription;
    private SinkSubscription<FlatFileSink> m_FileSubscription;
    private bool m_Disposed = false;

    private void Restart()
    {
      ViewModel.Trace("Entering Restart");
      Start();
    }
    #endregion
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || m_Disposed)
        return;
      m_Subscription?.Dispose();
      m_Subscription = null;
      m_FileSubscription?.Sink.FlushAsync();
      m_FileSubscription?.Dispose();
      m_FileSubscription = null;
    }
  }
  public static class SinkExtensions
  {

    public static SinkSubscription<ObservableEventListener> CreateSink(this ObservableEventListener eventStream, Action<EventEntry> feedback)
    {
      IDisposable subscription = eventStream.Subscribe(feedback);
      return new SinkSubscription<ObservableEventListener>(subscription, eventStream);
    }

    public static SinkSubscription<ObservableEventListener> CreateSink(Action<EventEntry> feedback)
    {
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(feedback);
      return new SinkSubscription<ObservableEventListener>(subscription, _listener);
    }
    public static SinkSubscription<FlatFileSink> CreateSink(EventSource eventSource, string path)
    {
      ObservableEventListener _listener = new ObservableEventListener();
      _listener.EnableEvents(eventSource, EventLevel.LogAlways, Keywords.All);
      return _listener.LogToFlatFile(path);
    }

  }
}


