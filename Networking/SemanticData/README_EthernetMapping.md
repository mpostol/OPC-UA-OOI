# Underlying Transport over Ethernet (*PRELIMINARY*)

> **This article is under development and will be subject of further modification after collecting more feedback from software developers and OPC Foundation.**

The main goal of this document is to provide instruction on how to expand the transport layer for OOI Networking of the **Semantic Data** over the `Ethernet` to be compliant with the specifications mentioned in the section *Normative references*.

Implementation of the messages exchange over the [`Ethernet`][Ethernet] protocol is out of the scope of this project. The library intentionally is designed to use any transport protocol meeting some basic requirements using external components. External components implementing [`Ethernet`][Ethernet] connectivity can be seamlessly integrated with this SDK using dependency injection concept illustrated by the following domain model:

![Figure 1. Domain Model](../../CommonResources/Media/DataManagementExternalLibraries.png)

Instruction for implementer is covered in the section *Notices for Implementer*.

## Normative references

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [ISO/IEC 19464:2014: Advanced Message Queuing Protocol (AMQP) v1.0][ISO.AMQP]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

## Introduction

### Ethernet

> TBD - call for contributor.

#### 802.3

#### EtherType

#### Time Sensitive Networks (TSN)

#### VLAN

A VLAN represents a broadcast domain. VLANs are identified by a VLAN ID (a number between 0 – 4095). 

#### Priority Code Point

Priority Code Point (PCP) is a means of classifying and managing network traffic and of providing quality of service (QoS) in modern Layer 2 Ethernet networks. EEE 802.1p was specified by an IEEE 802.1 Task Group to address traffic prioritization for Quality of Service (QoS). 802.1p is not a separate IEEE 802.1 standard, but is defined in Annex G of the IEEE 802.1Q-2005 standard.

IEEE 802.1p defines a 3-bit field called the Priority Code Point (PCP) within an IEEE 802.1Q tag. The PCP value defines 8 priority levels, with 7 the highest priority and 1 the lowest priority. The priority level of 0 is the default. Each priority level defines a class of service that identifies separate traffic classes of transmitted packets.

For more information about this value

- [IEEE 802.1p Priority Levels](https://msdn.microsoft.com/ja-jp/library/hh451379(v=vs.85).aspx)
- [QoS Protocols section](https://technet.microsoft.com/en-us/library/cc728211(v=ws.10).aspx)

### OPC UA PubSub

The [OPC.UA.PubSub][OPC.UA.PubSub] offers the publish/subscribe communication pattern as an option to client-server pattern. The detailed description of the [OPC.UA.PubSub][OPC.UA.PubSub] has been covered by the document [OPC Unified Architecture Part 14: PubSub Main Technology Features][README.PubSubMTF].

Among others, the specification recognizes the following actors as the communication parties:

- **Publisher**: is the actor that pushes `NetworkMessage` structures to a selected AMQP **Node**.
- **Subscriber**: is the actor that consumes data encapsulated by the `NetworkMessage` structure, which is polled from the selected AMQP **Node**.

According to the specification the **Publisher** and **Subscriber** don't have any subscriptions management functionality, namely, they follow a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client may receive a message that it has never requested. The **Subscriber** must use a filtering mechanism to process only messages it is interested in.

Lack of subscriptions management functionality defined by the [OPC.UA.PubSub][OPC.UA.PubSub] could not be mitigated by applying the [`Ethernet`][Ethernet].

## Ethernet Mapping

OPC UA over Ethernet uses EtherType **`B62C`** that is used to transport UADP NetworkMessages directly as payload of the Ethernet frame without IP or UDP headers. To properly format the Ethernet frame the following parameters must be defined for the underlying transport protocol:

- **MAC address** - media access control address (MAC address) is a unique on the local area network (broadcast domain) identifier assigned to a network interface controller (NIC)
- **VLAN ID** - a number identifying the VLAN
- **Priority Code Point** - is a means of classifying and managing network traffic and of providing quality of service (QoS) in modern Layer 2 Ethernet networks.

The specification propose the following syntax to represent address information of the Ethernet transport protocol

`opc.eth://<host>[:<VID>[.PCP]]`

where:

- `host`- The host is a MAC address, an IP address or a registered name like a host name
- `VID` - is the VLAN ID as number.
- `PCP` - is the Priority Code Point as one digit number.

The format of a MAC address is six groups of hexadecimal digits, separated by hyphens (e.g. 01-23-45-67-89-ab). An application may also accept host names and/or IP addresses if it provides means to resolve it to a MAC address (e.g. DNS and Reverse-ARP).

> NOTE: the above mentioned addressing parameters are not mapped to any of the PubSub `UADP NetworkMessage` parameters. In other word there is not semantic relationship with the OPC UA and, therefore, the parameters must be provided separately by the application configuration.

The messages (`UADP NetworkMessage`) are transparently transported as the payload of the Ethernet frame. For OPC UA Ethernet the MaxNetworkMessageSize plus additional headers shall be limited to an Ethernet frame size of 1522 Byte

## See also

- [`IEEE 802.3`][Ethernet]
- [OPC Unified Architecture Part 14: PubSub Main Technology Features][README.PubSubMTF]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

[OPC.UA.PubSub]: https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[Ethernet]: https://en.wikipedia.org/wiki/IEEE_802.3
[README.PubSubMTF]:README.PubSubMTF.md





