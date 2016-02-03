
using System;
using System.Diagnostics;
using System.IO;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Configuration.Networking
{
  public static class ConfigurationDataFactoryIO
  {
    /// <summary>
    /// Creates the <typeparam name="ConfigurationDataType" /> instance using specified loader.
    /// </summary>
    /// <typeparam name="ConfigurationDataType">The type of the configuration data type.</typeparam>
    /// <param name="loader">The delegate <see cref="Func{ConfigurationDataType}" /> capturing the loader of functionality
    /// of the class derived from <see cref="ConfigurationData" />.</param>
    /// <param name="onChanged">A delegate <see cref="Action" /> encapsulating operation called when this instance is changed.</param>
    /// <returns>An instance of <typeparam name="ConfigurationDataType" /> derived from <see cref="ConfigurationData" />.</returns>
    public static ConfigurationDataType Load<ConfigurationDataType>(Func<ConfigurationDataType> loader, Action onChanged)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      ConfigurationDataType _configuration = loader();
      _configuration.OnChanged = onChanged;
      _configuration.OnLoaded();
      return _configuration;
    }
    public static ConfigurationDataType Load<ConfigurationDataType>(SerializerType serializer, FileInfo configurationFile, Action<TraceEventType, int, string> trace, Action onChanged)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      Func<FileInfo, Action<TraceEventType, int, string>, ConfigurationDataType> _loader = null;
      if (serializer == SerializerType.Xml)
        _loader = (file, tracer) => XmlDataContractSerializers.Load<ConfigurationDataType>(file, tracer);
      else
        _loader = (conf, tracer) => JSONDataContractSerializers.Load<ConfigurationDataType>(conf, tracer);
      ConfigurationDataType _configuration = _loader(configurationFile, (x, y, z) => trace?.Invoke(x, y, z));
      _configuration.OnChanged = onChanged;
      _configuration.OnLoaded();
      return _configuration;
    }
    internal static void Save<ConfigurationDataType>(ConfigurationDataType configuration, Action<ConfigurationDataType> saver)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      configuration.OnSaving();
      saver(configuration);
    }
    internal static void Save<ConfigurationDataType>(ConfigurationDataType configuration, SerializerType serializer, FileInfo configurationFile, Action<TraceEventType, int, string> trace)
      where ConfigurationDataType : class, IConfigurationDataFactory, new()
    {
      configuration?.OnSaving();
      Action<FileInfo, ConfigurationDataType, Action<TraceEventType, int, string>> _saver = null;
      if (serializer == SerializerType.Xml)
        _saver = (conf, file, tracer) => XmlDataContractSerializers.Save<ConfigurationDataType>(conf, file, tracer);
      else
        _saver = (conf, file, tracer) => JSONDataContractSerializers.Save<ConfigurationDataType>(conf, file, tracer);
      _saver(configurationFile, configuration, (x, y, z) => trace?.Invoke(x, y, z));
    }

  }
}
