# OPC Unified Architecture

## Introduction

OPC Unified Architecture (OPC UA) is described in a layered set of specifications broken into parts. It is purposely described in abstract terms and only in selected parts coupled to existing technology on which software can be built. This layering is intentional and helps isolate changes in OPC UA from changes in the technology used to implement it.

The OPC UA specifications are organized as a multipart document comprising the following sets:

- Core specification
- Access type specification
- Utility specification

The first set specifies the core capabilities of OPC UA. Those core capabilities define the concept and structure of the Address Space and the services that operate on it. The access type set applies those core capabilities to specific models of data access. Like in OPC Classic, there are distinguished: Data Access (DA), Alarms and Conditions (A&C) and Historical Access (HA). A new access mode is provided as a result of introducing the programs concept and aggregation mechanisms. This set also specifies the UA server discovery process. Those mechanisms are not directly dedicated to support data exchange, but play a very important role in the whole interoperability process.

The core set contains the following specifications:

- Part 1 – Overview and Concepts: presents the concepts and overview of OPC Unified Architecture.
- Part 2 – Security Model: describes the model for securing interactions between OPC UA clients and servers.
- Part 3 – Address Space Model: describes an object model that servers use to expose underlying real-time processes to create an OPC UA connectivity space.
- Part 4 – Services: specifies the services provided by OPC UA servers.
- Part 5 – Information Model: specifies information representations - types that OPC UA servers use to expose underlying real-time processes.
- Part 6 – Mappings: specifies transport mappings and data encoding supported by OPC UA.
- Part 7 – Profiles: introduces the concept of profiles and defines available profiles that are groups of services or functionality.

The access type set contains the following specifications:

- Part 8 – Data Access: specifies the use of OPC UA for data access.
- Part 9 – Alarms and Conditions: specifies the use of OPC UA support for accessing alarms and conditions.
- Part 10 – Programs: specifies OPC UA support for accessing programs.
- Part 11 – Historical Access: specifies the use of OPC UA for historical access. This access includes both historical data and historical events.

The utility specification parts contain the following specifications:

- Part 12 – Discovery: introduces the concept of the Discovery Server and specifies how OPC UA clients and servers should interact to recognize OPC UA connectivity.
- Part 13 – Aggregates: describes ways of aggregating data.
- Part 14 - PubSub This specification defines the OPC Unified Architecture (OPC UA) PubSub communication model. The PubSub communication model defines an OPC UA publish-subscribe pattern instead of the client-server pattern defined by the Services in Part 4.
Part 100: Device Information Model.  Companion Specification featuring an Information Model for Devices.

## Overview and Concepts

This part describes the goal of OPC UA and introduces the following models to achieve it:

- Address Space and information model to represent structure, behavior, semantics, and infrastructure of the underlying real-time system.
- Message model to interact between applications.
- Communication models to transfer data over the network.
- Conformance model to guarantee interoperability between systems.
- Security model to guarantee cyber security addressing client/server authorization, data integrity and encryption.

## Security Model

This part describes the OPC UA security model. OPC UA provides countermeasures to resist threats that can be made against the environments in which OPC UA will be deployed. It describes how OPC UA relies upon other standards for security. The proposed architecture is structured in an application layer and a communication layer. Introduced security policies specify which security mechanisms are to be used. The server uses security policies to announce what mechanisms it supports and the client - to select one of those available policies to be used when establishing the connection.

## Address Space

There is no doubt that information technology and process control engineering have to be integrated to benefit from macro optimization and synergy effect. To integrate them, we must make systems interoperable. It causes the necessity of exchanging information, but to exchange information, it has to be represented as computer centric (saveable in a binary memory) and transferable (a stream of bits) data. According to the specification, a set of objects that an OPC UA server makes available to clients as data representing an underlying real-time system is referred to as its Address Space. The breaking feature of the Address Space concept allows representing both real process environment and real-time process behavior - by a unique means, mutually understandable by diverse systems.

