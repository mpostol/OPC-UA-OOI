//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers
{
  internal static class TestData
  {
    public static UANodeSet CreateNodeSetModel()
    {
      UANodeSet _ns = new UANodeSet()
      {
        NamespaceUris = new string[] { @"http://cas.eu/UA/Demo/" },
        Aliases = new NodeIdAlias[] {
                                      new NodeIdAlias() { Alias = "Boolean", Value = "i=1" },
                                      new NodeIdAlias() { Alias = "HasSubtype", Value = "i=45" }
                                    },
        Items = new UANode[] { CreateUAObject() }
      };
      return _ns;
    }

    public static UAObject CreateUAObject()
    {
      return new UAObject()
      {
        NodeId = "ns=1;i=1",
        BrowseName = "1:NewUAObject",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "New UA Object" } },
        References = new Reference[]
        {
          new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), IsForward = true, Value = ObjectTypeIds.BaseObjectType.ToString() },
          new Reference() { ReferenceType = ReferenceTypeIds.Organizes.ToString(), IsForward= false, Value = "i=85" }
        },
        // UAInstance
        ParentNodeId = string.Empty,
        // UAObject
        EventNotifier = 0x01,
      };
    }

    public static UAReferenceType CreateUAReferenceType()
    {
      return new UAReferenceType()
      {
        NodeId = "ns=1;i=985",
        BrowseName = "1:FlowTo",
        DisplayName = new XML.LocalizedText[] { new XML.LocalizedText() { Value = "FlowTo" } },
        Symmetric = true,
        References = new Reference[] { new Reference() { ReferenceType = "HasSubtype", IsForward = false, Value = "i=32" } }
      };
    }

    internal static UANodeType Recalculate<UANodeType>(this UANodeType node)
      where UANodeType : UANode
    {
      Mock<IUAModelContext> modelMock = new Mock<IUAModelContext>();
      modelMock.Setup(x => x.ImportNodeId(It.IsAny<string>(), It.IsAny<Action<TraceMessage>>())).Returns<string, Action<TraceMessage>>((q, w) => NodeId.Parse(q));
      modelMock.Setup(x => x.ImportBrowseName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<TraceMessage>>())).Returns<string, string, Action<TraceMessage>>((a, b, c) => (QualifiedName.Parse(a), NodeId.Parse(b)));
      modelMock.Setup(x => x.ModelUri);
      UAObject toTest = TestData.CreateUAObject();
      node.RecalculateNodeIds(modelMock.Object, XML => Assert.Fail());
      return node;
    }
  }
}