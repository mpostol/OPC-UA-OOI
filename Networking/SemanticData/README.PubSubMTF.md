# OPC UA PubSub Main Technology Features

## Introduction

[OPC UA Part 14: PubSub][OPC.UA.PubSub] promotes interoperability of loosely coupled **PubSub Applications**. By design, they often will not even know each other. Their primary relationship is the shared understanding of:

- specific semantics of exchanged data,
- the syntax and semantics of messages that include these data, and
- common underlying messages transport layer.

In general speaking the following two distinct patterns are used to transfer data between communicating parties:

- Connection-oriented: requires a session that has to be established before any data can be sent between sender and receiver.
- Connectionless-oriented: the sender may start sending messages  (called packets or datagrams) to the destination without any preceding handshake procedure.

Each has its own advantages and disadvantages. In general, the OPC UA is a session centric communication. The session is established by the **OPC UA Clint** that must connect to the **OPC UA Server** before any data can be exchanged between them. In this Client/Server scenario defined by the Services in Part 4, the data flow is bidirectional over the session. The session entities communicate over a secure channel that is created in the underlying communication layer and relies upon it for secure communication. It enables to log-in using user authentication and authorization. More details you can find in the article:

- [OPC Unified Architecture – Main Technological Features][wordpress.OPCUAMTF]

Using the connection-oriented communication pattern it is difficult or even impossible to gather and process data from mobile things (e.g. smart devices, cigarettes box, drug blister, etc.), which is one of the **Internet of Things** paradigms. More details you can find in [IoT versus SCADA/DCS Data Acquisition Patterns][wordpress.IoTVersus].

> The [OPC.UA.PubSub][OPC.UA.PubSub] specification offers the connectionless approach as an additional option to session based client-server interoperability and is a consistent part of the OPC UA specifications suit.
> **As the result it can be recognized as the IoT ready technology.**

The specification claims that the PubSub integrates into the existing OPC UA technology but as result of applying the connectionless communication it is easier to implement low power and low-latency communications on local networks. Additionally, the specification states that PubSub is based on the **[OPC UA Information Model][CAS.OPCUAIMD]** with the aim of seamless integration into **OPC UA Servers** and **OPC UA Clients**. Nevertheless, the PubSub communication does not require such a role dependency, i.e there is no necessity for **Publisher** or **Subscriber** to be either an **OPC UA Server** or an **OPC UA Client** to participate in the communication.

> Note: Unfortunately,**OPC UA Information Model** is not used to promote **PubSub Applications** interoperability. This concept is only employed to define **Security Key Service** and **Configuration Service** models, which have an only indirect impact on the **PubSub Applications** interoperability.

## Services

**PubSub Applications** exchange messages formatted as the `NetworkMessage` structure using underlying transport layer. As illustrated in the following domain model,  directly or indirectly the specification defines the following actors:

- **Publisher**: pushes the current process data formatted as the `NetworkMessage` structure to an underlying transport layer.
- **Subscriber**: consumes the process data, which is recovered from the `NetworkMessages` structure polled from the underlying transport layer.
- **Security Key Management** - provides security keys that can be used to sign and encrypt messages.
- **Configuration Management** - an external application used to remotely configure **PubSub Application**.

![Class Diagram of Concrete Containers and Nodes](../../CommonResources/Media/PubSubMainComponents.png)

The **Publisher** is the actor that pushes `NetworkMessage` structures to an underlying transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station or a stock exchange. It may be also **OPC UA Client**, **OPC UA Server** or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

The **Subscriber** actors are the consumers of `NetworkMessage` structures, which are polled from the underlying transport layer. They may be **OPC UA Client**, **OPC UA Server** or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

To interchange the process data **Publisher** and all associated **Subscribers** depends on a common **Distribution Channel**. **Distribution Channel** models common knowledge necessary to use a common underlying messages transport layer, i.e. common underlying messaging transport layer and relevant parameters to route the messages over it.

