using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  public partial class ConfigurationData
  {
    /// <summary>
    /// Loads the specified loader.
    /// </summary>
    /// <param name="loader">The loader.</param>
    /// <returns>ConfigurationData.</returns>
    public static ConfigurationData Load(Func<ConfigurationData> loader)
    {
      return loader();
    }
  }
}
