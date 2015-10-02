using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  public partial class ConfigurationData
  {
    public static ConfigurationData Load(Func<ConfigurationData> loader)
    {
      return loader();
    }
  }
}
