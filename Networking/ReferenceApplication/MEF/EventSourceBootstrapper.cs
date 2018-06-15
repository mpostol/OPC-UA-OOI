
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using UAOOI.Networking.ReferenceApplication.Diagnostic;
using UAOOI.Networking.SemanticData.Diagnostics;

namespace UAOOI.Networking.ReferenceApplication.MEF
{
  [Export(typeof(EventSourceBootstrapper))]
  public class EventSourceBootstrapper : IDisposable
  {

    #region composition
    [ImportMany(typeof(INetworkingEventSourceProvider))]
    public IEnumerable<INetworkingEventSourceProvider> EventSources { get; set; }
    #endregion

    #region API
    internal void Run(Action<EventEntry> action)
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
      IObservable<EventEntry> _last = _listenersDisposable.Cast<IObservable<EventEntry>>().Merge<EventEntry>();
      m_FileSubscription = _last.ObserveOn<EventEntry>(Scheduler.Default).Do<EventEntry>(action).LogToFlatFile(Properties.Settings.Default.LogFilePath);

    }
    #endregion

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    protected virtual void Dispose(bool disposing)
    {
      if (disposedValue)
        return;
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(EventSourceBootstrapper), disposing);
      if (disposing)
      {
        m_FileSubscription?.Sink.FlushAsync();
        m_FileSubscription?.Dispose();
        m_FileSubscription = null;
      }
      disposedValue = true;
    }
    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion

    #region private
    private SinkSubscription<FlatFileSink> m_FileSubscription;
    #endregion

  }
}
