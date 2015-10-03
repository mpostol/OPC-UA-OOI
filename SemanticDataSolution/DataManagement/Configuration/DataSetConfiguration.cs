
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  [DataContract]
  public class DataSetConfiguration
  {
    [DataMember]
    public DataMemberConfiguration[] Members { get; set; }
    public string RepositoryGroup { get; set; }

  }
}
