
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.Configuration;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

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
      TestConfiguration(_cnf.Associations);
      TestConfiguration(_cnf.MessageTransport);
    }
    #endregion

    #region private
    private void TestConfiguration(MessageTransportConfiguration[] messageTransportConfiguration)
    {
      foreach (MessageTransportConfiguration _item in messageTransportConfiguration)
        TestConfiguration(_item);
    }
    private void TestConfiguration(MessageTransportConfiguration _item)
    {
      foreach (string _ax in _item.Associations)
        AssociationsDictionary.Add(_ax, _ax);
      MessageTransportConfigurationDictionary.Add(_item.Name, _item);
      Assert.IsNull(_item.Configuration);
    }
    private void TestConfiguration(AssociationConfiguration[] associationConfiguration)
    {
      foreach (AssociationConfiguration _acx in associationConfiguration)
      {
        AssociationConfigurationDictionary.Add(_acx.Alias, _acx);
        Uri _nu = new Uri(_acx.InformationModelURI);
        Assert.IsFalse(String.IsNullOrEmpty(_acx.DataSymbolicName));
        TestDataSet(_acx);
        AssociationConfigurationGuidDictionary.Add(_acx.Id, _acx);
      }
    }
    private void TestDataSet(AssociationConfiguration dataSetConfiguration)
    {
      RepositoryGroupDictionary.Add(dataSetConfiguration.RepositoryGroup, dataSetConfiguration);
      TestConfiguration(dataSetConfiguration.DataSet);
    }
    private void TestConfiguration(DataMemberConfiguration[] dataMemberConfiguration)
    {
      foreach (DataMemberConfiguration _dmx in dataMemberConfiguration)
      {
        Assert.IsFalse(String.IsNullOrEmpty(_dmx.ProcessValueName));
        Assert.IsFalse(String.IsNullOrEmpty(_dmx.SourceEncoding));
        Assert.IsFalse(String.IsNullOrEmpty(_dmx.SymbolicName));
      }
    }
    private static Dictionary<string, MessageTransportConfiguration> MessageTransportConfigurationDictionary = new Dictionary<string, MessageTransportConfiguration>();
    private static Dictionary<string, string> AssociationsDictionary = new Dictionary<string, string>();
    private static Dictionary<string, AssociationConfiguration> AssociationConfigurationDictionary = new Dictionary<string, AssociationConfiguration>();
    private static Dictionary<Guid, AssociationConfiguration> AssociationConfigurationGuidDictionary = new Dictionary<Guid, AssociationConfiguration>();
    private static Dictionary<string, AssociationConfiguration> RepositoryGroupDictionary = new Dictionary<string, AssociationConfiguration>();
    #endregion

  }
}