A **Security Key Management** provides keys for message security that can be used by the **Publisher** to sign and encrypt `NetworkMessages` and by the **Subscriber** to verify the signature of and decrypt the `NetworkMessages`. The specification defines OPC UA Information model for **Security Key Services** that enables to implement this class as the **OPC UA Server** or **OPC UA Client**.

**Publishers** and **Subscribers** may be configurable through vendor-specific engineering tools or using the dedicated configuration **OPC UA Information Model** described in this standard. This model allows a standard **OPC UA Client** based configuration tool to configure a **PubSub Application** connecting to the embedded **OPC UA Server**. Using remote **Configuration Tool** over an **OPC UA Session** does not determine how dynamic the configuration can be.

> It is worth stressing that the configuration model doesn't provide any definition dedicated to being used for the data bindings configuration.

## Interoperability

## Preface

The **PubSub Applications** are decoupled by exchanging messages over an underlying transport layer. It is worth stressing that by design the **PubSub Application** don't expose any API that can be used to transfer upper layer data over the network, i.e. it is not communication layer. It means that these applications must produce and/or consume the process data.

## Transport protocol mapping

**PubSub Applications** interoperability doesn't depend on any functionality provided by the underlying transport layer. According to the specification, the **Subscriber** and **Publisher** can be interconnected using any transparent messages transport infrastructure. It defines two groups of solutions:

- A broker-less: the transport layer is the network infrastructure that is able to route datagram-based messages, e.g [UDP][RFC.UDP], [AMQP][AMQP], ETHERNET.
- A broker-based: the core component of the transport layer is a message broker, e.g. [AMQP][AMQP] or [MQTT][MQTT].

In both cases, the one-to-many relationship between **Publisher** and **Subscriber** can be obtained. For UDP multicast messages distribution may be applied to send Internet Protocol (IP) datagrams to a group of interested receivers in a single transmission. For the broker-based transport, all messages are published to specific queues (e.g. topics, nodes) that the broker exposes and **Subscribers** can listen to these queues.

Additionally, the specification lists the following protocols that can be selected as the transport for massages and their possible combinations with message mappings:

- OPC UA UDP
- OPC UA Ethernet
- AMQP
- MQTT

Because the specification doesn't define normative references for `OPC UA UDP` and `OPC UA Ethernet` in section *References* they are inferred from the context. Based on this mapping in the figure below the architecture of protocol stack is determined as the domain diagram.  The diagram has been worked out on the best effort approach.

![Transport protocols architecture](../../CommonResources/Media/Networking/StackDomainModel.png)

Following the specification, the transport protocol mapping is modeled appropriately as the four top-level classes called `Ethernet`, `UDP`, `MQTT`, `AMQP`. They may be recognized as the underlying API of the protocols stack and are aggregated into one common communication layer used to exchange the messages over the network (section ...).

Here it must be stressed that the mentioned in the section title term `transport layer` has nothing in common with the Open System Interconnection Reference Model (OSI model) Transport Layer. Referring to the OSI model the  `MQTT` and `AMQP` protocols should be recognized as the Application Layer protocols. The Application Layer is the one at the top of the model. Because the PubSub specification defines also the protocol on this layer some functionality is redundant in this case. For traversing the network the messages the PubSub application uses both protocols as a transparent communication service. Applying the broker-based approach also means that some functionality related to communication reliability, data selection, and distribution is delegated to them. Details related to MQTT mapping are covered by the section ..... Details related to AMQP mapping are covered by the section .......

The OSI Presentation Layer represents the services that are responsible for the translation of the application data encoding to network encoding, and translation back from the network encoding to application encoding. In other words, the layer “presents” data for the application or the network. This functionality is embedded in the definition of the PubSub message syntax rules. This syntax rules (Part 6) are common for the OPC UA suit as one whole. For the sake of simplicity, the `OSI Presentation Layer` is not present in the diagram.

