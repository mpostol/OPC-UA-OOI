
using CAS.UA.IServerConfiguration;
using System;

namespace UAOOI.DataBindings
{
  /// <summary>
  /// Class ConfigurationBase - Provides basic implementation of the <see cref="IConfiguration"/>.
  /// </summary>
  public abstract class ConfigurationBase<ConfigurationDataType>// : IConfiguration
  {
    //TODO - to be removed 
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationBase{ConfigurationDataType}"/> class. 
    /// Default configuration is loader.
    /// </summary>
    public ConfigurationBase(Func<ConfigurationDataType> configurationLoader)
    {
      //TODO - fine where it was used:
    }

  }
}
