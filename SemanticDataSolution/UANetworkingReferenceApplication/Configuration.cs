using System;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  /// <summary>
  /// Class Configuration - contains hardcoded local configuration
  /// </summary>
  /// <remarks>In production environment it shall be replaced by reading a configuration file.</remarks>
  internal static class Configuration
  {

    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData LoadConsumer()
    {
      return new ConfigurationData() { Associations = GetAssociations(AssociationRole.Consumer), MessageTransport = GetMessageTransport( AssociationRole.Consumer) };
    }
    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData LoadProducer()
    {
      return new ConfigurationData() { Associations = GetAssociations(AssociationRole.Producer), MessageTransport = GetMessageTransport( AssociationRole.Producer) };
    }
    internal static Guid AssociationConfigurationId = new Guid(Properties.Settings.Default.AssociationConfigurationId);

    #region configuration
    private static MessageTransportConfiguration[] GetMessageTransport(AssociationRole associationRole)
    {
      return new MessageTransportConfiguration[] { new MessageTransportConfiguration() { Associations = GetTransportAssociations(),
                                                                                         Configuration = null,
                                                                                         Name = "UDP",
                                                                                         TransportRole = associationRole } };
    }
    private static string[] GetTransportAssociations()
    {
      return new string[] { AssociationConfigurationAlias };
    }
    private static AssociationConfiguration[] GetAssociations(AssociationRole associationRole)
    {
      return new AssociationConfiguration[] { new AssociationConfiguration() { Alias = AssociationConfigurationAlias,
                                                                               AssociationRole = associationRole,
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
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = "https://github.com/mpostol/OPC-UA-OOI";
    #endregion


  }
}