In the published/subscriber communication pattern the `OSI Session Layer` is empty, so it is ignored in the domain model presented in figure above.

The specification doesn't define particular mapping rules referring to protocol stack used by the `AMQP` and `MQTT`, so an abstract `OSI Transport Layer` is used as the underlying communication layer for them. In this case, all requirements against relevant specifications apply.

on the other hand, according to the mapping rules the User Datagram Protocol (UDP) protocol is pointed out by the PubSub specification as the only concrete implementation of the `OSI Transport Layer`. In this case, the protocol can be recognized as the base for the mapping rules `UDP` stated by the specification and a part of the abstract `OSI Transport Layer`.

The specification doesn't define any subscription management services, namely, it offers a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client receives a message that it has never requested. Using broker-less approach the **Subscriber** must use a filtering mechanism to process only messages it is interested in.

In case the broker-less approach over the UDP is selected for communication some multicast functionality must be offered by the protocol stack. UDP is one-to-one connectionless protocol and cannot be used for this purpose. The specification recommends using `Internet Protocol (IP)` multicast option to fulfill this requirement. Formally there are no additional mapping rules defined for this protocol, but as a result, this concrete protocol has been selected as the base for `UDP` protocol and is embedded part of the `OSI Protocol Layer`.

This approach has some drawbacks. Using IP multicast special equipment and dedicated configuration of that equipment are required.  Both make this solution applicable only for the local network segments in the administration realm of the protocol users. It is hard to imagine the usage of this communication option in case of enterprise scoped networks. Detailed description of the `UDP` mapping rules are covered by the section.....

I guess that the removal of the UDP and IP protocols from the communication stack is recognized by the specification authors as a mean to improve the performance of the communication. As a result `Ethernet` mapping rules have been defined (see figure above). The ETHERNET term is recognized as a keyword with a very broad meaning. The specification doesn't define normative reference in this respect. In the figure above it is presented as a concrete implementation compliant with the IEEE 802.3 standard suit. In cas the UDP protocol is removed form the stack to replace the application selection functionality offered by the socket concept the registered B62C EtherType is recommended.

Further communication performance improvement and extension of the functionality may be obtained by applying for example implementation of the :

- Time-Sensitive Network (TSN)
- Virtual Local Network (VLAN)
- Quality of Service QoS)

They are only partially mentioned in the specification but should be recognized as the embedded part of the 802.3 implementations. In any case, these solutions are invisible for the implementation of the communication layers above 802.3, so they are invisible for upper layers and doesn't have any impact on the PubSub interoperability. It is also worth stressing that these solutions can be used in spite of the above communication stack selection. In other words, the mentioned solutions are not dedicated to OPC UA at all.

## Message Mapping

The syntax and semantics of the messages exchanged between the **PubSub Application** are described as the `NetworkMessage` structure. Each `NetworkMessage` includes header information (e.g. identification and security data) and one or more `DataSetMessages`. The `DataSetMessages` may be signed and encrypted in accordance with the configured message security. Each `DataSetMessage` contains process data.

The `NetworkMessage` structure can be serialized using the following encoding:

- UADP: optimized binary encoding.
- JSON: text format as defined in [RFC JSON][RFC.JSON].

Using UADP message mapping the following encodings may be supported:

- `Variant`: the Variant can contain a `StatusCode` instead of the expected `DataType` if the status of the field is `Bad`. The `Variant` can contain a `DataValue` with the value and the statusCode if the status of the field is `Uncertain`.

- RawData: the data fields are encoded in the `DataTypes` specified in the DataSetMetaData for the DataSet. The encoding is handled like a Structure DataType where the DataSet fields are handled like Structure fields and fields with Structure DataType are handled like nested structures. All restrictions for the encoding of Structure DataTypes also apply to the RawData Field Encoding.

