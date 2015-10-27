using System;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  /// <summary>
  /// Class ConsumerConfiguration - contains manually created local configuration 
  /// </summary>
  internal static class ConsumerConfiguration
  {
    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData Load()
    {
      return new ConfigurationData() { Associations = GetAssociations(), MessageTransport = GetMessageTransport() };
    }
    #region configuration
    private static MessageTransportConfiguration[] GetMessageTransport()
    {
      return new MessageTransportConfiguration[] { new MessageTransportConfiguration() { Associations = GetTransportAssociations(),
                                                                                         Configuration = null,
                                                                                         Name = "UDP",
                                                                                         TransportRole = AssociationRole.Consumer } };
    }
    private static string[] GetTransportAssociations()
    {
      return new string[] { AssociationConfigurationAlias };
    }
    private static AssociationConfiguration[] GetAssociations()
    {
      return new AssociationConfiguration[] { new AssociationConfiguration() { Alias = AssociationConfigurationAlias,
                                                                               AssociationRole = AssociationRole.Consumer,
                                                                               DataSet = GetDataSet(),
                                                                               DataSymbolicName = "DataSymbolicName",
                                                                               Id = AssociationConfigurationId,
                                                                               InformationModelURI= AssociationConfigurationInformationModelURI
        } };
    }
    private static DataSetConfiguration GetDataSet()
    {
      return new DataSetConfiguration() { Members = GetMembers(), RepositoryGroup = m_RepositoryGroup };
    }
    private static DataMemberConfiguration[] GetMembers()
    {
      return new DataMemberConfiguration[]
      {
          new DataMemberConfiguration() { ProcessValueName = "Value1", SourceEncoding = "System.DateTime", SymbolicName = "Value1" },
          new DataMemberConfiguration() { ProcessValueName = "Value2", SourceEncoding = "System.Double", SymbolicName = "Value2" },
      };
    }
    #endregion

    #region preconfigured settings
    private static Guid AssociationConfigurationId;
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = "https://github.com/mpostol/OPC-UA-OOI";
    #endregion


  }
}
