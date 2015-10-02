
namespace UAOOI.SemanticData.DataManagement
{

  public interface IBindingFactory
  {
    IBinding GetDataBroker(string variableName);
  }

}
