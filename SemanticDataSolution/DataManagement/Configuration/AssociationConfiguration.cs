
using System;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.DataManagement.Configuration
{

  /// <summary>
  /// Class <see cref="DataSetConfiguration"/> represent a data set as a collection of members to be used as a description to populate (encode) messages sent over the wire 
  /// or analyze (decode) the message read from the wire to retrieve data items. For the senders it also provides criteria on when to forward the new message using the message 
  /// writers associated with this set of data. All communication parties must use the same <see cref="DataSetConfiguration"/> description to decode the message content.
  /// Current value can be loaded from the local configuration or obtained using meta-data exchange centric communication mechanism.
  /// For example it could be read from a file or discovered using OPC UA session from OPC UA server.
  /// </summary>
  [DataContract]
  public class DataSetConfiguration
  {

    [DataMember]
    public AssociationRole AssociationRole { get; set; }
    [DataMember]
    public string Alias { get; set; }
    /// <summary>
    /// Gets or sets the repository group - add groping possibility to the configuration if this configuration is common for large number of devices.
    /// </summary>
    /// <value>The repository group name.</value>
    [DataMember]
    public string RepositoryGroup { get; set; }
    [DataMember]
    public string InformationModelURI { get; set; }
    [DataMember]
    public string DataSymbolicName { get; set; }
    /// <summary>
    /// Gets or sets the members to be used as a description of items to populate the message. The sequence of items in the message is the same as in this table.
    /// The message filter informs if an item is absent in the message and must be skipped.
    /// </summary>
    /// <value>The array of the message item members description.</value>
    [DataMember]
    public DataMemberConfiguration[] DataSet { get; set; }
    [DataMember]
    public Guid Id { get; set; }

  }
}
