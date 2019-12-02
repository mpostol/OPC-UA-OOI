//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest.Instrumentation
{

  [DataContractAttribute(Name = "CustomConfigurationData", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  [XmlRoot(Namespace = CommonDefinitions.Namespace)]
  public class CustomConfigurationData : ConfigurationData
  {
    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public string CustomProperty { get; set; }

    internal static CustomConfigurationData LoadConsumer()
    {
      return new CustomConfigurationData()
      {
        CustomProperty = nameof(CustomProperty),
        DataSets = ReferenceConfiguration.GetDataSetConfigurations(AssociationRole.Consumer),
        MessageHandlers = ReferenceConfiguration.GetMessageHandlers(AssociationRole.Consumer),
        TypeDictionaries = ReferenceConfiguration.TypeDictionaries()
      };
    }
    public override void OnLoaded()
    {
      OnLoadedCount++;
      base.OnLoaded();
    }
    public override void OnSaving()
    {
      OnSavingCount++;
      base.OnSaving();
    }
    internal int OnLoadedCount = 0;
    internal int OnSavingCount = 0;

  }

  internal class CommonDefinitions
  {

    internal const string Namespace = "http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/CustomConfigurationData.xsd";

  }
}
