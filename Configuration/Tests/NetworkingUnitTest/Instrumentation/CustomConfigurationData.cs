//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest.Instrumentation
{
  [SerializableAttribute()]
  public class CustomConfigurationData : ConfigurationData
  {
    public string CustomProperty { get; set; }
  }
}
