
namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class MessageTransportConfiguration - provide configuration for transport used to transfer messages over the wire.
  /// </summary>
  public partial class MessageTransportConfiguration
  {

    /// <summary>
    /// Gets or sets the association names.
    /// </summary>
    /// <value>The association names.</value>
    public string[] AssociationNames
    {
      get { return AssociationNamesArrayOfString.ToArray(); }
      set
      {
        ArrayOfString _associations = new ArrayOfString();
        _associations.AddRange(value);
        AssociationNamesArrayOfString = _associations;
      }
    }

  }
}
