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
using System.Diagnostics;
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
    public void ParseUANodeSetModelHeaderTest()
    {
      UANodeSet nodeSet = TestData.CreateNodeSetModel();
      Assert.IsNotNull(nodeSet);
      Mock<IAddressSpaceURIRecalculate> asMock = new Mock<IAddressSpaceURIRecalculate>();
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(null, asMock.Object, logMock));
      Assert.AreEqual<int>(0, trace.Count);
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(nodeSet, null, logMock));
      Assert.AreEqual<int>(0, trace.Count);
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, null));
      Assert.AreEqual<int>(0, trace.Count);
      UAModelContext modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.IsNotNull(modelContext);
      Assert.AreEqual<int>(1, trace.Count);
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
      Assert.IsTrue(modelContext.ModelUri.ToString().StartsWith(@"http://cas.eu/UA/Demo/"));
    }

    [TestMethod]
    public void ImportNodeIdTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] {
          new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" },
          new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
      };
      Mock<IAddressSpaceURIRecalculate> asMock = new Mock<IAddressSpaceURIRecalculate>();
      asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      string randomURI = string.Empty;
      asMock.Setup(x => x.GetIndexOrAppend(It.Is<string>(z => z.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown")))).Returns<string>(x => { randomURI = x; return 20; });
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      //start testing
      Assert.AreEqual<string>("ns=10;i=1", _modelContext.ImportNodeId("Boolean"));
      Assert.AreEqual<string>("i=45", _modelContext.ImportNodeId("HasSubtype"));
      Assert.AreEqual<string>("ns=20;i=2", _modelContext.ImportNodeId("ns=2;i=2"));
      asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Once);
      asMock.Verify(x => x.GetIndexOrAppend(randomURI), Times.Once);
      Assert.AreEqual<string>("ns=20;i=3", _modelContext.ImportNodeId("ns=2;i=3"));
      asMock.Verify(x => x.GetIndexOrAppend(randomURI), Times.Exactly(2));
      Assert.AreEqual<string>("ns=20;i=4", _modelContext.ImportNodeId("ns=2;i=4"));
      asMock.Verify(x => x.GetIndexOrAppend(randomURI), Times.Exactly(3));
      asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Once);
      Assert.AreEqual<int>(2, trace.Count);
      Assert.IsTrue(trace[0].Message.Contains("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"));
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Information, trace[0].TraceLevel);
      Assert.IsTrue(trace[1].Message.Contains(randomURI));
      Assert.AreEqual<string>("P3-0802020000", trace[1].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Information, trace[1].TraceLevel);
    }

    [TestMethod]
    public void ImportQualifiedNameTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" } }
      };
      Mock<IAddressSpaceURIRecalculate> asMock = new Mock<IAddressSpaceURIRecalculate>();
      asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      Action<TraceMessage> logMock = z => Assert.Fail();
      UAModelContext modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.AreEqual<string>("10:Boolean", modelContext.ImportQualifiedName("1:Boolean"));
      Assert.AreEqual<string>("10:AnyText", modelContext.ImportQualifiedName("1:AnyText"));
      Assert.AreEqual<string>("HasSubtype", modelContext.ImportQualifiedName("HasSubtype"));
      Assert.AreEqual<string>("ns=10;i=232323", modelContext.ImportNodeId("ns=1;i=232323"));
      asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Exactly(3));
    }

    [TestMethod]
    public void ImportQualifiedNameWrongNamespaceIndexTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" } }
      };
      Mock<IAddressSpaceURIRecalculate> asMock = new Mock<IAddressSpaceURIRecalculate>();
      asMock.Setup(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")).Returns(10);
      string randomURI = string.Empty;
      asMock.Setup(x => x.GetIndexOrAppend(It.Is<string>(z => z.Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown")))).Returns<string>(x => { randomURI = x; return 20; });
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.AreEqual<string>("ns=20;i=232323", _modelContext.ImportNodeId("ns=2;i=232323"));
      asMock.Verify(x => x.GetIndexOrAppend("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"), Times.Never);
      asMock.Verify(x => x.GetIndexOrAppend(randomURI), Times.Once);
      Assert.AreEqual<int>(1, trace.Count);
      Assert.IsTrue(trace[0].Message.Contains(randomURI));
      Assert.AreEqual<string>("P3-0802020000", trace[0].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Information, trace[0].TraceLevel);
    }

    [TestMethod]
    public void ModelUriTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" } }
      };
      Mock<IAddressSpaceURIRecalculate> asMock = new Mock<IAddressSpaceURIRecalculate>();
      asMock.Setup(x => x.GetIndexOrAppend(It.IsAny<string>())).Returns(10);
      Action<TraceMessage> logMock = z => Assert.Fail();
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.AreEqual<string>(nodeSet.Models[0].ModelUri, _modelContext.ModelUri.ToString());
    }

    [TestMethod]
    public void ModelUriModelsIsEmptyTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://opcfoundation.org/UA/ADI/", "http://opcfoundation.org/UA/DI/" },
      };
      Mock<IAddressSpaceURIRecalculate> asMock = new Mock<IAddressSpaceURIRecalculate>();
      asMock.Setup(x => x.GetIndexOrAppend(It.IsAny<string>())).Returns(10);
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.IsNull(nodeSet.Models);
      Assert.AreEqual<string>("http://opcfoundation.org/UA/ADI/", _modelContext.ModelUri.ToString());
      Assert.AreEqual<int>(1, trace.Count);
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Information, trace[0].TraceLevel);
    }

    [TestMethod]
    public void RecalculateNodeIdsUANodeSetTest()
    {
      UANodeSet nodeSet = new UANodeSet()
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
      IUAModelContext model = nodeSet.ParseUAModelContext(addressSpaceMock.Object, _logMock);
      Assert.IsNotNull(model);
      addressSpaceMock.Verify(x => x.GetIndexOrAppend(@"http://cas.eu/UA/Demo/"), Times.AtLeastOnce());
      Assert.AreEqual<string>("ns=2;i=24", nodeSet.Aliases[0].Value);
      Assert.AreEqual<string>("Alias name", nodeSet.Aliases[0].Alias);
      Assert.AreEqual<string>("ns=2;i=24", nodeSet.Items[0].NodeId);
      Assert.AreEqual<string>("2:NewUAObject", nodeSet.Items[0].BrowseName);
      Assert.AreEqual<string>("ns=2;i=2", ((UAVariableType)nodeSet.Items[1]).DataType);
      Assert.AreEqual<string>("2:NewUAObject", ((UAVariableType)nodeSet.Items[1]).BrowseName);
    }
  }
}