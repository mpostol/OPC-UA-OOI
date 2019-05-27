//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class UANodeContextUnitTest
  {
    [TestMethod]
    public void ConstructorNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _addressSpaceMock.Object);
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.IsTrue(new QualifiedName() == _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.IsTrue(_toTest.NodeIdContext.ToString() == "ns=1;i=11");
      Assert.IsNull(_toTest.UANode);
      XML.UANode _node = UnitTest.Helpers.TestData.CreateUAObject();
      int _registerReferenceCounter = 0;
      _toTest.Update(_node, x => _registerReferenceCounter++);
      Assert.AreEqual<int>(2, _registerReferenceCounter);
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.AreEqual<QualifiedName>(new QualifiedName("NewUAObject", 1), _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>(_toTest.NodeIdContext.ToString(), "ns=1;i=11");
      Assert.IsNotNull(_toTest.UANode);
    }
    [TestMethod]
    public void UpdateDuplicatedNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      UANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object);
      Mock<IBuildErrorsHandling> _traceMock = new Mock<IBuildErrorsHandling>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      _traceMock.Setup(x => x.TraceEvent(It.IsAny<TraceMessage>())).Callback<TraceMessage>(x => _traceBuffer.Add(x));
      _newNode.Log = _traceMock.Object;
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
    public void UpdateNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _addressSpaceMock.Object);
      XML.UANode _node = new UAObject()
      {
        NodeId = "ns=1;i=1",
        BrowseName = "1:NewUAObject",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "New UA Object" } },
        References = new Reference[]
        {
          new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = ObjectTypeIds.BaseObjectType.ToString() },
          new Reference() { ReferenceType = ReferenceTypeIds.Organizes.ToString(), IsForward= false, Value = "i=85" }
        },
        // UAInstance
        ParentNodeId = string.Empty,
        // UAObject
        EventNotifier = 0x01,
      };
      int _registerReferenceCounter = 0;
      _toTest.Update(_node, x => _registerReferenceCounter++);
      Assert.AreEqual<int>(2, _registerReferenceCounter);
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.AreEqual<QualifiedName>(new QualifiedName("NewUAObject", 1), _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>(_toTest.NodeIdContext.ToString(), "ns=1;i=11");
      Assert.IsNotNull(_toTest.UANode);
    }
    [TestMethod]
    public void UpdateWithDifferentNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      QualifiedName qualifiedName = QualifiedName.Parse("EURange");
      IUANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object);
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
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CalculateNodeReferencesNullFactoryTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IValidator> _validatorMoc = new Mock<IValidator>();
      IUANodeBase _first = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object);
      _first.CalculateNodeReferences(null, _validatorMoc.Object);
    }
    [TestMethod]
    public void CalculateNodeReferencesNullUANodeTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      _asMock.Setup(x => x.GetMyReferences(It.IsAny<IUANodeBase>())).Returns(new List<UAReferenceContext>());
      Mock<INodeFactory> _mockNodeFactory = new Mock<INodeFactory>();
      Mock<IValidator> _validatorMoc = new Mock<IValidator>();
      _validatorMoc.Setup(x => x.ValidateExportNode(It.IsAny<IUANodeBase>(), It.IsAny<IUANodeBase>(), _mockNodeFactory.Object, It.IsAny<UAReferenceContext>()));
      IUANodeBase _node = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object);
      _node.CalculateNodeReferences(_mockNodeFactory.Object, _validatorMoc.Object);
      _asMock.Verify(x => x.GetMyReferences(It.IsAny<IUANodeBase>()), Times.Once);
      _validatorMoc.Verify(x => x.ValidateExportNode(It.IsAny<IUANodeBase>(), It.IsAny<IUANodeBase>(), _mockNodeFactory.Object, It.IsAny<UAReferenceContext>()), Times.Never);
    }
    [TestMethod]
    public void EqualsTest()
    {
      AddressSpaceBuildContext _as = new AddressSpaceBuildContext();
      UANodeContext _first = _as.InstanceToTest;
      UANodeContext _second = _as.InstanceToTest;
      Assert.IsTrue(_first.Equals(_second));
    }
    [TestMethod]
    public void GetDerivedInstances4ObjectTest()
    {
      AddressSpaceBuildContext _as = new AddressSpaceBuildContext();
      UANodeContext _testInstance = _as.InstanceToTest;
      Assert.IsTrue(_testInstance.UANode.GetType() == typeof(UAObject));
      Assert.AreEqual<string>("1:InstanceOfDerivedFromComplexObjectType", _testInstance.BrowseName.ToString());
      Dictionary<string, IUANodeBase> _result = _testInstance.GetDerivedInstances();
      Assert.IsNotNull(_result);
      Assert.AreEqual<int>(4, _result.Count);
    }
    [TestMethod]
    public void GetDerivedInstances4TypeDefinition()
    {
      AddressSpaceBuildContext _as = new AddressSpaceBuildContext();
      UANodeContext _testInstance = _as.TypeToTest;
      Assert.IsNotNull(_testInstance);
      Assert.IsTrue(_testInstance.UANode.GetType() == typeof(UAObjectType));
      Assert.AreEqual<string>("1:DerivedFromComplexObjectType", _testInstance.BrowseName.ToString());
      Dictionary<string, IUANodeBase> _result = _testInstance.GetDerivedInstances();
      Assert.IsNotNull(_result);
      Assert.AreEqual<int>(4, _result.Count);
    }
    [TestMethod]
    public void BuildSymbolicIdTest()
    {
      Mock<IBuildErrorsHandling> _traceMock = new Mock<IBuildErrorsHandling>();
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      _traceMock.Setup(x => x.TraceEvent(It.IsAny<TraceMessage>())).Callback<TraceMessage>(x => _traceBuffer.Add(x));
      NodeId _nodeId = NodeId.Parse("ns=1;i=11");
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object);
      _toTest.Log = _traceMock.Object;
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
      UANodeSet _tm = TestData.CreateNodeSetModel();
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      _asMock.Setup(x=> x.GetNamespace(0)).Returns<ushort>(x => "tempuri.org");
      UANode _nodeFactory = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      UANodeContext _node = new UANodeContext(NodeId.Parse("ns=1;i=47"), _asMock.Object);
      _node.Update(_nodeFactory, x => Assert.Fail());
      XmlQualifiedName _resolvedName =_node.ExportNodeBrowseName();
      _asMock.Verify(x => x.GetNamespace(0), Times.Once);
      Assert.IsNotNull(_resolvedName);
      Assert.AreEqual<string>("tempuri.org:EURange", _resolvedName.ToString());
    }

    #region instrumentation
    private class AddressSpaceBuildContext : IAddressSpaceBuildContext
    {

      #region constructor
      public AddressSpaceBuildContext()
      {
        CreateAddressSpace();
      }
      #endregion

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
      public void GetChildren(IUANodeContext rootNode, List<IUANodeBase> nodes)
      {
        Assert.IsNotNull(rootNode);
        IEnumerable<IUANodeContext> _children = m_References.Values.Where<UAReferenceContext>(x => x.SourceNode == rootNode).
                                                                    Where<UAReferenceContext>(x => (x.ReferenceKind == ReferenceKindEnum.HasProperty || x.ReferenceKind == ReferenceKindEnum.HasComponent)).
                                                                    Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
        nodes.AddRange(_children);
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
      public IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeContext node)
      {
        throw new NotImplementedException();
      }
      #endregion

      #region private instrumentation
      internal UANodeContext InstanceToTest { get; private set; }
      internal UANodeContext TypeToTest { get; private set; }
      private readonly Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
      private readonly Dictionary<string, IUANodeContext> m_NodesDictionary = new Dictionary<string, IUANodeContext>();
      private void Add2mNodesDictionary(UANodeContext node)
      {
        m_NodesDictionary.Add(node.NodeIdContext.ToString(), node);
      }
      private UANodeContext NewNode(UANode newNode)
      {
        UANodeContext _newNode = new UANodeContext(NodeId.Parse(newNode.NodeId), this);
        _newNode.Update(newNode, x => { m_References.Add(x.Key, x); });
        Add2mNodesDictionary(_newNode);
        return _newNode;
      }
      private void CreateAddressSpace()
      {
        NewNode(new UAReferenceType() { NodeId = ReferenceTypeIds.HasProperty.ToString(), BrowseName = "HasProperty" });
        NewNode(new UAReferenceType() { NodeId = ReferenceTypeIds.HasTypeDefinition.ToString(), BrowseName = "HasTypeDefinition" });
        NewNode(new UAVariable()
        {
          NodeId = "i=112",
          BrowseName = "NamingRule",
          ParentNodeId = "i=78",
          DataType = "i=120",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "NamingRule" } }
        });
        NewNode(new UAObjectType()
        {
          NodeId = "i=77",
          BrowseName = "ModellingRuleType",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ModellingRuleType" } }
        });
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
        });
        NewNode(new UAReferenceType()
        {
          NodeId = "i=37",
          BrowseName = "HasModellingRule",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "HasModellingRule" } }
        });
        NewNode(new UAMethod()
        {
          NodeId = "ns=1;i=25",
          BrowseName = "1:ChildMethod",
          ParentNodeId = "ns=1;i=16",
          MethodDeclarationId = "ns=1;i=10",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildMethodNewName" } },
          References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), IsForward = true, Value = "i=78" } }
        });
        NewNode(new UAReferenceType()
        {
          NodeId = "i=47",
          BrowseName = "HasComponent",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildVariable" } }
        });
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=47",
          BrowseName = "EURange",
          ParentNodeId = "ns=1;i=43",
          DataType = "i=884",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
        });
        NewNode(new UAVariable()
        {
          NodeId = "i=2369",
          BrowseName = "EURange",
          ParentNodeId = "i=2368",
          DataType = "i=884",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
        });
        NewNode(new UAVariableType()
        {
          NodeId = "i=2368",
          BrowseName = "AnalogItemType",
          DataType = "Number",
          ValueRank = -2,
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "AnalogItemType" } },
          References = new Reference[] { new Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value = "i=2369" } }
        });
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
        });
        NewNode(new UAObjectType()
        {
          NodeId = "i=58",
          BrowseName = "BaseObjectType",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "BaseObjectType" } }
        });
        NewNode(new UAVariableType()
        {
          NodeId = "i=68",
          BrowseName = "PropertyType",
          ValueRank = -2,
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "PropertyType" } }
        });
        NewNode(new UAVariable()
        {
          NodeId = "i=11511",
          BrowseName = "NamingRule",
          ParentNodeId = "i=11510",
          DataType = "i=120",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "NamingRule" } },
          References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "i=68", IsForward = true } }
        });
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
        });
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
        }
        );
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
        });
        NewNode(new UAMethod()
        {
          NodeId = "ns=1;i=10",
          BrowseName = "1:ChildMethod",
          ParentNodeId = "ns=1;i=1",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildMethod" } },
          References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), Value = "i=78", IsForward = true } }
        });
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
        });
        NewNode(new UAReferenceType()
        {
          NodeId = "i=45",
          BrowseName = "HasSubtype",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "HasSubtype" } },
        });
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
        });
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=32",
          BrowseName = "1:ChildProperty",
          SymbolicName = "BrowseName4node66",
          ParentNodeId = "ns=1;i=30",
          DataType = "LocalizedText",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildProperty" } }
        });
        NewNode(new UAVariable()
        {
          NodeId = "ns=1;i=59",
          BrowseName = "EURange",
          ParentNodeId = "ns=1;i=55",
          DataType = "i=884",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } },
        });
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
        });
        NewNode(new UAMethod()
        {
          NodeId = "ns=1;i=39",
          BrowseName = "1:ChildMethod",
          ParentNodeId = "ns=1;i=30",
          MethodDeclarationId = "ns=1;i=10",
          DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ChildMethodNewName" } }
        });
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
        }
        );
      }
      #endregion

    }
    #endregion

  }
}
