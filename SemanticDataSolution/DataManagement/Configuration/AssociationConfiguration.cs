
using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  [Serializable]
  public class AssociationConfiguration
  {

    public AssociationRole AssociationRole { get; set; }
    public string Alias { get; set; }
    public string InformationModelURI { get; set; }
    public string DataSymbolicName { get; set; }
    public DataSetConfiguration DataSet { get; set; }

  }
  public enum AssociationRole { Consumer, Producer }
}
