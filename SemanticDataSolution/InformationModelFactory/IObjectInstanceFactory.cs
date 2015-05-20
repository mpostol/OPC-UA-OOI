
namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface IObjectInstanceFactory : IInstanceFactory
  {
    bool SupportsEvents { set; }

    bool SupportsEventsSpecified { set; }

  }
}
