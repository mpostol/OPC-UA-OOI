//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
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
    public void GetConsumerBindingSerializationTest()
    {
      PartBindingFactory newInstance = new PartBindingFactory();
      string repositoryName = "RepositoryGroup -tHttp1 -dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey -i2000";
      IConsumerBinding binding = newInstance.GetConsumerBinding(repositoryName, "processValueName", new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(binding);
      binding.Assign2Repository("New value");
      string dto = newInstance.GetDTO(repositoryName);
      Debug.Write(dto);
      Assert.AreEqual<int>(32, dto.Length);
      Assert.AreEqual<string>("{\"processValueName\":\"New value\"}", dto);
    }
  }
}