
using CAS.UA.IServerConfiguration;
using System.Xml;

namespace UAOOI.DataBindings.UnitTest
{
  /// <summary>
  /// Class NodeDescriptor provides test implementation of the <see cref="INodeDescriptor"/>
  /// </summary>
  internal class NodeDescriptor : NodeDescriptorBase
  {

    /// <summary>
    /// Gets the test instance.
    /// </summary>
    /// <returns>NodeDescriptor.</returns>
    internal static NodeDescriptor GetTestInstance()
    {
      return new NodeDescriptor() { b_NodeIdentifier = GetDefaultNodeIdentifier() };
    }
    internal static XmlQualifiedName GetDefaultNodeIdentifier()
    {
      return new XmlQualifiedName("NodeIdentifier", "NodeIdentifierNS");
    }
    /// <summary>
    /// Gets the binding description that allows the editor to create automatically bindings.
    /// </summary>
    /// <value>The binding description.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public override string BindingDescription
    {
      get
      {
        return "BindingDescription";
      }
    }
    /// <summary>
    /// Gets the type of the node of of the Variable NodeClass
    /// </summary>
    /// <value>The type of the data.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public override XmlQualifiedName DataType
    {
      get
      {
        return new XmlQualifiedName("DataType", "NameSpace");
      }
    }
    /// <summary>
    /// Gets a value indicating whether it is instance declaration - may have many instances in the created address space.
    /// </summary>
    /// <value><c>true</c> if the node is instance declaration; otherwise, <c>false</c>.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public override bool InstanceDeclaration
    {
      get
      {
        return false;
      }
    }
    /// <summary>
    /// Gets the node class.
    /// </summary>
    /// <value>The node class.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public override InstanceNodeClassesEnum NodeClass
    {
      get
      {
        return InstanceNodeClassesEnum.Object;
      }
    }
    /// <summary>
    /// Gets the node unique identifier, i.e. the browse path.
    /// </summary>
    /// <value>The node identifier.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public override XmlQualifiedName NodeIdentifier
    {
      get { return b_NodeIdentifier; }
    }

    private NodeDescriptor() { }
    private XmlQualifiedName b_NodeIdentifier = null;

  }
}
