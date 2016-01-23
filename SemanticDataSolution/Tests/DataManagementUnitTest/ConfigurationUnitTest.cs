
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using UAOOI.SemanticData.UANetworking.Configuration;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ConfigurationUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_Configuration")]
    public void ConfigurationDataCreateTestMethod()
    {
      ConfigurationData _cnf = ConfigurationDataFactoryIO.Load<ConfigurationData>(Create, () => { });
      Assert.IsNotNull(_cnf);
      Assert.IsNotNull(_cnf.DataSets);
      TestAssociations(_cnf.DataSets);
    }
    private void TestAssociations(DataSetConfiguration[] associationConfiguration)
    {
      foreach (DataSetConfiguration _ac in associationConfiguration)
      {
        Assert.IsNotNull(_ac.DataSet);
        Assert.IsFalse(String.IsNullOrEmpty(_ac.AssociationName));
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
    private void TestMembers(FieldMetaData[] dataMemberConfiguration)
    {
      foreach (FieldMetaData _dmc in dataMemberConfiguration)
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
