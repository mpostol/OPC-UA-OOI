//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class UANodeContextUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      IUAModelContext _mc = new UAModelContext();
      UANodeContext _toTest = new UANodeContext(new AddressSpaceBuildContext(), _mc, NodeId.Parse("ns=1;i=11"));
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.IsTrue(new QualifiedName() == _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(_toTest.IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.IsTrue(_toTest.NodeIdContext.ToString() == "ns=1;i=11");
      Assert.AreSame(_mc, _toTest.UAModelContext);
      Assert.IsNull(_toTest.UANode);
    }
    [TestMethod]
    public void EqualsTest()
    {
      IUAModelContext _mc = new UAModelContext();
      UANodeContext _first = new UANodeContext(new AddressSpaceBuildContext(), _mc, NodeId.Parse("ns=1;i=11"));
      UANodeContext _second = new UANodeContext(new AddressSpaceBuildContext(), _mc, NodeId.Parse("ns=1;i=11"));
      Assert.IsTrue(_first.Equals(_second));

    }
    private class AddressSpaceBuildContext : IAddressSpaceBuildContext
    {
      public Parameter ExportArgument(Argument argument, XmlQualifiedName dataType)
      {
        throw new NotImplementedException();
      }

      public XmlQualifiedName ExportBrowseName(NodeId id)
      {
        throw new NotImplementedException();
      }

      public void GetDerivedInstances(IUANodeContext rootNode, List<IUANodeBase> list)
      {
        throw new NotImplementedException();
      }

      public ushort GetIndexOrAppend(string identifier)
      {
        throw new NotImplementedException();
      }

      public IEnumerable<UAReferenceContext> GetMyReferences(IUANodeContext index)
      {
        throw new NotImplementedException();
      }

      public string GetNamespace(ushort namespaceIndex)
      {
        throw new NotImplementedException();
      }

      public IUANodeContext GetOrCreateNodeContext(NodeId id, IUAModelContext uAModelContext)
      {
        throw new NotImplementedException();
      }

      public IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeContext node)
      {
        throw new NotImplementedException();
      }
    }
    private class UAModelContext : IUAModelContext
    {
      public Parameter ExportArgument(Argument item)
      {
        throw new NotImplementedException();
      }

      public XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue)
      {
        throw new NotImplementedException();
      }

      public XmlQualifiedName ExportQualifiedName(QualifiedName source)
      {
        throw new NotImplementedException();
      }

      public IUANodeContext GetOrCreateNodeContext(string nodeId, bool lookupAlias)
      {
        throw new NotImplementedException();
      }

      public NodeId ImportNodeId(string nodeId, bool lookupAlias)
      {
        throw new NotImplementedException();
      }

      public QualifiedName ImportQualifiedName(QualifiedName broseName)
      {
        throw new NotImplementedException();
      }
    }
  }
}
