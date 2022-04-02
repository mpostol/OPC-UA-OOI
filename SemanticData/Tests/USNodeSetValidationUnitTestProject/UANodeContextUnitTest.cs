//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class UANodeContextUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      Mock<IAddressSpaceBuildContext> _addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _addressSpaceMock.Object, x => { });
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>("ns=1;i=11", _toTest.NodeIdContext.ToString());
      Assert.IsNull(_toTest.UANode);
      Assert.AreEqual<string>("NodeId=\"ns=1;i=11\", BrowseName=\" ???? \", ModellingRule=\"\"", _toTest.ToString());
    }

    [TestMethod]
    public void UpdateTest()
    {
      Mock<IAddressSpaceBuildContext> _addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _addressSpaceMock.Object, x => { });
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>("ns=1;i=11", _toTest.NodeIdContext.ToString());
      Assert.IsNull(_toTest.UANode);
      XML.UANode _node = UnitTest.Helpers.TestData.CreateUAObject();
      string browseName = _node.BrowseName;
      string nodeId = _node.NodeId;
      int _registerReferenceCounter = 0;
      _toTest.Update(_node, x => _registerReferenceCounter++);
      Assert.AreEqual<int>(2, _registerReferenceCounter);
      Assert.IsNotNull(_toTest.UANode);
      Assert.AreEqual<QualifiedName>(browseName, _toTest.UANode.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>(_toTest.NodeIdContext.ToString(), "ns=1;i=11");
    }

    [TestMethod]
    public void UpdateDuplicatedNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      UANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, x => _traceBuffer.Add(x));
      UAVariable _nodeFactory = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      _newNode.Update(_nodeFactory, x => Assert.Fail());
      _newNode.Update(_nodeFactory, x => Assert.Fail());
      Assert.AreEqual<int>(1, _traceBuffer.Count);
      Assert.AreEqual<string>(_traceBuffer[0].BuildError.Identifier, BuildError.NodeIdDuplicated.Identifier);
    }

    [TestMethod]
    public void UpdateBrowseNameIndex0Test()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      UANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, x => _traceBuffer.Add(x));
      UAVariable _nodeFactory = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "0:BrowseName",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } },
      };
      _newNode.Update(_nodeFactory, x => Assert.Fail());
      Assert.AreEqual<int>(0, _traceBuffer.Count);
      Assert.AreEqual<string>("0:BrowseName", _newNode.UANode.BrowseName.ToString());
    }

    [TestMethod]
    public void UpdateBrowseNameIndex1Test()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      UANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, x => _traceBuffer.Add(x));
      UAVariable _nodeFactory = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "1:BrowseName",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      _newNode.Update(_nodeFactory, x => Assert.Fail());
      Assert.AreEqual<int>(0, _traceBuffer.Count);
      Assert.AreEqual<string>("1:BrowseName", _newNode.UANode.BrowseName.ToString());
    }

    [TestMethod]
    public void UpdateReferencesTest()
    {
      Mock<IAddressSpaceBuildContext> addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      addressSpaceMock.Setup(x => x.GetOrCreateNodeContext(It.IsAny<NodeId>(), It.IsAny<Func<NodeId, UANodeContext>>()));
      List<TraceMessage> traceBuffer = new List<TraceMessage>();
      UANodeContext toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), addressSpaceMock.Object, x => traceBuffer.Add(x));
      XML.UANode _node = new UAObject()
      {
        NodeId = "ns=1;i=1",
        BrowseName = "1:NewUAObject",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "New UA Object" } },
        References = new Reference[]
        {
          new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), IsForward= true, Value = ObjectTypeIds.BaseObjectType.ToString() },
          new Reference() { ReferenceType = ReferenceTypeIds.Organizes.ToString(), IsForward= false, Value = "i=85" }
        },
        // UAInstance
        ParentNodeId = string.Empty,
        // UAObject
        EventNotifier = 0x01,
      };
      _node.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      List<UAReferenceContext> _registerReference = new List<UAReferenceContext>();
      toTest.Update(_node, x => _registerReference.Add(x));
      addressSpaceMock.Verify(x => x.GetOrCreateNodeContext(It.IsAny<NodeId>(), It.IsAny<Func<NodeId, UANodeContext>>()), Times.Never);
      Assert.AreEqual<int>(2, _registerReference.Count);
      Assert.AreSame(toTest, _registerReference[0].ParentNode);
      Assert.AreSame(toTest, _registerReference[0].SourceNode);
      Assert.IsNull(_registerReference[0].TargetNode);
      Assert.IsNull(_registerReference[1].SourceNode);
      Assert.AreSame(toTest, _registerReference[1].ParentNode);
      Assert.AreSame(toTest, _registerReference[1].TargetNode);

      Assert.IsFalse(toTest.InRecursionChain);
      Assert.IsFalse(toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)toTest).IsPropertyVariableType);
      Assert.IsFalse(toTest.ModelingRule.HasValue);
      Assert.IsNotNull(toTest.NodeIdContext);
      Assert.AreEqual<string>(toTest.NodeIdContext.ToString(), "ns=1;i=11");
      Assert.IsNotNull(toTest.UANode);
    }

    [TestMethod]
    public void UpdateWithDifferentNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      IUANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, x => _traceBuffer.Add(x));
      Assert.AreEqual<string>("ns=1;i=11", _newNode.NodeIdContext.ToString());
      UANode _nodeFactory = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      _newNode.Update(_nodeFactory, x => Assert.Fail()); // Update has different NodeId - no change is expected.
      Assert.AreEqual<string>("ns=1;i=11", _newNode.NodeIdContext.ToString());
      Assert.AreEqual<string>("ns=1;i=47", _newNode.UANode.NodeId);
      Assert.AreEqual<int>(0, _traceBuffer.Count);
    }

    [TestMethod]
    public void CalculateNodeReferencesNullArguments()
    {
      Mock<IAddressSpaceBuildContext> _addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      Mock<INodeFactory> _mockNodeFactory = new Mock<INodeFactory>();
      Mock<IValidator> _validatorMoc = new Mock<IValidator>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      IUANodeBase _first = new UANodeContext(NodeId.Parse("ns=1;i=11"), _addressSpaceMock.Object, x => _traceBuffer.Add(x));
      Assert.ThrowsException<ArgumentNullException>(() => _first.CalculateNodeReferences(null, null, _validatorMoc.Object, y => { }));
      Assert.ThrowsException<ArgumentNullException>(() => _first.CalculateNodeReferences(_mockNodeFactory.Object, null, null, y => { }));
      Assert.ThrowsException<ArgumentNullException>(() => _first.CalculateNodeReferences(_mockNodeFactory.Object, null, _validatorMoc.Object, null));
    }

    [TestMethod]
    public void CalculateNodeReferencesNullUANodeTest()
    {
      //TODO The exported model doesn't contain all nodes #653
      //Assert.Inconclusive("The exported model doesn't contain all nodes #653");
      Reference reference = new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.Organizes.ToString(), Value = ObjectTypeIds.BaseObjectType.ToString() };
      reference.RecalculateNodeIds(x => NodeId.Parse(x));

      Mock<IUANodeContext> typeMock = new Mock<IUANodeContext>();
      typeMock.Setup(x => x.NodeIdContext).Returns(ReferenceTypeIds.Organizes);
      Mock<IUANodeContext> targetMock = new Mock<IUANodeContext>();
      targetMock.Setup(x => x.NodeIdContext).Returns(new NodeId("ns=1;i=12"));
      targetMock.Setup(x => x.UANode).Returns(
        new UAObject()
        {
          NodeId = "ns=1;i=6599",
          BrowseName = "1:<NetworkIdentifier>",
          SymbolicName = "NetworkIdentifier",
          ParentNodeId = "ns=1;i=6308",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "<NetworkIdentifier>" } }
        });

      Mock<IAddressSpaceBuildContext> addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      addressSpaceMock.Setup(x => x.GetOrCreateNodeContext(It.IsAny<NodeId>(), It.IsAny<Func<NodeId, IUANodeContext>>())).Returns(typeMock.Object);
      addressSpaceMock.Setup(x => x.GetOrCreateNodeContext(It.Is<NodeId>(z => z == reference.ValueNodeId), It.IsAny<Func<NodeId, IUANodeContext>>())).Returns(targetMock.Object);

      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      UANodeContext node2Test = new UANodeContext(NodeId.Parse("ns=1;i=11"), addressSpaceMock.Object, x => _traceBuffer.Add(x));
      List<UAReferenceContext> listOfReferences = new List<UAReferenceContext>() { new UAReferenceContext(reference, addressSpaceMock.Object, node2Test) };
      addressSpaceMock.Setup(x => x.GetMyReferences(It.IsAny<IUANodeBase>())).Returns(listOfReferences);
      addressSpaceMock.Setup(x => x.ExportBrowseName(It.IsAny<NodeId>(), It.IsAny<NodeId>())).Returns(new XmlQualifiedName("name", "ns"));

      Mock<IReferenceFactory> referenceFactoryMock = new Mock<IReferenceFactory>();
      Mock<INodeFactory> _mockNodeFactory = new Mock<INodeFactory>();
      _mockNodeFactory.Setup(x => x.NewReference()).Returns(referenceFactoryMock.Object);
      Mock<IValidator> _validatorMoc = new Mock<IValidator>();
      _validatorMoc.Setup(x => x.ValidateExportNode(It.IsAny<IUANodeBase>(), null, _mockNodeFactory.Object, It.IsAny<Action<IUANodeContext>>(), It.IsAny<UAReferenceContext>()));

      //testing
      int counter = 0;
      ((IUANodeBase)node2Test).CalculateNodeReferences(_mockNodeFactory.Object, null, _validatorMoc.Object, y => counter++);

      //validation
      Assert.AreEqual<int>(1, counter);
      addressSpaceMock.Verify(x => x.GetMyReferences(It.IsAny<IUANodeBase>()), Times.Once);
      addressSpaceMock.Verify(x => x.ExportBrowseName(It.IsAny<NodeId>(), It.IsAny<NodeId>()), Times.Once);
      _validatorMoc.Verify(x => x.ValidateExportNode(It.Is<IUANodeBase>(z => z == targetMock.Object), null, _mockNodeFactory.Object, It.IsAny<Action<IUANodeContext>>(), It.Is<UAReferenceContext>(y => y == listOfReferences[0])), Times.Never);
      Assert.AreEqual<int>(0, _traceBuffer.Count, _traceBuffer.Count == 0 ? "" : _traceBuffer[0].Message);
    }

    [TestMethod]
    public void EqualsTest()
    {
      AddressSpaceBuildContext _as = new AddressSpaceBuildContext(x => { });
      UANodeContext _first = _as.InstanceToTest;
      UANodeContext _second = _as.InstanceToTest;
      Assert.IsTrue(_first.Equals(_second));
    }

    [TestMethod]
    public void GetDerivedInstances4ObjectTest()
    {
      AddressSpaceBuildContext _as = new AddressSpaceBuildContext(x => { });
      UANodeContext _testInstance = _as.InstanceToTest;
      Assert.IsTrue(_testInstance.UANode.GetType() == typeof(UAObject));
      Assert.AreEqual<string>("1:InstanceOfDerivedFromComplexObjectType", _testInstance.UANode.BrowseNameQualifiedName.ToString());
      Dictionary<string, IUANodeBase> _result = _testInstance.GetDerivedInstances();
      Assert.IsNotNull(_result);
      Assert.AreEqual<int>(4, _result.Count);
    }

    [TestMethod]
    public void GetDerivedInstances4TypeDefinition()
    {
      AddressSpaceBuildContext _as = new AddressSpaceBuildContext(x => { });
      UANodeContext _testType = _as.TypeToTest;
      Assert.IsNotNull(_testType);
      Assert.IsInstanceOfType(_testType.UANode, typeof(UAObjectType));
      Assert.AreEqual<string>("1:DerivedFromComplexObjectType", _testType.UANode.BrowseNameQualifiedName.ToString());
      Dictionary<string, IUANodeBase> _result = _testType.GetDerivedInstances();
      Assert.IsNotNull(_result);
      Assert.AreEqual<int>(4, _result.Count);
    }

    [TestMethod]
    public void BuildSymbolicIdTest()
    {
      NodeId _nodeId = NodeId.Parse("ns=1;i=11");
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, x => _traceBuffer.Add(x));
      List<string> path = new List<string>();
      _toTest.BuildSymbolicId(path);
      Assert.AreEqual<int>(1, _traceBuffer.Count);
      Assert.AreEqual<string>("P3-0403040000", _traceBuffer[0].BuildError.Identifier);
      Assert.AreEqual<string>("The target node NodeId=ns=1;i=11, current path ", _traceBuffer[0].Message);
      Assert.AreEqual<int>(0, path.Count);
    }

    [TestMethod]
    public void ExportBrowseNameTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      _asMock.Setup(x => x.GetNamespace(0)).Returns<ushort>(x => Namespaces.OpcUa);
      _asMock.Setup(x => x.GetNamespace(1)).Returns<ushort>(x => "tempuri.org");
      UANode _nodeFactory = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      _nodeFactory.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      UANodeContext _node = new UANodeContext(NodeId.Parse("ns=1;i=47"), _asMock.Object, x => _traceBuffer.Add(x));
      _node.Update(_nodeFactory, x => Assert.Fail());
      XmlQualifiedName _resolvedName = _node.ExportNodeBrowseName();
      _asMock.Verify(x => x.GetNamespace(0), Times.Once);
      _asMock.Verify(x => x.GetNamespace(1), Times.Never);
      Assert.IsNotNull(_resolvedName);
      Assert.AreEqual<string>("http://opcfoundation.org/UA/:EURange", _resolvedName.ToString());
      Assert.AreEqual<int>(0, _traceBuffer.Count);
    }

    [TestMethod]
    public void EqualsUAVariableTestMethod()
    {
      IUAModelContext modelMock = new ModelContextMock();
      UAVariable _derivedNode = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "0:BrowseName",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      _derivedNode.RecalculateNodeIds(modelMock, x => Assert.Fail());
      Assert.IsNotNull(_derivedNode.BrowseNameQualifiedName);
      UANode _baseNode = new UAVariable()
      {
        NodeId = "i=17568",
        BrowseName = "BrowseName",
        ParentNodeId = "i=15318",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      _baseNode.RecalculateNodeIds(modelMock, x => Assert.Fail());
      Assert.IsNotNull(_baseNode.BrowseNameQualifiedName);
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      IUANodeContext _derivedNodeContext = new UANodeContext(NodeId.Parse("ns=1;i=47"), _asMock.Object, x => { });
      _derivedNodeContext.Update(_derivedNode, x => Assert.Fail());
      UANodeContext _baseNodeContext = new UANodeContext(NodeId.Parse("i=17568"), _asMock.Object, x => { });
      _baseNodeContext.Update(_baseNode, x => Assert.Fail());

      Assert.IsTrue(_derivedNode.Equals(_baseNode));
      Assert.IsTrue(_derivedNodeContext.Equals(_baseNodeContext));
    }

    [TestMethod]
    public void RemoveInheritedValuesTest()
    {
      UAVariable _derivedNode = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",  //Range
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      UANode _baseNode = new UAVariable()
      {
        NodeId = "i=17568",
        BrowseName = "EURange",
        ParentNodeId = "i=15318",
        DataType = "i=884", //Range
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      IUANodeContext _derivedNodeContext = new UANodeContext(NodeId.Parse("ns=1;i=47"), _asMock.Object, x => { });
      _derivedNodeContext.Update(_derivedNode, x => Assert.Fail());
      UANodeContext _baseNodeContext = new UANodeContext(NodeId.Parse("i=17568"), _asMock.Object, x => { });
      _baseNodeContext.Update(_baseNode, x => Assert.Fail());
      _derivedNodeContext.RemoveInheritedValues(_baseNodeContext);
      Assert.AreEqual<string>("EURange", _derivedNode.BrowseName);
      Assert.IsNull(_derivedNode.DataType);
      Assert.IsNull(_derivedNode.Description);
    }

    #region instrumentation

    private class ModelContextMock : IUAModelContext
    {
      public Uri ModelUri => throw new NotImplementedException();

      public (QualifiedName browseName, NodeId nodeId) ImportBrowseName(string browseNameText, string nodeIdText, Action<TraceMessage> trace)
      {
        return (QualifiedName.Parse(browseNameText), NodeId.Parse(nodeIdText));
      }

      public NodeId ImportNodeId(string nodeId, Action<TraceMessage> trace)
      {
        return NodeId.Parse(nodeId);
      }

      public void RegisterUAReferenceType(QualifiedName browseName)
      {
      }
    }

    private class AddressSpaceBuildContext : IAddressSpaceBuildContext
    {
      #region constructor

      public AddressSpaceBuildContext(Action<TraceMessage> traceMessageCallback)
      {
        CreateAddressSpace(traceMessageCallback);
      }

      #endregion constructor

      #region IAddressSpaceBuildContext

      public Parameter ExportArgument(Argument argument, XmlQualifiedName dataType)
      {
        throw new NotImplementedException();
      }

      public XmlQualifiedName ExportBrowseName(NodeId id, NodeId defaultValue)
      {
        throw new NotImplementedException();
      }

      public IUANodeBase GetBaseTypeNode(NodeClassEnum nodeClass)
      {
        return null;
      }

      public IEnumerable<IUANodeBase> GetChildren(IUANodeBase node)
      {
        return m_References.Values.Where<UAReferenceContext>(x => Object.ReferenceEquals(x.SourceNode, node)).
                                                             Where<UAReferenceContext>(x => x.ChildConnector).
                                                             Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
      }

      public ushort GetIndexOrAppend(string identifier)
      {
        throw new NotImplementedException();
      }

      public IEnumerable<UAReferenceContext> GetMyReferences(IUANodeBase index)
      {
        List<UAReferenceContext> contexts = new List<UAReferenceContext>();
        return contexts;
      }

      public string GetNamespace(ushort namespaceIndex)
      {
        throw new NotImplementedException();
      }

      public IUANodeContext GetOrCreateNodeContext(NodeId nodeId, Func<NodeId, IUANodeContext> createUAModelContext)
      {
        return m_NodesDictionary[nodeId.ToString()];
      }

      public IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeBase node)
      {
        throw new NotImplementedException();
      }

      public Parameter ExportArgument(Argument argument)
      {
        throw new NotImplementedException();
      }

      public void GetBaseTypes(IUANodeContext rootNode, List<IUANodeContext> inheritanceChain)
      {
        if (rootNode == null)
          throw new ArgumentNullException("rootNode");
        inheritanceChain.Add(rootNode);
        if (rootNode.InRecursionChain)
          throw new ArgumentOutOfRangeException("Circular reference");
        rootNode.InRecursionChain = true;
        IEnumerable<IUANodeContext> _derived = m_References.Values.Where<UAReferenceContext>(x => (x.TypeNode.NodeIdContext == ReferenceTypeIds.HasSubtype) && (x.TargetNode == rootNode)).
                                                                   Select<UAReferenceContext, IUANodeContext>(x => x.SourceNode);
        if (_derived.Count<IUANodeContext>() > 1)
          throw new ArgumentOutOfRangeException("To many subtypes");
        else if (_derived.Count<IUANodeContext>() == 1)
          GetBaseTypes(_derived.First<IUANodeContext>(), inheritanceChain);
        rootNode.InRecursionChain = false;
      }

      #endregion IAddressSpaceBuildContext

      #region private instrumentation

      internal UANodeContext InstanceToTest { get; private set; }
      internal UANodeContext TypeToTest { get; private set; }
      private readonly Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
      private readonly Dictionary<string, IUANodeContext> m_NodesDictionary = new Dictionary<string, IUANodeContext>();

      private void Add2mNodesDictionary(UANodeContext node)
      {
        m_NodesDictionary.Add(node.NodeIdContext.ToString(), node);
      }

      private UANodeContext NewNode(UANode newNode, Action<TraceMessage> traceMessageCallback)
      {
        newNode.RecalculateNodeIds(new ModelContextMock(), traceMessageCallback);
        UANodeContext _newNode = new UANodeContext(NodeId.Parse(newNode.NodeId), this, traceMessageCallback);
        _newNode.Update(newNode, x => { m_References.Add(x.Key, x); });
        Add2mNodesDictionary(_newNode);
        return _newNode;
      }

      private void CreateAddressSpace(Action<TraceMessage> traceMessageCallback)
      {
        NewNode(new UAReferenceType() { NodeId = ReferenceTypeIds.HasProperty.ToString(), BrowseName = "HasProperty" }, traceMessageCallback);
        NewNode(new UAReferenceType() { NodeId = ReferenceTypeIds.HasTypeDefinition.ToString(), BrowseName = "HasTypeDefinition" }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "i=112",
          BrowseName = "NamingRule",
          ParentNodeId = "i=78",
          DataType = "i=120",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "NamingRule" } }
        }, traceMessageCallback);
        NewNode(new UAObjectType()
        {
          NodeId = "i=77",
          BrowseName = "ModellingRuleType",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ModellingRuleType" } }
        }, traceMessageCallback);
        NewNode(new UAObject()
        {
          NodeId = "i=78",
          BrowseName = "Mandatory",
          SymbolicName = "ModellingRule_Mandatory",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "Mandatory" } },
          References = new Reference[]
          {
            new Reference(){ IsForward = true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value="i=112" },
            new Reference(){ IsForward = true, ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value="i=77" }
          }
        }, traceMessageCallback);
        NewNode(new UAReferenceType()
        {
          NodeId = "i=37",
          BrowseName = "HasModellingRule",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "HasModellingRule" } }
        }, traceMessageCallback);
        NewNode(new UAMethod()
        {
          NodeId = "ns=1;i=25",
          BrowseName = "1:ChildMethod",
          ParentNodeId = "ns=1;i=16",
          MethodDeclarationId = "ns=1;i=10",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildMethodNewName" } },
          References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), IsForward = true, Value = "i=78" } }
        }, traceMessageCallback);
        NewNode(new UAReferenceType()
        {
          NodeId = "i=47",
          BrowseName = "HasComponent",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildVariable" } }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=47",
          BrowseName = "EURange",
          ParentNodeId = "ns=1;i=43",
          DataType = "i=884",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "i=2369",
          BrowseName = "EURange",
          ParentNodeId = "i=2368",
          DataType = "i=884",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
        }, traceMessageCallback);
        NewNode(new UAVariableType()
        {
          NodeId = "i=2368",
          BrowseName = "AnalogItemType",
          DataType = "Number",
          ValueRank = -2,
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "AnalogItemType" } },
          References = new Reference[] { new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value = "i=2369" } }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=43",
          BrowseName = "1:ChildVariable",
          ParentNodeId = "ns=1;i=1",
          DataType = "Number",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildVariable" } },
          References = new Reference[]
            {
                new Reference() { IsForward=true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value="ns=1;i=47"},
                new Reference(){ IsForward=true, ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value="i=2368"},
                new Reference() {IsForward=true, ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), Value="i=78" }
            }
        }, traceMessageCallback);
        NewNode(new UAObjectType()
        {
          NodeId = "i=58",
          BrowseName = "BaseObjectType",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "BaseObjectType" } }
        }, traceMessageCallback);
        NewNode(new UAVariableType()
        {
          NodeId = "i=68",
          BrowseName = "PropertyType",
          ValueRank = -2,
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "PropertyType" } }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "i=11511",
          BrowseName = "NamingRule",
          ParentNodeId = "i=11510",
          DataType = "i=120",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "NamingRule" } },
          References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "i=68", IsForward = true } }
        }, traceMessageCallback);
        NewNode(new UAObject()
        {
          NodeId = "i=11510",
          BrowseName = "MandatoryPlaceholder",
          SymbolicName = "ModellingRule_MandatoryPlaceholder",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "MandatoryPlaceholder" } },
          References = new Reference[]
            {
             new Reference(){ ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value="i=11511", IsForward= true },
             new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "i=77", IsForward= true }
            }
        }, traceMessageCallback);
        NewNode(new UAObject()
        {
          NodeId = "ns=1;i=2",
          BrowseName = "1:ChildObject",
          ParentNodeId = "ns=1;i=1",
          EventNotifier = 1,
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildObject" } },
          References = new Reference[]
            {
              new  Reference() { IsForward= true, ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value="i=58" },
              new Reference() { IsForward= true, ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), Value="i=11510" }
            }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=3",
          BrowseName = "1:ChildProperty",
          SymbolicName = "BrowseName4node66",
          ParentNodeId = "ns=1;i=1",
          DataType = "LocalizedText",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildProperty" } },
          References = new Reference[]
            {
              new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value="i=68", IsForward=true },
              new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), Value="i=78", IsForward=true }
            }
        }, traceMessageCallback);
        NewNode(new UAMethod()
        {
          NodeId = "ns=1;i=10",
          BrowseName = "1:ChildMethod",
          ParentNodeId = "ns=1;i=1",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildMethod" } },
          References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), Value = "i=78", IsForward = true } }
        }, traceMessageCallback);
        NewNode(new UAObjectType()
        {
          NodeId = "ns=1;i=1",
          BrowseName = "1:ComplexObjectType",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ComplexObjectType" } },
          References = new Reference[]
            {
              new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasComponent.ToString(), Value="ns=1;i=2" },
              new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(),  Value="ns=1;i=3" },
              new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasComponent.ToString(), Value="ns=1;i=43" },
              new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasComponent.ToString(), Value="ns=1;i=10" }
            }
        }, traceMessageCallback);
        NewNode(new UAReferenceType()
        {
          NodeId = "i=45",
          BrowseName = "HasSubtype",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "HasSubtype" } },
        }, traceMessageCallback);
        TypeToTest = NewNode(new UAObjectType()
        {
          NodeId = "ns=1;i=16",
          BrowseName = "1:DerivedFromComplexObjectType",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "DerivedFromComplexObjectType" } },
          References = new Reference[]
            {
              new Reference(){  ReferenceType = ReferenceTypeIds.HasComponent.ToString(), IsForward=true, Value= "ns=1;i=25" },
              new Reference() { ReferenceType = ReferenceTypeIds.HasSubtype.ToString(), IsForward = false, Value= "ns=1;i=1" }
            }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=32",
          BrowseName = "1:ChildProperty",
          SymbolicName = "BrowseName4node66",
          ParentNodeId = "ns=1;i=30",
          DataType = "LocalizedText",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildProperty" } }
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=59",
          BrowseName = "EURange",
          ParentNodeId = "ns=1;i=55",
          DataType = "i=884",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } },
        }, traceMessageCallback);
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=55",
          BrowseName = "1:ChildVariable",
          ParentNodeId = "ns=1;i=30",
          DataType = "Number",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildVariable" } },
          References = new Reference[]
            {
              new Reference(){ IsForward=true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value="ns=1;i=59"},
              new Reference(){ IsForward=true, ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "i=2368" },
            }
        }, traceMessageCallback);
        NewNode(new UAMethod()
        {
          NodeId = "ns=1;i=39",
          BrowseName = "1:ChildMethod",
          ParentNodeId = "ns=1;i=30",
          MethodDeclarationId = "ns=1;i=10",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildMethodNewName" } }
        }, traceMessageCallback);
        InstanceToTest = NewNode(new UAObject()
        {
          BrowseName = "1:InstanceOfDerivedFromComplexObjectType",
          NodeId = "ns=1;i=30",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "InstanceOfDerivedFromComplexObjectType" } },
          References = new Reference[]
          {
            new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value = "ns=1;i=32" },
            new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasComponent.ToString(), Value = "ns=1;i=55" },
            new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasComponent.ToString(), Value = "ns=1;i=39" },
            new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "ns=1;i=16" }
          }
        }, traceMessageCallback);
      }

      #endregion private instrumentation
    }

    #endregion instrumentation
  }
}