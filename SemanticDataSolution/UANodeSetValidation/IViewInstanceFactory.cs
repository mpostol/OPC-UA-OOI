
namespace UAOOI.SemanticData.UANodeSetValidation
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
