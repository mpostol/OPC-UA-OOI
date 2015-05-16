
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportObjectInstanceFactory : IExportInstanceFactory
  {
    bool SupportsEvents { set; }

    bool SupportsEventsSpecified { set; }

  }
}
