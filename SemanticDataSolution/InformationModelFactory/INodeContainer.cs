using System;
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface INodeContainer
  {
    NodeFactory NewExportNodeFFactory<NodeFactory>()
      where NodeFactory : INodeFactory;
  }
}
