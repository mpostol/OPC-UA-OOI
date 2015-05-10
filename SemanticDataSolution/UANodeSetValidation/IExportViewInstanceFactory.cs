
namespace UAOOI.SemanticData.UANodeSetValidation
{

  public interface IExportViewInstanceFactory : IExportInstanceFactory
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
