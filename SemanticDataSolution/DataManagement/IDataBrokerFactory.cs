
namespace UAOOI.SemanticData.DataManagement
{

  public interface IDataBrokerFactory
  {
    DataBroker GetDataBroker(string variableName);
  }

}
