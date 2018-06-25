
using System.ComponentModel.Composition;
using UAOOI.Networking.DataLogger;
using UAOOI.Networking.SimulatorInteroperabilityTest;

namespace UAOOI.Networking.ReferenceApplication
{

  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ApplicationSettings
  {

    [Export(SimulatorCompositionSettings.ConfigurationFileNameContract)]
    public string ProducerConfigurationFileName => Properties.Settings.Default.ProducerConfigurationFileName;
    [Export(ConsumerCompositionSettings.ConfigurationFileNameContract)]
    public string ConsumerConfigurationFileName => Properties.Settings.Default.ConsumerConfigurationFileName;
  }

}
