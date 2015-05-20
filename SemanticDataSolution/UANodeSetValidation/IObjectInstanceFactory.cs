
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IObjectInstanceFactory : IInstanceFactory
  {
    bool SupportsEvents { set; }

    bool SupportsEventsSpecified { set; }

  }
}
