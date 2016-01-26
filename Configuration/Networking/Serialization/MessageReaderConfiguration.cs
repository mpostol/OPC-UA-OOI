using System.Linq;

namespace UAOOI.Configuration.Networking.Serialization
{
  public partial class MessageReaderConfiguration
  {
    public override bool Associated(string associationName)
    {
      return this.ConsumerAssociationConfigurations.Where<ConsumerAssociationConfiguration>(x => x.AssociationName == associationName).Any<ConsumerAssociationConfiguration>();
    }
  }
}
