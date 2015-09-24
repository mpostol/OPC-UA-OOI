
# Using OPC UA Data Ouside of the Server

## Domain Model

Main challenge that must be faced up by engineering of the data processing process is preservation of the data semantics at all the stages of this process, i.e. to manipulate the data meaningfully. This diagram is a domain model, which presents a high level conceptual framework of the OPC **UA Semantic Data** supporting contextual data processing outside of the *OPC UA Server*. It presents all the major contributors processing the data in context of **UA Information Model**.

**UA Semantic Data** is directly used by the following classes of actors:
* **Address Space Management** -  part of the *OPC UA Server* optionally supporting integration services
* **UA Client** -  custom OPC **UA Client** supporting additionally integration services
* **UA Data Application** - an application program processing the data in context of the OPC UA metadata without the *OPC UA Server* session.

Integration services are responsible for exchanging the data and metadata with other parties using:

* Network communication
* Database access   

![Domain Model] (https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/Media/UADataIntegrationServices.Domain.png)

## UA Semantic Data

### Concept

The OPC **UA Semantic Data** represents the following triple:

* Uniform Resource Identifier (URI)
* Type as an entity of the *OPC UA Information Model*
* A set of related nodes in the context of *OPC UA Address Space*


Using the URI as a standard for global identifiers allows for a worldwide reference for any data. This means that we can tell when any two applications anywhere in the world are referring to the same data. It could answer the question when is a data in one location the same data as a data in another location? Using URI, therefore, we can introduce the notion of the global data identity. The data identity allows creation of variety of dictionaries collecting supplementary information independently and outside of the server Address Space context.

Value of the data can change in time. It is worth nothing that in the presented concept if two instances of the data has the same URI it does not mean that both have the same value, i.e. the same bits pattern. For example if we are measuring the temperature in the output pipe of the selected boiler with the 1 second interval, we are getting a new value every second. The URI cannot be used to distinguish instances among each other, but still it could be used to use common key and sign each value, and finally send it over the wire to a destination application. The destination application can use recognizable key (indexed by the URI) retrieved from a dictionary and check the data against the not-repudiation security scenario.

Any data object is always just a stream of bits. To understand the binary data we must have defined a data type – a description how to create an appropriate bits pattern. Simplifying, the data type determines a set of valid values and rules needed to assign the information (understand the data) to a selected bits pattern. OPC UA specification provides additional possibility to define meaning of the data, i.e. exposing the data in the context of metadata. In this case the data is made available as the *Value* attribute of the *Variable* node and surrounded by other nodes creating part of relationship. A well defined set of interconnected nodes we can be recognized as a complex data ([to learn more visit: OPC UA Makes Complex Data Processing Possible](https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/)). Complex in this context means that the type must additionally define a relationship between the components of the binary data, i.e. how to selectively get a component of the complex data. To associate the type and the data the URI must be defined unambiguously in context of the selected *OPC UA Information Model*.

To promote data processing against type definitions we shall assume that types of the complex data are stable and therefore may not be preserved in the data instance provided a precise nodes selection rules are defined and observed. To meet this requirement, any **UA Semantic Data** instance is a set of nodes organized as the tree structure defined recursively as a collection of nodes (starting at a root node), where each node called parent is the root of subtree, i.e. all nodes that has "part of" relationship called children. The collection of children may be empty and in this case the node is called leaf. In this definition “part of” means that the parent has a HasProperty or HasComponent Reference to its children. This type of references prevent loops.

For any OPC Information Model the proposed above selection rule unambiguously yields a set of nodes that can make up the **UA Semantic Data** instance. The URI of this instance is the following couple

* Selected URI for the root element that qualifies the symbolic name part
* Symbolic name of the root element

