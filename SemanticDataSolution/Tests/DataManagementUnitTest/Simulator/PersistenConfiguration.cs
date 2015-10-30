
using System;
using global::UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{
  /// <summary>
  /// Class PersistentConfiguration - simulates a persistent configuration, like EEPROM, file, etc.
  /// </summary>
  internal static class PersistentConfiguration
  {

    internal static ConfigurationData GetLocalConfiguration()
    {
      return new ConfigurationData()
        {
          Associations = new DataSetConfiguration[] { GetAssociationConfiguration(), GetAssociationConfiguration(), GetAssociationConfiguration() },
          MessageHandlers = new MessageTransportConfiguration[] { GetMessageTransportConfiguration(), GetMessageTransportConfiguration(), GetMessageTransportConfiguration() }

        };
    }
    private static MessageTransportConfiguration GetMessageTransportConfiguration()
    {
      return new MessageTransportConfiguration()
      {
        AssociationNames = new string[] { "Associations".AddId(AssociationId) },
        Configuration = null,
        Name = "Name".AddId(MessageTransportId),
        TransportRole = AssociationRole.Consumer
      };
    }
    internal static DataSetConfiguration GetAssociationConfiguration()
    {
      return new DataSetConfiguration()
      {
        AssociationName = "Alias".AddId(AssociationId),
        AssociationRole = Configuration.AssociationRole.Consumer,
        DataSet = GetMembers(),
        DataSymbolicName = "DataSymbolicName".AddId(AssociationId),
        Id = Guid.NewGuid(),
        RepositoryGroup = "RepositoryGroup".AddId(DataMemberId),
        InformationModelURI = "http://www.commsvr.com".AddId(AssociationId)
      };
    }
    internal static DataMemberConfiguration[] GetMembers()
    {
      return new DataMemberConfiguration[] { GetDataMember(), GetDataMember(), GetDataMember() };
    }
    private static DataMemberConfiguration GetDataMember()
    {
      return new DataMemberConfiguration() { ProcessValueName = "ProcessValueName".AddId(DataMemberId), SymbolicName = "SymbolicName".AddId(DataMemberId), SourceEncoding = "SourceEncoding".AddId(DataMemberId) };
    }
    private static int MessageTransportId { get { return p_MessageTransportId++; } }
    private static int p_MessageTransportId;
    internal static int AssociationId { get { return p_AssociationId++; } }
    private static int DataSetId { get { return p_DataSet++; } }
    public static int DataMemberId { get { return p_DataMemberId++; } }
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
