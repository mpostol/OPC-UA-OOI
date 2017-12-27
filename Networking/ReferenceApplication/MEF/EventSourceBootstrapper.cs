using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using UAOOI.Networking.ReferenceApplication.Consumer;
using UAOOI.Networking.SemanticData.Diagnostics;
using UAOOI.Networking.SemanticData.MessageHandling;
using System.Linq;

namespace UAOOI.Networking.ReferenceApplication.MEF
{
  [Export(typeof(EventSourceBootstrapper))]
  public class EventSourceBootstrapper : IDisposable
  {

    #region composition
    [ImportMany(typeof(INetworkingEventSourceProvider))]
    public IEnumerable<INetworkingEventSourceProvider> EventSources { get; set; }
    /// <summary>
    /// Gets or sets the message handler factory.
    /// </summary>
    /// <value>The message handler factory.</value>
    [Import(typeof(IMessageHandlerFactory))]
    public IMessageHandlerFactory MessageHandlerFactory { get; set; }
    [Import(typeof(IConsumerViewModel))]
    internal IConsumerViewModel ViewModel
    {
      get; set;
    }
    #endregion

    internal void Run()
    {
      CompositeDisposable _listenersDisposable = new CompositeDisposable();
      if (EventSources != null)
        foreach (INetworkingEventSourceProvider _eventSources in EventSources)
        {
          ObservableEventListener _newEventListener = new ObservableEventListener();
          _newEventListener.EnableEvents(_eventSources.GetPartEventSource(), EventLevel.LogAlways, Keywords.All);
          _listenersDisposable.Add(_newEventListener);
        }
      if (_listenersDisposable.Count == 0)
        return;
      INetworkingEventSourceProvider _messageHandlerProvider = MessageHandlerFactory as INetworkingEventSourceProvider;
      if (_messageHandlerProvider != null)
      {
        ObservableEventListener _newMessageHandlerListener = new ObservableEventListener();
        _newMessageHandlerListener.EnableEvents(_messageHandlerProvider.GetPartEventSource(), EventLevel.LogAlways, Keywords.All);
        _listenersDisposable.Add(_newMessageHandlerListener);
      }
      IObservable<EventEntry> _last = _listenersDisposable.Cast<IObservable<EventEntry>>().Concat();
      m_FileSubscription = _last.LogToFlatFile(Properties.Settings.Default.LogFilePath);
      m_Subscription = new SinkSubscription<CompositeDisposable>(_last.Subscribe< EventEntry>(x => ViewModel.Trace(x.FormattedMessage)), _listenersDisposable);
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          m_FileSubscription?.Sink.FlushAsync();
          m_FileSubscription?.Dispose();
          m_FileSubscription = null;
          m_Subscription?.Dispose();
          m_Subscription = null;
        }
        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.
        disposedValue = true;
      }
    }
    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~EventSourceBootsraper() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }
    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion

    #region private
    private SinkSubscription<CompositeDisposable> m_Subscription;
    private SinkSubscription<FlatFileSink> m_FileSubscription;
    #endregion

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
