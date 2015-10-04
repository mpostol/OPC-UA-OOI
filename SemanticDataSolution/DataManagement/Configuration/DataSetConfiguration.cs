
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  /// <summary>
  /// Class DataSetConfiguration represent DataSet as a collection of members to be used an a description to populate messages send over the wire 
  /// and the criteria on when to forward new message using the MessageWriters associated with this set of data.
  /// All communication parties must use the same DatSet description to decode the message content.
  /// This class represents the PublishedDataSetType defined in the specification, see P14 - 5.1.3.2.
  /// Current value can be loaded from the local configuration or obtained using meta-data exchange centric communication mechanism.
  /// For example it could be read from a file or discovered using OPC UA session from OPC UA server.
  /// </summary>
  [DataContract]
  public class DataSetConfiguration
  {
    /// <summary>
    /// Gets or sets the members to be used as a description of items to populate the message. The sequence of items in the message is the same as in this table.
    /// The message filter informs if an item is absent in the message and must be skiped.
    /// </summary>
    /// <value>The array of the message item members description.</value>
    [DataMember]
    public DataMemberConfiguration[] Members { get; set; }
    /// <summary>
    /// Gets or sets the repository group - add groping possibility to the configuration if this configuration is common for large number of devices.
    /// </summary>
    /// <value>The repository group name.</value>
    public string RepositoryGroup { get; set; }

  }
}
