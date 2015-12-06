using System.Linq;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{
  public partial class MessageReaderConfiguration
  {
    internal override bool Associated(string associationName)
    {
      return this.ConsumerAssociationConfigurations.Where<ConsumerAssociationConfiguration>(x => x.AssociationName == associationName).Any<ConsumerAssociationConfiguration>();
    }
  }
}
