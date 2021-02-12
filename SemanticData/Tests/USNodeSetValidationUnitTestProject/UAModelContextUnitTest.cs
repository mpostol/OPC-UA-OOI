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
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{
  [TestClass]
  //TODO UAModelContext must provide default namespaceIndex #517
  public class UAModelContextUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      IUANodeSetModelHeader _tm = TestData.CreateNodeSetModel();
      Mock<IAddressSpaceURIRecalculate> _asMock = new Mock<IAddressSpaceURIRecalculate>();
      int logCount = 0;
      Action<TraceMessage> _logMock = z => logCount++;
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(null, _asMock.Object, _logMock));
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(_tm, _asMock.Object, null));
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(_tm, null, _logMock));
      UAModelContext _mc = UAModelContext.ParseUANodeSetModelHeader(_tm, _asMock.Object, _logMock);
      Assert.IsNotNull(_mc);
    }

    [TestMethod]
    public void ModeltUriTest()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      Mock<IAddressSpaceURIRecalculate> _asMock = new Mock<IAddressSpaceURIRecalculate>();
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => trace.Add(z);
      UAModelContext _mc = null;
      _mc = UAModelContext.ParseUANodeSetModelHeader(_tm, _asMock.Object, _logMock);
      Assert.IsTrue(_mc.ModeltUri.ToString().StartsWith(@"http://cas.eu/UA/Demo/"));
      Assert.AreEqual<int>(1, trace.Count);
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
    }

    [TestMethod]
    public void AliasesConversionTest()
    {
      IUANodeSetModelHeader _nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] {
          new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" },
          new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
      };
      Mock<IAddressSpaceURIRecalculate> _asMock = new Mock<IAddressSpaceURIRecalculate>();
      _asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      string uri = string.Empty;
      _asMock.Setup(x => x.GetIndexOrAppend(It.Is<string>(z => z.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown")))).Returns<string>(x => { uri = x; return 20; });
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(_nodeSet, _asMock.Object, _logMock);
      //start testing
      Assert.AreEqual<string>("ns=10;i=1", _modelContext.ImportNodeId("Boolean"));
      Assert.AreEqual<string>("i=45", _modelContext.ImportNodeId("HasSubtype"));
      Assert.AreEqual<string>("ns=20;i=2", _modelContext.ImportNodeId("ns=2;i=2"));
      _asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Once);
      _asMock.Verify(x => x.GetIndexOrAppend(It.Is<string>(z => z.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown"))), Times.Once);
      Assert.AreEqual<string>("ns=20;i=3", _modelContext.ImportNodeId("ns=2;i=3"));
      _asMock.Verify(x => x.GetIndexOrAppend(It.Is<string>(z => z.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown"))), Times.Exactly(2));
      Assert.AreEqual<string>("ns=20;i=4", _modelContext.ImportNodeId("ns=2;i=4"));
      _asMock.Verify(x => x.GetIndexOrAppend(It.Is<string>(z => z.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown"))), Times.Exactly(3));
      Assert.AreEqual<int>(2, _logsCache.Count);
      Assert.IsTrue(_logsCache[0].Message.Contains("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"));
      Assert.IsTrue(_logsCache[1].Message.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown"));
    }

    [TestMethod]
    public void ImportQualifiedNameTest()
    {
      IUANodeSetModelHeader _nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" } }
      };
      Mock<IAddressSpaceURIRecalculate> _asMock = new Mock<IAddressSpaceURIRecalculate>();
      _asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(_nodeSet, _asMock.Object, _logMock);
      Assert.AreEqual<string>("10:Boolean", _modelContext.ImportQualifiedName("1:Boolean"));
      Assert.AreEqual<string>("HasSubtype", _modelContext.ImportQualifiedName("HasSubtype"));
      _asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Once);
      Assert.AreEqual<int>(0, _logsCache.Count);
    }

    [TestMethod]
    public void ImportNamespaceIndexTest()
    {
      Assert.Inconclusive("Not implemented");
    }

    [TestMethod]
    public void RecalculateNodeIdsUANodeSetTest()
    {
      UANodeSet _toTest = new UANodeSet()
      {
        NamespaceUris = new string[] { @"http://cas.eu/UA/Demo/" },
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "Alias name", Value = "ns=1;i=24" } },
        Items = new UANode[] { new UAObject()
              {
                NodeId = "Alias name",
                BrowseName = "1:NewUAObject",
                DisplayName = new LocalizedText[] { new LocalizedText() { Value = "New UA Object" } },
                References = new Reference[]
                {
                  new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), Value = ObjectTypeIds.BaseObjectType.ToString() },
                  new Reference() { ReferenceType = ReferenceTypeIds.Organizes.ToString(), IsForward= false, Value = "i=85" }
                },
                // UAInstance
                ParentNodeId = string.Empty,
                // UAObject
                EventNotifier = 0x01,
              },
              new UAVariableType()
              {
                NodeId = "ns=1;i=1",
                BrowseName = "1:NewUAObject",
                DisplayName = new LocalizedText[] { new LocalizedText() { Value = "New UA Object" } },
                References = new Reference[]{},
                // UAObject
                DataType = "ns=1;i=2",
              }
        }
      };
      Mock<IAddressSpaceURIRecalculate> addressSpaceMock = new Mock<IAddressSpaceURIRecalculate>();
      addressSpaceMock.Setup(x => x.GetIndexOrAppend(@"http://cas.eu/UA/Demo/")).Returns<string>(x => 2);
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      IUAModelContext model = _toTest.ParseUAModelContext(addressSpaceMock.Object, _logMock);
      Assert.IsNotNull(model);
      addressSpaceMock.Verify(x => x.GetIndexOrAppend(@"http://cas.eu/UA/Demo/"), Times.AtLeastOnce());
      Assert.AreEqual<string>("ns=2;i=24", _toTest.Aliases[0].Value);
      Assert.AreEqual<string>("Alias name", _toTest.Aliases[0].Alias);
      Assert.AreEqual<string>("ns=2;i=24", _toTest.Items[0].NodeId);
      Assert.AreEqual<string>("ns=2;i=2", ((UAVariableType)_toTest.Items[1]).DataType);
      Assert.AreEqual<string>("2:BleBle", model.ImportQualifiedName("1:BleBle"));
      Assert.AreEqual<string>("s=1:BleBle", model.ImportNodeId("1:BleBle"));
    }
  }
}