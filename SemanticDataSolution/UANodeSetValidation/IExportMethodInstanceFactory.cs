
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportMethodInstanceFactory : IExportInstanceFactory
  {
    bool NonExecutable
    {
      set;
    }
    bool NonExecutableSpecified
    {
      set;
    }

    IExportMethodInstanceFactory Clone();
  }
}
