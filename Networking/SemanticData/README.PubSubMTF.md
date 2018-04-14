
# OPC UA PubSub Main Technology Features

# Introduction

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

The specification claims that the PubSub integrates into the existing OPC UA technology but as result of applying the connectionless communication it is easier to implement low power and low-latency communications on local networks. Additionally, the specification states that PubSub is based on the **[OPC UA Information Model][CAS.OPCUAIMD]** with the aim of seamless integration into **OPC UA Servers** and **OPC UA Clients**. Unfortunately,**OPC UA Information Model** is not used to promote **PubSub Applications** interoperability. This concept is only employed to define **Security Key Service** and **Configuration Service** models, which have an only indirect impact on the **PubSub Applications** interoperability. Nevertheless, the PubSub communication does not require such a role dependency, i.e there is no necessity for **Publisher** or **Subscriber** to be either an **OPC UA Server** or an **OPC UA Client** to participate in the communication.

# Services

**PubSub Applications** exchange messages formatted as the `NetworkMessage` structure using underlying transport layer. Directly or indirectly the specification defines the following actors:

* **Publisher**: pushes the current process data formatted as the `NetworkMessage` structure to an underlying transport layer.
* **Subscriber**: consumes the process data, which is recovered from the `NetworkMessages` structure polled from the underlying transport layer.
* **Security Key Service** - provides security keys that can be used to sign and encrypt messages.
* **Configuration Tool** - an external application used to remotely configure **PubSub Application**.

The **Publisher** is the actor that pushes `NetworkMessage` structures to an underlying transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station or a stock exchange. It may be also **OPC UA Client**, **OPC UA Server** or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

The **Subscriber** actors are the consumers of `NetworkMessage` structures, which are polled from the underlying transport layer. They may be **OPC UA Client**, **OPC UA Server** or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

A **Security Key Service (SKS)** provides keys for message security that can be used by the
**Publisher** to sign and encrypt `NetworkMessages` and by the **Subscriber** to verify the signature of and decrypt the `NetworkMessages`.

**Publishers** and **Subscribers** may be configurable through vendor-specific engineering tools or using the dedicated configuration **OPC UA Information Model** described in this standard. This model allows a standard **OPC UA Client** based configuration tool to configure a **PubSub Application** connecting to the embedded **OPC UA Server**. Using remote **Configuration Tool** over an **OPC UA Session** does not determine how dynamic the configuration can be. It is worth stressing that the configuration model doesn't provide any definition dedicated to being used for the data bindings configuration.

# Interoperability

The **PubSub Applications** are decoupled by exchanging messages over an underlying transport layer. It is worth stressing that by design the **PubSub Application** don't expose any API that can be used to transfer upper layer data over the network, i.e. it is not communication layer. It means that these applications must produce and/or consume the process data.

**PubSub Applications** interoperability doesn't depend on any functionality provided by the underlying transport layer. According to the specification, the **Subscriber** and **Publisher** can be interconnected using any transparent messages transport infrastructure. It defines two groups of solutions:

- A broker-less: the transport layer is the network infrastructure that is able to route datagram-based messages, e.g [UDP][RFC.UDP], ETHERNET.
- A broker-based: the core component of the transport layer is a message broker, e.g. [AMQP][AMQP] or [MQTT][MQTT].

In both cases, one-to-many relationship between **Publisher** and **Subscriber** can be obtained. For UDP multicast messages distribution may be applied to send Internet Protocol (IP) datagrams to a group of interested receivers in a single transmission. For the broker-based transport, all messages are published to specific queues (e.g. topics, nodes) that the broker exposes and **Subscribers** can listen to these queues.

The specification doesn't define any subscription management services, namely, it offers a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client receives a message that it has never requested. Using broker-less approach the **Subscriber** must use a filtering mechanism to process only messages it is interested in. Applying broker-based approach means that some functionality related to communication reliability, data selection, and distribution is delegated to it.

The syntax and semantics of the messages exchanged between the **PubSub Application** are described as the `NetworkMessage` structure. Each `NetworkMessage` includes header information (e.g. identification and security data) and one or more `DataSetMessages`. The `DataSetMessages` may be signed and encrypted in accordance with the configured message security. Each `DataSetMessage` contains process data. 

The `NetworkMessage` structure can be serialized using the following encoding:

- UADP: optimized binary encoding.
- JSON: text format as defined in [RFC JSON][RFC.JSON].

This specification lists the following protocols that have been selected to be used as the underlying transport layer:

- [Advanced Message Queuing Protocol][AMQP]
- [Message Queuing Telemetry Transport ][MQTT]
- [User Datagram Protocol][RFC.UDP]
- Ethernet defined by IEEE 802.3

# Normative References

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

# Glossary

- **PubSub Applications**
 
> Any instance of the software program that conforms to the PubSub specification.
 
- Publisher-subscriber communication pattern
 
> Publish-subscribe is a messages distribution scenario where senders of messages, called publishers, do not send them directly to specific receivers, called subscribers, but instead categorize published messages into classes without knowledge of which subscribers if any, there may be. Similarly, subscribers express interest in one or more classes and only receive messages that are of interest, without knowledge of which publishers, if any, there are. In the publish-subscribe model, subscribers typically receive only a subset of the total messages published. The process of selecting messages for reception and processing is called filtering. There are two common forms of filtering: topic-based and content-based.

- Connection-oriented communication pattern

> Data exchange scenario that requires a session connection be established before any data can be sent. Connection-oriented services set up virtual links between applications through a network. The session is responsible to retain a state information or status about each communicating partner for the duration of multiple requests. An OPC UA Client/Server connection is a stateful connection because both systems maintain information about the session itself during its life.

- Connectionless-oriented communication pattern
> Messages exchange scenario that does not require a session connection between sender and receiver. The sender simply starts sending packets (called datagrams) to the destination. Neither system must maintain state information for the systems that they send messages to or receive messages from.


# References

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
