
using System.Xml;
using UAOOI.Configuration.Core;

namespace UAOOI.Configuration.DataBindings.UnitTest
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
      return new NodeDescriptor()
      {
        NodeIdentifier = GetDefaultNodeIdentifier(),
        BindingDescription = "BindingDescription",
        DataType = new XmlQualifiedName("DataType", "NameSpace"),
        InstanceDeclaration = false,
        NodeClass = InstanceNodeClassesEnum.Object,

      };
    }
    internal static XmlQualifiedName GetDefaultNodeIdentifier()
    {
      return new XmlQualifiedName("NodeIdentifier", "NodeIdentifierNS");
    }

    private NodeDescriptor() { }

  }
}
