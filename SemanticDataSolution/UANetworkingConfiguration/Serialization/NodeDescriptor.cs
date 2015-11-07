
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
    private class NodeDescriptorWrapper : NodeDescriptorBase, System.ComponentModel.INotifyPropertyChanged
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
        set
        {
          PropertyChanged.RaiseHandler<string>(value, m_repository.BindingDescription, x => m_repository.BindingDescription = x, "BindingDescription", this);
        }
      }
      public override XmlQualifiedName DataType
      {
        get
        {
          return m_repository.DataType;
        }
        set
        {
          PropertyChanged.RaiseHandler<XmlQualifiedName>(value, m_repository.DataType, x => m_repository.DataType = x, "DataType", this);
        }
      }
      public override bool InstanceDeclaration
      {
        get
        {
          return m_repository.InstanceDeclaration;
        }
        set
        {
          PropertyChanged.RaiseHandler<bool>(value, m_repository.InstanceDeclaration, x => m_repository.InstanceDeclaration = x, "InstanceDeclaration", this);
        }
      }
      public override InstanceNodeClassesEnum NodeClass
      {
        get
        {
          return m_repository.NodeClass;
        }
        set
        {
          PropertyChanged.RaiseHandler<InstanceNodeClassesEnum>(value, m_repository.NodeClass, x => m_repository.NodeClass = x, "NodeClass", this);
        }
      }
      public override XmlQualifiedName NodeIdentifier
      {
        get
        {
          return m_repository.NodeIdentifier;
        }
        set
        {
          PropertyChanged.RaiseHandler<XmlQualifiedName>(value, m_repository.NodeIdentifier, x => m_repository.NodeIdentifier = x, "NodeIdentifier", this);
        }
      }
      public event PropertyChangedEventHandler PropertyChanged;
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
