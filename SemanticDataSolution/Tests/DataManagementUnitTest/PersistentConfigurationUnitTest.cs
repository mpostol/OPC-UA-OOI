
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class PersistentConfigurationUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_PersistentConfiguration")]
    public void GetLocalConfigurationTestMethod()
    {
      ConfigurationData _cnf = PersistentConfiguration.GetLocalConfiguration();
      TestConfiguration(_cnf.DataSets);
      TestConfiguration(_cnf.MessageHandlers);
    }
    #endregion

    #region private
    private void TestConfiguration(MessageHandlerConfiguration[] messageTransportConfiguration)
    {
      foreach (MessageHandlerConfiguration _item in messageTransportConfiguration)
        TestConfiguration((MessageReaderConfiguration)_item);
    }
    private void TestConfiguration(MessageReaderConfiguration _item)
    {
      foreach (ConsumerAssociationConfiguration _ax in _item.ConsumerAssociationConfigurations)
        AssociationsDictionary.Add(_ax.AssociationName, _ax);
      MessageTransportConfigurationDictionary.Add(_item.Name, _item);
      Assert.IsNull(_item.Configuration);
    }
    private void TestConfiguration(DataSetConfiguration[] associationConfiguration)
    {
      foreach (DataSetConfiguration _acx in associationConfiguration)
      {
        AssociationConfigurationDictionary.Add(_acx.AssociationName, _acx);
        Uri _nu = new Uri(_acx.InformationModelURI);
        Assert.IsFalse(String.IsNullOrEmpty(_acx.DataSymbolicName));
        TestDataSet(_acx);
        AssociationConfigurationGuidDictionary.Add(_acx.Id, _acx);
      }
    }
    private void TestDataSet(DataSetConfiguration dataSetConfiguration)
    {
      RepositoryGroupDictionary.Add(dataSetConfiguration.RepositoryGroup, dataSetConfiguration);
      TestConfiguration(dataSetConfiguration.DataSet);
    }
    private void TestConfiguration(FieldMetaData[] dataMemberConfiguration)
    {
      foreach (FieldMetaData _dmx in dataMemberConfiguration)
      {
        Assert.IsFalse(String.IsNullOrEmpty(_dmx.ProcessValueName));
        Assert.AreNotEqual<BuiltInType>(BuiltInType.Null, _dmx.Encoding);
        Assert.IsFalse(String.IsNullOrEmpty(_dmx.SymbolicName));
      }
    }
    private static Dictionary<string, MessageHandlerConfiguration> MessageTransportConfigurationDictionary = new Dictionary<string, MessageHandlerConfiguration>();
    private static Dictionary<string, AssociationConfiguration> AssociationsDictionary = new Dictionary<string, AssociationConfiguration>();
    private static Dictionary<string, DataSetConfiguration> AssociationConfigurationDictionary = new Dictionary<string, DataSetConfiguration>();
    private static Dictionary<Guid, DataSetConfiguration> AssociationConfigurationGuidDictionary = new Dictionary<Guid, DataSetConfiguration>();
    private static Dictionary<string, DataSetConfiguration> RepositoryGroupDictionary = new Dictionary<string, DataSetConfiguration>();
    #endregion

  }
}
