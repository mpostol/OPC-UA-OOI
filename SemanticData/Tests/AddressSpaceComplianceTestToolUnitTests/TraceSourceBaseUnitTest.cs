//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UAOOI.SemanticData.AddressSpacePrototyping.Instrumentation;

namespace UAOOI.SemanticData.AddressSpacePrototyping
{
  [TestClass]
  public class TraceSourceBaseUnitTest
  {
    [TestMethod]
    public void AssemblyTraceEventTestMethod()
    {
      TraceSource tracer = new TraceSource("AddressSpacePrototyping");
      Assert.AreEqual<string>("AddressSpacePrototyping", tracer.Name, $"Actual tracer name: {tracer.Name}");
      //Assert.AreEqual(1, Trace.Listeners.Count);
      Dictionary<string, TraceListener> _listeners = tracer.Listeners.Cast<TraceListener>().ToDictionary<TraceListener, string>(x => x.Name);
      Assert.IsNotNull(_listeners);
      Assert.IsTrue(_listeners.ContainsKey("LogFile"));
      TraceListener _listener = _listeners["LogFile"];
      Assert.IsNotNull(_listener);
      Assert.IsInstanceOfType(_listener, typeof(DelimitedListTraceListener));
      DelimitedListTraceListener _advancedListener = _listener as DelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener.Filter);
      Assert.IsInstanceOfType(_advancedListener.Filter, typeof(EventTypeFilter));
      EventTypeFilter _eventTypeFilter = _advancedListener.Filter as EventTypeFilter;
      Assert.AreEqual(SourceLevels.All, _eventTypeFilter.EventType);
      string _testPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Assert.AreEqual<string>(Path.Combine(_testPath, @"asp.log"), _advancedListener.GetFileName());
    }

    [TestMethod]
    public void LogFileExistsTest()
    {
      TraceSource tracer = new TraceSource("AddressSpacePrototyping");
      TraceListener _listener = tracer.Listeners.Cast<TraceListener>().Where<TraceListener>(x => x.Name == "LogFile").First<TraceListener>();
      DelimitedListTraceListener _advancedListener = _listener as DelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener);
      Assert.IsFalse(string.IsNullOrEmpty(_advancedListener.GetFileName()));
      FileInfo _logFileInfo = new FileInfo(_advancedListener.GetFileName());
      long _startLength = _logFileInfo.Exists ? _logFileInfo.Length : 0;
      tracer.TraceEvent(TraceEventType.Information, 0, "LogFileExistsTest is executed");
      Assert.IsFalse(string.IsNullOrEmpty(_advancedListener.GetFileName()));
      _logFileInfo.Refresh();
      Assert.IsTrue(_logFileInfo.Exists);
      Assert.IsTrue(_logFileInfo.Length > _startLength);
    }
  }
}