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
using System.Diagnostics;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  [TestClass]
  public class UAModelContextUnitTest
  {
    [TestMethod]
    public void ParseUANodeSetModelHeaderTest()
    {
      UANodeSet nodeSet = TestData.CreateNodeSetModel();
      Assert.IsNotNull(nodeSet);
      Mock<INamespaceTable> nsMock = new Mock<INamespaceTable>();
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(null, nsMock.Object, logMock));
      Assert.AreEqual<int>(0, trace.Count);
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(nodeSet, null, logMock));
      Assert.AreEqual<int>(0, trace.Count);
      Assert.ThrowsException<ArgumentNullException>(() => UAModelContext.ParseUANodeSetModelHeader(nodeSet, nsMock.Object, null));
      Assert.AreEqual<int>(0, trace.Count);
      UAModelContext modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, nsMock.Object, logMock);
      Assert.IsNotNull(modelContext);
      Assert.AreEqual<int>(1, trace.Count);
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
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
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"))).Returns(10);
      Uri randomURI = null;
      asMock.Setup(x => x.GetURIIndexOrAppend(It.Is<Uri>(z => z.ToString().Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown")))).Returns<Uri>(x => { randomURI = x; return 20; });
      asMock.Setup(x => x.RegisterModel(It.IsAny<IModelTableEntry>()));
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      //start testing
      asMock.Verify(x => x.RegisterModel(It.IsAny<IModelTableEntry>()), Times.Once);
      Assert.AreEqual<string>("ns=10;i=1", _modelContext.ImportNodeId("Boolean", x => Assert.Fail()).ToString());
      Assert.AreEqual<string>("i=45", _modelContext.ImportNodeId("HasSubtype", x => Assert.Fail()).ToString());
      Assert.AreEqual<string>("ns=20;i=2", _modelContext.ImportNodeId("ns=2;i=2", x => Assert.Fail()).ToString());
      asMock.Verify(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")), Times.Once);
      asMock.Verify(x => x.GetURIIndexOrAppend(randomURI), Times.Once);
      asMock.Verify(x => x.RegisterModel(It.IsAny<IModelTableEntry>()), Times.Once);
      Assert.AreEqual<string>("ns=20;i=3", _modelContext.ImportNodeId("ns=2;i=3", x => Assert.Fail()).ToString());
      asMock.Verify(x => x.GetURIIndexOrAppend(randomURI), Times.Exactly(2));
      Assert.AreEqual<string>("ns=20;i=4", _modelContext.ImportNodeId("ns=2;i=4", x => Assert.Fail()).ToString());
      asMock.Verify(x => x.GetURIIndexOrAppend(randomURI), Times.Exactly(3));
      asMock.Verify(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")), Times.Once);
      Assert.AreEqual<int>(2, trace.Count);
      Assert.IsTrue(trace[0].Message.Contains("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"));
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Warning, trace[0].TraceLevel);
      Assert.IsTrue(trace[1].Message.Contains(randomURI.ToString()));
      Assert.AreEqual<string>("P3-0802020000", trace[1].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Warning, trace[1].TraceLevel);
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
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"))).Returns(10);
      asMock.Setup(x => x.RegisterModel(It.IsAny<IModelTableEntry>()));
      Action<TraceMessage> logMock = z => Assert.Fail();
      UAModelContext modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.AreEqual<string>("(10:Boolean, ns=10;i=1)", modelContext.ImportBrowseName("1:Boolean", "ns=1;i=1", x => Assert.Fail()).ToString());
      Assert.AreEqual<string>("(10:AnyText, ns=10;i=1)", modelContext.ImportBrowseName("1:AnyText", "ns=1;i=1", x => Assert.Fail()).ToString());
      Assert.AreEqual<string>("(10:HasSubtype, ns=10;i=1)", modelContext.ImportBrowseName("1:HasSubtype", "ns=1;i=1", x => Assert.Fail()).ToString());
      Assert.AreEqual<string>("ns=10;i=232323", modelContext.ImportNodeId("ns=1;i=232323", x => Assert.Fail()).ToString());
      asMock.Verify(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")), Times.Exactly(7));
      asMock.Verify(x => x.RegisterModel(It.IsAny<IModelTableEntry>()), Times.Once);
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
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"))).Returns(10);
      Uri randomURI = null;
      asMock.Setup(x => x.GetURIIndexOrAppend(It.Is<Uri>(z => z.ToString().Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown")))).Returns<Uri>(x => { randomURI = x; return 20; });
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.AreEqual<string>("ns=20;i=232323", _modelContext.ImportNodeId("ns=2;i=232323", y => Assert.Fail()).ToString());
      asMock.Verify(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")), Times.Never);
      asMock.Verify(x => x.GetURIIndexOrAppend(randomURI), Times.Once);
      Assert.AreEqual<int>(1, trace.Count);
      Assert.IsTrue(trace[0].Message.Contains(randomURI.ToString()));
      Assert.AreEqual<string>("P3-0802020000", trace[0].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Warning, trace[0].TraceLevel);
    }

    [TestMethod]
    public void DefaultUriUriTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" } }
      };
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup<ushort>(x => x.GetURIIndexOrAppend(It.IsAny<Uri>())).Returns(10);
      Action<TraceMessage> logMock = z => Assert.Fail();
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.AreEqual<string>(nodeSet.NamespaceUris[0], _modelContext.DefaultUri.ToString());
    }

    [TestMethod]
    public void ModelUriModelsIsEmptyTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] { new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }, new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://opcfoundation.org/UA/ADI/", "http://opcfoundation.org/UA/DI/" },
      };
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup(x => x.GetURIIndexOrAppend(It.IsAny<Uri>())).Returns(10);
      asMock.Setup(x => x.RegisterModel(It.IsAny<IModelTableEntry>()));
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      Assert.IsNull(nodeSet.Models);
      //Assert.AreEqual<string>("http://opcfoundation.org/UA/ADI/", _modelContext.ModelUri.ToString());
      Assert.AreEqual<int>(1, trace.Count);
      Assert.AreEqual<string>("P0-0001030000", trace[0].BuildError.Identifier);
      Assert.AreEqual<TraceEventType>(TraceEventType.Warning, trace[0].TraceLevel);
      asMock.Verify(x => x.RegisterModel(It.IsAny<IModelTableEntry>()), Times.Once);
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
                DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "New UA Object" } },
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
                DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "New UA Object" } },
                References = new Reference[]{},
                // UAObject
                DataType = "ns=1;i=2",
              }
        }
      };
      Mock<INamespaceTable> addressSpaceMock = new Mock<INamespaceTable>();
      addressSpaceMock.Setup(x => x.GetURIIndexOrAppend(new Uri(@"http://cas.eu/UA/Demo/"))).Returns<Uri>(x => 2);
      List<TraceMessage> _logsCache = new List<TraceMessage>();
      Action<TraceMessage> _logMock = z => _logsCache.Add(z);
      Uri model = nodeSet.ParseUAModelContext(addressSpaceMock.Object, _logMock);
      Assert.IsNotNull(model);
      Assert.AreEqual<string>(nodeSet.NamespaceUris[0], model.ToString());
      addressSpaceMock.Verify(x => x.GetURIIndexOrAppend(new Uri(@"http://cas.eu/UA/Demo/")), Times.AtLeastOnce());
      Assert.AreEqual<string>("ns=2;i=24", nodeSet.Aliases[0].ValueNodeId.ToString());
      Assert.AreEqual<string>("Alias name", nodeSet.Aliases[0].Alias);
      Assert.AreEqual<string>("ns=2;i=24", nodeSet.Items[0].GetIUANode().NodeId.ToString());
      Assert.AreEqual<string>("2:NewUAObject", nodeSet.Items[0].GetIUANode().BrowseName.ToString());
      Assert.AreEqual<string>("ns=2;i=2", ((IUAVariableType)nodeSet.Items[1]).DataType.ToString());
      Assert.AreEqual<string>("2:NewUAObject", ((IUAVariableType)nodeSet.Items[1]).BrowseName.ToString());
    }

    [TestMethod]
    public void RecalculateNodeIdsTest()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] {
          new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" },
          new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest", RequiredModel = null } }
      };
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"))).Returns(10);
      Uri randomURI = null;
      asMock.Setup(x => x.GetURIIndexOrAppend(It.Is<Uri>(z => z.ToString().Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown")))).Returns<Uri>(x => { randomURI = x; return 20; });
      asMock.Setup(x => x.RegisterModel(It.IsAny<IModelTableEntry>()));
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      _modelContext.RegisterUAReferenceType(new QualifiedName("QualifiedName", 10));
      Assert.AreEqual<int>(0, trace.Count);
      asMock.Verify(x => x.RegisterModel(It.IsAny<IModelTableEntry>()), Times.Once);
      asMock.Verify(x => x.GetURIIndexOrAppend(new Uri("http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest")), Times.Never);
      asMock.Verify(x => x.GetURIIndexOrAppend(It.Is<Uri>(z => z.ToString().Contains("github.com/mpostol/OPC-UA-OOI/NameUnknown"))), Times.Never);
      _modelContext.RegisterUAReferenceType(new QualifiedName("QualifiedName", 10));
      Assert.AreEqual<int>(1, trace.Count);
      Assert.AreEqual<string>(BuildError.DuplicatedReferenceType.Identifier, trace[0].BuildError.Identifier);
      Assert.AreEqual<string>("The BrowseName of a ReferenceType shall be unique.", trace[0].BuildError.Descriptor);
      Assert.AreEqual<string>("The UAReferenceType duplicated BrowseName=10:QualifiedName. It is not allowed that two different ReferenceTypes have the same BrowseName", trace[0].Message);
      Debug.WriteLine(trace[0].ToString());
    }

    [TestMethod]
    public void ParseUANodeSetModelHeaderCallBack()
    {
      IUANodeSetModelHeader nodeSet = new UANodeSet
      {
        Aliases = new NodeIdAlias[] {
          new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" },
          new NodeIdAlias() { Alias = "Boolean", Value = "ns=1;i=1" } },
        NamespaceUris = new string[] { "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest" },
        Models = new ModelTableEntry[] { new ModelTableEntry() { ModelUri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest",
                                                                 RequiredModel = new ModelTableEntry[]{ new ModelTableEntry(){ ModelUri = "http://a.b.c.com" } } }
                                                                }
      };
      Mock<INamespaceTable> asMock = new Mock<INamespaceTable>();
      asMock.Setup(x => x.RegisterModel(It.IsAny<IModelTableEntry>()));
      List<TraceMessage> trace = new List<TraceMessage>();
      Action<TraceMessage> logMock = z => trace.Add(z);
      UAModelContext _modelContext = UAModelContext.ParseUANodeSetModelHeader(nodeSet, asMock.Object, logMock);
      asMock.Verify(x => x.RegisterModel(It.IsAny<IModelTableEntry>()), Times.Once);
      Assert.AreEqual<int>(0, trace.Count);
    }

    /// <summary>
    /// Gets the default model table entry.
    /// </summary>
    /// <param name="modelUri">The model URI.</param>
    /// <returns>IModelTableEntry.</returns>
    private static IModelTableEntry GetDefaultModelTableEntry(string modelUri)
    {
      return new ModelTableEntry
      {
        AccessRestrictions = 0xC,
        ModelUri = modelUri,
        PublicationDate = DateTime.UtcNow.Date,
        PublicationDateSpecified = true,
        RequiredModel = null,
        RolePermissions = new XML.RolePermission[] { new XML.RolePermission() },
        Version = new Version(1, 0).ToString()
      };
    }
  }

  internal static class Conversions
  {
    internal static IUANode GetIUANode(this UANode node)
    {
      return node;
    }
  }
}