
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using UAOOI.DataBindings.Serializers;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]

  public class SerializationUnitTest
  {
    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void GuidTestMethod1()
    {
      Guid _newGuid = Guid.NewGuid();
      DataSetConfiguration _newConfiguration = new DataSetConfiguration()
      {
        Id = _newGuid,
      };
      Assert.IsNotNull(_newConfiguration);
      Assert.AreEqual<Guid>(_newGuid, _newConfiguration.Id);
    }
    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void AssociationNamesTestMethod()
    {
      MessageHandlerConfiguration _transport = new MessageHandlerConfiguration();
      string[] _refArray = new string[] { "First", "Second", "Third" };
      _transport.AssociationNames = _refArray;
      CollectionAssert.AreEqual(_transport.AssociationNames, _refArray);
      Assert.AreEqual<int>(_refArray.Length, _transport.AssociationNamesArrayOfString.Count);
    }
    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void ConfigurationDataXmlTestMethod()
    {
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationData.xml");
      Assert.IsTrue(_configFile.Exists);
      string _message = null;
      ConfigurationData _cd = ConfigurationData.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configFile, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); }));
      Console.WriteLine(_message);
      Assert.IsNotNull(_cd);
      Assert.IsFalse(String.IsNullOrEmpty(_message));
      Assert.IsTrue(_message.Contains(_configFile.FullName));
      FileInfo _2Write = new FileInfo(@"TestData\ConfigurationData.bak");
      XmlDataContractSerializers.Save<ConfigurationData>(_2Write, _cd, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); });
      FileInfo _2Test = new FileInfo(@"TestData\ConfigurationData.bak");
      Assert.IsTrue(_2Test.Exists);
      Console.WriteLine(_message);
    }
    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void ConfigurationDataConsumerXmlTestMethod()
    {
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataConsumer.xml");
      Assert.IsTrue(_configFile.Exists);
      string _message = null;
      ConfigurationData _cd = ConfigurationData.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configFile, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); }));
      Console.WriteLine(_message);
      Assert.IsNotNull(_cd);
      Assert.IsFalse(String.IsNullOrEmpty(_message));
      Assert.IsTrue(_message.Contains(_configFile.FullName));
    }
    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void ConfigurationDataProducerXmlTestMethod()
    {
      FileInfo _configFile = new FileInfo(@"TestData\ConfigurationDataProducer.xml");
      Assert.IsTrue(_configFile.Exists);
      string _message = null;
      ConfigurationData _cd = ConfigurationData.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configFile, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); }));
      Console.WriteLine(_message);
      Assert.IsNotNull(_cd);
      Assert.IsFalse(String.IsNullOrEmpty(_message));
      Assert.IsTrue(_message.Contains(_configFile.FullName));
    }

  }

}
