
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest
{
  /// <summary>
  /// Class ReferenceConfiguration - creates a configuration for testing purpose.
  /// </summary>
  internal static class ReferenceConfiguration
  {

    #region API
    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData LoadConsumer()
    {
      return new ConfigurationData() { DataSets = GetDataSetConfigurations(AssociationRole.Consumer), MessageHandlers = GetMessageHandlers(AssociationRole.Consumer), TypeDictionaries = TypeDictionaries() };
    }

    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData LoadProducer()
    {
      return new ConfigurationData() { DataSets = GetDataSetConfigurations(AssociationRole.Producer), MessageHandlers = GetMessageHandlers(AssociationRole.Producer), TypeDictionaries = TypeDictionaries() };
    }
    #endregion

    #region configuration
    private static MessageHandlerConfiguration[] GetMessageHandlers(AssociationRole associationRole)
    {
      MessageHandlerConfiguration[] _ret = null;
      switch (associationRole)
      {
        case AssociationRole.Consumer:
          _ret = new MessageReaderConfiguration[] { new MessageReaderConfiguration() { ConsumerAssociationConfigurations = GetConsumerAssociationConfiguration(),
          Configuration = null,
          Name = "UDP",
          TransportRole = associationRole }};
          break;
        case AssociationRole.Producer:
          _ret = new MessageWriterConfiguration[] { new MessageWriterConfiguration() {  ProducerAssociationConfigurations = GetProducerAssociationConfiguration(),
          Configuration = null,
          Name = "UDP",
          TransportRole = associationRole }};
          break;
      }
      return _ret;
    }
    private static ConsumerAssociationConfiguration[] GetConsumerAssociationConfiguration()
    {
      return new ConsumerAssociationConfiguration[] { new ConsumerAssociationConfiguration() { AssociationName = AssociationConfigurationAlias, DataSetWriterId = DefaultDataSetWriterId, PublisherId = DefaultAssociationConfigurationId } };
    }
    private static ProducerAssociationConfiguration[] GetProducerAssociationConfiguration()
    {
      return new ProducerAssociationConfiguration[] { new ProducerAssociationConfiguration() { AssociationName = AssociationConfigurationAlias, DataSetWriterId = DefaultDataSetWriterId, FieldEncoding = FieldEncodingEnum.VariantFieldEncoding } };
    }
    private static TypeDictionary[] TypeDictionaries()
    {
      return new TypeDictionary[] { new TypeDictionary() { TargetNamespace = "http://commsvr.com/UAOOI/Configuration/Networking/UnitTest/TargetNamespace.xsd", Items = GetItems() } };
    }
    private static TypeDescription[] GetItems()
    {
      TypeDescription _ret = new StructuredType()
      {
        Name = "StructuredTypeName",
        Field = GetFields()
      };
      return new TypeDescription[] { _ret };
    }
    private static FieldType[] GetFields()
    {
      List<FieldType> _fields = new List<FieldType>();
      _fields.Add(new FieldType() { Name = "FieldName1", TypeName = new System.Xml.XmlQualifiedName("Int32", @"http://opcfoundation.org/UA/"), SwitchOperandSpecified = false, SwitchValueSpecified = false });
      _fields.Add(new FieldType() { Name = "FieldName2", TypeName = new System.Xml.XmlQualifiedName("Int32", @"http://opcfoundation.org/UA/"), SwitchOperandSpecified = false, SwitchValueSpecified = false });
      return _fields.ToArray();
    }
    private static DataSetConfiguration[] GetDataSetConfigurations(AssociationRole associationRole)
    {
      return new DataSetConfiguration[]
      { new DataSetConfiguration()
        { AssociationName = AssociationConfigurationAlias,
          AssociationRole = associationRole,
          DataSet = GetMembers(),
          DataSymbolicName = "DataSymbolicName",
          Id = DefaultAssociationConfigurationId,
          RepositoryGroup = m_RepositoryGroup,
          InformationModelURI= AssociationConfigurationInformationModelURI,
          ConfigurationGuid = m_ConfigurationGuid,
          ConfigurationVersion = new ConfigurationVersionDataType() { MajorVersion = 0x0, MinorVersion=0x0  },
          MaxBufferTime = 100,
          PublishingInterval = 1000,
          Root = new NodeDescriptor(  ) { NodeIdentifier = new System.Xml.XmlQualifiedName("NodeDescriptor", "NodeDescriptorNS")  }
        }
      };
    }
    private static FieldMetaData[] GetMembers()
    {
      return new FieldMetaData[]
      {
        new FieldMetaData() { ProcessValueName = "Value1", TypeInformation = new UATypeInfo( BuiltInType.DateTime), SymbolicName = "Value1" },
        new FieldMetaData() { ProcessValueName = "Value2", TypeInformation =  new UATypeInfo( BuiltInType.Double), SymbolicName = "Value2" },
      };
    }
    #endregion

    #region Compare
    /// <exclude />
    internal static void Compare(ConfigurationData source, ConfigurationData mirror)
    {
      Assert.AreEqual<int>(source.DataSets.Length, mirror.DataSets.Length);
      Compare(source.DataSets, mirror.DataSets);
      Assert.AreEqual<int>(source.MessageHandlers.Length, mirror.MessageHandlers.Length);
      Compare(source.MessageHandlers, mirror.MessageHandlers);
      CompareArrays<TypeDictionary>(source.TypeDictionaries, mirror.TypeDictionaries, x => x.TargetNamespace, CompareTypeDictionary);
    }
    private static void CompareTypeDictionary(TypeDictionary source, TypeDictionary mirror)
    {
      CompareArrays<TypeDescription>(source.Items, mirror.Items, x => x.Name, CompareTypeDescription);
      Assert.AreEqual<string>(source.TargetNamespace, mirror.TargetNamespace);
    }
    private static void CompareTypeDescription(TypeDescription source, TypeDescription mirror)
    {
      Assert.AreEqual<string>(source.Name, mirror.Name);
      Assert.AreSame(source.GetType(), mirror.GetType());
      if (source is StructuredType)
        Compare((StructuredType)source, (StructuredType)mirror);
      else
        Assert.Fail();
    }
    private static void Compare(StructuredType source, StructuredType mirror)
    {
      CompareArrays<FieldType>(source.Field, mirror.Field, x => x.Name, CompareFieldType);
    }
    private static void CompareFieldType(FieldType source, FieldType mirror)
    {
      Assert.AreEqual<string>(source.Name, mirror.Name);
      Assert.AreEqual<string>(source.SwitchField, mirror.SwitchField);
      Assert.AreEqual<bool>(source.SwitchOperandSpecified, mirror.SwitchOperandSpecified);
      if (source.SwitchOperandSpecified)
        Assert.AreEqual<SwitchOperand>(source.SwitchOperand, mirror.SwitchOperand);
      Assert.AreEqual<bool>(source.SwitchValueSpecified, mirror.SwitchValueSpecified);
      if (source.SwitchValueSpecified)
        Assert.AreEqual<uint>(source.SwitchValue, mirror.SwitchValue);
      Assert.AreEqual<XmlQualifiedName>(source.TypeName, mirror.TypeName);
    }
    private static void CompareArrays<type>(type[] source, type[] mirror, Func<type, string> selector, Action<type, type> compareItems)
      where type : class
    {
      if (source == null && mirror == null)
        return;
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      Assert.AreEqual<int>(source.Length, mirror.Length);
      if (source.Length == 0)
        return;
      Dictionary<string, type> _dictionary = source.ToDictionary<type, string>(selector);
      foreach (type _item in mirror)
        compareItems(_dictionary[selector(_item)], _item);
    }
    private static void Compare(MessageHandlerConfiguration[] source, MessageHandlerConfiguration[] mirror)
    {
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      Dictionary<string, MessageHandlerConfiguration> _mirror2Dictionary = mirror.ToDictionary<MessageHandlerConfiguration, string>(x => x.Name);
      foreach (MessageHandlerConfiguration _configItem in source)
        Compare(_configItem, _mirror2Dictionary[_configItem.Name]);
    }
    private static void Compare(MessageHandlerConfiguration source, MessageHandlerConfiguration mirror)
    {
      switch (source.TransportRole)
      {
        case AssociationRole.Consumer:
          CompareMessageReaderConfiguration((MessageReaderConfiguration)source, (MessageReaderConfiguration)mirror);
          break;
        case AssociationRole.Producer:
          CompareMessageWriterConfiguration((MessageWriterConfiguration)source, (MessageWriterConfiguration)mirror);
          break;
      }
    }
    private static void CompareMessageWriterConfiguration(MessageWriterConfiguration source, MessageWriterConfiguration mirror)
    {
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      Assert.AreEqual<AssociationRole>(source.TransportRole, mirror.TransportRole);
      Dictionary<string, ProducerAssociationConfiguration> _mirror2Dictionary = mirror.ProducerAssociationConfigurations.ToDictionary<ProducerAssociationConfiguration, string>(x => x.AssociationName);
      foreach (ProducerAssociationConfiguration _item in source.ProducerAssociationConfigurations)
        Compare(_item, _mirror2Dictionary[_item.AssociationName]);
    }
    private static void CompareMessageReaderConfiguration(MessageReaderConfiguration source, MessageReaderConfiguration mirror)
    {
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      Assert.AreEqual<AssociationRole>(source.TransportRole, mirror.TransportRole);
      Assert.IsNotNull(mirror.ConsumerAssociationConfigurations);
      Dictionary<string, ConsumerAssociationConfiguration> _mirror2Dictionary = mirror.ConsumerAssociationConfigurations.ToDictionary<ConsumerAssociationConfiguration, string>(x => x.AssociationName);
      Assert.IsNotNull(source.ConsumerAssociationConfigurations);
      foreach (ConsumerAssociationConfiguration _item in source.ConsumerAssociationConfigurations)
      {
        Assert.IsTrue(_mirror2Dictionary.ContainsKey(_item.AssociationName));
        Compare(_item, _mirror2Dictionary[_item.AssociationName]);
      }
    }
    private static void Compare(ProducerAssociationConfiguration source, ProducerAssociationConfiguration mirror)
    {
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      Assert.AreEqual<string>(source.AssociationName, mirror.AssociationName);
      Assert.AreEqual<UInt32>(source.DataSetWriterId, mirror.DataSetWriterId);
    }
    private static void Compare(ConsumerAssociationConfiguration source, ConsumerAssociationConfiguration mirror)
    {
      Assert.AreEqual<string>(source.AssociationName, mirror.AssociationName);
      Assert.AreEqual<UInt32>(source.DataSetWriterId, mirror.DataSetWriterId);
      Assert.AreEqual<Guid>(source.PublisherId, mirror.PublisherId);
    }
    private static void Compare(DataSetConfiguration[] dataSets1, DataSetConfiguration[] dataSets2)
    {
      Dictionary<string, DataSetConfiguration> _dataSets2Dictionary = dataSets2.ToDictionary<DataSetConfiguration, string>(x => x.AssociationName);
      foreach (DataSetConfiguration item in dataSets1)
        Compare(item, _dataSets2Dictionary[item.AssociationName]);
    }
    private static void Compare(DataSetConfiguration item1, DataSetConfiguration item2)
    {
      Assert.AreEqual<AssociationRole>(item1.AssociationRole, item2.AssociationRole);
      Assert.AreEqual<string>(item1.DataSymbolicName, item2.DataSymbolicName);
      Assert.AreEqual<string>(item1.Guid, item2.Guid);
      Assert.AreEqual<string>(item1.InformationModelURI, item2.InformationModelURI);
      Assert.AreEqual<string>(item1.RepositoryGroup, item2.RepositoryGroup);
      Compare(item1.Root, item2.Root);
    }
    private static void Compare(NodeDescriptor item1, NodeDescriptor item2)
    {
      if (item1 == null && item2 == null)
        return;
      Assert.IsNotNull(item1);
      Assert.IsNotNull(item2);
      Assert.AreEqual<string>(item1.BindingDescription, item2.BindingDescription);
      Assert.AreEqual<XmlQualifiedName>(item1.DataType, item2.DataType);
      Assert.AreEqual<bool>(item1.InstanceDeclaration, item2.InstanceDeclaration);
      Assert.AreEqual<InstanceNodeClassesEnum>(item1.NodeClass, item2.NodeClass);
      Assert.AreEqual<XmlQualifiedName>(item1.NodeIdentifier, item2.NodeIdentifier);
      Assert.AreEqual<string>(item1.ToString(), item2.ToString());
    }

    #endregion
    #region preconfigured settings
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = @"https://github.com/mpostol/OPC-UA-OOI";
    private static readonly Guid DefaultAssociationConfigurationId = new Guid("C1F53FFB-6552-4CCC-84C9-F847147CDC85");
    private const UInt16 DefaultDataSetWriterId = 12345;
    private static Guid m_ConfigurationGuid = new Guid("D3DEA20A-1F65-4744-ABF5-3D8120960D7B");
    #endregion

  }
}
