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
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class UANodeContextUnitTest
  {
    [TestMethod]
    public void ConstructorNodeIdTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUAModelContext> _modelMock = new Mock<IUAModelContext>();
      _modelMock.Setup<QualifiedName>(x => x.ImportQualifiedName(It.IsAny<QualifiedName>())).Returns(() => new QualifiedName("QualifiedName", 1));
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, _modelMock.Object);
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.IsTrue(new QualifiedName() == _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.IsTrue(_toTest.NodeIdContext.ToString() == "ns=1;i=11");
      Assert.AreSame(_modelMock.Object, _toTest.UAModelContext);
      Assert.IsNull(_toTest.UANode);
      XML.UANode _node = UnitTest.Helpers.TestData.CreateUAObject();
      _toTest.Update(_node, x => { }); //TODO add reference registration test.
      Assert.IsNotNull(_toTest.BrowseName);
      Assert.AreEqual<QualifiedName>(new QualifiedName("QualifiedName", 1), _toTest.BrowseName);
      Assert.IsFalse(_toTest.InRecursionChain);
      Assert.IsFalse(_toTest.IsProperty);
      Assert.IsFalse(((IUANodeBase)_toTest).IsPropertyVariableType);
      Assert.IsFalse(_toTest.ModelingRule.HasValue);
      Assert.IsNotNull(_toTest.NodeIdContext);
      Assert.AreEqual<string>(_toTest.NodeIdContext.ToString(), "ns=1;i=11");
      Assert.AreSame(_modelMock.Object, _toTest.UAModelContext);
      Assert.IsNotNull(_toTest.UANode);
    }
    [TestMethod]
    public void UpdateNullTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUAModelContext> _modelMock = new Mock<IUAModelContext>();
      IUANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, _modelMock.Object);
      Func<UANode> _nodeFactory = () => new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      List<TraceMessage> _trace = new List<TraceMessage>();
      BuildErrorsHandling.Log.TraceEventAction += x => _trace.Add(x);
      _newNode.Update(_nodeFactory(), x => Assert.Fail());
      _newNode.Update(_nodeFactory(), x => Assert.Fail());
      Assert.AreEqual<int>(1, _trace.Count);
      BuildErrorsHandling.Log.TraceEventAction -= x => _trace.Add(x);
    }
    [TestMethod]
    public void UpdateTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUAModelContext> _modelMock = new Mock<IUAModelContext>();
      QualifiedName qualifiedName = QualifiedName.Parse("EURange");
      _modelMock.Setup<QualifiedName>(x => x.ImportQualifiedName(qualifiedName)).Returns<QualifiedName>(x => x);
      IUANodeContext _newNode = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, _modelMock.Object);
      Func<UANode> _nodeFactory = () => new UAVariable()
          {
            NodeId = "ns=1;i=47",
            BrowseName = "EURange",
            ParentNodeId = "ns=1;i=43",
            DataType = "i=884",
            DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
          };
      _newNode.Update(_nodeFactory(), x => Assert.Fail());
      _modelMock.Verify(x => x.ImportQualifiedName(qualifiedName), Times.Once);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CalculateNodeReferencesNullFactoryTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUAModelContext> _modelMock = new Mock<IUAModelContext>();
      IUANodeBase _first = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, _modelMock.Object);
      _first.CalculateNodeReferences(null);
    }
    [TestMethod]
    //[ExpectedException(typeof(ArgumentNullException))]
    public void CalculateNodeReferencesNullUANodeTest()
    {
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUAModelContext> _modelMock = new Mock<IUAModelContext>();
      Mock<INodeFactory> _mockNodeFactory = new Mock<INodeFactory>();
      IUANodeBase _first = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, _modelMock.Object);
      _first.CalculateNodeReferences(_mockNodeFactory.Object);
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
      Assert.IsNotNull(_testInstance);
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
      BuildErrorsHandling _logger = BuildErrorsHandling.Log;
      List<TraceMessage> _messages = new List<TraceMessage>();
      _logger.TraceEventAction += x => _messages.Add(x);
      NodeId _nodeId = NodeId.Parse("ns=1;i=11");
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUAModelContext> _modelMock = new Mock<IUAModelContext>();
      UANodeContext _toTest = new UANodeContext(NodeId.Parse("ns=1;i=11"), _asMock.Object, _modelMock.Object);
      List<string> path = new List<string>();
      _toTest.BuildSymbolicId(path);
      Assert.AreEqual<int>(1, _messages.Count);
      Assert.AreEqual<string>("P3-0403040000", _messages[0].BuildError.Identifier);
      Assert.AreEqual<string>("The target node NodeId=ns=1;i=11, current path ", _messages[0].Message);
      Assert.AreEqual<int>(0, path.Count);
      _logger.TraceEventAction -= x => _messages.Add(x);
    }

    #region instrumentation
    private class AddressSpaceBuildContext : IAddressSpaceBuildContext
    {

      #region constructor
      public AddressSpaceBuildContext()
      {
        m_IUAModelContextMock = new Mock<IUAModelContext>();
        Model = m_IUAModelContextMock.Object;
        m_IUAModelContextMock.Setup<IUANodeContext>(x => x.GetOrCreateNodeContext(It.IsAny<string>())).Returns<string>(x => m_NodesDictionary[x]);
        m_IUAModelContextMock.Setup<QualifiedName>(x => x.ImportQualifiedName(It.IsAny<QualifiedName>())).Returns<QualifiedName>(x => x);
        CreateAddressSpace();
      }
      #endregion

      #region IAddressSpaceBuildContext
      public Parameter ExportArgument(Argument argument, XmlQualifiedName dataType)
      {
        throw new NotImplementedException();
      }
      public XmlQualifiedName ExportBrowseName(NodeId id)
      {
        throw new NotImplementedException();
      }
      public IUANodeBase GetBaseTypeNode(NodeClassEnum nodeClass)
      {
        return null;
      }
      public void GetDerivedInstances(IUANodeContext rootNode, List<IUANodeBase> list)
      {
        //IUANodeContext _derived = m_References.Values.Where<UAReferenceContext>(x => (x.TypeNode.NodeIdContext == ReferenceTypeIds.HasSubtype) && (x.TargetNode == rootNode)).
        //                                              Select<UAReferenceContext, IUANodeContext>(x => x.SourceNode).
        //                                              FirstOrDefault<IUANodeContext>();
        Assert.IsNotNull(rootNode);
        IEnumerable<IUANodeContext> _children = m_References.Values.Where<UAReferenceContext>(x => Object.ReferenceEquals(x.SourceNode, rootNode)).
                                                                    Where<UAReferenceContext>(x => (x.ReferenceKind == ReferenceKindEnum.HasProperty || x.ReferenceKind == ReferenceKindEnum.HasComponent)).
                                                                    Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
        list.AddRange(_children);
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
      public IUANodeContext GetOrCreateNodeContext(NodeId id, IUAModelContext uAModelContext)
      {
        throw new NotImplementedException();
      }
      public IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeContext node)
      {
        throw new NotImplementedException();
      }
      #endregion

      #region private instrumentation
      internal IUAModelContext Model { get; private set; }
      internal UANodeContext InstanceToTest { get; private set; }
      internal UANodeContext TypeToTest { get; private set; }
      private Mock<IUAModelContext> m_IUAModelContextMock;
      private readonly Dictionary<string, UAReferenceContext> m_References = new Dictionary<string, UAReferenceContext>();
      private readonly Dictionary<string, IUANodeContext> m_NodesDictionary = new Dictionary<string, IUANodeContext>();
      private void Add2mNodesDictionary(UANodeContext node)
      {
        m_NodesDictionary.Add(node.NodeIdContext.ToString(), node);
      }
      private UANodeContext NewNode(UANode newNode)
      {
        UANodeContext _newNode = new UANodeContext(NodeId.Parse(newNode.NodeId), this, Model);
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
          References = new Reference[]{new Reference(){ ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(), Value="i=78",  IsForward=true }}
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
