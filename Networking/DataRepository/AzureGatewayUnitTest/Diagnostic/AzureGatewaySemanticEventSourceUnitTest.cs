//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.Tracing;
using UAOOI.Networking.DataRepository.AzureGateway.Diagnostic;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test.Diagnostic
{
  [TestClass]
  public class AzureGatewaySemanticEventSourceUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      AzureGatewaySemanticEventSource _itemToTest = AzureGatewaySemanticEventSource.Log();
      Assert.IsNotNull(_itemToTest);
      Assert.IsNull(_itemToTest.ConstructionException);
      Assert.AreEqual<Guid>(Guid.Parse("BC7E8C08-C708-4E3C-A27E-237F093F175C"), _itemToTest.Guid);
      Assert.AreEqual<string>("UAOOI-Networking-DataRepository-AzureGateway-Diagnostic", _itemToTest.Name);
      Assert.AreEqual<EventSourceSettings>(EventSourceSettings.EtwManifestEventFormat, _itemToTest.Settings);
    }
  }
}