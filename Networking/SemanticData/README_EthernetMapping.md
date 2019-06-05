# Underlying Transport over Ethernet (*PRELIMINARY*)

> **This article is under development and will be subject of further modification after collecting more feedback from software developers and OPC Foundation.**

The main goal of this document is to provide instruction on how to expand the transport protocol stack for OOI Networking of the **Semantic Data** over the `Ethernet` to be compliant with the specifications mentioned in the section *Normative references*.

Implementation of the messages exchange over the [`Ethernet`][Ethernet] protocol is outside of the scope of this project. The library intentionally is designed to use any transport protocol meeting some basic requirements using external components. External components implementing [`Ethernet`][Ethernet] connectivity can be seamlessly integrated with this SDK using dependency injection concept illustrated by the following domain model:

![Figure 1. Domain Model](../../CommonResources/Media/DataManagementExternalLibraries.png)

Instruction for implementer is covered in the section *Notices for Implementer*.

## Normative references

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [`IEEE 802.3`][Ethernet]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

## OPC UA Ethernet

In [Part 14 PubSub][OPC.UA.PubSub] the term **OPC UA Ethernet** is defined as a simple Ethernet-based protocol using `EtherType` `B62C` that is used to transport `UADP` `NetworkMessage` structures as the payload of the Ethernet II frame without IP or UDP headers. Fortunately, the specification doesn't really define any new protocol based on the Ethernet, but only mapping to an existing one. Because both terms `UADP` encoding and `NetworkMessage` data structure are not defined by the OPC UA core standard the prefix OPC UA before Ethernet is meaningless and confusing. The Ethernet term has a very broad meaning, and unfortunately, the specification doesn't provide also any normative references in this respect. Hence, the rest of this section is prepared on the assumption that the meaning has been inferred correctly from the specification editors intention.

Ethernet term is well known as a common name of a set of the IEEE 802.3 standards for Ethernet networks developed by the [IEEE 802.3 Working Group][Ethernet]. The most promising starting point for furthe investigation seems to be the document [802.3-2018 - IEEE Standard for Ethernet][8023]. From this document, we can learn that 802.3 (Ethernet) is a concrete protocol located at `OSI Data Link Layer` (section [*OPC Unified Architecture Part 14: PubSub Main Technology Features*][README.PubSubMTF]).

More about layering can be found in the document [802-2014 - IEEE Standard for Local and Metropolitan Area Networks: Overview and Architecture][802]. Next section covers the most important features in context of the PubSub mapping and related directly or indirectly to the 802.3 (Ethernet).

### IEEE 802 - Local and Metropolitan Area Networks

From the [IEEE 802][[802]] specification we can learn that LAN is a peer-to-peer communication network that enables stations to communicate directly on a point-to-point, or point-to-multipoint, basis without requiring them to communicate with any intermediate stations that perform forwarding or filtering. This is in contrast to wide area networks (WANs) that interconnect communication parties in different parts of a country or are used as a public utility.

A LAN is generally owned, used, and operated by a single organization (management realm).

IEEE 802 networks can also be used to perform the task of an access network, i.e., to connect end stations to a larger, heterogeneous network, e.g., the Internet.

In order to provide a balance between the proliferation of a very large number of different and incompatible local and metropolitan networks, on the one hand, and the need to accommodate rapidly changing technology and to satisfy certain applications or cost goals, on the other hand, several types of medium access technologies are currently specified in the family of IEEE 802 standards.

#### Reference Model

The IEEE 802 Reference Model (RM) is derived from the Open Systems Interconnection basic reference model (OSI/RM), ISO/IEC 7498-1:1994. The IEEE 802 standards emphasize the functionality of the lowest two layers of the OSI/RM, i.e., `OSI Physical Layer` (PHY) and `OSI Data Link Layer` (DLL). The IEEE 802 Reference Model is similar to the OSI/RM in terms of its layers and the placement of its service boundaries. These map onto the same two layers in the IEEE 802 RM. 

For the data services supported by all IEEE 802 networks, the `Data Link Layer` is structured as follows

- Logical Link Control (LLC) 
- Medium Access Control (MAC)

