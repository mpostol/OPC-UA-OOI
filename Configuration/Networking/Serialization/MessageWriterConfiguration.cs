
using System.Linq;

namespace UAOOI.Configuration.Networking.Serialization
{
  public partial class MessageWriterConfiguration
  {

    public override bool Associated(string associationName)
    {
      return this.ProducerAssociationConfigurations.Where<ProducerAssociationConfiguration>(x => x.AssociationName == associationName).Any<ProducerAssociationConfiguration>();
    }

  }
}