- DataValue: the DataSet fields are encoded as DataValue. This option is set if the DataSet is configured to send more than the Value.

This specification lists the following protocols that have been selected to be used as the underlying transport layer:

- [Advanced Message Queuing Protocol][AMQP]
- [Message Queuing Telemetry Transport][MQTT]
- [User Datagram Protocol][RFC.UDP]
- Ethernet defined by IEEE 802.3

## Normative References

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

## Glossary

- Publisher-subscriber communication pattern

> Publish-subscribe is a messages distribution scenario where senders of messages, called publishers, do not send them directly to specific receivers, called subscribers, but instead categorize published messages into classes without knowledge of which subscribers if any, there may be. Similarly, subscribers express interest in one or more classes and only receive messages that are of interest, without knowledge of which publishers, if any, there are. In the publish-subscribe model, subscribers typically receive only a subset of the total messages published. The process of selecting messages for reception and processing is called filtering. There are two common forms of filtering: topic-based and content-based.

- Connection-oriented communication pattern

> Data exchange scenario that requires a session connection be established before any data can be sent. Connection-oriented services set up virtual links between applications through a network. The session is responsible to retain a state information or status about each communicating partner for the duration of multiple requests. An OPC UA Client/Server connection is a stateful connection because both systems maintain information about the session itself during its life.

- Connectionless-oriented communication pattern

> Messages exchange scenario that does not require a session connection between sender and receiver. The sender simply starts sending packets (called datagrams) to the destination. Neither system must maintain state information for the systems that they send messages to or receive messages from.

## References

- [OPC Unified Architecture][wordpress.opc-ua]

[wordpress.opc-ua]: https://mpostol.wordpress.com/opc-ua/

- [IoT versus SCADA/DCS Data Acquisition Patterns][wordpress.IoTVersus]

[wordpress.IoTVersus]: https://mpostol.wordpress.com/2017/09/19/iot-versus-scadadcs/

- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]

[wordpress.OPCUACD]: https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/

- [OPC Unified Architecture – Main Technological Features][wordpress.OPCUAMTF]

[wordpress.OPCUAMTF]: https://mpostol.wordpress.com/2013/08/04/opc-unified-architecture-main-technological-features/

- [OPC UA Information Model Deployment][CAS.OPCUAIMD]

[CAS.OPCUAIMD]: http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/20140301E_DeploymentInformationModel.pdf

- [RFC 768: User Datagram ProtocolJ, August 1980][RFC.UDP]

[RFC.UDP]:https://tools.ietf.org/html/rfc768

- [RFC: 791 INTERNET PROTOCOL, September 1981][RFC.UDP.IP]

[RFC.UDP.IP]:https://tools.ietf.org/html/rfc791

- [RFC 8259: The JavaScript Object Notation (JSON) Data Interchange Format][RFC.JSON]

[RFC.JSON]:https://tools.ietf.org/html/rfc8259

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

[OPC.UA.PubSub]: https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/

- [OPC Unified Architecture Specification  Part 7: Profiles Release 1.04 November 1, 2017][OPC.UA.Profiles]

[OPC.UA.Profiles]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-7-profiles/

- [OASIS MQTT Version 3.1.1 specification][MQTT]
  
[MQTT]:http://docs.oasis-open.org/mqtt/mqtt/v3.1.1/mqtt-v3.1.1.html

- [OASIS Advanced Message Queuing Protocol (AMQP) Version 1.0][AMQP]

[AMQP]:http://docs.oasis-open.org/amqp/core/v1.0/os/amqp-core-overview-v1.0-os.html

- [OOI.Networking.UDPMessageHandler][OOI.Networking.UDPMessageHandler]

[OOI.Networking.UDPMessageHandler]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/UDPMessageHandler

- [OOI.Networking.ReferenceApplication][OOI.Networking.ReferenceApplication]

[OOI.Networking.ReferenceApplication]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/ReferenceApplication
