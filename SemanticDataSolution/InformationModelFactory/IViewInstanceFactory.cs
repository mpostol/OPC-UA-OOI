
namespace UAOOI.SemanticData.InformationModelFactory
{

  public interface IViewInstanceFactory : IInstanceFactory
  {

    bool SupportsEvents
    {
      set;
    }
    bool ContainsNoLoops
    {
      set;
    }

  }
}
