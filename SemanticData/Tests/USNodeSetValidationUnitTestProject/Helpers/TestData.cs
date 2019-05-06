//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
        Items = new UANode[] { CreateUAReferenceType() }
      };
      return _ns;
    }
    public static UAReferenceType CreateUAReferenceType()
    {
      return new UAReferenceType()
      {
        NodeId = "ns=1;i=985",
        BrowseName = "1:FlowTo",
        DisplayName = new LocalizedText[] { new LocalizedText() { Value = "FlowTo" } },
        Symmetric = true,
        References = new Reference[] { new Reference() { ReferenceType = "HasSubtype", IsForward = false, Value = "i=32" } }
      };
    }
    public static UAObject CreateUAObject()
    {
      return new UAObject()
      {
        NodeId = "ns=1;i=1",
        BrowseName = "1:NewUAObject",
        DisplayName = new LocalizedText[] { new LocalizedText() { Value = "New Display Name" } },
        References = new Reference[] { new Reference() { ReferenceType = ReferenceTypeIds.HasTypeDefinition.ToString(), IsForward = false, Value = ObjectTypeIds.BaseObjectType.ToString() } },
        // UAInstance
        ParentNodeId = "ParentNodeId",
        // UAObject
        EventNotifier = 0x01
      };
    }

  }
}
