using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UnitTest.Helpers
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
        Items = new UANode[]
        {
          new UAReferenceType() {  NodeId = "ns=1;i=985", BrowseName="1:FlowTo", DisplayName = new LocalizedText[]{ new LocalizedText()  { Value = "FlowTo"}}, Symmetric = true, 
                                   References = new Reference[] { new Reference() {ReferenceType="HasSubtype", IsForward=false, Value = "i=32"}  }
                                }
        }
      };
      return _ns;
    }

  }
}
