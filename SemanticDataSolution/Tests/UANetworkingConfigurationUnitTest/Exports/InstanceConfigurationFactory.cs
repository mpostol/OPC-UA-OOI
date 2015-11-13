
using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest.Exports
{
  [Export(typeof(IInstanceConfigurationFactory))]
  //TODO Move to UT and to external component.
  public class InstanceConfigurationFactory : IInstanceConfigurationFactory
  {
    public IInstanceConfiguration GetIInstanceConfiguration(IEnumerable<DataSetConfiguration> dataSets, MessageHandlerConfiguration[] availableHandlers)
    {
      return new InstanceConfiguration()
      {
        MessageHandlers = availableHandlers,
        DataSetConfigurations = dataSets.ToArray<DataSetConfiguration>()
      };
    }
    private class InstanceConfiguration : IInstanceConfiguration
    {
      #region IInstanceConfiguration
      public void ClearConfiguration()
      {
        throw new NotImplementedException();
      }
      public void Edit()
      {
        throw new NotImplementedException();
      }
      #endregion
      public MessageHandlerConfiguration[] MessageHandlers { get; set; }
      public DataSetConfiguration[] DataSetConfigurations { get; set; }
      public override string ToString()
      {
        return $"Configuration of: {String.Join(", ", DataSetConfigurations.Select<DataSetConfiguration, string>(x => x.DataSymbolicName))}";
      }
    }

  }
}
