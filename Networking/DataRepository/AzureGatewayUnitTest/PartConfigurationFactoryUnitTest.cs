//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Azure.Devices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{
    [TestClass]
    [DeploymentItem(@".\TestingData\ConfigurationDataConsumer.BoilersSet.xml", @".\")]
    public class PartConfigurationFactoryUnitTest
    {
        [TestMethod]
        [ClassInitialize()]
        public static void TestingData(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            Assert.IsTrue(File.Exists(_ConfigurationFilePath));
        }

        [TestMethod]
        public void Constructor()
        {
            PartConfigurationFactory configurationFactory = new PartConfigurationFactory(_ConfigurationFilePath);
            Assert.IsNull(configurationFactory.Configuration);
            ConfigurationData configuartion = configurationFactory.GetConfiguration();
            Assert.IsNotNull(configuartion);
            Assert.IsNotNull(configurationFactory.Configuration);
            Assert.AreEqual<int>(4, configuartion.DataSets.Length);
        }

        [TestMethod]
        public void RepositoryGroupTest()
        {
            PartConfigurationFactory configurationFactory = new PartConfigurationFactory(_ConfigurationFilePath);
            ConfigurationData configuartion = configurationFactory.GetConfiguration();
            Assert.AreEqual<string>("BoilersArea_Boiler_#1 -tHttp1 -dAzureDeviceId -sAzureScopeId -pAzurePrimaryKey -kAzureSecondaryKey -i2000", configuartion.DataSets[0].RepositoryGroup);
            AzureDeviceParameters instaneToTest = AzureDeviceParameters.ParseRepositoryGroup(configuartion.DataSets[0].RepositoryGroup);
            Assert.IsNotNull(instaneToTest);
            Assert.AreEqual<string>("AzureDeviceId", instaneToTest.AzureDeviceId);
            Assert.AreEqual<string>("AzurePrimaryKey", instaneToTest.AzurePrimaryKey);
            Assert.AreEqual<string>("AzureScopeId", instaneToTest.AzureScopeId);
            Assert.AreEqual<string>("AzureSecondaryKey", instaneToTest.AzureSecondaryKey);
            Assert.AreEqual<TimeSpan>(TimeSpan.FromSeconds(2.0), instaneToTest.PublishingInterval());
            Assert.AreEqual<TransportType>(TransportType.Http1, instaneToTest.TransportType);
            Assert.AreEqual<string>("BoilersArea_Boiler_#1", instaneToTest.ResourceGroupName);
        }

        private const string _ConfigurationFilePath = @".\ConfigurationDataConsumer.BoilersSet.xml";
    }
}