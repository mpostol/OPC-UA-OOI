
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{

  [TestClass]
  public class ConfigurationDataUnitTest
  {
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void LoadTestMethod()
    {
      LocalConfigurationData _configuration = ConfigurationData.Load<LocalConfigurationData>(LocalConfigurationData.Loader);
      Assert.IsNotNull(_configuration);
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataConsumerTestMethod()
    {
      ConfigurationData _consumer = ReferenceConfiguration.LoadConsumer();
      FileInfo _consumerFile = new FileInfo(@"ConfigurationDataConsumer.xml");
      DataBindings.Serializers.DataContractSerializers.Save<ConfigurationData>(_consumerFile, _consumer, (x, y, z) => { Console.WriteLine(z); });
    }
    [TestMethod]
    [TestCategory("Configuration_ConfigurationDataUnitTest")]
    public void ConfigurationDataProducerTestMethod()
    {
      ConfigurationData _Producer = ReferenceConfiguration.LoadProducer();
      FileInfo _consumerFile = new FileInfo(@"ConfigurationDataProducer.xml");
      DataBindings.Serializers.DataContractSerializers.Save<ConfigurationData>(_consumerFile, _Producer, (x, y, z) => { Console.WriteLine(z); });
    }
    #region private
    private class LocalConfigurationData : ConfigurationData
    {
      internal static LocalConfigurationData Loader()
      {
        return new LocalConfigurationData();
      }
      private LocalConfigurationData()
      {

      }
    }
    internal static class ReferenceConfiguration
    {

      #region API
      /// <summary>
      /// Created the configuration from the local data.
      /// </summary>
      /// <remarks>In production release shall be replaced by reading from the file.</remarks>
      /// <returns>ConfigurationData.</returns>
      internal static ConfigurationData LoadConsumer()
      {
        return new ConfigurationData() { DataSets = GetAssociations(AssociationRole.Consumer), MessageHandlers = GetMessageTransport(AssociationRole.Consumer) };
      }
      /// <summary>
      /// Created the configuration from the local data.
      /// </summary>
      /// <remarks>In production release shall be replaced by reading from the file.</remarks>
      /// <returns>ConfigurationData.</returns>
      internal static ConfigurationData LoadProducer()
      {
        return new ConfigurationData() { DataSets = GetAssociations(AssociationRole.Producer), MessageHandlers = GetMessageTransport(AssociationRole.Producer) };
      }
      #endregion

      #region configuration
      private static MessageHandlerConfiguration[] GetMessageTransport(AssociationRole associationRole)
      {
        return new MessageHandlerConfiguration[] { new MessageHandlerConfiguration() { AssociationNames = GetTransportAssociations(),
                                                                                         Configuration = null,
                                                                                         Name = "UDP",
                                                                                         TransportRole = associationRole } };
      }
      private static string[] GetTransportAssociations()
      {
        return new string[] { AssociationConfigurationAlias };
      }
      private static DataSetConfiguration[] GetAssociations(AssociationRole associationRole)
      {
        return new DataSetConfiguration[] { new DataSetConfiguration() { AssociationName = AssociationConfigurationAlias,
                                                                               AssociationRole = associationRole,
                                                                               DataSet = GetMembers(),
                                                                               DataSymbolicName = "DataSymbolicName",
                                                                               Id = DefaultAssociationConfigurationId,
                                                                               RepositoryGroup = m_RepositoryGroup,
                                                                               InformationModelURI= AssociationConfigurationInformationModelURI
        } };
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
      private const string AssociationConfigurationInformationModelURI = @"https://github.com/mpostol/OPC-UA-OOI";
      private static readonly Guid DefaultAssociationConfigurationId = new Guid("C1F53FFB-6552-4CCC-84C9-F847147CDC85");
      #endregion

    }

    #endregion
  }
}
