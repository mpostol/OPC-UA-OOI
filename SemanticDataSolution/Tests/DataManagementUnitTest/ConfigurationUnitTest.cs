
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using global::UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

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
    private void TestAssociations(DataSetConfiguration[] associationConfiguration)
    {
      foreach (DataSetConfiguration _ac in associationConfiguration)
      {
        Assert.IsNotNull(_ac.DataSet);
        Assert.IsFalse(String.IsNullOrEmpty(_ac.Alias));
        Assert.IsFalse(String.IsNullOrEmpty(_ac.DataSymbolicName));
        Assert.IsFalse(String.IsNullOrEmpty(_ac.InformationModelURI));
        TestDataSets(_ac);
      }
    }
    private void TestDataSets(DataSetConfiguration dataSetConfiguration)
    {
      Assert.IsNotNull(dataSetConfiguration.DataSet);
      TestMembers(dataSetConfiguration.DataSet);
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
      return PersistentConfiguration.GetLocalConfiguration();
    }

  }
}