The MAC sublayer of the IEEE 802 RM exists between the PHY and the LLC sublayer to provide a service for the LLC sublayer.

#### Upper Layer Protocol discrimination

To allow multiple network layer protocols to coexist above the `OSI Data Link Layer` the dedicated methods are provided for the following:

—The coexistence of multiple network layer protocols
—The migration of existing networks to future standard protocols
—The accommodation of future higher layer protocols

Within a given layer, entities can exchange data by a mutually agreed upon protocol mechanism. A pair of entities that do not support a common protocol cannot communicate with each other. For multiple protocols to coexist within a layer, it is necessary to determine which protocol is to be invoked to process a service data unit delivered by the lower layer. Various network and higher layer protocols have been reserved LPD addresses or EtherTypes. These addresses permit multiple protocols to coexist at a single MAC station. The higher layer protocol discrimination entity (HLPDE) is used to determine the higher layer protocol to which to deliver a protocol data unit (PDU). Two methods may be used in the HLPDE as follows:

- `EtherType` protocol discrimination (EPD), which uses the `EtherType`
- LLC protocol discrimination (LPD), which uses the addresses defined in ISO/IEC 8802-2, including the Subnetwork Access Protocol (SNAP) format

The `EtherType` is a 2-octet value, assigned by the IEEE Registration Authority (RA), that provides context for interpretation of a data field of a frame (protocol identification). IEEE Std 802.3 is capable of natively representing the EtherType within its MAC frame format, which is used to support EPD. IEEE Std 802.3 also natively supports ISO/IEC 8802-2 LPD (over a limited range of frame sizes).

Examples of EtherTypes are 0x0800 and 0x8DD, which are used to identify IPv4 and IPv6, respectively. 

A detailed description of the EPD is covered by the specification [IEEE 802][802]. More information on EtherTypes can also be found on the [IEEE RA web site][IEEERA].

> **Note**: EtherType **0xB62C** is recommended by the [OPC UA PubSub][OPC.UA.PubSub] to identify Ethernet mapping forPubSub protocol. Unfortunately, at the date of this document publication, this EtherType number is not listed as an entry in the official registry available here [IEEE 802 Numbers][802Numbers]. This number is classified by the IEEE as the OUI Extended EtherType. 

[IEEE 802][802] defines three assigned EtherType groups of vendor-specific protocol identifier:

Name|Value
-|-
Local Experimental EtherType 1|88-B5
Local Experimental EtherType 2|88-B6
OUI Extended EtherType|88-B7

The vendor-specific protocol identifier is a means whereby protocol developers may assign permanent protocol identifier values without consuming type values from the globally available limited resource. This can be useful for **prototype, experimental, and private/proprietary protocols** to be developed without impacting the global EtherType namespace. The OUI Extended `EtherType` allows an organization to apply protocol identifiers using SNAP. An organization allocates protocol identifiers to its own protocols in a manner that ensures that the protocol identifier is globally unique. SNAP provides a method for multiplexing and demultiplexing of private and public protocols among multiple users of the LLC sublayer. An organization that has an OUI assigned to it may use its OUI to assign universally unique protocol identifiers to its own protocols, for use in the protocol identification field of SNAP data units.

> **NOTE**: to use EPD the PubSub cannot directly access Ethernet PDU as it is required in the specification because these this services are provided by the MAC sublayer.

#### Virtual Bridged Network

Additionally, the [IEEE 802.1Q][8021Q] introduces a concept of Virtual Bridged Network. VLANs and their identifiers (VID) provide a convenient and consistent network-wide reference for VLAN Bridges. A VLAN represents a broadcast domain. VLANs are identified by a VLAN ID (a number between 0 – 4095). Portions of the network which are VLAN-aware (i.e., IEEE 802.1Q conformant) can include VLAN tags. When a frame enters the VLAN-aware portion of the network, a tag is added to represent the VLAN membership. Each frame must be distinguishable as being within exactly one VLAN. 802.1Q adds a 32-bit field between the source MAC address and the EtherType fields of the original frame.


Each tag comprises the following sequential information elements:
- A Tag Protocol Identifier
- Tag Control Information (TCI) that is dependent on the tag type
- Additional information, if and as required by the tag type and TCI.

