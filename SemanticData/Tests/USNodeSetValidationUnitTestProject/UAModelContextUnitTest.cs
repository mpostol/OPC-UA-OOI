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
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{
  [TestClass]
  public class UAModelContextUnitTest
  {
    [TestMethod]
    [TestCategory("Code")]
    public void ConstructorTest()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      int logCount = 0;
      Action<TraceMessage> _logMock = z => logCount++;
      UAModelContext _mc = null;
      _mc = new UAModelContext(_tm.Aliases, _tm.NamespaceUris, _asMock.Object, _logMock);
      _mc = new UAModelContext(null, _tm.NamespaceUris, _asMock.Object, _logMock);
      _mc = new UAModelContext(_tm.Aliases, null, _asMock.Object, _logMock);
      Assert.ThrowsException<ArgumentNullException>(() => new UAModelContext(_tm.Aliases, _tm.NamespaceUris, _asMock.Object, null));
      Assert.ThrowsException<ArgumentNullException>(() => new UAModelContext(_tm.Aliases, _tm.NamespaceUris, null, _logMock));
    }

    [TestMethod]
    public void AliasesConversionTest()
    {
      UANodeSet _nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] {
          new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" },
          new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
      };
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      _asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      _asMock.Setup(x => x.GetIndexOrAppend("http://tempuri.org/NameUnknown0")).Returns(20);
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      UAModelContext _modelContext = new UAModelContext(_nodeSet.Aliases, _nodeSet.NamespaceUris, _asMock.Object, _logMock);
      //start testing
      Assert.AreEqual<string>("ns=10;i=1", _modelContext.ImportNodeId("Boolean"));
      Assert.AreEqual<string>("i=45", _modelContext.ImportNodeId("HasSubtype"));
      Assert.AreEqual<string>("ns=20;i=2", _modelContext.ImportNodeId("ns=2;i=2"));
      _asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Once);
      _asMock.Verify(x => x.GetIndexOrAppend("http://tempuri.org/NameUnknown0"), Times.Once);
      Assert.AreEqual<string>("ns=20;i=3", _modelContext.ImportNodeId("ns=2;i=3"));
      _asMock.Verify(x => x.GetIndexOrAppend("http://tempuri.org/NameUnknown0"), Times.Exactly(2));
      Assert.AreEqual<string>("ns=20;i=4", _modelContext.ImportNodeId("ns=2;i=4"));
      _asMock.Verify(x => x.GetIndexOrAppend("http://tempuri.org/NameUnknown0"), Times.Exactly(3));
      Assert.AreEqual<int>(1, _logsCache.Count);
      Assert.IsTrue(_logsCache[0].Message.Contains("http://tempuri.org/NameUnknown0"));
    }

    [TestMethod]
    public void ImportQualifiedNameTest()
    {
      UANodeSet _nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
      };
      Mock<IAddressSpaceBuildContext> _asMock = new Mock<IAddressSpaceBuildContext>();
      _asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      UAModelContext _modelContext = new UAModelContext(_nodeSet.Aliases, _nodeSet.NamespaceUris, _asMock.Object, _logMock);
      Assert.AreEqual<string>("10:Boolean", _modelContext.ImportQualifiedName("1:Boolean"));
      Assert.AreEqual<string>("HasSubtype", _modelContext.ImportQualifiedName("HasSubtype"));
      _asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Once);
      Assert.AreEqual<int>(0, _logsCache.Count);
    }
  }
}