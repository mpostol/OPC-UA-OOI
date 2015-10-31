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
    public static type Load<type>(Func<type> loader)
      where type : ConfigurationData
    {
      return loader();
    }
  }
}
