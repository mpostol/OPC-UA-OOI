
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UnitTest
{

  [TestClass]
  [DeploymentItem(@"XMLModels\", @"XMLModels\")]
  public class NodeSetUnitTest
  {

    #region TestContext
    private TestContext testContextInstance;
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }
    #endregion

    #region TestMethod
    [TestMethod]
    [ExpectedExceptionAttribute(typeof(System.InvalidOperationException))]
    public void WrongFileNFormatTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ReferenceTest\ReferenceTest.NodeSet.xml");  //File not compliant with the schema.
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ImportUANodeSet(_testDataFileInfo);
    }
    [TestMethod]
    public void UAReferenceTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ReferenceTest\ReferenceTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<UANodeContext> _nodes = ValidationUnitTest(_testDataFileInfo, 1);
      Assert.IsFalse(_nodes.Where<UANodeContext>(x => x.UANode == null).Any<UANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<UANodeContext>(x => x.UANode is UAType).Count<UANodeContext>());
    }
    [TestMethod]
    public void UAObjectTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\ObjectTypeTest\ObjectTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<UANodeContext> _nodes = ValidationUnitTest(_testDataFileInfo, 14);
      Assert.IsFalse(_nodes.Where<UANodeContext>(x => x.UANode == null).Any<UANodeContext>());
      Assert.AreEqual<int>(2, _nodes.Where<UANodeContext>(x => x.UANode is UAType).Count<UANodeContext>());
    }
    [TestMethod]
    public void UAVariableTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\VariableTypeTest\VariableTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<UANodeContext> _nodes = ValidationUnitTest(_testDataFileInfo, 4);
      Assert.IsFalse(_nodes.Where<UANodeContext>(x => x.UANode == null).Any<UANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<UANodeContext>(x => x.UANode is UAType).Count<UANodeContext>());
    }
    [TestMethod]
    public void UADataTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\CorrectModels\DataTypeTest\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<UANodeContext> _nodes = ValidationUnitTest(_testDataFileInfo, 18);
      Assert.IsFalse(_nodes.Where<UANodeContext>(x => x.UANode == null).Any<UANodeContext>());
      Assert.AreEqual<int>(4, _nodes.Where<UANodeContext>(x => x.UANode is UAType).Count<UANodeContext>());
    }
    #endregion

    #region private
    private void TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors, ref int diagnosticCounter)
    {
      Console.WriteLine(msg.ToString());
      if (msg.BuildError.Focus == Focus.Diagnostic)
      {
        diagnosticCounter++;
      }
      else
        errors.Add(msg);
    }
    private List<UANodeContext> ValidationUnitTest(FileInfo _testDataFileInfo, int nodes)
    {
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      List<UANodeContext> _nodes = ((AddressSpaceContext)_as).UTValidateAndExportModel(1);
      Assert.AreEqual<int>(nodes, ((AddressSpaceContext)_as).UTValidateAndExportModel(1).Count);
      _as.ValidateAndExportModel();
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      return _nodes;
    }
    #endregion

  }
}
