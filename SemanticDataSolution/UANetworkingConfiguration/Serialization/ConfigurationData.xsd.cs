
using System.Runtime.Serialization;
using System;
using System.Xml.Serialization;
using System.Xml;

[assembly: ContractNamespaceAttribute("http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/Serialization.xsd", ClrNamespace = "UAOOI.SemanticData.UANetworking.Configuration.Serialization")]

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [DataContractAttribute(Name = "ConfigurationData", Namespace = CommonDefinitions.Namespace)]
  [System.SerializableAttribute()]
  [XmlRoot(Namespace = CommonDefinitions.Namespace)]
  //[XmlType(Namespace = CommonDefinitions.Namespace)]
  public partial class ConfigurationData : object, IExtensibleDataObject
  {

    private ExtensionDataObject extensionDataField;
    private DataSetConfiguration[] DataSetsField;
    private MessageHandlerConfiguration[] MessageHandlersField;
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [DataMemberAttribute(EmitDefaultValue = false)]
    [XmlElementAttribute(IsNullable = false)]
    public DataSetConfiguration[] DataSets
    {
      get
      {
        return this.DataSetsField;
      }
      set
      {
        this.DataSetsField = value;
      }
    }

    [DataMemberAttribute(EmitDefaultValue = true)]
    //[XmlElementAttribute(IsNullable = false)]
    [XmlArray(IsNullable = false)]
    [XmlArrayItem(Type = typeof(MessageWriterConfiguration), ElementName = "MessageWriterConfiguration") ]
    [XmlArrayItem(Type = typeof(MessageReaderConfiguration), ElementName = "MessageReaderConfiguration")]
    public MessageHandlerConfiguration[] MessageHandlers
    {
      get
      {
        return this.MessageHandlersField;
      }
      set
      {
        this.MessageHandlersField = value;
      }
    }

  }

  [DataContractAttribute(Name = "DataSetConfiguration", Namespace = CommonDefinitions.Namespace)]
  [System.SerializableAttribute()]
  public partial class DataSetConfiguration : object, IExtensibleDataObject
  {

    #region private
    [System.NonSerializedAttribute()]
    private ExtensionDataObject extensionDataField;
    private AssociationRole AssociationRoleField;
    [OptionalFieldAttribute()]
    private string AssociationNameField;
    [OptionalFieldAttribute()]
    private string RepositoryGroupField;
    [OptionalFieldAttribute()]
    private string InformationModelURIField;
    [OptionalFieldAttribute()]
    private string DataSymbolicNameField;
    [OptionalFieldAttribute()]
    private FieldMetaData[] DataSetField;
    [OptionalFieldAttribute()]
    private string GuidField;
    [OptionalFieldAttribute()]
    private NodeDescriptor RootField;
    [OptionalFieldAttribute()]
    private double PublishingIntervalField;
    [OptionalFieldAttribute()]
    private double MaxBufferTimeField;
    [OptionalFieldAttribute()]
    private Guid ConfigurationGuidField;
    [OptionalFieldAttribute()]
    private ConfigurationVersionDataType ConfigurationVersionField;
    #endregion

    #region public
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }
    [DataMemberAttribute(IsRequired = true)]
    public AssociationRole AssociationRole
    {
      get
      {
        return this.AssociationRoleField;
      }
      set
      {
        this.AssociationRoleField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public string AssociationName
    {
      get
      {
        return this.AssociationNameField;
      }
      set
      {
        this.AssociationNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public string RepositoryGroup
    {
      get
      {
        return this.RepositoryGroupField;
      }
      set
      {
        this.RepositoryGroupField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
    public string InformationModelURI
    {
      get
      {
        return this.InformationModelURIField;
      }
      set
      {
        this.InformationModelURIField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 4)]
    public string DataSymbolicName
    {
      get
      {
        return this.DataSymbolicNameField;
      }
      set
      {
        this.DataSymbolicNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 5)]
    public FieldMetaData[] DataSet
    {
      get
      {
        return this.DataSetField;
      }
      set
      {
        this.DataSetField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 6)]
    public string Guid
    {
      get
      {
        return this.GuidField;
      }
      set
      {
        this.GuidField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 7, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public NodeDescriptor Root
    {
      get { return RootField; }
      set { RootField = value; }
    }
    /// <summary>
    /// Gets or sets the publishing interval - The interval in milliseconds for sampling the Variables and publishing the Values in a DataSet by the related MessageWriter. 
    /// The Duration DataType is a subtype of Double and allows configuration of intervals smaller than a millisecond.
    /// </summary>
    /// <value>The publishing interval.</value>
    [DataMemberAttribute(EmitDefaultValue = true, Order = 8, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public double PublishingInterval
    {
      get { return PublishingIntervalField; }
      set { PublishingIntervalField = value; }
    }
    /// <summary>
    /// Gets or sets the maximum buffer time. The MaxBufferTime defines the maximum time the delivery of the DataSet may be delayed by the 
    /// MessageWriter, to allow for the collection of additional Messages. This parameter allows the Producer to reduce the number of network packets necessary to send the Messages.
    /// </summary>
    /// <value>The maximum buffer time.</value>
    [DataMemberAttribute(EmitDefaultValue = true, Order = 9, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public double MaxBufferTime
    {
      get { return MaxBufferTimeField; }
      set { MaxBufferTimeField = value; }
    }
    /// <summary>
    /// Gets or sets the configuration unique identifier. It provides a unique identifier for the current configuration of this object. 
    /// Any change of the ConfigurationVersion Property triggers a creation of a new value.
    /// </summary>
    /// <value>The configuration unique identifier.</value>
    [DataMemberAttribute(EmitDefaultValue = true, Order = 10, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public Guid ConfigurationGuid
    {
      get { return ConfigurationGuidField; }
      set { ConfigurationGuidField = value; }
    }
    [DataMemberAttribute(EmitDefaultValue = true, Order = 11, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public ConfigurationVersionDataType ConfigurationVersion
    {
      get { return ConfigurationVersionField; }
      set { ConfigurationVersionField = value; }
    }
    #endregion

  }
  public class ConfigurationVersionDataType
  {
    private byte MajorVersionField;
    private byte MinorVersionField;

    /// <summary>
    /// Gets or sets the major version. The major number reflects the primary format of the DataSet and must be equal in both Producer and Consumer.
    /// Removing fields from the DataSet, reordering fields, adding fields in between other fields or a DataType change in fields shall result in an update of the MajorVersion. 
    /// The initial value for the MajorVersion is 0. If the MajorVersion is incremented, the MinorVersion shall be set to 0.
    /// An overflow of the MajorVersion is treated like any other major version change and requires a meta data exchange.
    /// </summary>
    /// <value>The major version.</value>
    public byte MajorVersion
    {
      get { return MajorVersionField; }
      set { MajorVersionField = value; }
    }
    /// <summary>
    /// Gets or sets the minor version. The minor number reflects backward compatible changes of the DataSet like adding a field at the end of the DataSet.
    /// The initial value for the MinorVersion is 0. The MajorVersion shall be incremented after an overflow of the MinorVersion.
    /// </summary>
    /// <value>The minor version.</value>
    public byte MinorVersion
    {
      get { return MinorVersionField; }
      set { MinorVersionField = value; }
    }
  }
  [DataContractAttribute(Name = "AssociationConfiguration", Namespace = CommonDefinitions.Namespace)]
  [KnownType(typeof(ProducerAssociationConfiguration))]
  [KnownType(typeof(ConsumerAssociationConfiguration))]
  [SerializableAttribute()]
  public partial class AssociationConfiguration
  {

    private string AssociationNameField;
    private UInt32 DataSetWriterIdField;

    [DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
    public string AssociationName
    {
      get { return AssociationNameField; }
      set { AssociationNameField = value; }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public UInt32 DataSetWriterId
    {
      get { return DataSetWriterIdField; }
      set { DataSetWriterIdField = value; }
    }
  }
  [DataContractAttribute(Name = "ProducerAssociationConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class ProducerAssociationConfiguration : AssociationConfiguration { }
  [DataContractAttribute(Name = "ConsumerAssociationConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class ConsumerAssociationConfiguration : AssociationConfiguration
  {
    private Guid PublisherIdField;
    [DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
    public Guid PublisherId
    {
      get { return PublisherIdField; }
      set { PublisherIdField = value; }
    }
  }
  [DataContractAttribute(Name = "MessageWriterConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class MessageWriterConfiguration : MessageHandlerConfiguration
  {
    private ProducerAssociationConfiguration[] ProducerAssociationConfigurationField;

    [DataMemberAttribute(EmitDefaultValue = false)]
    [XmlArray(ElementName = "ProducerAssociationConfigurations")]
    public ProducerAssociationConfiguration[] ProducerAssociationConfigurations
    {
      get { return ProducerAssociationConfigurationField; }
      set { ProducerAssociationConfigurationField = value; }
    }

  }
  [DataContractAttribute(Name = "MessageReaderConfiguration")]
  [SerializableAttribute()]
  public partial class MessageReaderConfiguration : MessageHandlerConfiguration
  {

    private ConsumerAssociationConfiguration[] ConsumerAssociationConfigurationsFields;

    [DataMemberAttribute(EmitDefaultValue = false)]
    [XmlArray(ElementName = "ConsumerAssociationConfigurations")]
    public ConsumerAssociationConfiguration[] ConsumerAssociationConfigurations
    {
      get { return ConsumerAssociationConfigurationsFields; }
      set { ConsumerAssociationConfigurationsFields = value; }
    }

  }
  //[DataContractAttribute(Name = "MessageHandlerConfiguration", Namespace = CommonDefinitions.Namespace)]
  [DataContractAttribute()]
  [KnownType(typeof(MessageReaderConfiguration))]
  [KnownType(typeof(MessageWriterConfiguration))]
  [SerializableAttribute()]
  public partial class MessageHandlerConfiguration : object, IExtensibleDataObject
  {

    [System.NonSerializedAttribute()]
    private ExtensionDataObject extensionDataField;
    private string NameField;
    private MessageChannelConfiguration ConfigurationField;
    private AssociationRole TransportRoleField;

    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false)]
    public string Name
    {
      get
      {
        return this.NameField;
      }
      set
      {
        this.NameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public MessageChannelConfiguration Configuration
    {
      get
      {
        return this.ConfigurationField;
      }
      set
      {
        this.ConfigurationField = value;
      }
    }
    [DataMemberAttribute(IsRequired = true, Order = 3)]
    public AssociationRole TransportRole
    {
      get
      {
        return this.TransportRoleField;
      }
      set
      {
        this.TransportRoleField = value;
      }
    }

  }
  [DataContractAttribute(Name = "MessageChannelConfiguration", Namespace = CommonDefinitions.Namespace)]
  public class MessageChannelConfiguration
  {

  }
  [DataContractAttribute(Name = "AssociationRole", Namespace = CommonDefinitions.Namespace)]
  public enum AssociationRole : int
  {

    [EnumMemberAttribute()]
    Consumer = 0,

    [EnumMemberAttribute()]
    Producer = 1,
  }
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [DataContractAttribute(Name = "DataMemberConfiguration", Namespace = CommonDefinitions.Namespace)]
  [System.SerializableAttribute()]
  public partial class FieldMetaData : object, IExtensibleDataObject
  {

    #region private
    private ExtensionDataObject extensionDataField;
    private string SymbolicNameField;
    private string ProcessValueNameField;
    private BuiltInType SourceEncodingField;
    private int[] ArrayDimensionsField;
    private int ValueRankField;
    #endregion

    #region public
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }
    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>The name of the field.</value>
    [DataMemberAttribute(EmitDefaultValue = false)]
    public string SymbolicName
    {
      get
      {
        return this.SymbolicNameField;
      }
      set
      {
        this.SymbolicNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public string ProcessValueName
    {
      get
      {
        return this.ProcessValueNameField;
      }
      set
      {
        this.ProcessValueNameField = value;
      }
    }
    /// <summary>
    /// Gets or sets the source encoding information.  
    /// </summary>
    /// <remarks>
    /// TODO it must provide information about built in data type or it should be a TypeDescriptor - see how it is serialized.
    /// </remarks>
    /// <value>The source encoding.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public BuiltInType Encoding
    {
      get
      {
        return this.SourceEncodingField;
      }
      set
      {
        this.SourceEncodingField = value;
      }
    }
    /// <summary>
    /// Gets or sets the array dimensions. Indicates whether the dataType is an array and how many dimensions the array has.
    /// It may have the following values:
    /// n > 1: the dataType is an array with the specified number of dimensions.
    /// OneDimension(1): The dataType is an array with one dimension.
    /// OneOrMoreDimensions (0): The dataType is an array with one or more dimensions.
    /// Scalar (−1): The dataType is not an array.
    /// Any (−2): The dataType can be a scalar or an array with any number of dimensions.
    /// ScalarOrOneDimension(−3): The dataType can be a scalar or a one dimensional array.
    /// </summary>
    /// <remarks>
    /// <note>
    /// NOTE All DataTypes are considered to be scalar, even if they have array-like semantics like ByteString and String.
    /// </note>
    /// </remarks>
    /// <value>The array dimensions.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
    public int ValueRank
    {
      get { return ValueRankField; }
      set { ValueRankField = value; }
    }
    /// <summary>
    /// Gets or sets the array dimensions - Specifies the length of each dimension for an array dataType. 
    /// It is intended to describe the capability of the dataType, not the current size.
    /// The number of elements shall be equal to the value of the valueRank.Shall be null if valueRank ≤ 0.
    /// A value of 0 for an individual dimension indicates that the dimension has a variable length.
    /// </summary>
    /// <value>The array dimensions.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 4)]
    public int[] ArrayDimensions
    {
      get { return ArrayDimensionsField; }
      set { ArrayDimensionsField = value; }
    }
    #endregion

  }
}
