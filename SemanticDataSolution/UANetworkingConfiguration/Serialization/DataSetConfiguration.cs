
using CAS.UA.IServerConfiguration;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class <see cref="DataSetConfiguration"/> represent a data set as a collection of members to be used as a description to populate (encode) messages sent over the wire 
  /// or analyze (decode) the message read from the wire to retrieve data items. For the senders it also provides criteria on when to forward the new message using the message 
  /// writers associated with this set of data. All communication parties must use the same <see cref="DataSetConfiguration"/> description to decode the message content.
  /// Current value can be loaded from the local configuration or obtained using meta-data exchange centric communication mechanism.
  /// For example it could be read from a file or discovered using OPC UA session from OPC UA server.
  /// </summary>
  public partial class DataSetConfiguration : IInstanceConfiguration
  {

    #region IInstanceConfiguration
    public void ClearConfiguration()
    {
      throw new NotImplementedException();
    }
    public void Edit()
    {
      throw new NotImplementedException();
    }
    #endregion

    #region API
    /// <summary>
    /// Gets or sets the identifier <see cref="Guid"/> to/from the xml stream containing <see cref="string"/>.
    /// </summary>
    /// <value>The identifier.</value>
    [XmlIgnore]
    public Guid Id { get { return XmlConvert.ToGuid(Guid); } set { Guid = XmlConvert.ToString(value); } }
    internal static DataSetConfiguration Create(INodeDescriptor descriptor)
    {
      if (descriptor == null)
        throw new ArgumentNullException(nameof(descriptor));
      if (descriptor.NodeIdentifier == null || descriptor.NodeIdentifier.IsEmpty)
        throw new ArgumentNullException(nameof(descriptor.NodeIdentifier));
      DataSetConfiguration _new = new DataSetConfiguration()
      {
        AssociationName = descriptor.NodeIdentifier.ToString(),
        AssociationRole = AssociationRole.Producer,
        DataSet = new DataMemberConfiguration[0],
        DataSymbolicName = descriptor.NodeIdentifier.ToString(),
        ExtensionData = null,
        Id = System.Guid.NewGuid(),
        RepositoryGroup = "[RepositoryGroup]",
        Root = new NodeDescriptor()
        {
          BindingDescription = descriptor.BindingDescription,
          DataType = descriptor.DataType,
          InstanceDeclaration = descriptor.InstanceDeclaration,
          NodeClass = descriptor.NodeClass,
          NodeIdentifier = descriptor.NodeIdentifier
        }
      };
      return _new;
    }
    #endregion
    #region Object
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return $"{this.Root.NodeClass} : {this.DataSymbolicName}";
    }
    #endregion
  }
}
