using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  [Serializable]
  public class AssociationConfiguration
  {

    public string Alias { get; set; }
    public string InformationModelURI { get; set; }
    public string DataSymbolicName { get; set; }
    public DataSetConfiguration[] DataSets { get; set; }

  }
}
