using System;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
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
