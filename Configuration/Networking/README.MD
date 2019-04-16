# UA Data Networking Configuration

## Introduction

This library contains types that supports the configuration management of application implementing the paradigm described in [Semantic-Data Processing Architecture](../../SemanticData/README.MD). The configuration may be read to or write from the xml or json files. By design this library may be used to support variety kinds of applications at design and run time.

The project seems to be very important prototyping workspace to answer the question how far we can go with the configuration (design time approach) in context of the following problems we have:

1. Message content definition, i.e. items selected to be distributed (or fields, variables, values, etc.  – nor sure about terminology)
2. Metadata distribution – preparation of the consumer to be able to decode the messages,
3. Privileges/Permissions management in context of the distributed data as the primary resource (subject) for any OPC UA data processing application and in context of the message handlers as the data access communication channels (infrastructure).
4. Security tokens distribution to support all scenarios mentioned in 3 above.

Let me know if it works for you and if not how to converge proposed solution and your expectations. Examples how to use the library you can find in the Unit Tests aimed to test selected scenarios.

### Run time

By design it is a plug-in or using modern terminology application composition part of the library described in [Reactive Networking of Semantic-Data Library](../../Networking/SemanticData/README.MD#networking-of-semanticdata-library)  and at run time it can be used as the primary source of configuration for any standalone `OOI Reactive Application` and `UA Server` supporting PubSub role.

In this case the application configuration may:

* be derived from the proposed one
* be expanded by the proposed one

The proposed solution supports also possibility to handle the configuration change event at run time, but this option is not tested and its implementation may change in new releases.

### Design time


I hope the interface is ready to fulfill the following design time scenarios allowing for configuration in context of selected OPC Information Model/Address Space:

1. Plug in to a modeler (UA Information Model design tool) as the configuration editor tool (preferred for me) - now it is compatible with the [Address Space Model Designer](http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx)
2. OPC UA Server as an local editor – using server local GUI, Address Space and the UANetworkingConfiguration plug-in (in this case OPC UA server is just modeler)
3. OPC UA Client as an editor  - using client local GUI/UANetworkingConfiguration on the client side, and remote access to the Address Space
4. OPC UA Server as a remote editor – using custom configuration Information Model/UANetworkingConfiguration plug-in on the server side, and  generic configuration OPC UA Clint as the remote configuration tool,
5. OPC UA Server as a remote dedicated editor – using PubSub Information Model/UANetworkingConfiguration plug-in on the server side, and  dedicated (configuration editor GUI) PubSub configuration OPC UA Clint as the remote configuration tool (let me stress it is the only scenario using PubSub Information Model) – it is rather run time approach.

It is my opinion, but options 4 and 5 are only theoretically possible, because in real installations OPC server is untouchable artifact, but in a lab it is just Modeler and cannot be used as run time configuration tool.

It is good opportunity to review your requirements (if you have any) against the proposed solution.

## Configuration content

The detailed description of the configuration files content is captured in the code help documentation [API Browser][API Browser].

The XML schema of the configuration files is available at: [ConfigurationData.xsd](./Serialization/ConfigurationData.xsd). Intentionally data producers and consumers use the same schema for configuration files. It makes it easier to harmonize these roles configuration and supports two roles in one application.

## Code documentation

- [API Browser][API Browser]: the preliminary code help documentation.
 
[API Browser]: http://www.commsvr.com/download/OPC-UA-OOI/?topic=html/N-UAOOI.Networking.SemanticData.htm