The VLAN TCI field is two octets in length and encodes the VID and priority parameters. The VID is encoded in a 12-bit field. 

#### Priority Code Point 

This specification IEEE 802.1Q defines also the Priority Code Point  (PCP) parameter mentioned in the PubSub Ethernet mapping specification. The priority is encoded in the PCP field of the VLAN tag. Priority Code Point (PCP) by design is a means of classifying and managing network traffic and of providing quality of service (QoS). The PCP value defines 8 priority levels. The specification 802.1Q states that for each Port, the Priority Code Point Encoding Table has 16 entries, corresponding to each of the possible combinations of the eight possible values of priority (0 through 7) with the two possible values of drop_eligible (True or False). For each Port, the Priority Code Point Decoding Table has 8 entries, corresponding to each of the possible PCP values.

It is worth stressing that the above concepts are not defined in the context of Ethernet but have more general applicability.

### Medium Access Control

The MAC sublayer performs the functions necessary to provide frame-based, connectionless-modeﾠ(datagram style) data transfer between stations in support of the next higher sublayer.

One of the functions provided by this sublayer is addressing. The IEEE 802 defines a concept of universal addressing that is based on the idea that all potential members of a network need to have a unique identifier. The advantage of a universal address is that a station with such a MAC address can be attached to any IEEE 802 network in the world with an assurance that the MAC address is unique. The term MAC address is used to refer to a 48-bit or 64-bit number that is used to identify the source and destination MAC entities. A universal address is a MAC address that is globally unique. If interoperability through bridges is required, then 48-bit MAC addressing is required. The selected address bit (called I/G bit) of the MAC address is used to identify the destination MAC address as an individual MAC address or a group MAC address.

Unfortunately, the PubSub specification doesn't define mapping related to addressing and especially related to supporting point-to-multipoint architecture required to promote reusability of the same data published by one **PubSub Application** and consumed by many others.

### Time Sensitive Network

This concept is not mentioned within the PubSub specification but is very popular to point out a strategy of further development of the OPC UA targeting real-time applications. 

> **Note 1**: In computer science, the real-time application describes hardware and software systems subject to a "real-time constraint". Real-time programs must guarantee the expected time relationship between the selected events and outcomes of the data processing process.  In other words, deploying the real-time application, time must be considered as an important affecting the application correctness. Conversely, increasing processing or communication speed is not always required for the real-time application correctness. A very good example is Voice over IP where very hard time constraints required to correctly replay the sound can be meat using nondeterministic communication over the Internet. Another example where we have time constraints but the overall speed of processing engine is usually irrelevant is any thermal process.

### Ethernet

Ethernet is defined in the [IEEE 802][802] as a communication protocol specified by [802.3-2018 - IEEE Standard for Ethernet][8023]. The IEEE Std 802.3-2018 is composed of the documents defining a variety of protocols for Local and Metropolitan Area Networks (LANs and MANs), employing CSMA/CD as the shared media access method and the IEEE 802.3 (Ethernet) protocol and frame format for data communication. This international standard is intended to encompass a variety of media types and techniques for a variety of MAC data rates.

The Carrier Sense Multiple Access with Collision Detection (CSMA/CD) MAC protocol specifies shared medium (half duplex) operation, as well as full duplex operation. Speed specific Media Independent Interfaces (MIIs) provide an architectural and optional implementation interface to selected Physical Layer entities (PHY). The Physical Layer encodes frames for transmission and decodes received frames with the modulation specified for the speed of operation, transmission medium and supported link length. 

A companion document IEEE Std 802.3.1 describes Ethernet management information base (MIB) modules for use with the Simple Network Management Protocol (SNMP). IEEE Std 802.3.1 is updated to add management capability for enhancements to IEEE Std 802.3. 

It must be assumed that the IEEE Std 802.3 will continue to evolve. A more detailed description of this protocols family is irrelevant for further discussion. To get more information visit the specification document [IEEE 802.3][8023].

### OPC UA PubSub

By design the [OPC.UA.PubSub][OPC.UA.PubSub] should offer the publish/subscribe communication pattern as an option to client-server pattern. The detailed description of the [OPC UA PubSub][OPC.UA.PubSub] has been covered by the section [OPC Unified Architecture Part 14: PubSub Main Technology Features][README.PubSubMTF].

