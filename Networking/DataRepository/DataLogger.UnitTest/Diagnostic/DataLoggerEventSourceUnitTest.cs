//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.DataRepository.DataLogger.Diagnostic
{
  [TestClass]
  public class DataLoggerEventSourceUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      using (DataLoggerEventSource itemToTest = DataLoggerEventSource.Log())
      {
        Assert.IsNotNull(itemToTest);
        Assert.IsNull(itemToTest.ConstructionException);
        Assert.AreEqual<Guid>(Guid.Parse("B28CBA3C-E2B7-4C5B-A045-E21FD3158D9B"), itemToTest.Guid);
        Assert.AreEqual<string>("UAOOI.Networking.DataRepository.DataLogger.Diagnostic.DataLoggerEventSource", itemToTest.Name);
        Assert.AreEqual<EventSourceSettings>(EventSourceSettings.EtwManifestEventFormat, itemToTest.Settings);
        Assert.IsFalse(itemToTest.IsEnabled());
        Assert.AreSame(itemToTest, DataLoggerEventSource.Log());
      }
    }

    [TestMethod]
    public void DisposeTestMethod()
    {
      try
      {
        DataLoggerEventSource itemToTest = DataLoggerEventSource.Log();
        itemToTest.Dispose();
        Assert.IsNotNull(itemToTest);
        Assert.AreNotSame(itemToTest, DataLoggerEventSource.Log());
      }
      finally
      {
        DataLoggerEventSource.Log().Dispose();
      }
    }

    [TestMethod]
    public void EventListenerTest()
    {
      using (DataLoggerEventSource itemToTest = DataLoggerEventSource.Log())
      using (EventListener lisner = new EventListener())
      {
        List<EventSourceCreatedEventArgs> sourceList = new List<EventSourceCreatedEventArgs>();
        List<EventWrittenEventArgs> eventsList = new List<EventWrittenEventArgs>();
        lisner.EventSourceCreated += (o, es) => sourceList.Add(es);
        lisner.EventWritten += (source, entry) => eventsList.Add(entry);
        foreach (EventSourceCreatedEventArgs item in sourceList)
          Debug.WriteLine($"{item.EventSource.Name}:{item.EventSource.Guid}; Is enabled: {item.EventSource.IsEnabled()}");
        int esCount = sourceList.Count;
        Assert.AreEqual<int>(0, eventsList.Count);
        lisner.EnableEvents(itemToTest, EventLevel.LogAlways, EventKeywords.All);
        Assert.AreEqual<int>(esCount, sourceList.Count);
        Assert.AreEqual<int>(0, eventsList.Count);
      }
    }
  }
}