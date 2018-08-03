//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest.Instrumentation
{

  [DataContractAttribute(Name = "ConfigurationDataWrapper", Namespace = Definitions.m_Namespace)]
  [System.SerializableAttribute()]
  [XmlRoot(Namespace = CommonDefinitions.Namespace)]
  internal class ConfigurationDataWrapper : IConfigurationDataFactory
  {
    public ConfigurationDataWrapper()
    {
      ConfigurationData = ReferenceConfiguration.LoadConsumer();
    }

    [DataMember(EmitDefaultValue = false, IsRequired = true)]
    public ConfigurationData ConfigurationData { get; set; }

    #region IConfigurationDataFactory
    public ConfigurationData GetConfigurationData()
    {
      return ConfigurationData;
    }
    public void OnLoaded()
    {
      OnLoadedCount++;
    }
    public void OnSaving()
    {
      OnSavingCount++;
    }
    public Action OnChanged
    {
      get; set;
    }
    #endregion

    internal int OnSavingCount = 0;
    internal int OnLoadedCount = 0;

  }
  internal static class Definitions
  {
    internal const string m_Namespace = "http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/UnitTest/Serialization.xsd";
  }
}
