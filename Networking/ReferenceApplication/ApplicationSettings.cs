//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.ComponentModel.Composition;
using UAOOI.Networking.DataLogger;
using UAOOI.Networking.ReferenceApplication.Core;

namespace UAOOI.Networking.ReferenceApplication
{

  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ApplicationSettings
  {

    [Export(CompositionSettings.ConfigurationFileNameContract)]
    public string ProducerConfigurationFileName => Properties.Settings.Default.ProducerConfigurationFileName;
    [Export(ConsumerCompositionSettings.ConfigurationFileNameContract)]
    public string ConsumerConfigurationFileName => Properties.Settings.Default.ConsumerConfigurationFileName;
  }

}
