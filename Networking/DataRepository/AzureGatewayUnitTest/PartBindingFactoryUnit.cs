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
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.DataRepository.AzureGateway.Diagnostic;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{
  [TestClass]
  public class PartBindingFactoryUnit
  {
    [TestMethod]
    public void ConstructorTest()
    {
      PartBindingFactory _newInstance = new PartBindingFactory();
      Assert.ThrowsException<KeyNotFoundException>(() => _newInstance.GetDTO("random name"));
    }

    [TestMethod]
    public void GetConsumerBindingSerializationTest()
    {
      using (AzureGatewaySemanticEventSource log = AzureGatewaySemanticEventSource.Log())
      using (EventListener lisner = new EventListener())
      {
        //setup log instrumentation
        List<EventWrittenEventArgs> eventsList = new List<EventWrittenEventArgs>();
        lisner.EventWritten += (source, entry) => eventsList.Add(entry);
        Assert.AreEqual<int>(0, eventsList.Count);
        lisner.EnableEvents(log, EventLevel.LogAlways, EventKeywords.All);

        PartBindingFactory newInstance = new PartBindingFactory();
        string repositoryName = "RepositoryGroup -tHttp1 -dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey -i2000";
        IConsumerBinding binding = newInstance.GetConsumerBinding(repositoryName, "processValueName", new UATypeInfo(BuiltInType.String));
        Assert.IsNotNull(binding);
        binding.Assign2Repository("New value");
        string dto = newInstance.GetDTO(repositoryName);
        Debug.Write(dto);
        Assert.AreEqual<int>(32, dto.Length);
        Assert.AreEqual<string>("{\"processValueName\":\"New value\"}", dto);
        Assert.AreEqual<int>(2, eventsList.Count);
        EventWrittenEventArgs eventArgs = eventsList[0];
        Assert.AreEqual<string>(nameof(AzureGatewaySemanticEventSource.EnteringMethodBinding), eventArgs.EventName);
        Assert.AreEqual<string>("Entering method PartBindingFactory.GetConsumerBinding", String.Format(eventArgs.Message, eventArgs.Payload.Select<object, string>(x => x.ToString()).ToArray<string>()));
        eventArgs = eventsList[1];
        Assert.AreEqual<string>(nameof(AzureGatewaySemanticEventSource.EnteringMethodBinding), eventArgs.EventName);
        Assert.AreEqual<string>("Entering method PartBindingFactory.GetDTO", String.Format(eventArgs.Message, eventArgs.Payload.Select<object, string>(x => x.ToString()).ToArray<string>()));
      }
    }

    [TestMethod]
    public void GetProducerBindingTest()
    {
      using (AzureGatewaySemanticEventSource log = AzureGatewaySemanticEventSource.Log())
      using (EventListener lisner = new EventListener())
      {
        //setup log instrumentation
        List<EventWrittenEventArgs> eventsList = new List<EventWrittenEventArgs>();
        lisner.EventWritten += (source, entry) => eventsList.Add(entry);
        Assert.AreEqual<int>(0, eventsList.Count);
        lisner.EnableEvents(log, EventLevel.LogAlways, EventKeywords.All);
        //start testing
        PartBindingFactory newInstance = new PartBindingFactory();
        Assert.ThrowsException<NotImplementedException>(() => newInstance.GetProducerBinding("any repository random name", "any process variable random name", new UATypeInfo(BuiltInType.String)));
        //examine the results
        Assert.AreEqual<int>(1, eventsList.Count);
        EventWrittenEventArgs eventArgs = eventsList[0];
        Assert.AreEqual<string>(nameof(AzureGatewaySemanticEventSource.EnteringMethodBinding), eventArgs.EventName);
        Assert.AreEqual<string>("Entering method PartBindingFactory.GetProducerBinding", String.Format(eventArgs.Message, eventArgs.Payload.Select<object, string>(x => x.ToString()).ToArray<string>()));
      }
    }
  }
}