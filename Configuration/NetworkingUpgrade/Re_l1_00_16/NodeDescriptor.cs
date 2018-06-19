
using System;
using System.Runtime.Serialization;
using System.Xml;

namespace UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16
{

  /// <summary>
  /// Class NodeDescriptor.
  /// </summary>
  [DataContractAttribute(Name = "NodeDescriptor", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class NodeDescriptor
  {

    #region INodeDescriptor
    /// <summary>
    /// Gets or sets the binding description.
    /// </summary>
    /// <value>The binding description.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public string BindingDescription
    {
      get
      {
        return m_BindingDescriptionField;
      }
      set
      {
        m_BindingDescriptionField = value;
      }
    }
    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>The type of the data.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public XmlQualifiedName DataType
    {
      get { return m_DataTypeField; }
      set { m_DataTypeField = value; }
    }
    /// <summary>
    /// Gets or sets a value indicating whether [instance declaration].
    /// </summary>
    /// <value><c>true</c> if [instance declaration]; otherwise, <c>false</c>.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
    public bool InstanceDeclaration
    {
      get { return m_InstanceDeclarationField; }
      set { m_InstanceDeclarationField = value; }
    }
    /// <summary>
    /// Gets or sets the node class.
    /// </summary>
    /// <value>The node class.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 4)]
    public InstanceNodeClassesEnum NodeClass
    {
      get { return m_NodeClassField; }
      set { m_NodeClassField = value; }
    }
    /// <summary>
    /// Gets or sets the node identifier.
    /// </summary>
    /// <value>The node identifier.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 5)]
    public XmlQualifiedName NodeIdentifier
    {
      get { return m_NodeIdentifierField; }
      set { m_NodeIdentifierField = value; }
    }
    #endregion

    #region private
    private string m_BindingDescriptionField;
    private XmlQualifiedName m_DataTypeField;
    private bool m_InstanceDeclarationField;
    private InstanceNodeClassesEnum m_NodeClassField;
    private XmlQualifiedName m_NodeIdentifierField;
    #endregion

  }
}
