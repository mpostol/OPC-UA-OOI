# OOI Unsolicited Communication over MQTT
 
## Motivation

Main goal of this document is to provide instruction on how to expand the transport layer for OOI Networking of the **Semantic Data** over the Message Queue Telemetry Transport (MQTT) to be compliant with the specification mentioned in the section *Normative references*. Implementation of the `Messages` exchange over the MQTT protocol is out of the scope of this project. The library is intentionaly designed to use any transport protocol meeting some basic requirements using external components. External components implementing MQTT connectivity can be seamlessly integrated with this SDK using dependency injection concept ilustrated by the following domain model:

![](../../CommonResources/Media/DataManagementExternalLibraries.png)

Instruction for implementer are covered in the section *Notices for Implementer*. 

## Normative references

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

1. [OASIS Standards MQTT Version 3.1.1 specification](http://docs.oasis-open.org/mqtt/mqtt/v3.1.1/mqtt-v3.1.1.html)
2. [OPC Unified Architecture Specification Part 14: PubSub Release Candidate 1.04.24 February 9, 2017]()

## Introduction

### MQTT

The Message Queue Telemetry Transport (MQTT) is an open standard application layer protocol. MQTT is a `Client`/`Server` publish/subscribe messaging transport protocol. The protocol runs over TCP/IP, or over other network protocols that provide ordered, lossless, bi-directional connections. A cording to the specification its features include:

* Use of the publish/subscribe message pattern which provides one-to-many message distribution and decoupling of applications.
* A messaging transport that is agnostic to the content of the payload. 
* A small transport overhead and protocol exchanges minimized to reduce network traffic.
* A mechanism to notify interested parties when an abnormal disconnection occurs.

The MQTT protocol defines a binary protocol used to send and receive `Application Message`. The `Application Message` is the data carried by the MQTT protocol across the network for the application. When `Application Messages` are transported by MQTT they have an associated `Quality of Service` (`QoS`) and a `Topic Name`. `Topic Name` is the label attached to an `Application Message` which is matched against the `Subscriptions` known to the `Server`. The `Server` sends a copy of the `Application Message` to each Client that has a matching `Subscription`.

`Server` (called also a broker) acts as an intermediary between `Clients` which publish `Application Messages` and `Clients` which have made `Subscriptions`. The MQTT `Server`:

* Accepts `Network Connections` from `Clients`.
* Accepts `Application Messages` published by `Clients`.
* Processes subscribe and unsubscribe requests from `Clients`.
* Forwards `Application Messages` that match `Client` `Subscriptions`.

A `Server` may persist `Application Messages` so they can be delivered even if the `Subscriber` is not online. 

`Client` establishes the `Network Connection` to the `Server`. It can

* Publish `Application Messages` that other `Clients` might be interested in.
* Create `Subscribtion` to request `Application Messages` that it is interested in receiving.
* Unsubscribe (dispose `Subscription`) to remove a request for `Application Messages`.
* Disconnect from the `Server`.

A `Subscription` comprises a `Topic Filter` and a maximum `QoS`. A `Subscription` is associated with a single `Session`. A `Session` can contain more than one `Subscription`. Each `Subscription` within a `Session` has a different `Topic Filter`. `Topic Filter` is an expression contained in a `Subscription`, to indicate an interest in one or more topics. A `Topic Filter` can include wildcard characters.

Session is a stateful interaction between a `Client` and a `Server`. Some `Sessions` last only as long as the `Network Connection`, others can span multiple consecutive `Network Connections` between a `Client` and a `Server`.

The interaction between the `Client` and `Server` is controlled using `Control Packet`. The `Control Packet` is a packet of data that is sent across the `Network Connection`. The MQTT specification defines fourteen different types of `Control Packet`, one of which (the PUBLISH packet) is used to convey `Application Messages`.

The MQTT defines three `Quality of Service` (`QoS`) levels for `Application Messages` delivery:

* "At most once", where messages are delivered according to the best efforts of the operating environment. Message loss can occur. This level could be used, for example, with ambient sensor data where it does not matter if an individual reading is lost as the next one will be published soon after.
* "At least once", where messages are assured to arrive but duplicates can occur.
* "Exactly once", where message are assured to arrive exactly once. This level could be used, for example, with billing systems where duplicate or lost messages could lead to incorrect charges being applied.

MQTT introduces a concept of topic levels. The topic level separator is used to introduce structure into the `Topic Name`. If present, it divides the `Topic Name` into multiple topic levels. A `Subscription’s` `Topic Filter` can contain special wildcard characters, which allow `Client` to subscribe to multiple topics at once.

### OPC UA PubSub

This specification defines the OPC Unified Architecture (OPC UA) PubSub communication model. The PubSub communication model defines an OPC UA publish subscribe pattern instead of the Client/Server pattern defined by the Services in Part 4.

The specification consists of:

* a general introduction of the concepts,
* a definition of the PubSub communication parameters,
* Message Oriented Middleware 
* a PubSub configuration information model,
* mappings to messages and protocols

The specifications defines the following actors: 
* Publisher: pushes Messages to an underlying Transport layer.
* Subscriber:  polls Messages from the underlying Transport layer
* Message Oriented Middleware: is any infrastructure supporting sending and receiving Messages between distributed applications. 
* Security Key Service - provides security keys that can be used to sign and encrypt Messages.

The Publisher is the actor that pushes Messages to an underlying Transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station, or a stock exchange. It may be also UA Server **Address Space Management** component. The Publisher doesn't have any subscriptions management functionality, namely it offers a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client receives a message that it has never requested.

Subscribers are the consumers of Messages, which are polled from the underlying **Transport** layer. They may be OPC UA Clients, OPC UA Servers or in general any applications that understand the structure of Messages. Subscriber must use a filtering mechanism to process only Messages it is interested on. Subscribers do not have any subscription management functionality.

OPC UA PubSub interoperability doesn't depend on any functionality provided by the Message Oriented Middleware. In general, according to the OPC UA PubSub Specification Subscriber and Publisher can be interconnected using any transparent Messages Transport infrastructure providing unreliable service that provides no guarantees for delivery and no protection from duplication. Applying MQTT means that some functionality related to communication reliability, data selection and distribution is delegated to the MQTT `Server`.

> By design the component `DataManagemnt` implemented in the project [Networking.SemanticData](https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/SemanticData) is compliant with the OPC UA PubSub specification.
> It must be stressed that because the OPC UA Secyfication Part 14 PubSub is not jet released content of this document may change in future. 

## MQTT mapping.

### Requirements:

 1. Transparent communication for messages must be provided.
 2. There must be mapping defined for:
	 1. `Application Messages` payload encoding
	 2. naming
	 2. security
	 3. addressing
	 4. `Qualitry of Service` 
	 5. other

### Messages transport

A `Message` payload is encoded as defined for the UADP message mapping. The messages sent through MQTT are limited to one per `Application Messages`. It is expected that the software used to receive message can process it without needing to know that it was transported via MQTT instead of UDP or TCP for example.

If the encoded MQTT message size exceeds the `Server` limits it should be broken into multiple chunks as described in the OPC UA PubSub Specyfication.

### Security

Security with MQTT is primary provided by a TLS connection between the `Client` and the `Server`, however, this requires that the `Server` must be trusted. For that reason, it may be necessary to provide end-to-end security. Applications that require end-to-end security with MQTT need to use the binary message encoding and apply security protection defined in the PubSub specification. 

### Protocol parameters

This section is under development. It is expected that more requirements and recommendation will be provided after collecting some experience from prototyping. At least the following parameters should be addressed by this section:

* `QoS`
* `Topic Name`
* `Topic Filter`

Anyway, because the OPC UA Specification doesn't provide any requirements in this respect it is expected that only informative mapping information will be provided in this section.
 

## Notices for Implementer

The article [Networking of SemanticData Library](README.md#message-transport) the section *Message Transport* contains description covering instruction for the external MQTT handling components. An example how to implement the Transport layer for the UDP protocol is illustrated by the project [UA Data Example Application](../ReferenceApplication). This application uses two implementation of the `IMessageHandlerFactory`:

* `ConsumerMessageHandlerFactory` - to create communication infrastructure for the consumer role
* `ProducerMessageHandlerFactory` - to create communication infrastructure for the consumer role

It has been implements by the following classes providing the required interfaces:

* `BinaryUDPPackageReader` - implements `IMessageReader` inheriting from `BinaryDecoder`
* `BinaryUDPPackageWriter` - implements `IMessageWriter` inheriting from `BinaryEncoder`


 
