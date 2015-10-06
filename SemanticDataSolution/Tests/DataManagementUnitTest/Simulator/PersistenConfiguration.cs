
using System;
using global::UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  /// <summary>
  /// Class PersistentConfiguration - simulates a persistent configuration, like EEPROM, file, etc.
  /// </summary>
  internal static class PersistentConfiguration
  {

    internal static ConfigurationData LocalConfiguration = new ConfigurationData()
    {
      Associations = new AssociationConfiguration[] { GetAssociationConfiguration(), GetAssociationConfiguration(), GetAssociationConfiguration() }
    };
    internal static AssociationConfiguration GetAssociationConfiguration()
    {
      return new AssociationConfiguration() { Alias = "Alias".AddId(AssociationId), 
                                              DataSet = GetDataSet(), 
                                              DataSymbolicName = "DataSymbolicName".AddId(AssociationId), 
                                              InformationModelURI = "InformationModelURI".AddId(AssociationId) };
    }
    internal static DataSetConfiguration GetDataSet()
    {
      return new DataSetConfiguration() { Members = GetMembers() };
    }
    private static DataMemberConfiguration[] GetMembers()
    {
      return new DataMemberConfiguration[] { GetDataMember(), GetDataMember(), GetDataMember() };
    }
    private static DataMemberConfiguration GetDataMember()
    {
      return new DataMemberConfiguration() { ProcessValueName = "ProcessValueName".AddId(DataMemberId), SymbolicName = "SymbolicName".AddId(DataMemberId) };
    }
    internal static int AssociationId { get { return p_AssociationId++; } }
    private static int DataSetId { get { return p_DataSet++; } }
    public static int DataMemberId { get { return p_DataMemberId; } }
    private static int p_AssociationId = 0;
    private static int p_DataSet = 0;
    private static int p_DataMemberId = 0;
  }
  internal static class StringExtensions
  {
    public static string AddId(this string name, int id)
    {
      return String.Format("{0}{1}", name, id);
    }
  }
}
