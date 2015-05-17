
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  [DeploymentItem(@"XMLModels\ModelsWithErrors\", @"ModelsWithErrors\")]
  public class UANodeSetWithErrorsUnitTest
  {
    #region public
    [TestMethod]
    public void DeploymentTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\DeploymentTest.txt");
      Assert.IsTrue(_testDataFileInfo.Exists);
    }
    [TestMethod]
    public void DanglingReferenceTargetTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\DanglingReferenceTarget.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ValidateAndExportModel(@"http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest");
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    [TestMethod]
    public void ObjectEventNotifierOutOfRangeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongEventNotifier.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    [TestMethod]
    public void WrongReference2PropertyTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongReference2Property.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(2, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    [TestMethod]
    public void WrongValueRankTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongValueRank.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(2, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    [TestMethod]
    public void WrongAccessLevelTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongAccessLevel.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.IsTrue(_trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0506040000").Any<TraceMessage>());
    }
    [TestMethod]
    public void WrongInverseNameTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongInverseName.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(3, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual(3, _trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0503020000").Count<TraceMessage>());
    }
    [TestMethod]
    public void DuplicatedNodeIdTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\DuplicatedNodeId.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.IsTrue(_trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502020000").Any<TraceMessage>());
    }
    [TestMethod]
    public void WrongDisplayNameLength()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongDisplayNameLength.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.IsTrue(_trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-05020500").Any<TraceMessage>());
    }
    [TestMethod]
    public void WrongWriteMaskValue()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongWriteMask.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(2, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(2, _trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502070000").Count<TraceMessage>());
    }
    [TestMethod]
    public void NotSupportedFeature()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\NotSupportedFeature.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P0-0001010000").Count<TraceMessage>());
    }
    [TestMethod]
    public void WrongBrowseName()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongBrowseName.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.AreEqual<int>(2, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P6-0503011400").Count<TraceMessage>());
      Assert.AreEqual<int>(1, _trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P6-0F03000000").Count<TraceMessage>());
    }
    [TestMethod]
    public void WrongNodeId()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongNodeId.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      IAddressSpaceContext _as = new AddressSpaceContext(z => TraceDiagnostic(z, _trace, ref _diagnosticCounter));
      Assert.IsNotNull(_as);
      _as.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(4, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.AreEqual<int>(4, _trace.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502020001").Count<TraceMessage>());
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
    #endregion

  }
}
