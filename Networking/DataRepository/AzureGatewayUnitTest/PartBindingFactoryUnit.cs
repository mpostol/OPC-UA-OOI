//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UAOOI.Configuration.Networking.Serialization;
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
    public void GetConsumerBindingTest()
    {
      PartBindingFactory newInstance = new PartBindingFactory();
      IConsumerBinding binding = newInstance.GetConsumerBinding("repositoryGroup", "processValueName", new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(binding);
      dynamic repository = newInstance.GetDTO("repositoryGroup");
      Assert.IsNotNull(repository);
      Assert.IsTrue(string.IsNullOrEmpty(repository.processValueName));
      binding.Assign2Repository("New value");
      Assert.AreEqual<string>("New value", repository.processValueName);
    }
  }
}