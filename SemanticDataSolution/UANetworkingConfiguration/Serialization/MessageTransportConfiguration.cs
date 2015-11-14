
using System.Linq;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class MessageTransportConfiguration - provide configuration for transport used to transfer messages over the wire.
  /// </summary>
  public partial class MessageHandlerConfiguration
  {

    /// <summary>
    /// Gets or sets the association names.
    /// </summary>
    /// <value>The association names.</value>
    [XmlIgnore]
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
    internal bool Associated(string associationName)
    {
      return AssociationNamesArrayOfString.Where<string>( x=> x == associationName).Any<string>();
    }
  }
}
