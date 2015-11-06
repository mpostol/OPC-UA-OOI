using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{
  public partial class ConfigurationData
  {
    public static ConfigurationData CurrentConfigurationData { get; private set; }
    /// <summary>
    /// Loads the specified loader.
    /// </summary>
    /// <param name="loader">The loader.</param>
    /// <returns>ConfigurationData.</returns>
    public static type Load<type>(Func<type> loader)
      where type : ConfigurationData
    {
      type _configuration = loader();
      _configuration.Initialize();
      return _configuration;
    }
    internal IEnumerable<IInstanceConfiguration> GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      return DataSets.Where<DataSetConfiguration>(x => x.Root.CompareTo(descriptor) == 0);
    }

    #region private
    private void Initialize()
    {
      //TODO throw new NotImplementedException();
    }
    #endregion

  }
}
