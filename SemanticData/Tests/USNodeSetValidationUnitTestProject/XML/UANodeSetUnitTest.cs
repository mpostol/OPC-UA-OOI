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
using System.IO;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  [TestClass]
  [DeploymentItem(@"XMLModels\", @"XMLModels\")]
  public class UANodeSetUnitTest
  {
    #region tests

    [TestMethod]
    public void OpcUaNodeSet2TestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ReferenceTest\ReferenceTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      UANodeSet instance = UANodeSet.ReadModelFile(_testDataFileInfo);
      Assert.IsNotNull(instance);
      Assert.IsNotNull(instance.NamespaceUris);
      Assert.IsNotNull(instance.Models);
      Mock<INamespaceTable> asbcMock = new Mock<INamespaceTable>();
      asbcMock.Setup(x => x.GetURIIndexOrAppend(new Uri(@"http://cas.eu/UA/CommServer/UnitTests/ReferenceTest"))).Returns(1);
      List<TraceMessage> trace = new List<TraceMessage>();
      Uri model = instance.ParseUAModelContext(asbcMock.Object, x => trace.Add(x));
      Assert.IsNotNull(model);
      Assert.AreEqual<string>("http://cas.eu/UA/CommServer/UnitTests/ReferenceTest", model.ToString());
      Assert.AreEqual<int>(0, trace.Count);
      asbcMock.Verify(x => x.GetURIIndexOrAppend(It.IsAny<Uri>()), Times.Exactly(2));
    }

    [TestMethod]
    public void ReadUADefinedTypesTest()
    {
      UANodeSet instance = UANodeSet.ReadUADefinedTypes();
      Assert.IsNotNull(instance);
      Assert.IsNotNull(instance.NamespaceUris);
      Assert.IsNotNull(instance.Models);
      Mock<INamespaceTable> asbcMock = new Mock<INamespaceTable>();
      asbcMock.Setup(x => x.GetURIIndexOrAppend(It.IsAny<Uri>()));
      List<TraceMessage> trace = new List<TraceMessage>();
      Uri model = instance.ParseUAModelContext(asbcMock.Object, x => trace.Add(x));
      Assert.IsNotNull(model);
      Assert.AreEqual<string>("http://opcfoundation.org/UA/", model.ToString());
      Assert.AreEqual<int>(0, trace.Count);
      asbcMock.Verify(x => x.GetURIIndexOrAppend(It.IsAny<Uri>()), Times.Never);
    }

    [TestMethod]
    public void NodeClassEnumTest()
    {
      UANode _toTest = new UADataType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UADataType, _toTest.NodeClass);
      _toTest = new UAObject();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAObject, _toTest.NodeClass);
      _toTest = new UAObjectType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAObjectType, _toTest.NodeClass);
      _toTest = new UAReferenceType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAReferenceType, _toTest.NodeClass);
      _toTest = new UAVariable();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAVariable, _toTest.NodeClass);
      _toTest = new UAVariableType();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAVariableType, _toTest.NodeClass);
      _toTest = new UAView();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAView, _toTest.NodeClass);
      _toTest = new UAMethod();
      Assert.AreEqual<NodeClassEnum>(NodeClassEnum.UAMethod, _toTest.NodeClass);
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
      _derived.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      _base.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      Assert.IsTrue(_derived.Equals(_base));
    }

    [TestMethod]
    public void EqualsUAVariableTest()
    {
      UAVariable firsNode = new UAVariable()
      {
        NodeId = "ns=1;i=47",
        BrowseName = "EURange",
        ParentNodeId = "ns=1;i=43",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      UAVariable secondNode = new UAVariable()
      {
        NodeId = "i=17568",
        BrowseName = "EURange",
        ParentNodeId = "i=15318",
        DataType = "i=884",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "EURange" } }
      };
      firsNode.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      secondNode.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      Assert.IsTrue(firsNode.Equals(secondNode));
    }

    [TestMethod]
    public void NotEqualsInstancesTest()
    {
      UAObject firsNode = GetInstanceOfDerivedFromComplexObjectType();
      UAObject secondNode = GetInstanceOfDerivedFromComplexObjectType2();
      firsNode.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      secondNode.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      Assert.IsFalse(firsNode.Equals(secondNode));
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
      _enumeration.RecalculateNodeIds(new ModelContextMock(), x => Assert.Fail());
      Assert.AreEqual<string>("1:EnumerationDataType", _enumeration.BrowseName);
      Assert.AreEqual<string>("ns=1;i=11", _enumeration.NodeId);
      Assert.IsNotNull(_enumeration.BrowseName);
      Assert.IsNotNull(_enumeration.GetIUANode().NodeId);
      Assert.AreEqual<int>(1, _enumeration.GetIUANode().NodeId.NamespaceIndex);
      Assert.IsTrue(((IUANode)_enumeration).BrowseName.NamespaceIndexSpecified);

      Assert.AreEqual<int>(1, _enumeration.References[0].ValueNodeId.NamespaceIndex);
      Assert.AreEqual<int>(0, _enumeration.References[0].ReferenceTypeNodeid.NamespaceIndex);

      Assert.AreEqual<int>(1, _enumeration.References[1].ValueNodeId.NamespaceIndex);
      Assert.AreEqual<int>(0, _enumeration.References[1].ReferenceTypeNodeid.NamespaceIndex);

      Assert.AreEqual<string>("i=24", ((IUADataType)_enumeration).Definition.Field[0].DataTypeNodeId.ToString());
      Assert.AreEqual<string>("ns=1;i=24", ((IUADataType)_enumeration).Definition.Field[1].DataTypeNodeId.ToString());
    }

    #endregion tests

    #region test instrumentation

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
        throw new NotImplementedException();
      }
    }

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