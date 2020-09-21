# Reactive Networking of Semantic-Data Underlying Transport over UDP

## Motivation

The main goal of this document is to provide instruction on how to expand the transport layer for OOI Networking of the `Semantic-Data` over the User Datagram Protocol (UDP) to be compliant with the specifications mentioned in the section *Normative references*. Detailed description of this concept is covered by the document: [Reactive Networking of Semantic-Data Library][RxNetworkingSemantic-Data]

A reference implementation of the `Transport` over UDP is provided by the project [Networking.UDPMessageHandler][OOI.Networking.UDPMessageHandler]. Because intentionally the SDK is designed to use any transport protocol, a custom external component implementing UDP connectivity can be seamlessly integrated with this SDK using dependency injection concept.

## Normative References

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [Internet Standard RFC 768: User Datagram Protocol (UDP)l][RFC.UDP]
- [Internet Standard RFC: 791 Internet Protocol (IP)][RFC.UDP.IP]
- [Internet Standard RFC 1112 Host Extensions for IP Multicasting][RFC.IPMulticasting]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

## Notices for Implementer

The article [Reactive Networking of Semantic-Data Library][RxNetworkingSemantic-Data] contains a description covering instruction for the external UDP handling components. An example of how to implement the `Transport` layer for the UDP protocol is illustrated by the project [UA Data Example Application][OOI.Networking.ReferenceApplication]. This library implements the `IMessageHandlerFactory` in the class `MessageHandlerFactory` used to create communication infrastructure for the `Consumer` and `Producer` role as well.

The `JoinMulticastGroup` method subscribes the `UdpClient` to a multicast group using the specified `IPAddress`. After calling the `JoinMulticastGroup` method, the underlying `Socket` sends an Internet Group Management Protocol (IGMP) packet to the router requesting membership to the multicast group. The multicast address range is `224.0.0.0` to `239.255.255.255`. If you specify an address outside this range or if the router to which the request is made is not multicast enabled, `UdpClient` will throw a `SocketException`. Once the `UdpClient` is listed with the router as a member of the multicast group, it will be able to receive multicasted datagrams sent to the specified `IPAddress`.  `Publisher` do not need to belong to a multicast group to send datagrams to a multicast IP address. To get more details visit the MSDN online documentation.

[RxNetworkingSemantic-Data]:https://commsvr.gitbook.io/ooi/reactive-communication/semanticdata
[RFC.UDP]:https://tools.ietf.org/html/rfc768
[RFC.UDP.IP]:https://tools.ietf.org/html/rfc791
[RFC.IPMulticasting]:https://tools.ietf.org/html/rfc1112
[OPC.UA.PubSub]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[OOI.Networking.UDPMessageHandler]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/UDPMessageHandler
[OOI.Networking.ReferenceApplication]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/ReferenceApplication
