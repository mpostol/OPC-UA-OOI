//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.ReferenceApplication.Core.Diagnostic;
using static UAOOI.Networking.ReferenceApplication.Core.Diagnostic.ReferenceApplicationEventSource;

namespace UAOOI.Networking.ReferenceApplication.UnitTest.Diagnostic
{
  [TestClass]
  public class ReferenceApplicationEventSourceUnitTest
  {
    [TestMethod]
    public void StartingApplicationTest()
    {
      EventEntry _lastEvent = null;
      int _calls = 0;
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(x => { _calls++; _lastEvent = x; });
      using (SinkSubscription<ObservableEventListener> _sinkSubscription = new SinkSubscription<ObservableEventListener>(subscription, _listener))
      {
        Assert.IsNotNull(_sinkSubscription.Sink);

        ReferenceApplicationEventSource _log = ReferenceApplicationEventSource.Log;
        _sinkSubscription.Sink.EnableEvents(_log, EventLevel.LogAlways, EventKeywords.All);

        Assert.IsNull(_lastEvent);
        _log.StartingApplication("Message handler name");
        Assert.IsNotNull(_lastEvent);
        Assert.AreEqual<int>(1, _calls);

        //_lastEvent content
        Assert.AreEqual<int>(2, _lastEvent.EventId);
        Assert.AreEqual<Guid>(Guid.Empty, _lastEvent.ActivityId);
        string _message = "The application has been started using the message handling provider Message handler name.";
        Assert.AreEqual<string>(_message, _lastEvent.FormattedMessage, _lastEvent.FormattedMessage);
        //schema
        EventSchema _Schema = _lastEvent.Schema;
        Assert.AreEqual<string>("InfrastructureStart", _Schema.EventName);
        Assert.AreEqual<int>(2, _Schema.Id);
        //Assert.IsTrue((_Schema.Keywords & SemanticEventSource.Keywords.Diagnostic2) > 0);
        //Assert.AreEqual<string>("PackageContent", _Schema.KeywordsDescription);
        Assert.AreEqual<EventLevel>(EventLevel.Informational, _Schema.Level);
        Assert.AreEqual<string>("Start", _Schema.OpcodeName);
        Assert.AreEqual<EventOpcode>(EventOpcode.Start, _Schema.Opcode);
        Assert.AreEqual<Guid>(new Guid("D8637D00-5EAD-4538-9286-8C6DE346D8C8"), _Schema.ProviderId);
        Assert.AreEqual<string>("UAOOI-Networking-ReferenceApplication-Diagnostic", _Schema.ProviderName);
        Assert.AreEqual<string>("Infrastructure", _Schema.TaskName);
        Assert.AreEqual<EventTask>(Tasks.Infrastructure, _Schema.Task);
        Assert.AreEqual<int>(0, _Schema.Version);

        //Payload
        Assert.AreEqual<string>("System.Collections.ObjectModel.ReadOnlyCollection`1[System.Object]", _lastEvent.Payload.ToString(), _lastEvent.Payload.ToString());
        Assert.AreEqual<int>(1, _lastEvent.Payload.Count);
      }
    }
    [TestMethod]
    public void ReferenceApplicationEventSourceExtensionsTest()
    {
      List<EventEntry> _lastEvents = new List<EventEntry>();
      ObservableEventListener _listener = new ObservableEventListener();
      IDisposable subscription = _listener.Subscribe(x => {_lastEvents.Add(x); });
      using (SinkSubscription<ObservableEventListener> _sinkSubscription = new SinkSubscription<ObservableEventListener>(subscription, _listener))
      {
        Assert.IsNotNull(_sinkSubscription.Sink);

        ReferenceApplicationEventSource _log = ReferenceApplicationEventSource.Log;
        _sinkSubscription.Sink.EnableEvents(_log, EventLevel.LogAlways, EventKeywords.All);

        Assert.AreEqual<int>(0, _lastEvents.Count);
        NotImplementedException _ex = new NotImplementedException("testing exception", new NotImplementedException());
        _log.LogException(_ex);
        Assert.AreEqual<int>(2, _lastEvents.Count);

        //_lastEvent content
        Assert.AreEqual<int>(1, _lastEvents[0].EventId);
        Assert.AreEqual<Guid>(Guid.Empty, _lastEvents[0].ActivityId);
        string _message = "Application Failure: An exception has benn caught: of type NotImplementedException capturing the message: testing exception";
        Assert.AreEqual<string>(_message, _lastEvents[0].FormattedMessage);
        //schema
        EventSchema _Schema = _lastEvents[0].Schema;
        Assert.AreEqual<string>("InfrastructureInfo", _Schema.EventName);
        Assert.AreEqual<int>(1, _Schema.Id);
        //Assert.IsTrue((_Schema.Keywords & SemanticEventSource.Keywords.Diagnostic2) > 0);
        //Assert.AreEqual<string>("PackageContent", _Schema.KeywordsDescription);
        Assert.AreEqual<EventLevel>(EventLevel.Error, _Schema.Level);
        Assert.AreEqual<string>("Info", _Schema.OpcodeName);
        Assert.AreEqual<EventOpcode>(EventOpcode.Info, _Schema.Opcode);
        Assert.AreEqual<Guid>(new Guid("D8637D00-5EAD-4538-9286-8C6DE346D8C8"), _Schema.ProviderId);
        Assert.AreEqual<string>("UAOOI-Networking-ReferenceApplication-Diagnostic", _Schema.ProviderName);
        Assert.AreEqual<string>("Infrastructure", _Schema.TaskName);
        Assert.AreEqual<EventTask>(Tasks.Infrastructure, _Schema.Task);
        Assert.AreEqual<int>(0, _Schema.Version);

        //Payload
        Assert.AreEqual<string>("System.Collections.ObjectModel.ReadOnlyCollection`1[System.Object]", _lastEvents[0].Payload.ToString(), _lastEvents[0].Payload.ToString());
        Assert.AreEqual<int>(1, _lastEvents[0].Payload.Count);
      }
    }
  }
}
