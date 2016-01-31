
using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest
{
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
      return new ConfigurationData() { DataSets = GetDataSetConfigurations(AssociationRole.Producer), MessageHandlers = GetMessageHandlers(AssociationRole.Producer), TypeDictionaries  = TypeDictionaries()};
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
      return new TypeDictionary[] { new TypeDictionary() { DefaultByteOrderSpecified = false, TargetNamespace = "http://commsvr.com/UAOOI/Configuration/Networking/UnitTest/TargetNamespace.xsd",  } };
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
