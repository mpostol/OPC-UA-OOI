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
using System.Linq;
using UAOOI.Networking.DataRepository.AzureGateway.Diagnostic;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test.Diagnostic
{
  [TestClass]
  public class AzureGatewaySemanticEventSourceUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      using (AzureGatewaySemanticEventSource itemToTest = AzureGatewaySemanticEventSource.Log())
      {
        Assert.IsNotNull(itemToTest);
        Assert.IsNull(itemToTest.ConstructionException);
        Assert.AreEqual<Guid>(Guid.Parse("BC7E8C08-C708-4E3C-A27E-237F093F175C"), itemToTest.Guid);
        Assert.AreEqual<string>("UAOOI.Networking.DataRepository.AzureGateway.Diagnostic", itemToTest.Name);
        Assert.AreEqual<EventSourceSettings>(EventSourceSettings.EtwManifestEventFormat, itemToTest.Settings);
        Assert.IsFalse(itemToTest.IsEnabled());
        Assert.AreSame(itemToTest, AzureGatewaySemanticEventSource.Log());
      }
    }

    [TestMethod]
    public void DisposeTestMethod()
    {
      try
      {
        AzureGatewaySemanticEventSource itemToTest = AzureGatewaySemanticEventSource.Log();
        Assert.IsNotNull(itemToTest);
        itemToTest.Dispose();
        Assert.IsNotNull(itemToTest);
        Assert.AreNotSame(itemToTest, AzureGatewaySemanticEventSource.Log());
      }
      finally
      {
        AzureGatewaySemanticEventSource.Log().Dispose();
      }
    }

    [TestMethod]
    public void EventListenerTest()
    {
      using (AzureGatewaySemanticEventSource itemToTest = AzureGatewaySemanticEventSource.Log())
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

    [TestMethod]
    public void ProgramFailureTest()
    {
      using (AzureGatewaySemanticEventSource itemToTest = AzureGatewaySemanticEventSource.Log())
      using (EventListener lisner = new EventListener())
      {
        List<EventWrittenEventArgs> eventsList = new List<EventWrittenEventArgs>();
        lisner.EventWritten += (source, entry) => eventsList.Add(entry);
        Assert.AreEqual<int>(0, eventsList.Count);
        lisner.EnableEvents(itemToTest, EventLevel.LogAlways, EventKeywords.All);
        itemToTest.ProgramFailure("ClassName", "problem");
        Assert.AreEqual<int>(1, eventsList.Count);
        EventWrittenEventArgs eventArgs = eventsList[0];
        Assert.AreEqual<int>(1, eventArgs.EventId);
        Assert.AreEqual<string>("At ClassName.ProgramFailureTest encountered application failure: problem", String.Format(eventArgs.Message, eventArgs.Payload.Select<object, string>(x => x.ToString()).ToArray<string>()));
        Assert.AreEqual<EventChannel>(EventChannel.Admin, eventArgs.Channel);
        Assert.AreEqual<int>(1, eventArgs.EventId);
        Assert.AreEqual<string>(nameof(AzureGatewaySemanticEventSource.ProgramFailure), eventArgs.EventName);
        Assert.AreSame(AzureGatewaySemanticEventSource.Log(), eventArgs.EventSource);
        Assert.IsTrue((AzureGatewaySemanticEventSource.Keywords.Diagnostic & eventArgs.Keywords) > 0);
        Assert.AreEqual<EventLevel>(EventLevel.Error, eventArgs.Level);
        Assert.AreEqual<EventOpcode>(EventOpcode.Info, eventArgs.Opcode);
        Assert.AreEqual<EventTask>(AzureGatewaySemanticEventSource.Tasks.Code, eventArgs.Task);
        Assert.AreEqual<byte>(0x01, eventArgs.Version);
      }
    }
  }
}