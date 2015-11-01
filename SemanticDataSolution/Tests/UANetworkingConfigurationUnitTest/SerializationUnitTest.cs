using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using System.IO;
using UAOOI.DataBindings.Serializers;
using System.Diagnostics;

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
      MessageTransportConfiguration _transport = new MessageTransportConfiguration();
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
      ConfigurationData _cd = DataContractSerializers.Load<ConfigurationData>(_configFile, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); });
      Console.WriteLine(_message);
      Assert.IsNotNull(_cd);
      Assert.IsFalse(String.IsNullOrEmpty(_message));
      Assert.IsTrue(_message.Contains(_configFile.FullName));
      FileInfo _2Write = new FileInfo(@"TestData\ConfigurationData.bak");
      DataContractSerializers.Save<ConfigurationData>(_2Write, _cd, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); });
      Console.WriteLine(_message);
    }

  }

}
