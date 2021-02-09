//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  [TestClass]
  public class UANodeSetUnitTest
  {
    #region tests

    [TestMethod]
    public void NodeClassEnumTest()
    {
      UANode _toTest = new UADataType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UADataType, _toTest.NodeClassEnum);
      _toTest = new UAObject();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAObject, _toTest.NodeClassEnum);
      _toTest = new UAObjectType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAObjectType, _toTest.NodeClassEnum);
      _toTest = new UAReferenceType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAReferenceType, _toTest.NodeClassEnum);
      _toTest = new UAVariable();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAVariable, _toTest.NodeClassEnum);
      _toTest = new UAVariableType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAVariableType, _toTest.NodeClassEnum);
      _toTest = new UAView();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAView, _toTest.NodeClassEnum);
      _toTest = new UAMethod();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAMethod, _toTest.NodeClassEnum);
    }

    [TestMethod]
    public void RemoveInheritedKeepDifferentValuesTest()
    {
      UAObjectType _derived = GetDerivedFromComplexObjectType();
      UAObjectType _base = GetComplexObjectType();
      _derived.RemoveInheritedValues(_base);
      Assert.AreEqual<int>(1, _derived.DisplayName.Length);
      Assert.AreEqual<string>("DerivedFromComplexObjectType", _derived.DisplayName[0].Value);
    }

    [TestMethod]
    public void RemoveInheritedRemoveSameValuesTest()
    {
      UAObjectType _derived = GetDerivedFromComplexObjectType();
      UAObjectType _base = GetDerivedFromComplexObjectType();
      _derived.RemoveInheritedValues(_base);
      Assert.IsNull(_derived.DisplayName);
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void EqualsTypesTest()
    {
      UANode _derived = GetDerivedFromComplexObjectType();
      UANode _base = GetDerivedFromComplexObjectType();
      Assert.IsTrue(_derived.Equals(_base));
    }

    [TestMethod]
    public void EqualsInstancesTest()
    {
      UAObject _derived = GetInstanceOfDerivedFromComplexObjectType();
      UAObject _base = GetInstanceOfDerivedFromComplexObjectType();
      Assert.IsTrue(_derived.Equals(_base));
    }

    [TestMethod]
    public void EqualsUAVariableTest()
    {
      UAVariable _derivedNode = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      UANode _baseNode = new UAVariable()
      {
        NodeId = "i=17568",
        BrowseName = "EURange",
        ParentNodeId = "i=15318",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      Assert.IsTrue(_derivedNode.Equals(_baseNode));
    }

    [TestMethod]
    public void NotEqualsInstancesTest()
    {
      UAObject _derived = GetInstanceOfDerivedFromComplexObjectType();
      UAObject _base = GetInstanceOfDerivedFromComplexObjectType2();
      Assert.IsFalse(_derived.Equals(_base));
    }

    [TestMethod]
    public void RecalculateNodeIdsUADataTypeTest()
    {
      UADataType _enumeration = new UADataType()
      {
        NodeId = "ns=1;i=11",
        BrowseName = "1:EnumerationDataType",
        DisplayName = new LocalizedText[] { new LocalizedText() { Value = "EnumerationDataType" } },
        References = new Reference[]
          {
            new Reference() {ReferenceType = ReferenceTypeIds.HasProperty.ToString(), Value="ns=1;i=12", IsForward = true },
            new Reference() {ReferenceType = ReferenceTypeIds.HasSubtype.ToString(), Value="ns=1;i=9", IsForward = false }
          },
        Definition = new DataTypeDefinition()
        {
          Name = "EnumerationDataType",
          Field = new DataTypeField[]
          {
            new DataTypeField() { Name = "Field3", Value = 1 } ,
            new DataTypeField() { Name = "Field4", DataType = "ns=1;i=24" }
          }
        }
      };
      Mock<IUAModelContext> _uAModelContext = new Mock<IUAModelContext>();
      _uAModelContext.Setup<string>(x => x.ImportNodeId(It.IsAny<string>())).Returns<string>
        (x =>
        {
          NodeId nodeId = NodeId.Parse(x);
          if (nodeId.NamespaceIndex == 1)
            nodeId.SetNamespaceIndex(10);
          return nodeId.ToString();
        });
      _uAModelContext.Setup<string>(x => x.ImportQualifiedName(It.IsAny<string>())).Returns<string>
        (x =>
        {
          QualifiedName nodeId = QualifiedName.Parse(x);
          if (nodeId.NamespaceIndex == 1)
            nodeId.NamespaceIndex = 10;
          return nodeId.ToString();
        });
      _enumeration.RecalculateNodeIds(_uAModelContext.Object);
      Assert.AreEqual<string>("10:EnumerationDataType", _enumeration.BrowseName);
      Assert.AreEqual<int>(10, NodeId.Parse(_enumeration.NodeId).NamespaceIndex);
      Assert.AreEqual<int>(10, NodeId.Parse(_enumeration.References[0].Value).NamespaceIndex);
      Assert.AreEqual<int>(0, NodeId.Parse(_enumeration.References[0].ReferenceType).NamespaceIndex);
      Assert.AreEqual<int>(10, NodeId.Parse(_enumeration.References[1].Value).NamespaceIndex);
      Assert.AreEqual<int>(0, NodeId.Parse(_enumeration.References[1].ReferenceType).NamespaceIndex);
      Assert.AreEqual<string>("i=24", _enumeration.Definition.Field[0].DataType);
      Assert.AreEqual<string>("ns=10;i=24", _enumeration.Definition.Field[1].DataType);
    }

    [TestMethod]
    public void RecalculateNodeIdsUANodeSetTest()
    {
      UANodeSet _toTest = new UANodeSet()
      {
        NamespaceUris = new string[] { @"http://cas.eu/UA/Demo/" },
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "Alias name", Value = "ns=1;i=24" } },
        Items = new UANode[] { new UAObject()
              {
                NodeId = "Alias name",
                BrowseName = "1:NewUAObject",
                DisplayName = new LocalizedText[] { new LocalizedText() { Value = "New UA Object" } },
                References = new Reference[]
                {
                  new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = ObjectTypeIds.BaseObjectType.ToString() },
                  new Reference() { ReferenceType = ReferenceTypeIds.Organizes.ToString(), IsForward= false, Value = "i=85" }
                },
                // UAInstance
                ParentNodeId = string.Empty,
                // UAObject
                EventNotifier = 0x01,
              },
              new UAVariableType()
              {
                NodeId = "ns=1;i=1",
                BrowseName = "1:NewUAObject",
                DisplayName = new LocalizedText[] { new LocalizedText() { Value = "New UA Object" } },
                References = new Reference[]{},
                // UAObject
                DataType = "ns=1;i=2",
              }
        }
      };
      //Mock<IUAModelContext> _uAModelContext = new Mock<IUAModelContext>();
      //_uAModelContext.Setup<string>(x => x.ImportNodeId(It.IsAny<string>())).Returns<string>
      //  (x =>
      //  {
      //    NodeId nodeId = NodeId.Parse(x);
      //    if (nodeId.NamespaceIndex == 1)
      //      nodeId.SetNamespaceIndex(10);
      //    return nodeId.ToString();
      //  });
      //_uAModelContext.Setup<string>(x => x.ImportQualifiedName(It.IsAny<string>())).Returns<string>
      //  (x =>
      //  {
      //    QualifiedName nodeId = QualifiedName.Parse(x);
      //    if (nodeId.NamespaceIndex == 1)
      //      nodeId.NamespaceIndex = 10;
      //    return nodeId.ToString();
      //  });
      Mock<IAddressSpaceBuildContext> addressSpaceMock = new Mock<IAddressSpaceBuildContext>();
      addressSpaceMock.Setup(x => x.GetIndexOrAppend(@"http://cas.eu/UA/Demo/")).Returns<string>(x => 2);
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      IUAModelContext model = _toTest.UAModelContext(addressSpaceMock.Object, _logMock);
      Assert.IsNotNull(model);
      addressSpaceMock.Verify(x => x.GetIndexOrAppend(@"http://cas.eu/UA/Demo/"), Times.AtLeastOnce());
      Assert.AreEqual<string>("ns=2;i=24", _toTest.Aliases[0].Value);
      Assert.AreEqual<string>("Alias name", _toTest.Aliases[0].Alias);
      Assert.AreEqual<string>("ns=2;i=24", _toTest.Items[0].NodeId);
      Assert.AreEqual<string>("ns=2;i=2", ((UAVariableType)_toTest.Items[1]).DataType);
      Assert.AreEqual<string>("2:BleBle", model.ImportQualifiedName("1:BleBle"));
      Assert.AreEqual<string>("s=1:BleBle", model.ImportNodeId("1:BleBle"));
    }

    #endregion tests

    #region test instrumentation

    private static UAObject GetInstanceOfDerivedFromComplexObjectType()
    {
      return new UAObject()
      {
        BrowseName = "1:InstanceOfDerivedFromComplexObjectType",
        NodeId = "ns=1;i=30",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "InstanceOfDerivedFromComplexObjectType" } },
        References = new XML.Reference[]
        {
            new XML.Reference(){ IsForward = true, ReferenceType = "HasProperty", Value = "ns=1;i=32" }
        }
      };
    }

    private static UAObject GetInstanceOfDerivedFromComplexObjectType2()
    {
      return new UAObject()
      {
        BrowseName = "1:InstanceOfDerivedFromComplexObjectType",
        NodeId = "ns=1;i=30",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "NewDisplayName" } },
        References = new XML.Reference[]
        {
            new XML.Reference(){ IsForward = true, ReferenceType = "HasProperty", Value = "ns=1;i=32" }
        }
      };
    }

    private static UAObjectType GetDerivedFromComplexObjectType()
    {
      return new UAObjectType()
      {
        NodeId = "ns=1;i=16",
        BrowseName = "1:DerivedFromComplexObjectType",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "DerivedFromComplexObjectType" } },
        References = new XML.Reference[]
        {
            new XML.Reference(){ IsForward = true, ReferenceType = "HasSubtype", Value = "ns=1;i=25" }
        }
      };
    }

    private static UAObjectType GetComplexObjectType()
    {
      return new UAObjectType()
      {
        NodeId = "ns=1;i=1",
        BrowseName = "1:ComplexObjectType",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "ComplexObjectType" } },
      };
    }

    #endregion test instrumentation
  }
}