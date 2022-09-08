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
using System.Linq;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;
using UAOOI.SemanticData.UANodeSetValidation.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class AddressSpaceContextUnitTest
  {
    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextConstructorTest()
    {
      List<IUANodeBase> _invalidNodes = new List<IUANodeBase>();
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext();
      _asp.UTAddressSpaceCheckConsistency(x => _invalidNodes.Add(x));
      _asp.TestConsistency(0);
      _asp.UTReferencesCheckConsistency((x, y, z, v) => Assert.Fail());
      _asp.TestConsistency(0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextContentCheck()
    {
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext();
      List<IUANodeContext> _content = new List<IUANodeContext>();
      _asp.UTTryGetUANodeContext(VariableTypes.PropertyType, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      _content.Clear();
      _asp.UTTryGetUANodeContext(Objects.RootFolder, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      Assert.IsTrue(new NodeId(Objects.RootFolder) == _content[0].NodeIdContext);
      _content.Clear();
      _asp.UTTryGetUANodeContext(Objects.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      Assert.IsTrue(new NodeId(Objects.ObjectsFolder) == _content[0].NodeIdContext);
      _content.Clear();
      _asp.UTTryGetUANodeContext(ObjectTypes.FolderType, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      Assert.IsTrue(new NodeId(ObjectTypes.FolderType) == _content[0].NodeIdContext);
      _asp.TestConsistency(0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceReferencesContentCheck()
    {
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext();
      List<UAReferenceContext> _content = new List<UAReferenceContext>();
      _content.Clear();
      _asp.UTGetReferences(ObjectIds.RootFolder, x => _content.Add(x));
      Assert.AreEqual<int>(4, _content.Count);
      //RootFolder
      _content.Clear();
      _asp.UTGetReferences(ObjectIds.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(3, _content.Count);
      _asp.TestConsistency(0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextImportUANodeSetNull()
    {
      Mock<IBuildErrorsHandling> mock = new Mock<IBuildErrorsHandling>();
      IAddressSpaceContext _as = new AddressSpaceContext(mock.Object);
      UANodeSet _ns = null;
      Assert.ThrowsException<ArgumentNullException>(() => _as.ImportUANodeSet(_ns));
      FileInfo _fi = null;
      Assert.ThrowsException<ArgumentNullException>(() => UANodeSet.ReadModelFile(_fi));
      _fi = new FileInfo("NotExistingFileName.xml");
      Assert.IsFalse(_fi.Exists);
      Assert.ThrowsException<FileNotFoundException>(() => UANodeSet.ReadModelFile(_fi));
    }

    [TestMethod]
    public void ImportUANodeSetTest()
    {
      UANodeSet newNodeSet = TestData.CreateNodeSetModel();
      newNodeSet.Items = new UANode[]
      {
        new UAObjectType()
        {
          NodeId = "ns=1;i=12",
          BrowseName = "1:VehicleType",
          References = new Reference[]
          {
            new Reference() { ReferenceType = ReferenceTypeIds.HasSubtype.ToString(), IsForward = false,  Value = "i=58" }
          },
        },
        new UAVariable()
        {
          NodeId = "ns=1;i=13",
          BrowseName = "buildDate",
          ParentNodeId="ns=1;i=12",
          DataType="DateTime",
          References = new Reference[]
          {
            new Reference() { ReferenceType=ReferenceTypeIds.HasProperty.ToString(),  IsForward=false, Value = "ns=1;i=12" },
            new Reference() { ReferenceType =ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "i=63" },
            new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(),  Value = "i=78" }
          }
         }
      };
      TracedAddressSpaceContext tracedAddressSpace = new TracedAddressSpaceContext(newNodeSet);
      List<UAReferenceContext> references = new List<UAReferenceContext>();
      tracedAddressSpace.UTGetReferences(NodeId.Parse(newNodeSet.Items[0].NodeId), x => references.Add(x));
      Assert.AreEqual<int>(1, references.Count);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasProperty, references[0].ReferenceKind);
      references.Clear();
      tracedAddressSpace.UTGetReferences(NodeId.Parse(newNodeSet.Items[1].NodeId), x => references.Add(x));
      Assert.AreEqual<int>(2, references.Count);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasTypeDefinition, references[0].ReferenceKind);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasModellingRule, references[1].ReferenceKind);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextValidateAndExportModelOpcUa()
    {
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext();
      _asp.ValidateAndExportModel(new Uri(UAInformationModel.Namespaces.OpcUa));
      _asp.TestConsistency(0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddressSpaceContextValidateAndExportModelWrongNamespace()
    {
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext();
      _asp.ValidateAndExportModel(new Uri("http://www.example.com/afterthought/box"));
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextValidateAndExportIndex0()
    {
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext();
      IEnumerable<IUANodeContext> _returnValue = null;
      _asp.UTValidateAndExportModel(0, x => _returnValue = x);
      Assert.AreEqual<int>(4071, (_returnValue.Count<IUANodeContext>()));
      _asp.TestConsistency(0);
      _asp.UTValidateAndExportModel(1, x => _returnValue = x);
      Assert.AreEqual<int>(0, _returnValue.Count<IUANodeContext>());
      _asp.TestConsistency(0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void ImportObjectTest()
    {
      UANodeSet _newNodeSet = TestData.CreateNodeSetModel();
      TracedAddressSpaceContext _asp = new TracedAddressSpaceContext(_newNodeSet);
      _asp.UTAddressSpaceCheckConsistency(x => { Assert.Fail(); });
      _asp.TestConsistency(1);
      List<UAReferenceContext> _content = new List<UAReferenceContext>();
      _asp.UTGetReferences(ObjectIds.RootFolder, x => _content.Add(x));
      Assert.AreEqual<int>(4, _content.Count);
      //RootFolder
      _content.Clear();
      _asp.UTGetReferences(ObjectIds.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(4, _content.Count);
      IEnumerable<IUANodeContext> _toExport = _content.Where<UAReferenceContext>(x => x.TargetNode.NodeIdContext.NamespaceIndex == 1).Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
      Assert.AreEqual<int>(1, _toExport.Count<IUANodeContext>());
      _asp.TestConsistency(1);
    }

    [TestMethod]
    public void GetMyReferencesTest()
    {
      UANodeSet newNodeSet = TestData.CreateNodeSetModel();
      newNodeSet.Items = new UANode[]
      {
        new UAObjectType()
        {
          NodeId = "ns=1;i=12",
          BrowseName = "1:VehicleType",
          References = new Reference[]
          {
            new Reference() { ReferenceType = ReferenceTypeIds.HasSubtype.ToString(), IsForward = false,  Value = "i=58" }
          },
        },
        new UAVariable()
        {
          NodeId = "ns=1;i=13",
          BrowseName = "buildDate",
          ParentNodeId="ns=1;i=12",
          DataType="DateTime",
          References = new Reference[]
          {
            new Reference() { ReferenceType=ReferenceTypeIds.HasProperty.ToString(),  IsForward=false, Value = "ns=1;i=12" },
            new Reference() { ReferenceType =ReferenceTypeIds.HasTypeDefinition.ToString(), Value = "i=63" },
            new Reference() { ReferenceType = ReferenceTypeIds.HasModellingRule.ToString(),  Value = "i=78" }
          }
         }
      };
      TracedAddressSpaceContext asp = new TracedAddressSpaceContext(newNodeSet);
      IUANodeContext uaObjectType = asp.GetOrCreateNodeContext(NodeId.Parse(newNodeSet.Items[0].NodeId), x => { Assert.Fail(); return null; });
      Assert.IsNotNull(uaObjectType);
      IEnumerable<UAReferenceContext> myReferences = ((IAddressSpaceBuildContext)asp.AddressSpace).GetMyReferences(uaObjectType);
      Assert.AreEqual<int>(1, myReferences.Count<UAReferenceContext>());
      List<UAReferenceContext> _listOfMyReferences = myReferences.ToList<UAReferenceContext>();
      Assert.AreEqual<string>("buildDate", _listOfMyReferences[0].TargetNode.UANode.BrowseName.Name);
      Assert.AreEqual<string>("buildDate", _listOfMyReferences[0].ParentNode.UANode.BrowseName.Name);
      Assert.AreEqual<string>("VehicleType", _listOfMyReferences[0].SourceNode.UANode.BrowseName.Name);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasProperty, _listOfMyReferences[0].ReferenceKind);
    }

    [TestMethod]
    public void GetBaseTypesTest()
    {
      TracedAddressSpaceContext tasp = new TracedAddressSpaceContext();
      List<IUANodeContext> inheritanceChain = new List<IUANodeContext>();
      IUANodeContext hasPropertyNode = tasp.GetOrCreateNodeContext(ReferenceTypeIds.HasProperty, x => { Assert.Fail(); return null; });
      tasp.GetBaseTypes(hasPropertyNode, inheritanceChain);
      Assert.AreEqual<int>(5, inheritanceChain.Count);
      Assert.AreEqual<string>(ReferenceTypeIds.HasProperty.ToString(), inheritanceChain[0].NodeIdContext.ToString());
      Assert.AreEqual<string>(ReferenceTypeIds.References.ToString(), inheritanceChain[4].NodeIdContext.ToString());
    }
  }
}