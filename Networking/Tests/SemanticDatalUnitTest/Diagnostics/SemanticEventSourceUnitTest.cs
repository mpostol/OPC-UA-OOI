
using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.Diagnostics;

namespace UAOOI.Networking.SemanticData.UnitTest.Diagnostics
{
  [TestClass]
  public class SemanticEventSourceUnitTest
  {
    [TestMethod]
    public void MessageInconsistencyTest()
    {
      EventEntry _lastEvent = null;
      int _calls = 0;
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(x => { _calls++; _lastEvent = x; });
      using (SinkSubscription<ObservableEventListener> _sinkSubscription = new SinkSubscription<ObservableEventListener>(subscription, _listener))
      {
        Assert.IsNotNull(_sinkSubscription.Sink);

        SemanticEventSource _log = SemanticEventSource.Log;
        _sinkSubscription.Sink.EnableEvents(_log, EventLevel.LogAlways, EventKeywords.All);

        Assert.IsNull(_lastEvent);
        const int _position = 12345;
        _log.MessageInconsistency(_position);
        Assert.IsNotNull(_lastEvent);
        Assert.AreEqual<int>(1, _calls);

        //_lastEvent content
        Assert.AreEqual<int>(4, _lastEvent.EventId);
        Assert.AreEqual<Guid>(Guid.Empty, _lastEvent.ActivityId);
        string _message = "Unexpected end of message while reading 12345 element.";
        Assert.AreEqual<string>(_message, _lastEvent.FormattedMessage, _lastEvent.FormattedMessage);
        //schema
        EventSchema _Schema = _lastEvent.Schema;
        Assert.AreEqual<string>("ConsumerInfo", _Schema.EventName);
        Assert.AreEqual<int>(4, _Schema.Id);
        //Assert.IsTrue((_Schema.Keywords & SemanticEventSource.Keywords.Diagnostic2) > 0);
        //Assert.AreEqual<string>("PackageContent", _Schema.KeywordsDescription);
        Assert.AreEqual<EventLevel>(EventLevel.Warning, _Schema.Level);
        Assert.AreEqual<string>("Info", _Schema.OpcodeName);
        Assert.AreEqual<EventOpcode>(EventOpcode.Info, _Schema.Opcode);
        Assert.AreEqual<Guid>(new Guid("C8666C20-6FEF-4DD0-BB66-5807BA629DA8"), _Schema.ProviderId);
        Assert.AreEqual<string>("UAOOI-Networking-SemanticData-Diagnostics", _Schema.ProviderName);
        Assert.AreEqual<string>("Consumer", _Schema.TaskName);
        Assert.AreEqual<EventTask>(SemanticEventSource.Tasks.Consumer, _Schema.Task);
        Assert.AreEqual<int>(0, _Schema.Version);

        //Payload
        Assert.AreEqual<string>("System.Collections.ObjectModel.ReadOnlyCollection`1[System.Object]", _lastEvent.Payload.ToString(), _lastEvent.Payload.ToString());
        Assert.AreEqual<int>(1, _lastEvent.Payload.Count);
        Assert.IsInstanceOfType(_lastEvent.Payload[0], typeof(Int32));
        Assert.AreEqual<Int32>(_position, (Int32)_lastEvent.Payload[0]);
        Assert.AreEqual<string>("position", _lastEvent.Schema.Payload[0]);

      }
    }
  }
}
