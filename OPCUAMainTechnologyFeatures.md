# OPC UA Main Technology Features

## Introduction

One of the main goals of the [OPC Unified Architecture](OPCUnifiedArchitecture.md) is to provide a consistent mechanism for the integration of process control and management systems. It is assumed that it should be robust and the implementation should be platform independent. In this section, I will examine technologies and paradigms used as a foundation for the development of the OPC UA standard and discuss their impact on the final result.

## Object Oriented Information Model

To make systems interoperable, the data transfer mechanism must be associated with a consistent information representation model. OPC UA uses an object as a fundamental notion to represent data and activity of an underlying system. The objects are placeholders of variables, events, and methods and are interconnected by references. This concept is similar to well-known object-oriented programming (OOP) that is a programming paradigm using "objects" – data structures consisting of fields, events and methods – and their interactions to design applications and computer programs. The OPC UA Information Model provides features such as data abstraction, encapsulation, polymorphism, and inheritance.

The OPC UA object model allows servers to provide type definitions for objects and their components. Type definition may be abstract and may be inherited by new type to reflect polymorphism. They may also be common or they may be system-specific. Object types may be defined by standardization organizations, vendors or end-users.

The Information Model is a very powerful concept, but it is abstract and hence, in a real environment, it must be implemented in terms of bit streams (to make information transferable) and addresses (to make information selectively available). To meet this requirement, OPC UA introduces a Node notion as an atomic addressable entity that consists of attributes (value-holders) and references (address-holders of coupled nodes). The set of Nodes that an OPC UA Server makes available to clients is referred to as its Address Space, which enables the representation of both real process environment and real-time process behavior. The Address Space is described in depth in the [OPC UA eBook][CAS.EBOOK].

Each of the previous OPC Classic specifications defined their own address space model and their own set of services. OPC UA unifies the previous models into a single integrated Address Space with a single set of services.

A detailed description of this topic is covered by the section [*Semantic-Data Processing Architecture*][SDPA]. This section is an executive summary targeting the new concept called `Semantic-data` derived from the OPC Unified Architecture address space model.

## Service Oriented Architecture

At the very beginning of new solution development, we must address a question about its fundamental paradigms and architecture. OPC Classic is based on the functionality provided by an operating system and is actually an instruction on how to use the functionality to interconnect participants of the data exchange. This was recognized as one of the drawbacks making the lifetime of the OPC Classic standard dependent on the lifetime of the technology it is based on.

