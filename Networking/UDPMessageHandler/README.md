# Underlying Transport over UDP

## Motivation

The main goal of this document is to provide instruction on how to expand the transport layer for OOI Networking of the **Semantic Data** over the User Datagram Protocol (UDP) to be compliant with the specifications mentioned in the section *Normative references*.

A reference implementation of the `Transport` over UDP is provided by the project [Networking.UDPMessageHandler][OOI.Networking.UDPMessageHandler]. Because intentionally the SDK is designed to use any transport protocol, a custom external component implementing UDP connectivity can be seamlessly integrated with this SDK using dependency injection concept illustrated by the following domain model:

![Figure 1. Domain Model](../../CommonResources/Media/DataManagementExternalLibraries.png)

Instruction for implementers has been covered in the section *Notices for Implementer*.

## Normative References

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [Internet Standard RFC 768: User Datagram Protocol (UDP)l][RFC.UDP]
- [Internet Standard RFC: 791 Internet Protocol (IP)][RFC.UDP.IP]
- [Internet Standard RFC 1112 Host Extensions for IP Multicasting][RFC.IPMulticasting]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

## Introduction

### User Datagram Protocol

The User Datagram Protocol (UDP) is defined to make available a datagram mode of packet-switched computer communication in the environment of an interconnected set of computer networks. This protocol assumes that the [IP][RFC.UDP.IP] protocol is used as the underlying protocol.

This protocol provides a procedure for application programs to send `Message` datagrams to other programs with a minimum of protocol mechanism. Using this protocol the messages delivery and duplicate protection are not guaranteed.

The protocol is transparent for the user payload sent as the `data octets` (stream of bytes). The `Length` field in the protocol header contains the length in octets of the user datagram including the header and the `data octets`.

To identify the communication parties (processes) the following information is used:

- `Source Port`: - indicates the port of the sending process;
- `Destination Port`: indicates the port of the ultimate destination process;

`Source Port` and  `Destination Port` have a meaning only within the context of a particular internet address.

### OPC UA PubSub

The [OPC.UA.PubSub][OPC.UA.PubSub] offers the publish-subscribe communication pattern as an option to client-server pattern and is a consistent part of the OPC UA specifications suit. The detailed description of the [OPC.UA.PubSub][OPC.UA.PubSub] has been covered by the document [OPC Unified Architecture Part 14: PubSub Main Technology Features][README.PubSubMTF].

The specification defines the following actors:

- `Publisher`: pushes messages to an underlying transport` layer.
- `Subscriber`:  polls messages from the underlying transport layer.

The `Publisher` is the actor that pushes `NetworkMessage` structures to an underlying transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station, or a stock exchange. It may be also OPC UA Clients, OPC UA Servers or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

The `Subscriber` actors are the consumers of `NetworkMessage` structure, which are polled from the underlying transport layer. They may be OPC UA Clients, OPC UA Servers or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

According to the specification the `Publisher` and `Subscriber` don't have any subscriptions management functionality, namely, they follow a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client receives a message that it has never requested. The `Subscriber` must use a filtering mechanism to process only messages it is interested in.

Lack of subscriptions management functionality defined by the PubSub could be mitigated by applying the IP Multicast option defined by the [RFC 1112][RFC.IPMulticasting]. IP multicasting is the transmission of an IP datagram to a "host group", a set of zero or more hosts identified by a single IP destination address. A multicast datagram is delivered to all members of its destination host group with the same "best-efforts" reliability as regular unicast IP datagrams. Internetwork forwarding of IP multicast datagrams is handled by "multicast routers". It means that the router must be multicast enabled. Further discussion related to this topic is outside of the scope of this document.

## UDP Mapping

### General Requirements

There must be mapping defined for:

- Messages Transport: the PubSub `NetworkMessage` structure serialized as the UDP `Message` payload.
- Addressing: `Source Port`/`Destination Port` and appropriate Internet address must be provided by the PubSub upper communication layer.

A `Publisher` shall support all variations it allows through configuration. The required set of features is defined through profiles defined in [OPC UA Part 7: Profiles][OPC.UA.Profiles]. A `Subscriber` shall be able to process all possible `NetworkMessage` types and shall be able to skip information the `Subscriber` is not interested in. The `Subscriber` may not support all security policies.

### Messages Transport

The UADP `NetworkMessage` is sent as the UDP `data octets`. It is expected that the `Subscriber` process receiving the `NetworkMessage` can process it without needing to know that it was transported via UDP.

### Addressing

The syntax of the UDP transporting protocol URL has the following form:

> `opc.udp://<host>[:<port>]`

The host is either an IP address or a registered name like a host name or domain name. IP addresses can be unicast, multicast or broadcast addresses. It is the destination of the UDP datagram.

The IANA registered OPC UA port for UDP communication is `4840`. This is the default and recommended port for broadcast, multicast, and unicast communication. Alternative ports may be defined.

It is assumed that the IP multicasting is supported by the UDP protocol stock stack and the network infrastructure.

## Notices for Implementer

The article [Networking of SemanticData Library](README.MD#message-transport) contains description covering instruction for the external UDP handling components. An example how to implement the `Transport` layer for the UDP protocol is illustrated by the project [UA Data Example Application][OOI.Networking.ReferenceApplication]. This library implements the `IMessageHandlerFactory` in the class `MessageHandlerFactory` used to create communication infrastructure for the `Consumer` and `Producer` role as well.

The `JoinMulticastGroup` method subscribes the `UdpClient` to a multicast group using the specified `IPAddress`. After calling the `JoinMulticastGroup` method, the underlying `Socket` sends an Internet Group Management Protocol (IGMP) packet to the router requesting membership to the multicast group. The multicast address range is `224.0.0.0` to `239.255.255.255`. If you specify an address outside this range or if the router to which the request is made is not multicast enabled, `UdpClient` will throw a `SocketException`. Once the `UdpClient` is listed with the router as a member of the multicast group, it will be able to receive multicasted datagrams sent to the specified `IPAddress`.  `Publisher` do not need to belong to a multicast group to send datagrams to a multicast IP address. To get more details visit the MSDN online documentation.

[RFC.UDP]:https://tools.ietf.org/html/rfc768
[RFC.UDP.IP]:https://tools.ietf.org/html/rfc791
[RFC.IPMulticasting]:https://tools.ietf.org/html/rfc1112
[OPC.UA.PubSub]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[OPC.UA.Profiles]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-7-profiles/
[README.PubSubMTF]:../SemanticData/README.PubSubMTF.md
[OOI.Networking.UDPMessageHandler]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/UDPMessageHandler
[OOI.Networking.ReferenceApplication]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/ReferenceApplication
