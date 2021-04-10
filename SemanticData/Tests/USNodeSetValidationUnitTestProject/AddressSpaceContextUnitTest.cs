//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
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
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      _asp.AddressSpaceContext.UTAddressSpaceCheckConsistency(x => _invalidNodes.Add(x));
      _asp.TestConsistency(5, 0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void ReferencesCheckConsistencyTest()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      _asp.AddressSpaceContext.UTReferencesCheckConsistency((x, y, z, v) => Assert.Fail());
      _asp.TestConsistency(5, 0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextContentCheck()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      List<IUANodeContext> _content = new List<IUANodeContext>();
      _asp.AddressSpaceContext.UTTryGetUANodeContext(VariableTypes.PropertyType, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      _content.Clear();
      _asp.AddressSpaceContext.UTTryGetUANodeContext(Objects.RootFolder, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      Assert.IsTrue(new NodeId(Objects.RootFolder) == _content[0].NodeIdContext);
      _content.Clear();
      _asp.AddressSpaceContext.UTTryGetUANodeContext(Objects.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      Assert.IsTrue(new NodeId(Objects.ObjectsFolder) == _content[0].NodeIdContext);
      _content.Clear();
      _asp.AddressSpaceContext.UTTryGetUANodeContext(ObjectTypes.FolderType, x => _content.Add(x));
      Assert.AreEqual<int>(1, _content.Count);
      Assert.IsTrue(new NodeId(ObjectTypes.FolderType) == _content[0].NodeIdContext);
      _asp.TestConsistency(5, 0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceReferencesContentCheck()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      List<UAReferenceContext> _content = new List<UAReferenceContext>();
      _asp.AddressSpaceContext.UTGetReferences(ObjectIds.RootFolder, x => _content.Add(x));
      Assert.AreEqual<int>(4, _content.Count);
      //RootFolder
      _content.Clear();
      _asp.AddressSpaceContext.UTGetReferences(ObjectIds.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(3, _content.Count);
      _asp.TestConsistency(5, 0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextImportUANodeSetNull()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      UANodeSet _ns = null;
      Assert.ThrowsException<ArgumentNullException>(() => _as.ImportUANodeSet(_ns));
      FileInfo _fi = null;
      Assert.ThrowsException<ArgumentNullException>(() => _as.ImportUANodeSet(_fi));
      _fi = new FileInfo("NotExistingFileName.xml");
      Assert.IsFalse(_fi.Exists);
      Assert.ThrowsException<FileNotFoundException>(() => _as.ImportUANodeSet(_fi));
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
      List<TraceMessage> _traceLog = new List<TraceMessage>();
      AddressSpaceContext asp = new AddressSpaceContext(x => _traceLog.Add(x));
      ((IAddressSpaceContext)asp).ImportUANodeSet(newNodeSet);
      List<UAReferenceContext> references = new List<UAReferenceContext>();
      asp.UTGetReferences(NodeId.Parse(newNodeSet.Items[0].NodeId), x => references.Add(x));
      Assert.AreEqual<int>(1, references.Count);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasProperty, references[0].ReferenceKind);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasProperty, references[0].ReferenceKind);
      references.Clear();
      asp.UTGetReferences(NodeId.Parse(newNodeSet.Items[1].NodeId), x => references.Add(x));
      Assert.AreEqual<int>(2, references.Count);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasTypeDefinition, references[0].ReferenceKind);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasModellingRule, references[1].ReferenceKind);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextValidateAndExportModelOpcUa()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      ((IAddressSpaceContext)_asp.AddressSpaceContext).ValidateAndExportModel(new Uri(UAInformationModel.Namespaces.OpcUa));
      _asp.TestConsistency(684, 2);
      Assert.AreEqual<string>(BuildError.WrongReference2Property.Identifier, _asp.TraceList[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, _asp.TraceList[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddressSpaceContextValidateAndExportModelWrongNamespace()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      ((IAddressSpaceContext)_asp.AddressSpaceContext).ValidateAndExportModel(new Uri("http://www.example.com/afterthought/box"));
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void AddressSpaceContextValidateAndExportIndex0()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      IEnumerable<IUANodeContext> _returnValue = null;
      _asp.AddressSpaceContext.UTValidateAndExportModel(0, x => _returnValue = x);
      Assert.AreEqual<int>(3909, (_returnValue.Count<IUANodeContext>()));
      _asp.TestConsistency(5, 0);
      _asp.AddressSpaceContext.UTValidateAndExportModel(1, x => _returnValue = x);
      Assert.AreEqual<int>(0, _returnValue.Count<IUANodeContext>());
      _asp.TestConsistency(5, 0);
    }

    [TestMethod]
    [TestCategory("AddressSpaceContext")]
    public void ImportObjectTest()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      UANodeSet _newNodeSet = TestData.CreateNodeSetModel();
      ((IAddressSpaceContext)_asp.AddressSpaceContext).ImportUANodeSet(_newNodeSet);
      _asp.TestConsistency(9, 1);
      _asp.AddressSpaceContext.UTAddressSpaceCheckConsistency(x => { Assert.Fail(); });
      _asp.TestConsistency(9, 1);
      List<UAReferenceContext> _content = new List<UAReferenceContext>();
      _asp.AddressSpaceContext.UTGetReferences(ObjectIds.RootFolder, x => _content.Add(x));
      Assert.AreEqual<int>(4, _content.Count);
      //RootFolder
      _content.Clear();
      _asp.AddressSpaceContext.UTGetReferences(ObjectIds.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(4, _content.Count);
      IEnumerable<IUANodeContext> _toExport = _content.Where<UAReferenceContext>(x => x.TargetNode.NodeIdContext.NamespaceIndex == 1).Select<UAReferenceContext, IUANodeContext>(x => x.TargetNode);
      Assert.AreEqual<int>(1, _toExport.Count<IUANodeContext>());
      _asp.TestConsistency(09, 1);
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
      AddressSpaceWrapper asp = new AddressSpaceWrapper();
      ((IAddressSpaceContext)asp.AddressSpaceContext).ImportUANodeSet(newNodeSet);
      IUANodeContext uaObjectType = asp.AddressSpaceContext.GetOrCreateNodeContext(NodeId.Parse(newNodeSet.Items[0].NodeId), x => { Assert.Fail(); return null; });
      Assert.IsNotNull(uaObjectType);
      IEnumerable<UAReferenceContext> myReferences = ((IAddressSpaceBuildContext)asp.AddressSpaceContext).GetMyReferences(uaObjectType);
      Assert.AreEqual<int>(1, myReferences.Count<UAReferenceContext>());
      List<UAReferenceContext> _listOfMyReferences = myReferences.ToList<UAReferenceContext>();
      Assert.AreEqual<string>("buildDate", _listOfMyReferences[0].TargetNode.UANode.BrowseNameQualifiedName.Name);
      Assert.AreEqual<string>("buildDate", _listOfMyReferences[0].ParentNode.UANode.BrowseNameQualifiedName.Name);
      Assert.AreEqual<string>("VehicleType", _listOfMyReferences[0].SourceNode.UANode.BrowseNameQualifiedName.Name);
      Assert.AreEqual<ReferenceKindEnum>(ReferenceKindEnum.HasProperty, _listOfMyReferences[0].ReferenceKind);
    }

    [TestMethod]
    public void GetBaseTypesTest()
    {
      AddressSpaceWrapper asp = new AddressSpaceWrapper();
      List<IUANodeContext> inheritanceChain = new List<IUANodeContext>();
      IUANodeContext hasPropertyNode = asp.AddressSpaceContext.GetOrCreateNodeContext(ReferenceTypeIds.HasProperty, x => { Assert.Fail(); return null; });
      asp.AddressSpaceContext.GetBaseTypes(hasPropertyNode, inheritanceChain);
      Assert.AreEqual<int>(5, inheritanceChain.Count);
      Assert.AreEqual<string>(ReferenceTypeIds.HasProperty.ToString(), inheritanceChain[0].NodeIdContext.ToString());
      Assert.AreEqual<string>(ReferenceTypeIds.References.ToString(), inheritanceChain[4].NodeIdContext.ToString());
    }

    #region private

    private class AddressSpaceWrapper
    {
      public AddressSpaceWrapper()
      {
        AddressSpaceContext = new AddressSpaceContext(x => { TraceDiagnostic(x, TraceList, ref _diagnosticCounter); });
      }

      public void TestConsistency(int diagnosticCounter, int errorsCounter)
      {
        Assert.AreEqual<int>(diagnosticCounter, _diagnosticCounter);
        Assert.AreEqual<int>(errorsCounter, TraceList.Count);
      }

      internal List<TraceMessage> TraceList = new List<TraceMessage>();
      internal AddressSpaceContext AddressSpaceContext = null;

      private int _diagnosticCounter = 0;

      private void TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors, ref int diagnosticCounter)
      {
        Console.WriteLine(msg.ToString());
        diagnosticCounter++;
        if (msg.BuildError.Focus != Focus.Diagnostic)
          errors.Add(msg);
      }
    }

    #endregion private
  }
}