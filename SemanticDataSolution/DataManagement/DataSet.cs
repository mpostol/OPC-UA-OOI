
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{

  public class DataSet
  {
    public DataSet(IDataBrokerFactory brokerFactory, DataMemberConfiguration[] members)
    {
      if (brokerFactory == null)
        throw new System.ArgumentNullException("brokerFactory must not be null");
      if (members == null)
        throw new System.ArgumentNullException("members must not be null");
      this.BrokerFactory = brokerFactory;
      Members = members.Select<DataMemberConfiguration, DataMember>(x => new DataMember() { ProcessValueName = x.ProcessValueName, SymbolicName = x.SymbolicName }).ToArray<DataMember>();
    }
    public DataMember[] Members { get; private set; }
    public IDataBrokerFactory BrokerFactory { get; private set; }
  }
  public class DataMember
  {
    public string SymbolicName;
    public string ProcessValueName;
  }
}
