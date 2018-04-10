
# OPC Unified Architecture Part 14: PubSub Main Technology Features

## Introduction

The [OPC.UA.PubSub][OPC.UA.PubSub] offers the publish-subscribe communication pattern as an option to client-server pattern and is a consistent part of the OPC UA specifications suit. The detailed description of the  [OPC.UA.PubSub][OPC.UA.PubSub] has been covered by the document [ OPC Unified Architecture Part 14: PubSub Main Technology Features][README.PubSubMTF].

The specification defines the following actors: 

* `Publisher`: is the actor that pushes `NetworkMessage` structures to an underlying transport layer.
* `Subscriber`: is the actor that consumes data encapsulated by the `NetworkMessage` structure, which is polled from the underlying transport layer.

The `Publisher` is the actor that pushes `NetworkMessage` structures to an underlying transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station, or a stock exchange. It may be also OPC UA Clients, OPC UA Servers or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure. 

The `Subscriber` actors are the consumers of `NetworkMessage` structure, which are polled from the underlying transport layer. They may be OPC UA Clients, OPC UA Servers or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.


This specification defines the OPC Unified Architecture (OPC UA) PubSub communication model. The PubSub communication model defines an OPC UA publish-subscribe pattern instead of the Client/Server pattern defined by the Services in Part 4.

The specification consists of:

* a general introduction to the concepts,
* a definition of the PubSub communication parameters,
* Message Oriented Middleware 
* a PubSub configuration information model,
* mappings to messages and protocols

The specification defines the following actors: 
* Publisher: pushes Messages to an underlying Transport layer.
* Subscriber:  polls Messages from the underlying Transport layer
* Message Oriented Middleware: is any infrastructure supporting sending and receiving Messages between distributed applications. 
* Security Key Service - provides security keys that can be used to sign and encrypt Messages.

The Publisher is the actor that pushes Messages to an underlying Transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station, or a stock exchange. It may be also UA Server **Address Space Management** component. The Publisher doesn't have any subscriptions management functionality, namely, it offers a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client receives a message that it has never requested.

Subscribers are the consumers of Messages, which are polled from the underlying **Transport** layer. They may be OPC UA Clients, OPC UA Servers or in general any applications that understand the structure of Messages. Subscriber must use a filtering mechanism to process only Messages it is interested in. Subscribers do not have any subscription management functionality.

OPC UA PubSub interoperability doesn't depend on any functionality provided by the Message Oriented Middleware. In general, according to the OPC UA PubSub Specification Subscriber and Publisher can be interconnected using any transparent Messages Transport infrastructure providing unreliable service that provides no guarantees for delivery and no protection from duplication. Applying MQTT means that some functionality related to communication reliability, data selection, and distribution is delegated to the MQTT `Server`.

> By design the component `DataManagement` implemented in the project [Networking.SemanticData](https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/SemanticData) is compliant with the [OPC UA Specification Part 14 PubSub](https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/).
> It must be stressed that because the [OPC UA Specification Part 14 PubSub](https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/) has been just released the content of this document may change in future.

## Services

## Mapping

The syntax of the UDP transporting protocol URL doesn't provide information if a multicast group is to be joined. 

## Normative References

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]
- [OPC Unified Architecture Specification  Part 7: Profiles Release 1.04][OPC.UA.Profiles]

 



[RFC.UDP]:https://tools.ietf.org/html/rfc768
[RFC.UDP.IP]:https://tools.ietf.org/html/rfc791

[OPC.UA.PubSub]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[OPC.UA.Profiles]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-7-profiles/

[OOI.Networking.UDPMessageHandler]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/UDPMessageHandler
[OOI.Networking.ReferenceApplication]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/ReferenceApplication