## Services

The OPC UA services described in this part are a collection of abstract remote procedure calls that is to be implemented by the servers and called by the clients. The services are considered abstract because no particular implementation is defined in this part. The part Mappings describes more specific mappings supported for implementation. Separation of the service definition and implementation allows for harmonization with new emerging technologies by making new mappings.

## Information Model

To make the data exposed by the Address Space mutually understandable by diverse systems, the information model part standardizes the information representation as computer centric data. To promote interoperability, the information model defines the content of the Address Space of an empty OPC UA server. This content can be used as a starting browse point to discover all information relevant to any client. Definitions provided in this part are considered abstract because they do not define any particular representation on the wire. To make the solution open for new technologies, the representation mappings are postponed to the part Mappings. The solution proposed in this model is also open to defining vendor specific representations.

## Mappings

This part defines mappings between abstract definitions contained in the specification (e.g. in the parts: Information Model, Services, Security Model) and technologies that can be used to implement them. Mappings are organized into three groups: data encoding, security protocols and transport protocols. Different mappings are combined together to create stack profiles.

## Profiles

This part describes the OPC UA profiles as groups of services or functionality that can be used for conformance level certification. Individual features are grouped into conformance units, which are further grouped into profiles. All OPC UA applications shall implement at least one stack profile and can only communicate with other OPC UA applications that implement the same stack profile. Servers and clients will be tested against the profiles. Servers and clients must be able to describe which of the features they support.

## Data Access

This part describes the information model associated with the Data Access (DA) mode. It particularly includes an additional definition of variable types and a complementary description of Address Space objects. This part also includes additional descriptions of node classes and attributes needed for DA, as well as DA specific usage of services to access process data.

## Alarms and Conditions

This part describes the representation of events and alarms in the OPC UA Address Space and introduces the concepts of condition, dialog, acknowledge-able condition, configure-able condition and alarm. To expose above information, it extends the information model defined in other parts and describes alarm specific uses of services.

## Programs

This part extends the notion of methods and introduces the concept of programs as a complex, stateful functionality in a server or underlying system that can be invoked and managed by a OPC UA client. The provided definitions describe the standard representation of programs as part of the OPC Unified Architecture information model. The specific use of services is also discussed.
Historical Access

This part describes an extension of the information model associated with Historical Access (HA). It particularly includes additional and complementary definitions of the representation of historical time series data and historical event data. Additionally, this part covers HA specific usage of services to detect and access historical data and events.

## Discovery

The main aim of this part is to address the discovery process that allows the clients to first find servers on the network and then find out how to connect to them. This part describes how UA clients and servers interact to exchange information on resources available on the network in different scenarios. To achieve this goal, there are introduced the concepts of a discovery server that is a placeholder of global scope information and a local discovery server, whose main task is to manage information vital to local resources. Finally, this part describes how to discover UA applications when using common directory service protocols such as UDDI and LDAP.

## Aggregates

This part specifies the information model associated with aggregates and describes how to compute and return aggregates like minimum, maximum, average etc. Aggregates can be used with base (live) data as well as historical (HA) data. This part also addresses the aggregate specific usage of services.

## Conclusion

All of the features presented in this section are very important for assessing the specification against particular requirements vital for industrial IT application domain. For the rest of this paper they can be recognized as “must have option” to be surrounded by tools and deployment methodology to finally produce a widely accepted powerful technology. To meet the goal of this paper we will focus on the information representation rules proposed by this standard and methodology of practical deployment thereof. In this context there are two fundamental concepts introduced by the OPC UA specification:

- Address Space Model – all about exposing information in a standard way
- Information Model – all about unambiguous, computer centric definition of information

## See also

- [OPC Unified Architecture - Main Technological Features](http://wp.me/p3MGZj-i)
- [OPC UA Makes Smart Factory Possible](http://mpostol.wordpress.com)
- [OPC UA makes cloud computing possible](http://mpostol.wordpress.com)
