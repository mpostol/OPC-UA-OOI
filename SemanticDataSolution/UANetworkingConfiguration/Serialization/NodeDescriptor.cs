
using CAS.UA.IServerConfiguration;
using System;
using System.Runtime.Serialization;
using System.Xml;
using UAOOI.DataBindings;
using System.ComponentModel;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  [DataContractAttribute(Name = "NodeDescriptor", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class NodeDescriptor
  {

    /// <summary>
    /// Creates the wrapper of this instance.
    /// </summary>
    /// <returns>An instance of <see cref="NodeDescriptorBase"/>.</returns>
    internal NodeDescriptorBase CreateWrapper()
    {
      return new NodeDescriptorWrapper(this);
    }

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
    public XmlQualifiedName DataType
    {
      get { return DataTypeField; }
      set { DataTypeField = value; }
    }
    /// <summary>
    /// Gets or sets a value indicating whether [instance declaration].
    /// </summary>
    /// <value><c>true</c> if [instance declaration]; otherwise, <c>false</c>.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
    public bool InstanceDeclaration
    {
      get { return InstanceDeclarationField; }
      set { InstanceDeclarationField = value; }
    }
    /// <summary>
    /// Gets or sets the node class.
    /// </summary>
    /// <value>The node class.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 4)]
    public InstanceNodeClassesEnum NodeClass
    {
      get { return NodeClassField; }
      set { NodeClassField = value; }
    }
    /// <summary>
    /// Gets or sets the node identifier.
    /// </summary>
    /// <value>The node identifier.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 5)]
    public XmlQualifiedName NodeIdentifier
    {
      get { return NodeIdentifierField; }
      set { NodeIdentifierField = value; }
    }
    #endregion

    #region private
    /// <summary>
    /// Class NodeDescriptorWrapper - read only wrapper of the node descriptor
    /// </summary>
    private class NodeDescriptorWrapper : NodeDescriptorBase
    {
      public NodeDescriptorWrapper(NodeDescriptor repository)
      {
        m_repository = repository;
      }
      public override string BindingDescription
      {
        get
        {
          return m_repository.BindingDescription;
        }
      }
      public override XmlQualifiedName DataType
      {
        get
        {
          return m_repository.DataType;
        }
      }
      public override bool InstanceDeclaration
      {
        get
        {
          return m_repository.InstanceDeclaration;
        }
      }
      public override InstanceNodeClassesEnum NodeClass
      {
        get
        {
          return m_repository.NodeClass;
        }
      }
      [DisplayName("Name")]
      [Description("The node unique identifier.")]
      [Category("Node")]
      [ReadOnly(true)]
      public override XmlQualifiedName NodeIdentifier
      {
        get
        {
          return m_repository.NodeIdentifier;
        }
      }
      public override string ToString()
      {
        return $"{NodeClass}:{NodeIdentifier}";
      }
      private NodeDescriptor m_repository;
    }
    private string BindingDescriptionField;
    private XmlQualifiedName DataTypeField;
    private bool InstanceDeclarationField;
    private InstanceNodeClassesEnum NodeClassField;
    private XmlQualifiedName NodeIdentifierField;
    #endregion

  }
}
