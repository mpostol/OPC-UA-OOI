
using CAS.UA.IServerConfiguration;
using System;
using System.Runtime.Serialization;
using System.Xml;
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
    internal IComparable CreateWrapper()
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
    /// <summary>
    /// Class NodeDescriptorWrapper - read only wrapper of the node descriptor
    /// </summary>
    private class NodeDescriptorWrapper : INodeDescriptor, IComparable
    {
      public NodeDescriptorWrapper(NodeDescriptor repository)
      {
        m_repository = repository;
      }
      public string BindingDescription
      {
        get
        {
          return m_repository.BindingDescription;
        }
      }
      public XmlQualifiedName DataType
      {
        get
        {
          return m_repository.DataType;
        }
      }
      public bool InstanceDeclaration
      {
        get
        {
          return m_repository.InstanceDeclaration;
        }
      }
      public InstanceNodeClassesEnum NodeClass
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
      public XmlQualifiedName NodeIdentifier
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

      #region IComparable
      public int CompareTo(object obj)
      {
        throw new NotImplementedException();
      }
      #endregion

      private NodeDescriptor m_repository;

    }
    private string m_BindingDescriptionField;
    private XmlQualifiedName m_DataTypeField;
    private bool m_InstanceDeclarationField;
    private InstanceNodeClassesEnum m_NodeClassField;
    private XmlQualifiedName m_NodeIdentifierField;
    #endregion

  }
}
