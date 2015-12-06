
using System.Linq;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{
  public partial class MessageWriterConfiguration
  {

    internal override bool Associated(string associationName)
    {
      return this.ProducerAssociationConfigurations.Where<ProducerAssociationConfiguration>(x => x.AssociationName == associationName).Any<ProducerAssociationConfiguration>();
    }

  }
}