The OPC UA information modelling concept is based on domains [to learn more visit: OPC UA Information Model Deployment]( http://www.cas.internetdsl.pl/commserver/P_DowloadCenter/P_Publications/20140301EN_DeploymentInformationModel.pdf). A domain is a named self-contained collection of definitions. Any domain name is URI and  must be globally unique - it is an identification string that defines a realm of administrative autonomy and authority of responsibility. Type definition from one domain may inherit from type definitions located in other domains. To avoid circular references domains should be organized in layers, which expand step by step the basic model provided by the *OPC UA Specification*. The URI for the information modeldefined by the standard is http://opcfoundation.org/UA/. To make URI for the nodes defined by the standard unique the server URL shall be used instead.

The symbolic name of each Node is its BrowseName, or, when it is part of another Node, the BrowseName of the other Node, a "\_", and the BrowseName of itself. As above in this case “part of” means that the whole has a HasProperty or HasComponent Reference to its part. Since all Nodes not being part of another Node have a unique name, the symbolic name is unique.

For example, the ServerType defined in the specification has the symbolic name “ServerType”. One of its InstanceDeclarations would be identified as “ServerType_ServerCapabilities”. Since this Object is complex, another InstanceDeclaration of the ServerType is “ServerType_ServerCapabilities.MinSupportedSampleRate”. The Server Object is based on the ServerType and has the symbolic name “Server”. Therefore, the instance based on the InstanceDeclaration described above has the symbolic name “Server_ServerCapabilities_MinSupportedSampleRate”.

Unique identifier and both methods of data meaning definition makeup a concept of OPC **UA Semantic Data**. Again this concept can be implemented using the *OPC UA Information Model* to define types and *OPC UA Address Space Model* to expose the data in the context of metadata.

To keep all benefits of the OPC **UA Semantic Data** concept the remote end users (**UA Data Application**) are interested to process the data using relevant part of the *OPC UA Address Space*.

The **UA Semantic Data** may be used to implement the **Data Security** and **Discoverable Data** concepts.

### Data Security

Some scenarios where the OPC **UA Semantic Data** is used requires that the data is protected against:

* Unauthorized Access  
* Non-repudiation
* Integrity

Because any **UA Semantic Data** instance is a uniquely named standalone entity the security can be provided in context of the data but not in context of the server the data is exposed by.

Each node in the **UA Semantic Data** is a root of a tree structure created by nodes recursively pointed out by HierarchicalReferences in forward direction. Because each node in this hierarchy is globally recognizable it may be protected as a resource by any independent protection system (for example any Kerberos protocol implementation) supporting authentication and authorization operation and responsible to distribute security tokens used to protect the data as stated above. Hierarchical relationship supports permissions inheritance, i.e. permissions propagation to the children nodes.

For example if OPC UA Server prepares the data representing current state of the Boiler `#1` it gets the protection key using the URI of this data from a dictionary and send the data say over the AMQP to AMQP clients. The clients using the URI of the Boiler `#1` browse local dictionaries (e.g. configuration files) to get appropriate key to follow the selected protection scenario. Synchronization of the dictionaries is user dependent mechanism.

### Discoverable Data

In distributed systems the **UA Data Application** and **UA Client** (see diagram above) is interested to get access to or update the data and it may not care about which particular server provides the data. In this case the data must be discoverable over the network. The **UA Semantic Data** can be used to solve this requirement. In this approach the URI of the data can be used as a key to browse the *UA Semantic Data Domain Name Server* (sdDNS) to find recursively destination *OPC UA Server* exposing the requested data. In this scenario we can use well know two steps process:

1. discover the server using the resource (**UA Semantic Data**) unique domain based indentifier;
2. browse or update the data.

**Challenge 1** - A HMI prepared in advance to display state of boilers formally described by the *BoilerType* shall display the *Boiler `#1`* state

For this scenario we can apply the following work-flow:

1. HMI (*OPC UA Client*) using URI of the *Boiler `#1`* browses *UA Semantic Data Domain Name Server* starting form a local one to find a discovery server having all needed information about an *OPC UA Server* exposing *Boiler `#1`* *Object Node* representing a physical object named *Boiler `#1`*
2. Establishes *OPC UA Session* with the discovery server and gets all needed informations
3. Establishes *OPC UA Session* with the destination *OPC UA Server* to subscribe for requested data and events.

**Challenge 2** - A smart power meter (*AMQP Client*) shall expose in an *OPC UA Address Space* (also *AMQP Client*) current values related to *Consumer `#1`*

For this scenario we can apply the following work-flow:

1. The meter using preconfigured URI of the *Consumer `#1`* reads the needed information about the destination *OPC UA Server* from a configuration file, prepares new AMQP Message, and finally sends it using the *AMQP Broker*
2. *OPC UA Server* gets the message from AMQP Queue and using the URI of the *Consumer `#1`* exposes the data (updates *Value* attributes of relevant *Variable* *Nodes*) related to a virtual meter for *Consumer `#1`* in his *OPC UA Address Space*.

The Data discovery mechanism is part of the broader concept described in the document[
OPC Unified Architecture - Object Oriented Internet](../README.md), but its detailed description is out of scope of the this document.

## UA Information Model

The description of the *OPC UA Information Model* is captured in detail in:
* [OPC Unified Architecture e-book](http://www.commsvr.com/UAModelDesigner/Index.aspx)
* [OPC UA Information Model Deployment]( http://www.cas.internetdsl.pl/commserver/P_DowloadCenter/P_Publications/20140301EN_DeploymentInformationModel.pdf)

## Interoperable parties

### UA Data Application

OPC **UA Data** Centric **Application** Program (shortened to **UA Data Application**) is any software program designed to perform a specific function directly for the user or for another application programs. Application programs processes (consume and produce) the OPC **UA Semantic Data** according to selected algorithm to meet the user requirements.

The **UA Data Application** gets access to the **UA Semantic Data** as:
* Networked computer device;
* Client of Database Management System;

### Address Space Management

The **Address Space Management** is a typical part of any *OPC UA Server*. It is responsible for:
* *OPC UA Address Space* instantiation - creation and management of the server address space
* **Data Binding** - managing of the links to the underlain process **Raw Data**
* **Integration** services - optionally supporting interoperability with other parties outside the server     

OPC UA specification introduces a node notion as an atomic addressable entity that consists of attributes (value-holders) and references (address-holders of coupled nodes).  The set of nodes that an *OPC UA Server* makes available to clients is referred to as its address space, which enables representation of both underlying processes environment description and its state/behavior. The address space exposed by the server makes up a context the **Raw Data** is made available to the clients. Instantiation of this context depends on an application domain unique *OPC UA Information Model* and is governed by the *OPC UA Address Space Model* rules.

The **UA Server Services** access data and metadata and makes them available to the **UA Client** using a set of non expandable services compliant with the specification.

The **Address Space Management** activity includes also creation of data bindings with the underlying real-time process (**Raw Data**).  **Data binding** is responsible for coupling  **Raw Data** representing current state/behavior of the underlying real-time process with the appropriate nodes in the address space. **Data Binding**, therefore, is the process that establishes a connection between the *Variable* node and selected data source. The *Value* attribute of the *Variable* node reflects changes when made. It can also mean that when the *Value* attribute of the *Variable* is changed, the underlying data will reflect that change.

Optionally the **Address Space Management** activity may also provide systems integration services supporting the process of bringing together the component external applications (not OPC **UA Server Services** aware) into one system and ensuring that the applications function together as a whole.  It includes but is not limited to linking together different software applications physically or functionally on the basis of common data and metadata compliant with applied *OPC UA Information Models*.  These services should provide the following functions:

* Selection of the *Variable* nodes (integration data) that are to be made available to the external applications           
* Distribution and update of the selection set information to the inter-operable parties       
* Selection of the data exchange channels, i.e. network links, databases, etc.
* Data security protection services
* Data discovery support services   

Optionally the **Address Space Management** could expose supplementary **UA Information Model** aimed to control remotely the data exchange channels by an *OPC UA Client* customized to be compliant with this model.

The underlying process **Raw Data** may also be processed by any **Application** simultaneously. This kind of activity is outside of the presented concept scope.

### UA Client

It is an embedded part of the **OPC UA Client**. **OPC UA Client** shall be compliant with Part 4. It consumes all not optional services required by the Part 4 of the specification and must be compliant with the selected existing profile. To get data from the *OPC UA Server* it must establish the session.

Optionally the **OPC UA Client** activity may also provide systems integration services supporting the process of bringing together the component external applications (not OPC UA Server Services aware) into one system and ensuring that the applications function together as a whole.  It includes but is not limited to linking together different software applications physically or functionally on the basis of common data and metadata compliant with applied OPC UA Information Models.  These services should provide the following functions:

* Selection of the Variables (integration data) that are to be made available to the external applications
* Selection of the data exchange channels, i.e. network links, databases, etc.
* Data security protection services
* Data discovery support services

Optionally the **OPC UA Client** may also support importing and exporting services of the data to external semantics (**Raw Data**).

# OPC UA Semantic Data Deployment

TBD Whht is OPC UA Semantic Data Deployment

The detailed description of the solution content is captured in the document [Content Description](./Readme.SemanticDataDeplyment.MD)
