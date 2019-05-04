//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class UANodeContextUnitTest
  {
    [TestMethod]
    public void ConstructorNodeIdTest()
    {
      IUAModelContext _mc = new UAModelContext();
      UANodeContext _toTest = new UANodeContext(AddressSpaceBuildContext.NewAddressSpaceBuildContext, _mc, NodeId.Parse("ns=1;i=11"));
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.IsTrue(new QualifiedName() == _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.IsTrue(_toTest.NodeIdContext.ToString() == "ns=1;i=11");
      Assert.AreSame(_mc, _toTest.UAModelContext);
      Assert.IsNull(_toTest.UANode);
      XML.UANode _node = UnitTest.Helpers.TestData.CreateUAObject();
      _toTest.Update(_node, x => { }); //TODO add reference registration test.
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.AreEqual<string>(_node.BrowseName, _toTest.BrowseName.ToString());
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>(_toTest.NodeIdContext.ToString(), "ns=1;i=11");
      Assert.AreSame(_mc, _toTest.UAModelContext);
      Assert.IsNotNull(_toTest.UANode);
    }
    [TestMethod]
    public void EqualsTest()
    {
      IUAModelContext _mc = new UAModelContext();
      UANodeContext _first = new UANodeContext(AddressSpaceBuildContext.NewAddressSpaceBuildContext, _mc, NodeId.Parse("ns=1;i=11"));
      UANodeContext _second = new UANodeContext(AddressSpaceBuildContext.NewAddressSpaceBuildContext, _mc, NodeId.Parse("ns=1;i=11"));
      Assert.IsTrue(_first.Equals(_second));
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CalculateNodeReferencesNullFactoryTest()
    {
      IUAModelContext _mc = new UAModelContext();
      IUANodeBase _first = new UANodeContext(AddressSpaceBuildContext.NewAddressSpaceBuildContext, _mc, NodeId.Parse("ns=1;i=11"));
      _first.CalculateNodeReferences(null);
    }
    private class AddressSpaceBuildContext : IAddressSpaceBuildContext
    {
      internal static AddressSpaceBuildContext NewAddressSpaceBuildContext => _singleton.Value;
      private static readonly Lazy<AddressSpaceBuildContext> _singleton = new Lazy<AddressSpaceBuildContext>(() => new AddressSpaceBuildContext());
      private AddressSpaceBuildContext() { }
      public Parameter ExportArgument(Argument argument, XmlQualifiedName dataType)
      {
        throw new NotImplementedException();
      }
      public XmlQualifiedName ExportBrowseName(NodeId id)
      {
        throw new NotImplementedException();
      }
      public IUANodeBase GetBaseTypeNode(XML.NodeClassEnum nodeClass)
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
        List<UAReferenceContext> contexts = new List<UAReferenceContext>();
        return contexts;
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
      public IUANodeContext GetOrCreateNodeContext(string nodeId)
      {
        return new UANodeContext(AddressSpaceBuildContext.NewAddressSpaceBuildContext, this, NodeId.Parse(nodeId));
      }
      public NodeId ImportNodeId(string nodeId)
      {
        return NodeId.Parse(nodeId);
      }
      public QualifiedName ImportQualifiedName(QualifiedName broseName)
      {
        return broseName;
      }
    }
    private class NodeFactory : INodeFactory
    {
      public string BrowseName { set => throw new NotImplementedException(); }
      public XmlQualifiedName SymbolicName { set => throw new NotImplementedException(); }
      public uint WriteAccess { set => throw new NotImplementedException(); }
      public AccessRestrictions AccessRestrictions { set => throw new NotImplementedException(); }
      public XML.ReleaseStatus ReleaseStatus { set => throw new NotImplementedException(); }
      public XML.DataTypePurpose DataTypePurpose { set => throw new NotImplementedException(); }
      public string[] Category { set => throw new NotImplementedException(); }
      ReleaseStatus INodeFactory.ReleaseStatus { set => throw new NotImplementedException(); }
      DataTypePurpose INodeFactory.DataTypePurpose { set => throw new NotImplementedException(); }
      public void AddDescription(string localeField, string valueField)
      {
        throw new NotImplementedException();
      }
      public void AddDisplayName(string localeField, string valueField)
      {
        throw new NotImplementedException();
      }
      public NodeFactory1 AddNodeFactory<NodeFactory1>() where NodeFactory1 : INodeFactory
      {
        throw new NotImplementedException();
      }
      public IReferenceFactory NewReference()
      {
        throw new NotImplementedException();
      }
    }
  }
}