Among others, the specification recognizes the following actors as the communication parties:

- `Publisher`: is the actor that pushes `NetworkMessage` structures to the local area network - Ethernet protocol data unit in this case
- `Subscriber`: is the actor that consumes data encapsulated by the `NetworkMessage` structure, which is polled from the local area network - Ethernet protocol data unit in this case

According to the specification the `Publisher` and `Subscriber` don't have any subscriptions management functionality, namely, they follow a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client may receive a message that it has never requested. The `Subscriber` must use a filtering mechanism to process only messages it is interested in.

Lack of subscriptions management functionality defined by the [OPC.UA.PubSub][OPC.UA.PubSub] could not be mitigated by applying the Ethernet Mapping.

## Ethernet Mapping

According to the OPC UA over Ethernet mapping uses EtherType **`B62C`** that is used to transport UADP `NetworkMessages` directly as payload of the Ethernet frame without IP or UDP headers. To properly format the Ethernet frame the following parameters must be defined somehow:

- **MAC address** - media access control address (MAC address) is a unique on the local area network (broadcast domain) identifier assigned to a network interface controller (NIC)
- **VLAN ID** - a number identifying the VLAN
- **Priority Code Point** - is a means of classifying and managing network traffic and of providing quality of service (QoS) in modern Layer 2 Ethernet networks.

The specification propose the following syntax to represent address information of the `Ethernet` transport protocol

`opc.eth://<host>[:<VID>[.PCP]]`

where:

- `host`- The host is a MAC address, an IP address or a registered name like a host name
- `VID` - is the VLAN ID as number.
- `PCP` - is the Priority Code Point as one digit number.

The format of a MAC address is six groups of hexadecimal digits, separated by hyphens (e.g. 01-23-45-67-89-ab). An application may also accept host names and/or IP addresses if it provides means to resolve it to a MAC address (e.g. DNS and Reverse-ARP).

> NOTE: the above mentioned addressing parameters are not mapped to any of the PubSub `NetworkMessage` parameters. In other word there is no semantic relationship with the OPC UA and, therefore, the parameters must be provided separately by the application configuration.

According to this mapping, the `UADP NetworkMessage` structures are transparently transported as the payload of the Ethernet frame. For OPC UA Ethernet the MaxNetworkMessageSize plus additional headers shall be limited to an Ethernet frame size of 1522 Bytes.


## Conclusion

- OPC UA and Ethernet are unrelated, i.e. there is no semantic relationship between both
- OPC UA Pub/Sun is recognized as an Internet technology, but an Ethernet mapping only makes sense for a VLAN constrained broadcast domain (local network segment)
- OPC UA PubSub over the TSN is misleading term because each protocol can be transported over this particular Ethernet dialect
- Time Sensitive Network doesn’t mean real time – it means no jitter (improves deterministic communication)

## See also

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]
- [OPC Unified Architecture Part 14: PubSub Main Technology Features][README.PubSubMTF]
- [IEEE 802.3 ETHERNET WORKING GROUP][Ethernet]
- [802-2014 - IEEE Standard for Local and Metropolitan Area Networks: Overview and Architecture][802]
- [Registration Authority - Ethertype][IEEERA]
- [IEEE 802 Numbers][802Numbers]
- [802.3-2018 - IEEE Standard for Ethernet][8023]
- [802.1Q-2014 - IEEE Standard for Local and metropolitan area networks--Bridges and Bridged Networks, DOI: 10.1109/IEEESTD.2014.6991462][8021Q]

[8021Q]:https://ieeexplore.ieee.org/servlet/opac?punumber=6991460
[8023]: https://ieeexplore.ieee.org/document/8457469
[802Numbers]: https://www.iana.org/assignments/ieee-802-numbers/ieee-802-numbers.xhtml
[IEEERA]:http://standards.ieee.org/develop/regauth/ethertype/
[Ethernet]:http://www.ieee802.org/3/
[README.PubSubMTF]:README.PubSubMTF.md
[OPC.UA.PubSub]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[802]:https://ieeexplore.ieee.org/document/6847097
