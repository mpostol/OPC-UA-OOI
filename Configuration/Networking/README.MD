# Reactive Networking (RxNetworking) Configuration

## Introduction

By design, this library is a plug-in or using modern terminology application composition part of the `RxNetworking` application described in [Reactive Networking (`RxNetworking`) of Semantic-Data Library](../../Networking/SemanticData/README.MD). At run time it can be used as the primary source of configuration for any standalone `OOI Reactive Application` and `UA Server` supporting PubSub role. It provides a basic implementation of the `IConfigurationDataFactory`. The idea behind this interface implementation is late binding - to allow the adoption of a new functionality against inevitable requirement changes after deploying a solution based on the `RxNetworking` application. 

In this case, the application configuration may:

* be derived from the proposed one
* be expanded by the proposed one

The proposed solution supports also possibility to handle the configuration change event at run time, but this option is not tested and its implementation may change in new releases.

I hope the interface is ready to fulfill the following design time scenarios allowing for configuration in context of selected OPC Information Model/Address Space:

1. Plug in to a modeler (UA Information Model design tool) as the configuration editor tool (preferred for me) - now it is compatible with the [Address Space Model Designer][ASMD]
2. OPC UA Server as an local editor – using server local GUI, Address Space and the UANetworkingConfiguration plug-in (in this case OPC UA server is just modeler)
3. OPC UA Client as an editor - using client local GUI/UANetworkingConfiguration on the client side, and remote access to the Address Space
4. OPC UA Server as a remote editor – using custom configuration Information Model/UANetworkingConfiguration plug-in on the server side, and generic configuration OPC UA Clint as the remote configuration tool,
5. OPC UA Server as a remote dedicated editor – using PubSub Information Model/UANetworkingConfiguration plug-in on the server side, and dedicated (configuration editor GUI) PubSub configuration OPC UA Clint as the remote configuration tool (let me stress it is the only scenario using PubSub Information Model) – it is rather run time approach.

It is my opinion, but options 4 and 5 are only theoretically possible, because in real installations OPC server is untouchable artifact, but in a lab it is just Modeler and cannot be used as run time configuration tool.

It is good opportunity to review your requirements (if you have any) against the proposed solution.

## Configuration content

The detailed description of the configuration files content is captured in the code help documentation [API Browser][API Browser].

The XML schema of the configuration files is available at: [ConfigurationData.xsd](./Serialization/ConfigurationData.xsd). Intentionally data producers and consumers use the same schema for configuration files. It makes it easier to harmonize these roles configuration and supports two roles in one application.

## How to customize the configuration

By design it is a plug-in or using modern terminology application composition part of the library described in [Reactive Networking of Semantic-Data Library][RxNetworkingLib] and at run time it can be used as the primary source of configuration for any standalone `OOI Reactive Application` and `UA Server` supporting PubSub role.

In this case the application configuration may:

* be derived from the proposed one
* be expanded by the proposed one

The proposed solution supports also possibility to handle the configuration change event at run time.

### Custom class wrapping `ConfigurationData`

In this scenario, the custom class must implement `IConfigurationDataFactory` and contain a property with a public getter and setter of type `ConfigurationData`. The snippet below is an example illustrating this scenario. Complete code you may find in the Unit Test project.

```C#
[DataContractAttribute(Name = "ConfigurationDataWrapper", Namespace = Definitions.m_Namespace)]
[System.SerializableAttribute()]
[XmlRoot(Namespace = CommonDefinitions.Namespace)]
internal class ConfigurationDataWrapper : IConfigurationDataFactory
{

 ...
 [DataMember(EmitDefaultValue = false, IsRequired = true)]
 public ConfigurationData ConfigurationData { get; set; } 
 ...

}
```

### Custom class derived form `ConfigurationData`

In this scenario, the custom class is derived from `ConfigurationData` and may contain a vast variety of custom properties decorated with `DataMemberAttribute` to be serialized. The snippet below is an example illustrating this scenario. Complete code you may find in the Unit Test project.

```C#
[DataContractAttribute(Name = "CustomConfigurationData", Namespace = CommonDefinitions.Namespace)]
[SerializableAttribute()]
[XmlRoot(Namespace = CommonDefinitions.Namespace)]
public class CustomConfigurationData : ConfigurationData
{
 ...

 [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true)]
 [XmlElementAttribute(IsNullable = false)]
 public string CustomProperty { get; set; }

 ...
}
```

## See also

* [Reactive Networking (`RxNetworking`) of Semantic-Data Library][RxNetworkingLib]
* [API Browser][API Browser]: the preliminary code help documentation.
* [Address Space Model Designer][ASMD]

[RxNetworkingLib]:../../Networking/SemanticData/README.MD
[API Browser]:http://www.commsvr.com/download/OPC-UA-OOI/index.html
[ASMD]:https://github.com/mpostol/ASMD

<!--

## `IConfigurationDataFactory`

- Static doesn't mean injected. The idea behind this interface implementation is late binding - to allow modification after deploying the main library. It requires loosely coupled parts, but now Configuration is tightly coupled in your code and Reference application. Unfortunately, we don't have proof of the concept that dependency injection is possible in this respect.
- The Configuration library depends on Logger, so it must use any functionality to inject the logger part. Now it uses ServiceLocator, but because it is tightly coupled we can use constructors to provide a logger. 
- Part of your code may be recognized as a clone of the functionality provided by the `Configuration` library, i.e. serialization. The library provides the same functionality offering `DataContractSerializer` and `JsonConvert`. Visit the ReferenceApplication and Configuration library to get more.
- You have correctly applied the possibility to expand the base configuration by adding your stuff but using both serialization methods at the same time may be recognized as a violation of the best practice rules, this way we have >>16 versioning possibilities of the serialization stuff.

Dynamic configuration coupled with discovery functionality is an independent topic that we must discuss after collecting all ideas targeting this topic. 

Once more thanks for the comments. I will use them to improve the documentation. The work targeting this issue mpostol/OPC-UA-OOI#401 is conducted on the branch [Configuration](https://github.com/mpostol/OPC-UA-OOI/tree/Configuration)

-->
