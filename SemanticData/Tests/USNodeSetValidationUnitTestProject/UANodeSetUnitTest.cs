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
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{

  [TestClass]
  [DeploymentItem(@"XMLModels\", @"XMLModels\")]
  public class NodeSetUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("Deployment")]
    [ExpectedExceptionAttribute(typeof(System.InvalidOperationException))]
    public void WrongFileNFormatTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ReferenceTest\ReferenceTest.NodeSet.xml");  //File not compliant with the schema.
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace));
      Assert.IsNotNull(_as);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ImportUANodeSet(_testDataFileInfo);
    }
    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAReferenceTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ReferenceTest\ReferenceTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 1);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }
    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAObjectTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ObjectTypeTest\ObjectTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 85);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<IUANodeContext>(x => x.NodeIdContext == new DataSerialization.NodeId(413, 1)).Count<IUANodeContext>());
    }
    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAVariableTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\VariableTypeTest\VariableTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 5);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }
    [TestMethod]
    [TestCategory("Correct Model")]
    public void UADataTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\DataTypeTest\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 18);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(4, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }
    #endregion

    #region private
    private void TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors)
    {
      Console.WriteLine(msg.ToString());
      if (msg.BuildError.Focus != Focus.Diagnostic)
        errors.Add(msg);
    }
    private List<IUANodeContext> ValidateAndExportModelUnitTest(FileInfo testDataFileInfo, int numberOfNodes)
    {
      List<TraceMessage> _trace = new List<TraceMessage>();
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace));
      Assert.AreEqual<int>(0, _trace.Count);
      _as.ImportUANodeSet(testDataFileInfo);
      Assert.AreEqual<int>(0, _trace.Count);
      ((AddressSpaceContext)_as).UTAddressSpaceCheckConsistency(x => { Assert.Fail(); });
      ((AddressSpaceContext)_as).UTReferencesCheckConsistency((x, y, z, v) => Assert.Fail());
      IEnumerable<IUANodeContext> _nodes = null;
      ((AddressSpaceContext)_as).UTValidateAndExportModel(1, x => _nodes = x);
      Assert.AreEqual<int>(numberOfNodes, _nodes.Count<IUANodeContext>());
      _as.ValidateAndExportModel();
      Assert.AreEqual<int>(0, _trace.Count);
      return _nodes.ToList<IUANodeContext>();
    }
    #endregion

  }
}
