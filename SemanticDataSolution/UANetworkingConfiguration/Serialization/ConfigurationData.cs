using CAS.UA.IServerConfiguration;
using System;

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
    internal static ConfigurationData CreateDefault()
    {
      return new ConfigurationData() { DataSets = new DataSetConfiguration[] { }, MessageHandlers = new MessageHandlerConfiguration[] { } };
    }
    internal IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      //InstanceConfiguration sourceIC = new DataSetConfiguration(descriptor);
      //InstanceConfiguration ic = null;
      //if (Dictionary.TryGetValue(sourceIC.NodeDescriptor, out ic))
      //  return ic;
      //else
      //{
      //  Dictionary.Add(sourceIC.NodeDescriptor, sourceIC);
      //  return sourceIC;
      //}
      throw new NotImplementedException();
    }

    private void Initialize()
    {
      //TODO throw new NotImplementedException();
    }

  }
}
