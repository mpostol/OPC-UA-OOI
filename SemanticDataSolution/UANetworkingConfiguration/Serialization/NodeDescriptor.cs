
using CAS.UA.IServerConfiguration;
using System;
using System.Runtime.Serialization;
using System.Xml;
using UAOOI.DataBindings;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  [DataContractAttribute(Name = "DataSetConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class NodeDescriptor : NodeDescriptorBase
  {

    #region INodeDescriptor
    /// <summary>
    /// Gets or sets the binding description.
    /// </summary>
    /// <value>The binding description.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public override string BindingDescription
    {
      get
      {
        return BindingDescriptionField;
      }
      set
      {
        BindingDescriptionField = value;
      }
    }
    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>The type of the data.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public override XmlQualifiedName DataType
    {
      get { return DataTypeField; }
      set { DataTypeField = value; }
    }
    /// <summary>
    /// Gets or sets a value indicating whether [instance declaration].
    /// </summary>
    /// <value><c>true</c> if [instance declaration]; otherwise, <c>false</c>.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
    public override bool InstanceDeclaration
    {
      get { return InstanceDeclarationField; }
      set { InstanceDeclarationField = value; }
    }
    /// <summary>
    /// Gets or sets the node class.
    /// </summary>
    /// <value>The node class.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 4)]
    public override InstanceNodeClassesEnum NodeClass
    {
      get { return NodeClassField; }
      set { NodeClassField = value; }
    }
    /// <summary>
    /// Gets or sets the node identifier.
    /// </summary>
    /// <value>The node identifier.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 5)]
    public override XmlQualifiedName NodeIdentifier
    {
      get { return NodeIdentifierField; }
      set { NodeIdentifierField = value; }
    }
    #endregion

    #region private
    private string BindingDescriptionField;
    private XmlQualifiedName DataTypeField;
    private bool InstanceDeclarationField;
    private InstanceNodeClassesEnum NodeClassField;
    private XmlQualifiedName NodeIdentifierField;
    #endregion

  }
}
