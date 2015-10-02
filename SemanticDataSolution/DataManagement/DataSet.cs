
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{

  public class DataSet
  {
    public DataSet(IDataBrokerFactory brokerFactory, DataMemberConfiguration[] members)
    {
      Members = members.Select<DataMemberConfiguration, DataMember>(x => new DataMember() { ProcessValueName = x.ProcessValueName, SymbolicName = x.SymbolicName }).ToArray<DataMember>();
    }
    public DataMember[] Members { get; private set; }
    public IDataBrokerFactory brokerFactory { get; private set; }
  }
  public class DataMember
  {
    public string SymbolicName;
    public string ProcessValueName;
  }
}
