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
      UANodeSet instance = UANodeSet.ReadModelFile(_testDataFileInfo);
      List<TraceMessage> _trace = new List<TraceMessage>();
      Mock<Diagnostic.IBuildErrorsHandling> mockTrace = new Mock<Diagnostic.IBuildErrorsHandling>();
      IAddressSpaceContext _as = new AddressSpaceContext(mockTrace.Object);
      _as.ImportUANodeSet(instance);
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAReferenceTestMethod()
    {
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(@"CorrectModels\ReferenceTest\ReferenceTest.NodeSet2.xml", 1, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/ReferenceTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAObjectTypeTestMethod()
    {
      //TODO NetworkIdentifier is missing in generated Model Design for DI model #629
      //TODO The exported model doesn't contain all nodes #653
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(@"CorrectModels\ObjectTypeTest\ObjectTypeTest.NodeSet2.xml", 85, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
      Assert.AreEqual<int>(1, _nodes.Where<IUANodeContext>(x => x.NodeIdContext == new DataSerialization.NodeId(413, 1)).Count<IUANodeContext>());
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UAVariableTypeTestMethod()
    {
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(@"CorrectModels\VariableTypeTest\VariableTypeTest.NodeSet2.xml", 5, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(3, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }

    [TestMethod]
    [TestCategory("Correct Model")]
    public void UADataTypeTestMethod()
    {
      List<IUANodeContext> _nodes = ValidateAndExportModelUnitTest(@"CorrectModels\DataTypeTest\DataTypeTest.NodeSet2.xml", 22, new UriBuilder("http://cas.eu/UA/CommServer/UnitTests/DataTypeTest").Uri);
      Assert.IsFalse(_nodes.Where<IUANodeContext>(x => x.UANode == null).Any<IUANodeContext>());
      Assert.AreEqual<int>(4, _nodes.Where<IUANodeContext>(x => x.UANode is UAType).Count<IUANodeContext>());
    }

    #endregion TestMethod

    #region private

    private List<IUANodeContext> ValidateAndExportModelUnitTest(string testDataFileInfo, int numberOfNodes, Uri model)
    {
      TracedAddressSpaceContext tracedAddressSpaceContext = new TracedAddressSpaceContext(testDataFileInfo);
      tracedAddressSpaceContext.Clear();
      tracedAddressSpaceContext.TestConsistency(0);
      tracedAddressSpaceContext.UTAddressSpaceCheckConsistency(x => { Assert.Fail(); });
      tracedAddressSpaceContext.UTReferencesCheckConsistency((x, y, z, v) => Assert.Fail());
      IEnumerable<IUANodeContext> _nodes = null;
      tracedAddressSpaceContext.UTValidateAndExportModel(1, x => _nodes = x);
      Assert.AreEqual<int>(numberOfNodes, _nodes.Count<IUANodeContext>());
      tracedAddressSpaceContext.ValidateAndExportModel(model);
      tracedAddressSpaceContext.TestConsistency(0);
      return _nodes.ToList<IUANodeContext>();
    }

    #endregion private
  }
}