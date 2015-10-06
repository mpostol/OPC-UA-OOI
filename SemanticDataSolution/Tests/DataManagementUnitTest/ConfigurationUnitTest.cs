
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using global::UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ConfigurationUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_Configuration")]
    public void ConfigurationDataCreateTestMethod()
    {
      ConfigurationData _cnf = ConfigurationData.Load(Create);
      Assert.IsNotNull(_cnf);
      Assert.IsNotNull(_cnf.Associations);
      TestAssociations(_cnf.Associations);
    }
    private void TestAssociations(AssociationConfiguration[] associationConfiguration)
    {
      foreach (AssociationConfiguration _ac in associationConfiguration)
      {
        Assert.IsNotNull(_ac.DataSet);
        Assert.IsFalse(String.IsNullOrEmpty(_ac.Alias));
        Assert.IsFalse(String.IsNullOrEmpty(_ac.DataSymbolicName));
        Assert.IsFalse(String.IsNullOrEmpty(_ac.InformationModelURI));
        TestDataSets(_ac.DataSet);
      }
    }
    private void TestDataSets(DataSetConfiguration dataSetConfiguration)
    {
      Assert.IsNotNull(dataSetConfiguration.Members);
      TestMembers(dataSetConfiguration.Members);
    }
    private void TestMembers(DataMemberConfiguration[] dataMemberConfiguration)
    {
      foreach (DataMemberConfiguration _dmc in dataMemberConfiguration)
      {
        Assert.IsFalse(String.IsNullOrEmpty(_dmc.ProcessValueName));
        Assert.IsFalse(String.IsNullOrEmpty(_dmc.SymbolicName));
      }
    }
    private static ConfigurationData Create()
    {
      return PersistentConfiguration.LocalConfiguration;
    }

  }
}
