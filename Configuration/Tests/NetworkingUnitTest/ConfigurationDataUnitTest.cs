
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Configuration.Networking.UnitTest
{

  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]
  public class ConfigurationDataUnitTest
  {
    #region TestMethod
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void AfterCreationTest()
    {
      ConfigurationData _newOne = new ConfigurationData();
      Assert.IsNull(_newOne.DataSets);
      Assert.IsNull(_newOne.ExtensionData);
      Assert.AreSame(_newOne, _newOne.GetConfigurationData());
      Assert.IsNull(_newOne.MessageHandlers);
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void LoadSaveTestMethod()
    {
      LocalConfigurationData _configuration = ConfigurationDataFactoryIO.Load<LocalConfigurationData>(LocalConfigurationData.Loader, () => { });
      Assert.IsNotNull(_configuration);
      Assert.AreEqual<int>(1, _configuration.OnLoadedCount);
      Assert.AreEqual<int>(0, _configuration.OnSavingCount);
      ConfigurationDataFactoryIO.Save<LocalConfigurationData>(_configuration, (x) => { Assert.AreEqual<int>(1, x.OnSavingCount); });
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void SaveLoadTestMethod()
    {
      SaveLoadConfigurationData(Role.Consumer, SerializerType.Xml);
      SaveLoadConfigurationData(Role.Consumer, SerializerType.Json);
      SaveLoadConfigurationData(Role.Producer, SerializerType.Xml);
      SaveLoadConfigurationData(Role.Producer, SerializerType.Json);
      //Assert.Fail();
    }
    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void LoadUsingSerializerTestMethod()
    {
      LoadUsingSerializer(Role.Consumer, SerializerType.Xml);
      LoadUsingSerializer(Role.Consumer, SerializerType.Json);
      LoadUsingSerializer(Role.Producer, SerializerType.Xml);
      LoadUsingSerializer(Role.Producer, SerializerType.Json);
    }

    [TestMethod]
    [TestCategory("Configuration_SerializationUnitTest")]
    public void ExportXSD()
    {
      //create schema
      XsdDataContractExporter _exporter = new XsdDataContractExporter();
      Type _ConfigurationDataType = typeof(ConfigurationData);
      Assert.IsTrue(_exporter.CanExport(_ConfigurationDataType));

      _exporter.Export(_ConfigurationDataType);
      Console.WriteLine("number of schemas: {0}", _exporter.Schemas.Count);
      Console.WriteLine();

      //write out the schema
      XmlSchemaSet _Schemas = _exporter.Schemas;
      XmlQualifiedName XmlNameValue = _exporter.GetRootElementName(_ConfigurationDataType);
      string EmployeeNameSpace = XmlNameValue.Namespace;
      foreach (XmlSchema _schema in _Schemas.Schemas(EmployeeNameSpace))
        _schema.Write(Console.Out);
    }
    #endregion

    #region private
    private class LocalConfigurationData : ConfigurationData
    {
      /// <summary>
      /// Loads this <see cref="LocalConfigurationData"/>.
      /// </summary>
      /// <returns>LocalConfigurationData.</returns>
      internal static LocalConfigurationData Loader()
      {
        return new LocalConfigurationData();
      }
      public LocalConfigurationData() { }
      /// <summary>
      /// Called when the configuration is loaded.
      /// </summary>
      public override void OnLoaded()
      {
        base.OnLoaded();
        OnLoadedCount++;
      }
      /// <summary>
      /// Called before the saving the configuration.
      /// </summary>
      public override void OnSaving()
      {
        base.OnSaving();
        OnSavingCount++;
      }

      #region test instrumentation
      /// <exclude />
      internal int OnLoadedCount = 0;
      /// <exclude />
      internal int OnSavingCount = 0;
      #endregion

    }
    private enum Role { Producer, Consumer };
    private void SaveLoadConfigurationData(Role role, SerializerType serializer)
    {
      FileInfo _fileInfo = GetFileName(role, serializer, @"ConfigurationData{0}.{1}");
      ConfigurationData _configuration = null;
      switch (role)
      {
        case Role.Producer:
          _configuration = ReferenceConfiguration.LoadProducer();
          break;
        case Role.Consumer:
          _configuration = ReferenceConfiguration.LoadConsumer();
          break;
        default:
          break;
      }
      ConfigurationDataFactoryIO.Save<ConfigurationData>(_configuration, serializer, _fileInfo, (x, y, z) => { Console.WriteLine(z); });
      _fileInfo.Refresh();
      Assert.IsTrue(_fileInfo.Exists);
      ConfigurationData _mirror = ConfigurationDataFactoryIO.Load<ConfigurationData>(serializer, _fileInfo, (x, y, z) => { Console.WriteLine(z); }, () => { });
      ReferenceConfiguration.Compare(_configuration, _mirror);
    }
    private void LoadUsingSerializer(Role role, SerializerType serializer)
    {
      FileInfo _fileInfo = GetFileName(role, serializer, @"TestData\ConfigurationData{0}.{1}");
      Assert.IsTrue(_fileInfo.Exists, _fileInfo.ToString());
      ConfigurationData _mirror = null;
      ConfigurationData _source = null;
      switch (role)
      {
        case Role.Producer:
          _source = ReferenceConfiguration.LoadProducer();
          break;
        case Role.Consumer:
          _source = ReferenceConfiguration.LoadConsumer();
          break;
      }
      string _message = null;
      switch (serializer)
      {
        case SerializerType.Json:
          _mirror = ConfigurationDataFactoryIO.Load<ConfigurationData>
            (() => JSONDataContractSerializers.Load<ConfigurationData>(_fileInfo, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); }), () => { });
          break;
        case SerializerType.Xml:
          _mirror = ConfigurationDataFactoryIO.Load<ConfigurationData>
            (() => XmlDataContractSerializers.Load<ConfigurationData>(_fileInfo, (x, y, z) => { _message = z; Assert.AreEqual<TraceEventType>(TraceEventType.Verbose, x); }), () => { });
          break;
      }
      Console.WriteLine(_message);
      Assert.IsNotNull(_mirror);
      Assert.IsFalse(String.IsNullOrEmpty(_message));
      Assert.IsTrue(_message.Contains(_fileInfo.FullName));
      ReferenceConfiguration.Compare(_source, _mirror);
    }
    private static FileInfo GetFileName(Role role, SerializerType serializer, string fileNameTemplate)
    {
      string _extension = serializer == SerializerType.Xml ? "xml" : "json";
      string _fileName = String.Format(fileNameTemplate, role, _extension);
      return new FileInfo(_fileName);
    }
    #endregion
  }
}

