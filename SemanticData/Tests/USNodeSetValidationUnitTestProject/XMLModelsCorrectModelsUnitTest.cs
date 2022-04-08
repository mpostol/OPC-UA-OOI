//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  [DeploymentItem(@"XMLModels\CorrectModels", @"CorrectModels\")]
  public class XMLModelsCorrectModelsUnitTest
  {
    #region TestMethod

    [TestMethod]
    [TestCategory("Deployment")]
    [ExpectedExceptionAttribute(typeof(System.InvalidOperationException))]
    public void WrongFileNFormatTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"CorrectModels\ReferenceTest\ReferenceTest.NodeSet.xml");  //File not compliant with the schema.
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<TraceMessage> _trace = new List<TraceMessage>();
      Mock<Diagnostic.IBuildErrorsHandling> mockTrace = new Mock<Diagnostic.IBuildErrorsHandling>();
      IAddressSpaceContext _as = new AddressSpaceContext(mockTrace.Object);
      _as.ImportUANodeSet(_testDataFileInfo);
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAReferenceTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"CorrectModels\ReferenceTest\ReferenceTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 1, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/ReferenceTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAObjectTypeTestMethod()
    {
      //TODO NetworkIdentifier is missing in generated Model Design for DI model #629
      //TODO The exported model doesn't contain all nodes #653
      FileInfo _testDataFileInfo = new FileInfo(@"CorrectModels\ObjectTypeTest\ObjectTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 85, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<IUANodeContext>(x => x.NodeIdContext == new DataSerialization.NodeId(413, 1)).Count<IUANodeContext>());
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAVariableTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"CorrectModels\VariableTypeTest\VariableTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 5, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UADataTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"CorrectModels\DataTypeTest\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(_testDataFileInfo, 22, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/DataTypeTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(4, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }

    #endregion TestMethod

    #region private

    private List<IUANodeContext> ValidateAndExportModelUnitTest(FileInfo testDataFileInfo, int numberOfNodes, Uri model)
    {
      TracedAddressSpaceContext tracedAddressSpaceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext _as = tracedAddressSpaceContext.AddressSpaceContext;
      tracedAddressSpaceContext.Clear();
      _as.ImportUANodeSet(testDataFileInfo);
      Assert.AreEqual<int>(0, tracedAddressSpaceContext.TraceList.Count);
      ((AddressSpaceContext)_as).UTAddressSpaceCheckConsistency(x => { Assert.Fail(); });
      ((AddressSpaceContext)_as).UTReferencesCheckConsistency((x, y, z, v) => Assert.Fail());
      IEnumerable<IUANodeContext> _nodes = null;
      ((AddressSpaceContext)_as).UTValidateAndExportModel(1, x => _nodes = x);
      Assert.AreEqual<int>(numberOfNodes, _nodes.Count<IUANodeContext>());
      _as.ValidateAndExportModel(model);
      Assert.AreEqual<int>(0, tracedAddressSpaceContext.TraceList.Count);
      return _nodes.ToList<IUANodeContext>();
    }

    #endregion private
  }
}