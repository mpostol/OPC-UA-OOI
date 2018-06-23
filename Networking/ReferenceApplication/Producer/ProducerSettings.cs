
using System.ComponentModel.Composition;
using UAOOI.Networking.SimulatorInteroperabilityTest;

namespace UAOOI.Networking.ReferenceApplication.Producer
{

  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ProducerSettings
  {

    [Export(SimulatorCompositionSettings.ConfigurationFileNameContract)]
    public string ProducerConfigurationFileName => Properties.Settings.Default.ProducerConfigurationFileName;

  }

}
