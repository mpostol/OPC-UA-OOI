
using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest.Simulator
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
        DataSets = new DataSetConfiguration[] { GetAssociationConfiguration(), GetAssociationConfiguration(), GetAssociationConfiguration() },
        MessageHandlers = new MessageHandlerConfiguration[] { GetMessageReaderConfiguration(), GetMessageReaderConfiguration(), GetMessageReaderConfiguration() }

      };
    }
    private static MessageHandlerConfiguration GetMessageReaderConfiguration()
    {
      return new MessageReaderConfiguration()
      {
        ConsumerAssociationConfigurations = new ConsumerAssociationConfiguration[] { new ConsumerAssociationConfiguration() { AssociationName = "Associations".AddId(AssociationId), DataSetWriterId = UInt16.MaxValue, PublisherId = Guid.NewGuid() } },
        Configuration = new MessageChannelConfiguration() { ChannelConfiguration = "4840,True,239.255.255.1,True" },
        Name = "Name".AddId(MessageTransportId),
        TransportRole = AssociationRole.Consumer
      };
    }
    internal static DataSetConfiguration GetAssociationConfiguration()
    {
      return new DataSetConfiguration()
      {
        AssociationName = "Alias".AddId(AssociationId),
        AssociationRole = AssociationRole.Consumer,
        DataSet = GetMembers(),
        DataSymbolicName = "DataSymbolicName".AddId(AssociationId),
        Id = Guid.NewGuid(),
        RepositoryGroup = "RepositoryGroup".AddId(DataMemberId),
        InformationModelURI = "http://www.commsvr.com".AddId(AssociationId)
      };
    }
    internal static FieldMetaData[] GetMembers()
    {
      return new FieldMetaData[] { GetDataMember(), GetDataMember(), GetDataMember() };
    }
    private static FieldMetaData GetDataMember()
    {
      return new FieldMetaData() { ProcessValueName = "ProcessValueName".AddId(DataMemberId), SymbolicName = "SymbolicName".AddId(DataMemberId), TypeInformation = new UATypeInfo(BuiltInType.String) };
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
