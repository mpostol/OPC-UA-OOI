
# OPC UA PubSub Main Technology Features

# Introduction

Two distinct patterns are used to transfer data between communication parties:

- Connection-oriented - requires a session that has to be established before any data can be sent between sender and receiver.
- Connectionless - the sender may start sending packets (called datagrams) to the destination without any preceding handshake procedure.

Each has its own advantages and disadvantages. In general, the OPC UA is a session centric communication. The session is established by the **OPC UA Clint** that must connect to the **OPC UA Server** before any data can be exchanged between them. In this Client/Server scenario defined by the Services in Part 4, the data flow is bidirectional over the session. The session entities communicate over a secure channel that is created in the underlying communication layer and relies upon it for secure communication. It enables to log-in using user authentication and authorization. More details you can find in the article: 

- [OPC Unified Architecture – Main Technological Features][wordpress.OPCUAMTF]

Using the connection-oriented communication pattern it is difficult or even impossible to gather and process data from mobile things (devices), which is one of the **Internet of Things** paradigms. More details you can find in [IoT versus SCADA/DCS Data Acquisition Patterns][wordpress.IoTVersus]. The [OPC.UA.PubSub][OPC.UA.PubSub] specification offers the connectionless approach as an additional option to session based client-server interoperability and is a consistent part of the OPC UA specifications suit. PubSub fully integrates into the existing OPC UA technology but as result of applying the connectionless communication it is easier to implement low power and low-latency communications on local networks.

The specification claims that intentionally PubSub is based on the **[OPC UA Information Model][CAS.OPCUAIMD]** with the aim of seamless integration into **OPC UA Servers** and **OPC UA Clients**. Unfortunately **OPC UA Information Model** is not used to promote **PubSub Application** interoperability. This concept is only employed to define **Security Key Service** and **Configuration Service** models, which have only indirect impact on the **PubSub Application** interoperability. Nevertheless, the PubSub communication does not require such a role dependency, i.e there is no necessity for **Publisher** or **Subscriber** to be either an **OPC UA Server** or an **OPC UA Client** to participate in the communication.

# Services

## Introduction

**PubSub Applications** exchange messages formatted as the `NetworkMessage` structure using underlying transport layer. Directly or indirectly the specification defines the following actors:

* **Publisher**: pushes the current process data formatted as a `NetworkMessage` structure to an underlying transport layer.
* **Subscriber**: consumes the process data, which is recovered from the `NetworkMessages` structure polled from the underlying transport layer.
* **Security Key Service** - provides security keys that can be used to sign and encrypt messages.
* **Configuration Tool** - an external application used to remotely configure **PubSub Application**.

The **Publisher** is the actor that pushes `NetworkMessage` structures to an underlying transport layer. It represents a certain data source, for example, a control device, a manufacturing process, a weather station, or a stock exchange. It may be also **OPC UA Client**, **OPC UA Server** or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

The **Subscriber** actors are the consumers of `NetworkMessage` structure, which are polled from the underlying transport layer. They may be **OPC UA Client*, **OPC UA Server** or in general any applications that understand the syntax and semantics of the `NetworkMessage` structure.

The procedure and related data of messages exchange between the **Publisher** and **Subscriber** is described in the section *Communication*. 

The **Security Key Service** responsibility is to 

**Configuration Tool**

## Communication

## Messages Exchange Procedure

The **PubSub Applications** are decoupled by exchanging messages over an underlying transport layer. It is worth stressing that by design the **PubSub Application** don't expose any API that can be used to transfer process upper layer data over the network, i.e. it is not communication layer. It means that these applications must produce and consume the process data.

OPC UA PubSub interoperability doesn't depend on any functionality provided by the underlying transport layer called **Message Oriented Middleware**. In general, according to the specification the **Subscriber** and **Publisher** can be interconnected using any transparent messages transport infrastructure providing unreliable service that provides no guarantees for delivery and no protection from duplication. Applying MQTT means that some functionality related to communication reliability, data selection, and distribution is delegated to the MQTT `Server`.

The Publisher doesn't have any subscriptions management functionality, namely, it offers a communication paradigm called unsolicited notification. When unsolicited notification occurs, a client receives a message that it has never requested.

Subscriber must use a filtering mechanism to process only Messages it is interested in. Subscribers do not have any subscription management functionality.

## `NetworkMessage` serialization


## Transport Mapping

The syntax of the UDP transporting protocol URL doesn't provide information if a multicast group is to be joined.

## **Security Key Service**

## **Configuration Tool**

# Security

Communication security is based on encryption and signing of the messages. To protect message the **Publisher** may sign and encrypt **NetworkMessages** and the **Subscriber** verifies the signature end decrypt the **NetworkMessages**. Bot operation must use approprite security keys. The keys are provided by independent sever **Security Key Service** (SKS).

# Configuration



# Conclusion

> By design the component `DataManagement` implemented in the project [Networking.SemanticData](https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/SemanticData) is compliant with the [OPC UA Specification Part 14 PubSub](https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/).
> It must be stressed that because the [OPC UA Specification Part 14 PubSub](https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/) has been just released the content of this document may change in future.
 

# Normative References

The following documents, in whole or in part, are normatively referenced in this document and are indispensable for its application.

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]
- [OPC Unified Architecture Specification  Part 7: Profiles Release 1.04][OPC.UA.Profiles]

# Glossary

- PubSub Applications
> Any instance of software program that conforms to the PubSub specification
 
- Publisher-subscriber communication pattern

- session

>  <session>

- Connection-oriented communication pattern

> Requires a session connection (analogous to a phone call) be established before any data can be sent. This method is often called a "reliable" network service. It can guarantee that data will arrive in the same order. Connection-oriented services set up virtual links between end systems through a network, as shown in Figure 1. Note that the packet on the left is assigned the virtual circuit number 01. As it moves through the network, routers quickly send it through virtual circuit 01.

- Communication pattern    Does not require a session connection between sender and receiver. The sender simply starts sending packets (called datagrams) to the destination. This service does not have the reliability of the connection-oriented method, but it is useful for periodic burst transfers. Neither system must maintain state information for the systems that they send transmission to or receive transmission from. A connectionless network provides minimal services.


- data polling

> continuous checking of the sensors to see what state they are in, usually in multipoint or multidrop communication (a communication engine with multiple devices attached that share the same line) by sending a message to each device, one at a time, asking each to respond and send new data.

- data subscription 

> senders of messages containing the process data fetched by the sensor, called publishers, do not prepare the messages to be sent directly to specific receivers, called subscribers, but instead, they categorize published messages into topics without knowledge of which subscribers, if any, may receive the message. Similarly, subscribers express interest in one or more topics and only receive messages that are of interest, without knowledge of which publishers, if any, there are.

- process data

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

- [RFC 768: User Datagram ProtocolJ, 28 August 1980][RFC.UDP]

[RFC.UDP]:https://tools.ietf.org/html/rfc768

- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]

[OPC.UA.PubSub]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/

- [OPC Unified Architecture Specification  Part 7: Profiles Release 1.04][OPC.UA.Profiles]

[OPC.UA.Profiles]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-7-profiles/


[RFC.UDP.IP]:https://tools.ietf.org/html/rfc791


[OOI.Networking.UDPMessageHandler]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/UDPMessageHandler
[OOI.Networking.ReferenceApplication]:https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/ReferenceApplication

