//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.ComponentModel.Composition;
using UAOOI.Networking.DataLogger;

namespace UAOOI.Networking.ReferenceApplication
{

  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ApplicationSettings
  {

    [Export(UAOOI.Networking.ReferenceApplication.Core.CompositionSettings.ConfigurationFileNameContract)]
    public string ProducerConfigurationFileName => Properties.Settings.Default.ProducerConfigurationFileName;
    [Export(ConsumerCompositionSettings.ConfigurationFileNameContract)]
    public string ConsumerConfigurationFileName => Properties.Settings.Default.ConsumerConfigurationFileName;
  }

}
