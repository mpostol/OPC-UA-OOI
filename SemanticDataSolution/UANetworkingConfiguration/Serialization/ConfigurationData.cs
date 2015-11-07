
using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class ConfigurationData - contains configuration data of the UANetworking application.
  /// </summary>
  public partial class ConfigurationData
  {
    /// <summary>
    /// Creates the <typeparam name="ConfigurationDataType"/> instance using specified loader.
    /// </summary>
    /// <param name="loader">The delegate <see cref="Func{ConfigurationDataType}"/> capturing the loader of functionality 
    /// of the class derived from <see cref="ConfigurationData"/>.</param>
    /// <returns>An instance of <typeparam name="ConfigurationDataType"/> derived from <see cref="ConfigurationData"/>.</returns>
    internal static ConfigurationDataType Load<ConfigurationDataType>(Func<ConfigurationDataType> loader)
      where ConfigurationDataType : Serialization.ConfigurationData
    {
      ConfigurationDataType _configuration = loader();
      _configuration.OnLoaded();
      return _configuration;
    }
    /// <summary>
    /// Save the <paramref name="configuration" /> using specified delegate <paramref name="saver" />.
    /// </summary>
    /// <typeparam name="ConfigurationDataType">The type of the configuration instance to be saved.</typeparam>
    /// <param name="configuration">The configuration object of <typeparamref name="ConfigurationDataType"/> type </param>
    /// <param name="saver">The delegate <see cref="Action{ConfigurationDataType}"/> capturing the functionality used to save the <paramref name="configuration"/>.</param>
    internal static void Save<ConfigurationDataType>(ConfigurationDataType configuration, Action<ConfigurationDataType> saver)
      where ConfigurationDataType : Serialization.ConfigurationData
    {
      configuration.OnSaving();
      saver(configuration);
    }
    /// <summary>
    /// Gets the instance configuration - collection of data sets represented as the <see cref="IInstanceConfiguration"/>.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    /// <returns>IEnumerable&lt;IInstanceConfiguration&gt;.</returns>
    internal IEnumerable<IInstanceConfiguration> GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      return DataSets.Where<DataSetConfiguration>(x => x.Root.CompareTo(descriptor) == 0);
    }

    #region private
    /// <summary>
    /// Called when the configuration is loaded.
    /// </summary>
    protected virtual void OnLoaded() { }
    /// <summary>
    /// Called before the saving the configuration.
    /// </summary>
    protected virtual void OnSaving() { }
    #endregion

  }
}
