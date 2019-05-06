//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
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

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{

  [TestClass]
  public class AddressSpaceContextUnitTest
  {

    [TestMethod]
    public void AddressSpaceContextConstructorTest()
    {
      List<IUANodeBase> _uncompletedNodes = new List<IUANodeBase>();
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      _asp.AddressSpaceContext.UTAddressSpaceCheckConsistenct(x => _uncompletedNodes.Add(x));
      _asp.TestConsistency(6, 0);
    }
    [TestMethod]
    public void ReferencesCheckConsistencyTest()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      _asp.AddressSpaceContext.UTReferencesCheckConsistency((x, y, z, v) => Assert.Fail());
      _asp.TestConsistency(6, 0);
    }
    [TestMethod]
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
      _asp.TestConsistency(6, 0);
    }
    [TestMethod]
    public void AddressSpaceReferencesContentCheck()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      List<UAReferenceContext> _content = new List<UAReferenceContext>();
      _asp.AddressSpaceContext.UTGetReferences(ObjectIds.ObjectsFolder, x => _content.Add(x));
      Assert.AreEqual<int>(2, _content.Count);
      _asp.TestConsistency(6, 0);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddressSpaceContextImportUANodeSetNullTestMethod1()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      UANodeSet _ns = null;
      _as.ImportUANodeSet(_ns);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddressSpaceContextImportUANodeSetNullTestMethod2()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      FileInfo _fi = null;
      _as.ImportUANodeSet(_fi);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(FileNotFoundException))]
    public void AddressSpaceContextNotExistingFileNameTestMethod()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      FileInfo _fi = new FileInfo("NotExistingFileName.xml");
      Assert.IsFalse(_fi.Exists);
      _as.ImportUANodeSet(_fi);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextValidateAndExportModelTestMethod1()
    {
      AddressSpaceWrapper _asp = new AddressSpaceWrapper();
      ((IAddressSpaceContext)_asp.AddressSpaceContext).ValidateAndExportModel(UAInformationModel.Namespaces.OpcUa);
      _asp.TestConsistency(10, 0);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextValidateAndExportModelTestMethod2()
    {
      ValidateAndExportModelPreparation(out UANodeSet _ns, out IAddressSpaceContext _as);
      _as.ValidateAndExportModel();
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void AddressSpaceContextValidateAndExportModelTestMethod3()
    {
      ValidateAndExportModelPreparation(out UANodeSet _ns, out IAddressSpaceContext _as);
      _as.ValidateAndExportModel("Not existing namespace");
    }
    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextValidateAndExportModelTestMethod4()
    {
      ValidateAndExportModelPreparation(out UANodeSet _ns, out IAddressSpaceContext _as);
      List<IUANodeContext> _returnValue = null;
      ((AddressSpaceContext)_as).UTValidateAndExportModel(0, x => _returnValue = x);
      Assert.AreEqual<int>(3737, (_returnValue.Count));
      ((AddressSpaceContext)_as).UTValidateAndExportModel(1, x => _returnValue = x);
      Assert.AreEqual<int>(1, _returnValue.Count);
    }

    #region private
    private class AddressSpaceWrapper
    {
      public AddressSpaceWrapper()
      {
        AddressSpaceContext = new AddressSpaceContext(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      }
      public void TestConsistency(int diagnosticCounter, int errorsCounter)
      {
        Assert.AreEqual<int>(diagnosticCounter, _diagnosticCounter);
        Assert.AreEqual<int>(errorsCounter, _trace.Count);
      }
      private List<TraceMessage> _trace = new List<TraceMessage>();
      private int _diagnosticCounter = 0;
      internal AddressSpaceContext AddressSpaceContext = null;
    }
    //Helpers
    private static void ValidateAndExportModelPreparation(out UANodeSet _ns, out IAddressSpaceContext _as)
    {
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      _as = new AddressSpaceContext(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());

      _ns = TestData.CreateNodeSetModel();
      Assert.IsTrue(_ns.NamespaceUris.Length >= 1, "Wrong test data - NamespaceUris must contain more then 2 items");
      _as.ImportUANodeSet(_ns);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    #endregion

  }
}
