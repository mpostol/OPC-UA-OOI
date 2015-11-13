using CAS.UA.IServerConfiguration;
using System.Collections.Generic;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration
{
  public interface IInstanceConfigurationFactory
  {
    IInstanceConfiguration GetIInstanceConfiguration(IEnumerable<DataSetConfiguration> dataSets, MessageHandlerConfiguration[] availableHandlers);
  }
}
