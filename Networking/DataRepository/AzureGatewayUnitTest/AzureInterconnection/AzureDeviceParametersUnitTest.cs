//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test.AzureInterconnection
{
  [TestClass]
  public class AzureDeviceParametersUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      AzureDeviceParameters instaneToTest = AzureDeviceParameters.ParseRepositoryGroup(String.Empty);
      Assert.IsNotNull(instaneToTest);
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzureDeviceId));
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzurePrimaryKey));
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzureScopeId));
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.AzureSecondaryKey));
      Assert.AreEqual<TimeSpan>(TimeSpan.FromSeconds(1.0), instaneToTest.PublishingInterval());
      Assert.AreEqual<TransportType>(default(TransportType), instaneToTest.TransportType);
      Assert.IsTrue(String.IsNullOrEmpty(instaneToTest.ResourceGroupName));
    }

    [TestMethod]
    public void ParseFullTestMethod()
    {
      AzureDeviceParameters instaneToTest = AzureDeviceParameters.ParseRepositoryGroup("RepositoryGroup -tHttp1 -dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey -i2000");
      Assert.IsNotNull(instaneToTest);
      Assert.AreEqual<string>("AzureDeviceId", instaneToTest.AzureDeviceId);
      Assert.AreEqual<string>("AzurePrimaryKey", instaneToTest.AzurePrimaryKey);
      Assert.AreEqual<string>("AzureScopeId", instaneToTest.AzureScopeId);
      Assert.AreEqual<string>("AzureSecondaryKey", instaneToTest.AzureSecondaryKey);
      Assert.AreEqual<TimeSpan>(TimeSpan.FromSeconds(2.0), instaneToTest.PublishingInterval());
      Assert.AreEqual<TransportType>(TransportType.Http1, instaneToTest.TransportType);
      Assert.AreEqual<string>("RepositoryGroup", instaneToTest.ResourceGroupName);
    }

    [TestMethod]
    public void ParseMissingOptionalTestMethod()
    {
      AzureDeviceParameters instaneToTest = AzureDeviceParameters.ParseRepositoryGroup("RepositoryGroup -dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey ");
      Assert.IsNotNull(instaneToTest);
      Assert.AreEqual<string>("AzureDeviceId", instaneToTest.AzureDeviceId);
      Assert.AreEqual<string>("AzurePrimaryKey", instaneToTest.AzurePrimaryKey);
      Assert.AreEqual<string>("AzureScopeId", instaneToTest.AzureScopeId);
      Assert.AreEqual<string>("AzureSecondaryKey", instaneToTest.AzureSecondaryKey);
      Assert.AreEqual<TimeSpan>(TimeSpan.FromSeconds(1.0), instaneToTest.PublishingInterval());
      Assert.AreEqual<TransportType>(TransportType.Amqp, instaneToTest.TransportType);
      Assert.AreEqual<string>("RepositoryGroup", instaneToTest.ResourceGroupName);
    }

    [TestMethod]
    public void ParseMissingRepositoryGroupTestMethod()
    {
      AzureDeviceParameters instaneToTest = AzureDeviceParameters.ParseRepositoryGroup("-dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey ");
      Assert.IsNotNull(instaneToTest);
      Assert.AreEqual<string>("AzureDeviceId", instaneToTest.AzureDeviceId);
      Assert.AreEqual<string>("AzurePrimaryKey", instaneToTest.AzurePrimaryKey);
      Assert.AreEqual<string>("AzureScopeId", instaneToTest.AzureScopeId);
      Assert.AreEqual<string>("AzureSecondaryKey", instaneToTest.AzureSecondaryKey);
      Assert.AreEqual<TimeSpan>(TimeSpan.FromSeconds(1.0), instaneToTest.PublishingInterval());
      Assert.AreEqual<TransportType>(TransportType.Amqp, instaneToTest.TransportType);
      Assert.AreEqual<string>("", instaneToTest.ResourceGroupName);
    }

    [TestMethod]
    public void ParseMissingRequiredTest()
    {
      Assert.ThrowsException<AggregateException>(() => AzureDeviceParameters.ParseRepositoryGroup("RepositoryGroup -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey"));
      Assert.ThrowsException<AggregateException>(() => AzureDeviceParameters.ParseRepositoryGroup("RepositoryGroup -dAzureDeviceId -pAzurePrimaryKey -kAzureSecondaryKey"));
      Assert.ThrowsException<AggregateException>(() => AzureDeviceParameters.ParseRepositoryGroup("RepositoryGroup -dAzureDeviceId -sAzureScopeId -kAzureSecondaryKey"));
      Assert.ThrowsException<AggregateException>(() => AzureDeviceParameters.ParseRepositoryGroup("RepositoryGroup -dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey "));
    }
  }
}