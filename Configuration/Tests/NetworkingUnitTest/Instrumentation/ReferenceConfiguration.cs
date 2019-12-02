//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest.Instrumentation
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
    internal static MessageHandlerConfiguration[] GetMessageHandlers(AssociationRole associationRole)
    {
      MessageHandlerConfiguration[] _ret = null;
      switch (associationRole)
      {
        case AssociationRole.Consumer:
          _ret = new MessageReaderConfiguration[] { new MessageReaderConfiguration() { ConsumerAssociationConfigurations = GetConsumerAssociationConfiguration(),
          Configuration = new MessageChannelConfiguration() { ChannelConfiguration  = "4840,True,239.255.255.1,True" },
          Name = "UDP",
          TransportRole = associationRole }};
          break;
        case AssociationRole.Producer:
          _ret = new MessageWriterConfiguration[] { new MessageWriterConfiguration() {  ProducerAssociationConfigurations = GetProducerAssociationConfiguration(),
          Configuration = new MessageChannelConfiguration () { ChannelConfiguration = "4840,localhost" },
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
      return new ProducerAssociationConfiguration[] { new ProducerAssociationConfiguration() { AssociationName = AssociationConfigurationAlias, DataSetWriterId = DefaultDataSetWriterId, PublisherId = DefaultAssociationConfigurationId, FieldEncoding = FieldEncodingEnum.VariantFieldEncoding } };
    }
    internal static TypeDictionary[] TypeDictionaries()
    {
      return new TypeDictionary[] { new TypeDictionary() { TargetNamespace = "http://commsvr.com/UAOOI/Configuration/Networking/UnitTest/TargetNamespace.xsd", Items = GetItems() } };
    }
    private static TypeDescription[] GetItems()
    {
      TypeDescription _ret = new StructuredType()
      {
        Name = "StructuredTypeName",
        StructureKind = StructureKindEnum.Structure,
        Field = GetFields()
      };
      TypeDescription _retEnum = new EnumeratedType()
      {
        Name = "EnumeratedTypeName",
        EnumeratedValues = GetEnumeratedValue()
      };
      return new TypeDescription[] { _ret, _retEnum };
    }
    private static EnumeratedValue[] GetEnumeratedValue()
    {
      List<EnumeratedValue> _ret = new List<EnumeratedValue>();
      _ret.Add(new EnumeratedValue()
      {
        Documentation =
          new LocalizedText[] { new LocalizedText() { Locale = "en-us", Value = "Documentation" }, new LocalizedText() { Locale = "pl-pl", Value = "Dokumentacja" } },
        Name = "Field1",
        Value = 0,
      });
      _ret.Add(new EnumeratedValue()
      {
        Documentation =
          new LocalizedText[] { new LocalizedText() { Locale = "en-us", Value = "Documentation" }, new LocalizedText() { Locale = "pl-pl", Value = "Dokumentacja" } },
        Name = "Field2",
        Value = 1,
      });
      _ret.Add(new EnumeratedValue()
      {
        Documentation =
          new LocalizedText[] { new LocalizedText() { Locale = "en-us", Value = "Documentation" }, new LocalizedText() { Locale = "pl-pl", Value = "Dokumentacja" } },
        Name = "Field3",
        Value = 2,
      });
      return _ret.ToArray<EnumeratedValue>();
    }
    private static FieldType[] GetFields()
    {
      List<FieldType> _fields = new List<FieldType>();
      _fields.Add(new FieldType() { Name = "FieldName1", TypeName = new System.Xml.XmlQualifiedName("Int32", @"http://opcfoundation.org/UA/"), SwitchOperandSpecified = false, SwitchValueSpecified = false });
      _fields.Add(new FieldType() { Name = "FieldName2", TypeName = new System.Xml.XmlQualifiedName("Int32", @"http://opcfoundation.org/UA/"), SwitchOperandSpecified = false, SwitchValueSpecified = false });
      return _fields.ToArray();
    }
    internal static DataSetConfiguration[] GetDataSetConfigurations(AssociationRole associationRole)
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
        new FieldMetaData() { ProcessValueName = "Value1", TypeInformation = new UATypeInfo( BuiltInType.DateTime) { TypeName = new System.Xml.XmlQualifiedName("Value1Name", "Value1NS") } , SymbolicName = "Value1" },
        new FieldMetaData() { ProcessValueName = "Value2", TypeInformation =  new UATypeInfo( BuiltInType.Double)  { TypeName = new System.Xml.XmlQualifiedName("Value2Name", "Value2NS") }, SymbolicName = "Value2" },
      };
    }
    #endregion

    #region Compare
    /// <exclude />
    internal static void Compare(ConfigurationData source, ConfigurationData mirror)
    {
      Assert.AreEqual<int>(source.DataSets.Length, mirror.DataSets.Length);
      CompareArrays<DataSetConfiguration>(source.DataSets, mirror.DataSets, x => x.AssociationName, CompareDataSetConfiguration);
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
        Compare((EnumeratedType)source, (EnumeratedType)mirror);
    }
    private static void Compare(EnumeratedType source, EnumeratedType mirror)
    {
      Assert.AreEqual<string>(source.Name, mirror.Name);
      CompareArrays<EnumeratedValue>(source.EnumeratedValues, mirror.EnumeratedValues, x => x.Name, CompareEnumeratedValue);
    }
    private static void CompareEnumeratedValue(EnumeratedValue source, EnumeratedValue mirror)
    {
      Assert.AreEqual<string>(source.Name, mirror.Name);
      Assert.AreEqual<int>(source.Value, mirror.Value);
      CompareArrays<LocalizedText>(source.Documentation, mirror.Documentation, x => x.Locale, (x, y) => Assert.AreEqual<string>(x.Value, y.Value));
    }
    private static void Compare(StructuredType source, StructuredType mirror)
    {
      Assert.AreEqual<StructureKindEnum>(source.StructureKind, mirror.StructureKind);
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
      Assert.AreEqual<Guid>(source.PublisherId, mirror.PublisherId);
    }
    private static void Compare(ConsumerAssociationConfiguration source, ConsumerAssociationConfiguration mirror)
    {
      Assert.AreEqual<string>(source.AssociationName, mirror.AssociationName);
      Assert.AreEqual<UInt32>(source.DataSetWriterId, mirror.DataSetWriterId);
      Assert.AreEqual<Guid>(source.PublisherId, mirror.PublisherId);
    }
    private static void CompareDataSetConfiguration(DataSetConfiguration source, DataSetConfiguration mirror)
    {
      Assert.AreEqual<string>(source.AssociationName, mirror.AssociationName);
      Assert.AreEqual<AssociationRole>(source.AssociationRole, mirror.AssociationRole);
      Assert.AreEqual<Guid>(source.ConfigurationGuid, mirror.ConfigurationGuid);
      Compare(source.ConfigurationVersion, mirror.ConfigurationVersion);
      Assert.AreEqual<string>(source.DataSymbolicName, mirror.DataSymbolicName);
      CompareArrays<FieldMetaData>(source.DataSet, mirror.DataSet, x => x.SymbolicName, CompareFieldMetaData);
      Assert.AreEqual<string>(source.Guid, mirror.Guid);
      Assert.AreEqual<Guid>(source.Id, mirror.Id);
      Assert.AreEqual<string>(source.InformationModelURI, mirror.InformationModelURI);
      Assert.AreEqual<double>(source.MaxBufferTime, mirror.MaxBufferTime);
      Assert.AreEqual<double>(source.PublishingInterval, mirror.PublishingInterval);
      Assert.AreEqual<string>(source.RepositoryGroup, mirror.RepositoryGroup);
      Compare(source.Root, mirror.Root);
    }
    private static void Compare(ConfigurationVersionDataType source, ConfigurationVersionDataType mirror)
    {
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      Assert.AreEqual<int>(source.MajorVersion, mirror.MajorVersion);
      Assert.AreEqual<int>(source.MinorVersion, mirror.MinorVersion);
    }
    private static void CompareFieldMetaData(FieldMetaData source, FieldMetaData mirror)
    {
      Assert.AreEqual<string>(source.ProcessValueName, mirror.ProcessValueName);
      Assert.AreEqual<string>(source.SymbolicName, mirror.SymbolicName);
      Compare(source.TypeInformation, mirror.TypeInformation);
    }
    private static void Compare(UATypeInfo source, UATypeInfo mirror)
    {
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      CollectionAssert.AreEqual(source.ArrayDimensions, mirror.ArrayDimensions);
      Assert.AreEqual<BuiltInType>(source.BuiltInType, mirror.BuiltInType);
      Compare(source.TypeName, mirror.TypeName);
      Assert.AreEqual<int>(source.ValueRank, mirror.ValueRank);
    }
    private static void Compare(XmlQualifiedName source, XmlQualifiedName mirror)
    {
      if (source == null && mirror == null)
        return;
      Assert.IsNotNull(source);
      Assert.IsNotNull(mirror);
      if (source.IsEmpty && mirror.IsEmpty)
        return;
      Assert.AreEqual<bool>(source.IsEmpty, mirror.IsEmpty);
      Assert.AreEqual<string>(source.Name, mirror.Name);
      Assert.AreEqual<string>(source.Namespace, mirror.Namespace);
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
