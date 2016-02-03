
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace UAOOI.Configuration.Networking.Serialization
{

  ///// <remarks/>
  //[SerializableAttribute()]
  //[XmlTypeAttribute(AnonymousType = true, Namespace = CommonDefinitions.Namespace)]
  //[DataContractAttribute(Name = "Documentation", Namespace = CommonDefinitions.Namespace)]
  //public partial class Documentation
  //{

  //  private XmlElement[] itemsField;

  //  private string[] textField;

  //  //private XmlAttribute[] anyAttrField;

  //  /// <remarks/>
  //  [XmlAnyElementAttribute()]
  //  [DataMember]
  //  public XmlElement[] Items
  //  {
  //    get
  //    {
  //      return this.itemsField;
  //    }
  //    set
  //    {
  //      this.itemsField = value;
  //    }
  //  }

  //  /// <remarks/>
  //  [XmlTextAttribute()]
  //  [DataMember]
  //  public string[] Text
  //  {
  //    get
  //    {
  //      return this.textField;
  //    }
  //    set
  //    {
  //      this.textField = value;
  //    }
  //  }

    ///// <remarks/>
    //[XmlAnyAttributeAttribute()]
    //[DataMember]
    //public XmlAttribute[] AnyAttr
    //{
    //  get
    //  {
    //    return this.anyAttrField;
    //  }
    //  set
    //  {
    //    this.anyAttrField = value;
    //  }
    //}

  //}

  /// <remarks/>
  [SerializableAttribute()]
  [XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  [DataContractAttribute(Name = "FieldType", Namespace = CommonDefinitions.Namespace)]
  public partial class FieldType
  {

    //private Documentation documentationField;

    private string nameField;

    private XmlQualifiedName typeNameField;

    private uint lengthField;

    private bool lengthFieldSpecified;

    private string lengthFieldField;

    private bool isLengthInBytesField;

    private string switchFieldField;

    private uint switchValueField;

    private bool switchValueFieldSpecified;

    private SwitchOperand switchOperandField;

    private bool switchOperandFieldSpecified;

    private byte[] terminatorField;

    //private XmlAttribute[] anyAttrField;

    public FieldType()
    {
      this.isLengthInBytesField = false;
    }

    ///// <remarks/>
    //[DataMember]
    //public Documentation Documentation
    //{
    //  get
    //  {
    //    return this.documentationField;
    //  }
    //  set
    //  {
    //    this.documentationField = value;
    //  }
    //}

    /// <remarks/>
    [XmlAttributeAttribute()]
    [DataMember]
    public string Name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }

    /// <remarks/>
    [XmlAttributeAttribute()]
    [DataMember]
    public XmlQualifiedName TypeName
    {
      get
      {
        return this.typeNameField;
      }
      set
      {
        this.typeNameField = value;
      }
    }

    ///// <remarks/>
    //[XmlAttributeAttribute()]
    //[DataMember]
    //public uint Length
    //{
    //  get
    //  {
    //    return this.lengthField;
    //  }
    //  set
    //  {
    //    this.lengthField = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlIgnoreAttribute()]
    //[DataMember]
    //public bool LengthSpecified
    //{
    //  get
    //  {
    //    return this.lengthFieldSpecified;
    //  }
    //  set
    //  {
    //    this.lengthFieldSpecified = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlAttributeAttribute()]
    //[DataMember]
    //public string LengthField
    //{
    //  get
    //  {
    //    return this.lengthFieldField;
    //  }
    //  set
    //  {
    //    this.lengthFieldField = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlAttributeAttribute()]
    //[DefaultValueAttribute(false)]
    //[DataMember]
    //public bool IsLengthInBytes
    //{
    //  get
    //  {
    //    return this.isLengthInBytesField;
    //  }
    //  set
    //  {
    //    this.isLengthInBytesField = value;
    //  }
    //}

    /// <remarks/>
    [XmlAttributeAttribute()]
    [DataMember]
    public string SwitchField
    {
      get
      {
        return this.switchFieldField;
      }
      set
      {
        this.switchFieldField = value;
      }
    }

    /// <remarks/>
    [XmlAttributeAttribute()]
    [DataMember]
    public uint SwitchValue
    {
      get
      {
        return this.switchValueField;
      }
      set
      {
        this.switchValueField = value;
      }
    }

    /// <remarks/>
    [XmlIgnoreAttribute()]
    [DataMember]
    public bool SwitchValueSpecified
    {
      get
      {
        return this.switchValueFieldSpecified;
      }
      set
      {
        this.switchValueFieldSpecified = value;
      }
    }

    /// <remarks/>
    [XmlAttributeAttribute()]
    [DataMember]
    public SwitchOperand SwitchOperand
    {
      get
      {
        return this.switchOperandField;
      }
      set
      {
        this.switchOperandField = value;
      }
    }

    /// <remarks/>
    [XmlIgnoreAttribute()]
    [DataMember]
    public bool SwitchOperandSpecified
    {
      get
      {
        return this.switchOperandFieldSpecified;
      }
      set
      {
        this.switchOperandFieldSpecified = value;
      }
    }

    ///// <remarks/>
    //[XmlAttributeAttribute(DataType = "hexBinary")]
    //[DataMember]
    //public byte[] Terminator
    //{
    //  get
    //  {
    //    return this.terminatorField;
    //  }
    //  set
    //  {
    //    this.terminatorField = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlAnyAttributeAttribute()]
    //[DataMember]
    //public XmlAttribute[] AnyAttr
    //{
    //  get
    //  {
    //    return this.anyAttrField;
    //  }
    //  set
    //  {
    //    this.anyAttrField = value;
    //  }
    //}
  }

  /// <remarks/>
  [SerializableAttribute()]
  [XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  [DataContractAttribute(Name = "SwitchOperand", Namespace = CommonDefinitions.Namespace)]
  public enum SwitchOperand
  {

    /// <remarks/>
    [EnumMemberAttribute()]
    Equals,

    /// <remarks/>
    [EnumMemberAttribute()]
    GreaterThan,

    /// <remarks/>
    [EnumMemberAttribute()]
    LessThan,

    /// <remarks/>
    [EnumMemberAttribute()]
    GreaterThanOrEqual,

    /// <remarks/>
    [EnumMemberAttribute()]
    LessThanOrEqual,

    /// <remarks/>
    [EnumMemberAttribute()]
    NotEqual,

  }

  ///// <remarks/>
  //[SerializableAttribute()]
  //[XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  //[DataContractAttribute(Name = "EnumeratedValue", Namespace = CommonDefinitions.Namespace)]
  //public partial class EnumeratedValue
  //{

  //  //private Documentation documentationField;

  //  private string nameField;

  //  private int valueField;

  //  private bool valueFieldSpecified;

    ///// <remarks/>
    //public Documentation Documentation
    //{
    //  get
    //  {
    //    return this.documentationField;
    //  }
    //  set
    //  {
    //    this.documentationField = value;
    //  }
    //}

  //  /// <remarks/>
  //  [XmlAttributeAttribute()]
  //  public string Name
  //  {
  //    get
  //    {
  //      return this.nameField;
  //    }
  //    set
  //    {
  //      this.nameField = value;
  //    }
  //  }

  //  /// <remarks/>
  //  [XmlAttributeAttribute()]
  //  public int Value
  //  {
  //    get
  //    {
  //      return this.valueField;
  //    }
  //    set
  //    {
  //      this.valueField = value;
  //    }
  //  }

  //  /// <remarks/>
  //  [XmlIgnoreAttribute()]
  //  public bool ValueSpecified
  //  {
  //    get
  //    {
  //      return this.valueFieldSpecified;
  //    }
  //    set
  //    {
  //      this.valueFieldSpecified = value;
  //    }
  //  }
  //}

  /// <remarks/>
  [XmlIncludeAttribute(typeof(StructuredType))]
  //[XmlIncludeAttribute(typeof(OpaqueType))]
  //[XmlIncludeAttribute(typeof(EnumeratedType))]
  [KnownType(typeof(StructuredType))]
  //[KnownType(typeof(OpaqueType))]
  //[KnownType(typeof(EnumeratedType))]
  [SerializableAttribute()]
  [XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  [DataContractAttribute(Name = "TypeDescription", Namespace = CommonDefinitions.Namespace)]
  public partial class TypeDescription
  {

    //private Documentation documentationField;

    private string nameField;

    //private ByteOrder defaultByteOrderField;

    //private bool defaultByteOrderFieldSpecified;

    //private XmlAttribute[] anyAttrField;

    ///// <remarks/>
    //[DataMember]
    //public Documentation Documentation
    //{
    //  get
    //  {
    //    return this.documentationField;
    //  }
    //  set
    //  {
    //    this.documentationField = value;
    //  }
    //}

    /// <remarks/>
    [XmlAttributeAttribute(DataType = "NCName")]
    [DataMember]
    public string Name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }

    ///// <remarks/>
    //[XmlAttributeAttribute()]
    //[DataMember]
    //public ByteOrder DefaultByteOrder
    //{
    //  get
    //  {
    //    return this.defaultByteOrderField;
    //  }
    //  set
    //  {
    //    this.defaultByteOrderField = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlIgnoreAttribute()]
    //[DataMember]
    //public bool DefaultByteOrderSpecified
    //{
    //  get
    //  {
    //    return this.defaultByteOrderFieldSpecified;
    //  }
    //  set
    //  {
    //    this.defaultByteOrderFieldSpecified = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlAnyAttributeAttribute()]
    //[DataMember]
    //public XmlAttribute[] AnyAttr
    //{
    //  get
    //  {
    //    return this.anyAttrField;
    //  }
    //  set
    //  {
    //    this.anyAttrField = value;
    //  }
    //}
  }

  ///// <remarks/>
  //[SerializableAttribute()]
  //[XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  //[DataContractAttribute(Name = "ByteOrder", Namespace = CommonDefinitions.Namespace)]
  //public enum ByteOrder
  //{

  //  /// <remarks/>
  //  [EnumMemberAttribute()]
  //  BigEndian,

  //  /// <remarks/>
  //  [EnumMemberAttribute()]
  //  LittleEndian,
  //}

  /// <remarks/>
  [SerializableAttribute()]
  [XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  [DataContractAttribute(Name = "StructuredType", Namespace = CommonDefinitions.Namespace)]
  public partial class StructuredType : TypeDescription
  {

    private FieldType[] fieldField;

    /// <remarks/>
    [XmlElementAttribute("Field")]
    [DataMember]
    public FieldType[] Field
    {
      get
      {
        return this.fieldField;
      }
      set
      {
        this.fieldField = value;
      }
    }
  }

  /// <remarks/>
  //[XmlIncludeAttribute(typeof(EnumeratedType))]
  //[SerializableAttribute()]
  //[XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  //[DataContractAttribute(Name = "OpaqueType", Namespace = CommonDefinitions.Namespace)]
  //public partial class OpaqueType : TypeDescription
  //{

  //  private int lengthInBitsField;

  //  private bool lengthInBitsFieldSpecified;

  //  private bool byteOrderSignificantField;

  //  public OpaqueType()
  //  {
  //    this.byteOrderSignificantField = false;
  //  }

  //  /// <remarks/>
  //  [XmlAttributeAttribute()]
  //  [DataMember]
  //  public int LengthInBits
  //  {
  //    get
  //    {
  //      return this.lengthInBitsField;
  //    }
  //    set
  //    {
  //      this.lengthInBitsField = value;
  //    }
  //  }

  //  /// <remarks/>
  //  [XmlIgnoreAttribute()]
  //  [DataMember]
  //  public bool LengthInBitsSpecified
  //  {
  //    get
  //    {
  //      return this.lengthInBitsFieldSpecified;
  //    }
  //    set
  //    {
  //      this.lengthInBitsFieldSpecified = value;
  //    }
  //  }

  //  /// <remarks/>
  //  [XmlAttributeAttribute()]
  //  [DefaultValueAttribute(false)]
  //  [DataMember]
  //  public bool ByteOrderSignificant
  //  {
  //    get
  //    {
  //      return this.byteOrderSignificantField;
  //    }
  //    set
  //    {
  //      this.byteOrderSignificantField = value;
  //    }
  //  }
  //}

  ///// <remarks/>
  //[SerializableAttribute()]
  //[XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  //[DataContractAttribute(Name = "EnumeratedType", Namespace = CommonDefinitions.Namespace)]
  //public partial class EnumeratedType : OpaqueType
  //{

  //  private EnumeratedValue[] enumeratedValueField;

  //  /// <remarks/>
  //  [XmlElementAttribute("EnumeratedValue")]
  //  [DataMember]
  //  public EnumeratedValue[] EnumeratedValue
  //  {
  //    get
  //    {
  //      return this.enumeratedValueField;
  //    }
  //    set
  //    {
  //      this.enumeratedValueField = value;
  //    }
  //  }

  //}

  ///// <remarks/>
  //[SerializableAttribute()]
  //[XmlTypeAttribute(Namespace = CommonDefinitions.Namespace)]
  //[DataContractAttribute(Name = "ImportDirective", Namespace = CommonDefinitions.Namespace)]
  //public partial class ImportDirective
  //{

  //  private string namespaceField;

  //  private string locationField;

  //  /// <remarks/>
  //  [XmlAttributeAttribute()]
  //  public string Namespace
  //  {
  //    get
  //    {
  //      return this.namespaceField;
  //    }
  //    set
  //    {
  //      this.namespaceField = value;
  //    }
  //  }

  //  /// <remarks/>
  //  [XmlAttributeAttribute()]
  //  public string Location
  //  {
  //    get
  //    {
  //      return this.locationField;
  //    }
  //    set
  //    {
  //      this.locationField = value;
  //    }
  //  }
  //}

  /// <remarks/>
  [SerializableAttribute()]
  [XmlTypeAttribute(AnonymousType = true, Namespace = CommonDefinitions.Namespace)]
  [XmlRootAttribute(Namespace = CommonDefinitions.Namespace, IsNullable = false)]
  [DataContractAttribute(Name = "TypeDictionary", Namespace = CommonDefinitions.Namespace)]
  public partial class TypeDictionary
  {

    //private Documentation documentationField;

    //private ImportDirective[] importField;

    private TypeDescription[] itemsField;

    private string targetNamespaceField;

    //private ByteOrder defaultByteOrderField;

    //private bool defaultByteOrderFieldSpecified;

    //private XmlAttribute[] anyAttrField;

    ///// <remarks/>
    //public Documentation Documentation
    //{
    //  get
    //  {
    //    return this.documentationField;
    //  }
    //  set
    //  {
    //    this.documentationField = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlElementAttribute("Import")]
    //[DataMember]
    //public ImportDirective[] Import
    //{
    //  get
    //  {
    //    return this.importField;
    //  }
    //  set
    //  {
    //    this.importField = value;
    //  }
    //}

    /// <remarks/>
    //[XmlElementAttribute("EnumeratedType", typeof(EnumeratedType))]
    //[XmlElementAttribute("OpaqueType", typeof(OpaqueType))]
    [XmlElementAttribute("StructuredType", typeof(StructuredType))]
    [DataMember]
    public TypeDescription[] Items
    {
      get
      {
        return this.itemsField;
      }
      set
      {
        this.itemsField = value;
      }
    }

    /// <remarks/>
    [XmlAttributeAttribute()]
    [DataMember]
    public string TargetNamespace
    {
      get
      {
        return this.targetNamespaceField;
      }
      set
      {
        this.targetNamespaceField = value;
      }
    }

    ///// <remarks/>
    //[XmlAttributeAttribute()]
    //[DataMember (EmitDefaultValue = false, IsRequired = false )]
    //public ByteOrder DefaultByteOrder
    //{
    //  get
    //  {
    //    return this.defaultByteOrderField;
    //  }
    //  set
    //  {
    //    this.defaultByteOrderField = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlIgnoreAttribute()]
    //public bool DefaultByteOrderSpecified
    //{
    //  get
    //  {
    //    return this.defaultByteOrderFieldSpecified;
    //  }
    //  set
    //  {
    //    this.defaultByteOrderFieldSpecified = value;
    //  }
    //}

    ///// <remarks/>
    //[XmlAnyAttributeAttribute()]
    //[DataMember]
    //public XmlAttribute[] AnyAttr
    //{
    //  get
    //  {
    //    return this.anyAttrField;
    //  }
    //  set
    //  {
    //    this.anyAttrField = value;
    //  }
    //}

  }
}
