
using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Schema;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;
using static UAOOI.Networking.SemanticData.Diagnostics.ReactiveNetworkingEventSource;

namespace UAOOI.Networking.UDPMessageHandler.UnitTest.Diagnostic
{
  [TestClass]
  public class UDPMessageHandlerSemanticEventSourceUnitTest
  {

    [TestMethod]
    public void UDPMessageHandlerSemanticEventSourceTest()
    {
      UDPMessageHandlerSemanticEventSource _instance = UDPMessageHandlerSemanticEventSource.Log;
      Assert.IsNull(_instance.ConstructionException);
      Assert.AreEqual<string>("UAOOI-Networking-UDPMessageHandler-Diagnostic", _instance.Name);
      Assert.AreEqual<EventSourceSettings>(EventSourceSettings.EtwManifestEventFormat, _instance.Settings);
    }
    [TestMethod]
    public void ReactiveSubscribeTest()
    {
      EventEntry _lastEvent = null;
      int _calls = 0;
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(x => { _calls++; _lastEvent = x; });
      using (SinkSubscription<ObservableEventListener> _sinkSubscription = new SinkSubscription<ObservableEventListener>(subscription, _listener))
      {
        Assert.IsNotNull(_sinkSubscription.Sink);

        UDPMessageHandlerSemanticEventSource _log = UDPMessageHandlerSemanticEventSource.Log;
        _sinkSubscription.Sink.EnableEvents(_log, EventLevel.LogAlways, Keywords.All);

        Assert.IsNull(_lastEvent);
        _log.ReceivedMessageContent(new IPEndPoint(new IPAddress(new byte[] { 192, 168, 0, 0 }), 25), 100, new byte[] { 1, 2, 3, 4 });
        Assert.AreEqual<int>(1, _calls);
        Assert.IsNotNull(_lastEvent);

        //_lastEvent content
        Assert.AreEqual<int>(5, _lastEvent.EventId);
        Assert.AreEqual<Guid>(Guid.Empty, _lastEvent.ActivityId);
        string _message = "Received message: 192.168.0.0:25 [100]: 1,2,3,4";
        Assert.AreEqual<string>(_message, _lastEvent.FormattedMessage, _lastEvent.FormattedMessage);

        Assert.AreEqual<string>("System.Collections.ObjectModel.ReadOnlyCollection`1[System.Object]", _lastEvent.Payload.ToString(), _lastEvent.Payload.ToString());
        Assert.AreEqual<int>(1, _lastEvent.Payload.Count);
        Assert.IsInstanceOfType(_lastEvent.Payload[0], typeof(String));
        Assert.AreEqual<string>("192.168.0.0:25 [100]: 1,2,3,4", _lastEvent.Payload[0].ToString());
        Assert.AreEqual<string>("payload0", _lastEvent.Schema.Payload[0]);

        Assert.AreEqual<string>("Info", _lastEvent.Schema.OpcodeName);
        Assert.AreEqual<EventOpcode>(EventOpcode.Info, _lastEvent.Schema.Opcode);
        Assert.AreEqual<string>("Consumer", _lastEvent.Schema.TaskName);
        Assert.AreEqual<EventTask>(Tasks.Consumer, _lastEvent.Schema.Task);
      }
    }
    [TestMethod]
    public void GetIMessageHandlerTest()
    {
      EventEntry _lastEvent = null;
      int _calls = 0;
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(x => { _calls++; _lastEvent = x; });
      using (SinkSubscription<ObservableEventListener> _sinkSubscription = new SinkSubscription<ObservableEventListener>(subscription, _listener))
      {
        Assert.IsNotNull(_sinkSubscription.Sink);

        UDPMessageHandlerSemanticEventSource _log = UDPMessageHandlerSemanticEventSource.Log;
        _sinkSubscription.Sink.EnableEvents(_log, EventLevel.LogAlways, Keywords.All);

        Assert.IsNull(_lastEvent);
        _log.GetIMessageHandler("NameOfIMessageHandlerMethod");
        Assert.AreEqual<int>(1, _calls);
        Assert.IsNotNull(_lastEvent);

        //_lastEvent content
        Assert.AreEqual<int>(2, _lastEvent.EventId);
        Assert.AreEqual<Guid>(Guid.Empty, _lastEvent.ActivityId);
        string _message = "The IMessageHandlerFactory.NameOfIMessageHandlerMethod method has been called.";
        Assert.AreEqual<string>(_message, _lastEvent.FormattedMessage, _lastEvent.FormattedMessage);
        //schema
        EventSchema _Schema = _lastEvent.Schema;
        Assert.AreEqual<string>("CodeBehaviorStart", _Schema.EventName);
        Assert.AreEqual<int>(2, _Schema.Id);
        ////Assert.IsTrue((_Schema.Keywords & UDPMessageHandlerSemanticEventSource.Keywords.Diagnostic) > 0);
        //Assert.AreEqual<string>("PackageContent", _Schema.KeywordsDescription);
        Assert.AreEqual<EventLevel>(EventLevel.Informational, _Schema.Level);
        Assert.AreEqual<string>("Start", _Schema.OpcodeName);
        Assert.AreEqual<EventOpcode>(EventOpcode.Start, _Schema.Opcode);
        Assert.AreEqual<Guid>(new Guid("84C6B563-8282-47CB-9111-7B29D8B43E23"), _Schema.ProviderId);
        Assert.AreEqual<string>("UAOOI-Networking-UDPMessageHandler-Diagnostic", _Schema.ProviderName);
        Assert.AreEqual<string>("CodeBehavior", _Schema.TaskName);
        Assert.AreEqual<EventTask>(Tasks.Infrastructure, _Schema.Task);
        Assert.AreEqual<int>(0, _Schema.Version);

        //Payload
        Assert.AreEqual<string>("System.Collections.ObjectModel.ReadOnlyCollection`1[System.Object]", _lastEvent.Payload.ToString(), _lastEvent.Payload.ToString());
        Assert.AreEqual<int>(1, _lastEvent.Payload.Count);
        Assert.IsInstanceOfType(_lastEvent.Payload[0], typeof(String));
        Assert.AreEqual<string>("NameOfIMessageHandlerMethod", (string)_lastEvent.Payload[0]);
        Assert.AreEqual<string>("iMessageHandlerName", _lastEvent.Schema.Payload[0]);
      }
    }
    [TestMethod]
    public void JoiningMulticastGroupTest()
    {
      EventEntry _lastEvent = null;
      int _calls = 0;
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(x => { _calls++; _lastEvent = x; });
      using (SinkSubscription<ObservableEventListener> _sinkSubscription = new SinkSubscription<ObservableEventListener>(subscription, _listener))
      {
        Assert.IsNotNull(_sinkSubscription.Sink);

        UDPMessageHandlerSemanticEventSource _log = UDPMessageHandlerSemanticEventSource.Log;
        _sinkSubscription.Sink.EnableEvents(_log, EventLevel.LogAlways, Keywords.All);

        Assert.IsNull(_lastEvent);
        _log.JoiningMulticastGroup(new IPAddress(new byte[] { 192, 168, 0, 0 }));
        Assert.AreEqual<int>(1, _calls);
        Assert.IsNotNull(_lastEvent);

        //_lastEvent content
        Assert.AreEqual<int>(7, _lastEvent.EventId);
        Assert.AreEqual<Guid>(Guid.Empty, _lastEvent.ActivityId);
        string _message = "Joining the multicast group: 192.168.0.0";
        Assert.AreEqual<string>(_message, _lastEvent.FormattedMessage, _lastEvent.FormattedMessage);
        //schema
        EventSchema _Schema = _lastEvent.Schema;
        Assert.AreEqual<string>("StackStart", _Schema.EventName);
        Assert.AreEqual<int>(7, _Schema.Id);
        ////Assert.IsTrue((_Schema.Keywords & UDPMessageHandlerSemanticEventSource.Keywords.Diagnostic) > 0);
        //Assert.AreEqual<string>("PackageContent", _Schema.KeywordsDescription);
        Assert.AreEqual<EventLevel>(EventLevel.Informational, _Schema.Level);
        Assert.AreEqual<string>("Start", _Schema.OpcodeName);
        Assert.AreEqual<EventOpcode>(EventOpcode.Start, _Schema.Opcode);
        Assert.AreEqual<Guid>(new Guid("84C6B563-8282-47CB-9111-7B29D8B43E23"), _Schema.ProviderId);
        Assert.AreEqual<string>("UAOOI-Networking-UDPMessageHandler-Diagnostic", _Schema.ProviderName);
        Assert.AreEqual<string>("Stack", _Schema.TaskName);
        Assert.AreEqual<EventTask>(Tasks.Stack, _Schema.Task);
        Assert.AreEqual<int>(0, _Schema.Version);

        //Payload
        Assert.AreEqual<string>("System.Collections.ObjectModel.ReadOnlyCollection`1[System.Object]", _lastEvent.Payload.ToString(), _lastEvent.Payload.ToString());
        Assert.AreEqual<int>(1, _lastEvent.Payload.Count);
        Assert.IsInstanceOfType(_lastEvent.Payload[0], typeof(String));
        Assert.AreEqual<string>("192.168.0.0", (string)_lastEvent.Payload[0]);
        Assert.AreEqual<string>("multicastGroup", _lastEvent.Schema.Payload[0]);
      }
    }
    [TestMethod]
    public void LogFailure2LogToFlatFileTest()
    {
      string _filePath = $"{nameof(LogFailure2LogToFlatFileTest)}.log";
      FileInfo _logFile = new FileInfo(_filePath);
      if (_logFile.Exists)
        _logFile.Delete();
      MessageHandlerFactory _factory = new MessageHandlerFactory();
      ObservableEventListener _listener = new ObservableEventListener();
      UDPMessageHandlerSemanticEventSource _log = UDPMessageHandlerSemanticEventSource.Log;
      _listener.EnableEvents(_log, EventLevel.LogAlways, Keywords.All);
      SinkSubscription<FlatFileSink> _FlatFileSink = _listener.LogToFlatFile(_filePath);
      _logFile.Refresh();
      Assert.IsTrue(_logFile.Exists);
      Assert.AreEqual<long>(0, _logFile.Length);

      _log.Failure(nameof(UDPMessageHandlerSemanticEventSourceUnitTest), nameof(LogFailure2LogToFlatFileTest), "LogFailure");

      _FlatFileSink.Sink.FlushAsync();
      _logFile.Refresh();
      Assert.IsTrue(_logFile.Length > 100);
      _FlatFileSink.Dispose();
    }

  }
}
