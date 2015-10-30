
using System;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  [Serializable]
  public class AssociationConfiguration
  {

    public AssociationRole AssociationRole { get; set; }
    public string Alias { get; set; }
    /// <summary>
    /// Gets or sets the repository group - add groping possibility to the configuration if this configuration is common for large number of devices.
    /// </summary>
    /// <value>The repository group name.</value>
    [DataMember]
    public string RepositoryGroup { get; set; }
    public string InformationModelURI { get; set; }
    public string DataSymbolicName { get; set; }
    /// <summary>
    /// Gets or sets the members to be used as a description of items to populate the message. The sequence of items in the message is the same as in this table.
    /// The message filter informs if an item is absent in the message and must be skipped.
    /// </summary>
    /// <value>The array of the message item members description.</value>
    [DataMember]
    public DataMemberConfiguration[] DataSet { get; set; }
    public Guid Id { get; set; }
  }
  public enum AssociationRole { Consumer, Producer }

}
