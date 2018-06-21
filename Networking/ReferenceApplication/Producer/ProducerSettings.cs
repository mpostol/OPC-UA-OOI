
using System.ComponentModel.Composition;

namespace UAOOI.Networking.ReferenceApplication.Producer
{

  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ProducerSettings
  {

    [Export(ProducerCompositionSettings.ConfigurationFileNameContract)]
    public string ProducerConfigurationFileName => Properties.Settings.Default.ProducerConfigurationFileName;

  }

}