Observing continuous evolution of the IT domain, it seems that finding a solution that will guarantee an unlimited lifetime is a real challenge. However, decoupling the solution from any base technology increases the chance of it surviving the disappearance of the base technology from the market. Developing services and deploying them using a [Service Oriented Architecture](https://en.wikipedia.org/wiki/Service-oriented_architecture) (SOA) is the best way to invent IT systems to meet this challenge. A service differs from an object or a procedure because it is defined by messages that it exchanges with other services. SOA defines the way in which services are deployed and managed. Using an SOA approach leverages the solution re-usage, lowers overall cost, and improves the ability to rapidly change and evolve systems, whether old or new.

To make systems interoperable, any even brilliant idea is not enough. We need a data transfer technology, however – when defining data exchange in the context of messages – we do not need to bother with the different technologies used by the participants as long as they can absorb the messages.

Today, an ideal platform for the SOA concept implementation is Web Service technologies. They represent the most widely adopted distributed computing standards in industry history. Web Services are a set of specifications based on XML ([eXtensible Markup Language](https://en.wikipedia.org/wiki/XML)) and developed by W3C \( [World Wide Web Consortium](https://www.w3.org/) \). Those standards are generally marked with a WS-\* symbol. Because the WS-\* standards are developed without any initial assumption concerning the underlying system platform they are implemented on, they, therefore, must precisely define what must be on the "wire".

The WS-\* standards are the basic foundation for OPC UA but, using them alone, would not be enough to reach the expected data throughput performance in industrial applications. The OPC UA suite of protocols, therefore, expands the WS-\* standards by defining a few proprietary ones that can be used alternatively. OPC UA messages may be encoded as an XML text or in binary format for efficiency purposes. They may be transferred using multiple underlying kinds of transport, for example, TCP or SOAP over HTTP. Clients and servers that support multiple kinds of transport and encodings will allow end users to make decisions about tradeoffs between performance and XML Web Services compatibility at the time of deployment, rather than having these tradeoffs determined by the OPC vendor at the time of product selection.

## Abstraction and Mapping

To make systems interoperable, the data transfer mechanism must be associated with a consistent information representation model. OPC UA uses an object as a fundamental notion to represent data and activity of an underlying system. The objects are placeholders of variables, events, and methods and are interconnected by references. This concept is similar to well-known object-oriented programming (OOP) that is a programming paradigm using "objects" – data structures consisting of fields, events and methods – and their interactions to design applications and computer programs. The OPC UA Information Model provides features such as data abstraction, encapsulation, polymorphism, and inheritance.

The OPC UA object model allows servers to provide type definitions for objects and their components. Type definition may be abstract and may be inherited by new type to reflect polymorphism. They may also be common or they may be system-specific. Object types may be defined by standardization organizations, vendors or end-users.

The Information Model is a very powerful concept, but it is abstract and hence, in a real environment, it must be implemented in terms of bit streams (to make information transferable) and addresses (to make information selectively available). To meet this requirement, OPC UA introduces a Node notion as an atomic addressable entity that consists of attributes (value-holders) and references (address-holders of coupled nodes). The set of Nodes that an OPC UA Server makes available to clients is referred to as its Address Space, which enables the representation of both real process environment and real-time process behavior. The Address Space is described in depth in the [OPC UA eBook][CAS.EBOOK].

Each of the previous OPC Classic specifications defined their own address space model and their own set of services. OPC UA unifies the previous models into a single integrated Address Space with a single set of services.

## Security

Security is a fundamental aspect of computer systems, in particular, those dedicated to enterprise and process management. Especially in this kind of application, security must be robust and effective. Security infrastructure should also be flexible enough to support a variety of security policies required by different organizations. OPC UA may be deployed in diverse environments; from clients and servers residing on the same hosts, throughout hosts located on the same operation network protected by the security boundary protections that separate the operation network from external connections, up to applications running in global environments using public network infrastructure. Depending on the environment and application requirements, the communication services must provide different protections to make the solution secure, therefore OPC UA specification must offer scalability.

OPC UA Security is concerned with the authentication of clients and servers, the authorization of users, the integrity and confidentiality of their communications and the auditing of client-server interactions. To meet this goal, security is integrated into all aspects of the design and implementation of OPC UA Servers and Clients. The OPC Foundation has also addressed the security issues that arise from implementation. This includes independent reviews of all aspects of security starting from the design of in-depth security provided by the specification (which is built and model on the WS* specifications); to the actual implementation provided by the OPC Foundation. The OPC Foundation has chosen to use industry-standard security algorithms and industry standard security libraries to implement OPC UA Security.

Security mechanisms can be provided by diverse communication layers. Transport-level security is a solution limited to point-to-point messaging. In this case, messages can be protected by establishing a secure connection (association) between two hosts using for example Transport Layer Security (TLS) or IPSec protocols. But, if intermediaries are present when using secure transport, the initial sender and the ultimate receiver need to trust those intermediaries to help provide end-to-end security, because each hop is secured separately. In addition to explicit trust of all intermediaries, other risks such as local storage of messages and the potential for an intermediary to be compromised must be considered. Thus, using only transport security limits the robustness of the security solution to transport-specific features. For OPC UA over the session communication, the security association must survive beyond a single transport connection.

To meet the above requirements, the OPC UA security architecture is defined as a generic solution that allows the implementation of the required security features at various places in the application architecture. The OPC UA security architecture is structured in an application layer and a communication layer atop the transport layer.

The routine work of a client application and a server application to transmit plant information, settings, and commands is done in a session in the application layer. The application layer also manages user authentication and user authorization. OPC UA Client and Server applications identify and authenticate themselves with X.509 Certificates. Clients pass a user identity token to the OPC UA Server. The OPC UA Server authenticates the user token. Applications accept tokens in any of the following three forms: user-name/password, an X.509v3 certificate or a WS-SecurityToken

A session in the application layer communicates over a secure channel that is created in the communication layer and relies upon it for secure communication. All of the session data is passed to the communication layer for further processing. The secure channel is responsible for messages integrity, confidentiality, and applications authentication.

OPC UA uses symmetric and asymmetric encryption to protect confidentiality as a security objective. OPC UA relies upon the site cyber security management system to protect confidentiality on the network and system infrastructure and utilizes the Public Key Infrastructure to manage keys used for symmetric and asymmetric encryption. OPC UA uses symmetric and asymmetric signatures to address integrity as a security objective.

## Profiles

OPC UA is designed to support a variety of applications, from plant-floor PLCs to enterprise servers. Those applications require a variety of execution platforms and functional capabilities. Therefore, OPC UA defines a comprehensive set of capabilities, of which applications may implement a subset of. These subsets are referred to as Profiles, and applications may claim conformance to them. Clients can then discover the Profiles for a Server, and tailor their interactions with that server based on the Profiles. The client also contains Profiles which allow the end user the ability to match up server profiles to client profiles, making it easier to ensure that diverse clients and servers will interoperate. Servers can also discover these client profiles and could tailor their response to the client based on the client profile.

## Robustness

OPC UA is designed to provide robustness of published data. The major feature of all OPC UA Servers is the ability to publish data and event notifications. OPC UA provides mechanisms for clients to quickly detect and recover from communication failures associated with transfers without having to wait for long timeouts provided by the underlying protocols.

The design of OPC UA ensures that vendors can create redundant clients and redundant servers in a consistent manner. Redundancy may be used for high availability, fault tolerance, and load balancing. Generally, we can distinguish redundancy of servers/clients, communication paths and signals. Although the specification provides support only for client/server redundancy, product vendors can incorporate all kinds of redundancy into the framework proposed by the specification. For example, a server can establish a wireless connection as the means of recovery from cable connection failure or a server can use many data sources bound to a variable to provide continuous updating of the variable value even if one of the sensors has been damaged.

OPC UA requires a stateful model as the next feature that increases the solution robustness. State information is maintained inside an application session. Examples of state information are subscriptions, user credentials and continuation points for operations that span multiple requests.

Sessions are defined as logical connections between clients and servers. What is worth stressing, each session is independent of the underlying communications protocols. Failures of these protocols do not automatically cause the session to terminate. Sessions terminate based on:

- a client or server request
- the inactivity of the client

## See also

More you can find in the eBook at:

- [1] [OPC UA Address Space Model Designer, 2019][CAS.ASMD]
- [2] [OPC Unified Architecture e-book, 2010][CAS.EBOOK]
- [3] [Part 5: Information Model, OPC Foundation, Rel. 1.04, 2017-11-22][OPC.UA.Part5]
- [Semantic-Data Processing Architecture][SDPA]

[SDPA]:SemanticData/README.MD
[OPC.UA.Part5]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-5-information-model/
[CAS.ASMD]: http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx
[CAS.EBOOK]:http://www.commsvr.com/UAModelDesigner/
