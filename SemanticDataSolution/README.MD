
# OPC UA Data Processing Outside the Server

## Concept

The main challenge that must be faced up by engineering of data processing is the preservation of the data semantics at all the stages of this process to manipulate the data meaningfully. This diagram is a domain model, which presents a conceptual framework of the OPC **UA Semantic Data** supporting contextual data processing outside the *OPC UA Server*. It presents all the major contributors processing the data in the context of **UA Information Model**.

**UA Semantic Data** is directly used by the following classes of actors:

* **Address Space Management** -  part of the *OPC UA Server* optionally supporting integration services
* **UA Client** -  custom OPC **UA Client** supporting integration services
* **UA Data Application** - an application program processing the data in the context of the OPC UA metadata without the *OPC UA Server* session.

![Domain Model] (./Media/UADataIntegrationServices.Domain.png)

**Address Space Management** and **UA Client** are typical components for OPC UA technology. Description of the **UA Data Application** architecture is captured in the next section.

### UA Data Application

**UA Data Application** is a framework that provides integration services. The diagram below shows an example how the  **UA Data Application** may be expanded to implement integration services in context of selected Industrial IT technologies.

![UA Data Application] (./Media/UADataIntegrationServices.UADataApplication.png)

Integration services are responsible for exchanging the data and metadata with other parties using varieties of applications including but not limiting to:

* **UA Data Networking** - provides message centric distribution mechanism of the strong typed data
* **UA Data Archival** - collects data received in the messages and archives it in the database
* **UA Data Cloud Gateway** - it simplifies creation of a bridge to the cloud that enables data to stream from industrial equipment to a cloud messaging endpoint such as (Azure Event Hubs)[http://azure.microsoft.com/en-us/services/event-hubs/].
* **UA Data JSON Gateway** - ii factories objects using the message content and serializes them into the JSON text representation for further processing
* **UA Data XML Gateway** - ii factories objects using the message content and serializes them into XML text representation for further processing

In all the cases above the destination representation, i.e. message content, object types and database schema can be prepared in advance at design time using the OPC UA Information Model.

**UA Data Networking** is an example of transferring the UA Semantic Data over the network using message centric data transport standardized by the OPC UA Specification Part 14 Pub/sub Draft 1.04.08. The description of its architecture is captured in
[UA SemanticData Message Centric Communication](./README.MessageCentricCommunication.md#ua-semanticdata-message-centric-communication).

This concept have been implemented in the project [DataManagement](./DataManagement#introduction). This project provides an SDK library that can be used as a foundation of the communication infrastructure deployment.

The [UANetworkingReferenceApplication](./UANetworkingReferenceApplication#ua-data-example-application) contains a reference WPF application.

The **UA Data Archival** can also be used to transfer data from the OPC UA environment to Data Based Management System for further processing by Enterprise Management BI systems.

The **UA Data JSON Gateway** and **UA Data XML Gateway** supports strong typed objects serialization and deserialization using JSON and XML industrial standards. Converting the OPC UA objects into text based representation is required to publish and subscribe using Enterprise Service Base concept.

## UA Semantic Data

### Concept

The OPC **UA Semantic Data** represents the following triple:

* Uniform Resource Identifier (URI)
* Type as an entity of the *OPC UA Information Model*
* A set of related nodes in the context of *OPC UA Address Space*

Using the URI as a standard for global identifiers allows for a worldwide reference for any data. This means that we can tell when any two applications anywhere in the world are referring to the same thing. It could answer the question when a thing in one location is the same thing as thing in another location? Using URI, we can, therefore, introduce the notion of global data identity. The data identity allows for the creation of variety of dictionaries collecting supplementary information independently and outside the server Address Space context.

Any data instance (value) is always just a stream of bits. To understand the binary data we must have defined a data type a description how to understand a bits pattern. Simplifying, the data type determines a set of valid values and rules needed to assign information (understand the data) to a selected bits pattern. The OPC UA specification provides an additional possibility of defining the meaning of the data, i.e. exposing the data in the context of metadata. In this case the data is made available as the *Value* attribute of the *Variable* node and is surrounded by other nodes creating a part of relationship. A well defined set of interconnected nodes can be recognized as complex data ([to learn more visit: OPC UA Makes Complex Data Processing Possible](https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/)). Complex in this context means that the type must additionally define a relationship between the components of the binary data, i.e. how to selectively get a component of the complex data. To associate the type and the data, the URI must be defined unambiguously in the context of the selected *OPC UA Information Model* that provides type definitions.

To promote data processing against type definitions we shall assume that types are stable and therefore may not be preserved in the data instance, provided that precise nodes selection rules are defined and observed. To meet this requirement, any **UA Semantic Data** represents a set of nodes organized as a tree structure defined recursively as a collection of nodes (starting at a root node), where each node called parent is the root of a subtree, i.e. all nodes that have "part of" relationship called children. The collection of children may be empty and in this case the node is called a leaf. In this definition “part of” means that the parent has a `HasProperty` or `HasComponent` reference to its children. This type of references prevents loops.

For any *OPC Information Model* the selection rule proposed above unambiguously yields a set of nodes that can make up the **UA Semantic Data** instance. The URI of this instance is the following couple:

* Selected URI for a root element that qualifies the symbolic name part
* Symbolic name of a root element

The OPC UA information modeling concept is based on domains [to learn more visit: OPC UA Information Model Deployment]( http://www.cas.internetdsl.pl/commserver/P_DowloadCenter/P_Publications/20140301EN_DeploymentInformationModel.pdf). A domain is a named self-contained collection of definitions. Any domain name is URI and  must be globally unique - it is an identification string that defines a realm of administrative autonomy and authority of responsibility. The type definition from one domain may inherit from type definitions located in other domains. To avoid circular references, domains should be organized in layers, which expand step by step the basic model provided by the *OPC UA Specification*. The URI for the information model defined by the standard is `http://opcfoundation.org/UA/`. To make URI for the nodes defined by the standard unique, the server URL shall be used instead.

The symbolic name of each node is its `BrowseName`, or, when it is a part of another node, the `BrowseName` of the other node, a `"_"`, and the `BrowseName` of itself. It applies recursively. “Part of” means that the whole has a `HasProperty` or `HasComponent` reference to its part. Since all nodes not being the part of another node have a unique name, the symbolic name is unique. Any root element must not be an instance declaration. An instance declaration is an Object, Variable or Method that is the target node of a hierarchical reference from a type definition mode or another instance declaration.

For example, the `ServerType` defined in the specification has the symbolic name `ServerType`. One of its instance declarations would be identified as ‘ServerType_ServerCapabilities’. Since this Object is complex, another instance declaration of the `ServerType` is `ServerType_ServerCapabilities_MinSupportedSampleRate`. The `Server` `Object` node is based on the `ServerType` and has the symbolic name `Server`. Therefore, the instance based on the instance declaration described above has the symbolic name `Server_ServerCapabilities_MinSupportedSampleRate`.

The unique identifier and both methods of data meaning definition make up a concept of OPC **UA Semantic Data**. Again, this concept can be implemented using the *OPC UA Information Model* to define types and *OPC UA Address Space Model* to expose the data in the context of metadata.

To keep all benefits of the OPC **UA Semantic Data** concept, the remote end users (**UA Data Application**) are interested in the processing of the data using a relevant part of the *OPC UA Address Space*.

The **UA Semantic Data** may be used to implement the **Data Security** and **Discoverable Data** concepts described below.

### Data Encoding

As it was defined above any **UA Semantic Data** represents a well defined set of nodes organized as a tree structure. Type is responsible to define a meaning for this tree, e.g. from `BoilerType` we can learn how for this kind of objects their state is described - what values must be present in the data instance. This meaning is provided by the semantics rules of the type. To define valid bits pattern encoding must be defined by the type as well. Encoding rules depends on where the data is to be processed and it could change during the data life-cycle. Definition of the encoding is, therefore, outside of scope of **UA Semantic Data** definition and must be provided by the implementation of the integration services (see diagram above).

Value of the data can change in time. It is worth noting that, in the presented concept, if two instances of the data have the same URI it does not mean that both have the same value, i.e. the same bits pattern. For example, if we are measuring the temperature in an output pipe of a selected boiler at 1 second intervals, we are getting a new value (creating new data instance) every second. The URI cannot be used to distinguish data instances, but still it could be used to apply a common key and sign each value and, finally, send it over the wire to a destination application. The destination application can use a recognizable key (indexed by the URI) retrieved from a dictionary and check the data against a not-repudiation security scenario.

**Challenge 1**: An *OPC UA Server* exposed 123456 values representing the crud oil refinery process. To make the server maintainable its configuration has to be self-adoptable (zero-configuration, zero-device drivers required)

For this scenario we can apply the following work-flow:

1. The server instantiates the OPC UA Information Model for the crud oil refinery process and starts listening for UDP messages containing the   **UA Semantic Data** instances
2. The `Flow meter #A-4321` device supporting Modbus RTU is equipped with a Modbus => **UA Semantic Data** gateway (lightweight plug converter - actually converting Modbus responses into the UDP/IP frame according to the Pub/Sub specification - price is relevant!)
3. Using the OPC UA Information Model of the process the meter creates partial data instance containing value of the `InputFlow` `Variable` defined somewhere in the model
4. Using local private key of the device the payload is signed and sent to the server.
5. Embedded UDP receiver of the **Address Space Management** on the server side reads the message from the network and, using the URI of the `Flow meter #A-4321`, browses a global directory to find the sender certificate and get the public key to implement payload integrity check
6. Finally, *OPC UA Server* exposes the data (updates `Value` attributes of the relevant `Variable` nodes) related to a virtual meter for `Flow meter #A-4321` in his **Address Space Management** component (i.e. in the Address Space instantiated according to the Information Model used to configure the device)
7. *OPC UA Clients* connected to this server are updated over the sessions using the standard subscription mechanism
8. In case the `Flow meter #A-4321` device is replaced for some reasons the new one also has to update relevant properties and *OPC UA Clients* can adopt the behavior using new values (e.g. engineering units) and expose also the device identification data, e.g serial number, model, etc. The device must be equipped with a gateway supporting its field level protocol or directly supporting the **UA Semantic Data** implementation.

### Data Security

Some scenarios where the OPC **UA Semantic Data** is used requires that the data is protected against:

* Unauthorized Access  
* Non-repudiation
* Integrity

Because any **UA Semantic Data** instance is a uniquely named standalone object, the security can be provided in the context of the data but not in the context of the server the data is exposed by.

Each node in the **UA Semantic Data** is a root of a tree structure created by nodes recursively pointed out by `HierarchicalReferences` in forward direction. Because each node in this hierarchy is globally recognizable it may be protected as a resource by any independent protection system (for example any Kerberos protocol implementation) supporting authentication and authorization operation and responsible for the distribution of security tokens used to protect the data as above. A hierarchical relationship supports permissions inheritance, i.e. permissions propagation to the children nodes.

**Challenge 1**: A server wants to send information out to a large number of thin HMI devices in a local network – the creation of simultaneous OPC UA sessions is impractical or impossible for some reasons.

For this scenario we can apply the following work-flow:

1. *OPC UA Server* prepares an UDP message representing the current state of the `Boiler #1` and if needed it gets a protection security token using the URI of the data from an external dictionary
2. *OPC UA Server* multicasts (one-to-many distribution) using UDP/IP protocol to remote consumers (e.g. thin HMI devices)
3. Using the `BoilerType` a HMI device can be prepared in advance to display the state of the boiler and configured against the URI to display information about the `Boiler #1` - no *OPC UA Server* session is required but `BoilerType` must be known (no communication) in advance from the appropriate *OPC UA Information Model*
4. Using preconfigured URI, HMI learns from the message that it contains information about Boiler `#1`, otherwise it neglects the message (local filtering approach)
5. HMI browses local dictionaries (e.g. configuration files) to get an appropriate security token to follow the selected protection scenario. Synchronization of the dictionaries is a user dependent mechanism and does not require *OPC UA Server* engagement.
6. HMI updates the screen using the message content and it is done.

**Challenge 2**: A smart power meter - *AMQP Client* `(~123 USD)` shall expose current values related to `Consumer #1` in an *OPC UA Address Space* (also supporting embedded *AMQP Client*). Because there are ~165000 meters that must be handled, establishing so many simultaneous OPC UA session over the Internet is impractical or even impossible.

For this scenario we can apply the following work-flow:

1. The meter (*AMQP client*) reads the information (related only to the AMQP connectivity) about the destination *AMQP client* (embedded part of the *OPC UA Server*) from its configuration file.
2. The meter device prepared in advance using the `SmartPowerMeterType` creates a new AMQP message adding the URI of the `Consumer #1` and signs it using a private key - no *OPC UA Server* session is required but `SmartPowerMeterType` must be known (no communication) in advance from appropriate *OPC UA Information Model* (OPC UA interoperability is required).
3. Finally, the meter sends the message using an AMQP broker - cloud approach.
4. Embedded AMQP client of the *OPC UA Server* reads the message from the AMQP Queue and, using the URI of the `Consumer #1`, browses a global customers directory to find the sender certificate and get the public key to implement non-repudiation security algorithm (we are talking not only about power, but also about money!).
5. Finally, *OPC UA Server* exposes the data (updates `Value` attributes of relevant `Variable` nodes) related to a virtual meter for `Consumer #1` in his **Address Space Management** component.
6. *OPC UA Clients* connected to this server are updated over the sessions using the standard subscription mechanism.

### Discoverable Data

In distributed systems the **UA Data Application** and **UA Client** (see the diagram above) are interested in getting access to or update the data and they may not care about which particular server provides the data. In this case the data must be discoverable over the network. The **UA Semantic Data** can be used to meet this requirement. In this approach, the data URI can be used as a key to browse the *Global Data Discovery System* (GDDS – an expanded version of GDS) to find recursively the destination *OPC UA Server* exposing the requested data. In this scenario we can use a well-known two-step process:

1. discover the server using the resource (**UA Semantic Data**) unique domain based identifier - URI;
2. browse or update the data.

**Challenge 1**: A HMI prepared in advance to display the state of boilers formally described by the `BoilerType` shall display the `Boiler #1` state but the server exposing the data is unknown and may change occasionally (e.g. non transparent redundancy).

For this scenario we can apply the following work-flow:

1. HMI (*OPC UA Client*) using URI of the `Boiler #1` browses recursively *Global Data Discovery System* starting form a local server to find a discovery server having all needed information about the *OPC UA Server* exposing `Boiler #1` `Object` node representing a physical object named `Boiler #1`.
2. HMI establishes *OPC UA Session* with the discovery server and gets all needed information.
3. HMI establishes *OPC UA Session* with the destination *OPC UA Server* to subscribe for requested data and events.
4. In case the current server has been shut down HMI repeats the procedure starting from 1 and connects to another server if any.

The data discovery mechanism is a part of the broader concept captured in the document [OPC Unified Architecture - Object Oriented Internet](../README.md), but its detailed description is out of scope of this document.

## UA Information Model

The description of the *OPC UA Information Model* is captured in details in in the:
* [OPC Unified Architecture e-book](http://www.commsvr.com/UAModelDesigner/Index.aspx)
* [OPC UA Information Model Deployment]( http://www.cas.internetdsl.pl/commserver/P_DowloadCenter/P_Publications/20140301EN_DeploymentInformationModel.pdf)

## Interoperable parties

### UA Data Application

OPC **UA Data Application** Program (shortened to **UA Data Application**) is any software program designed to perform a specific function directly for the user or for other application programs. Application programs process (consume and produce) the OPC **UA Semantic Data** according to selected algorithm to meet the user requirements.

The **UA Data Application** gets access to the **UA Semantic Data** as:
* Networked computer device
* Client of Database Management System

### Address Space Management

The **Address Space Management** is a typical part of any *OPC UA Server*. It is responsible for:

* *OPC UA Address Space* instantiation – the creation and management of the server address space
* **Data Binding** - managing of the links to the underlying process **Raw Data**
* **Integration** services - optionally supporting interoperability with other parties outside the server

The OPC UA specification introduces a node notion as an atomic addressable entity that consists of attributes (value-holders) and references (address-holders of coupled nodes). The set of nodes that an *OPC UA Server* makes available to clients is referred to as its address space, which enables representation of both underlying processes environment description and the process state/behavior. The address space exposed by the server makes up a context the **Raw Data** is made available to the clients in. Instantiation of this context depends on an application domain unique *OPC UA Information Model* and is governed by the *OPC UA Address Space Model* rules.

The **UA Server Services** accesses data and metadata and makes them available to the **UA Client** using a set of non-expandable services compliant with the specification.

The **Address Space Management** activity includes also the creation of data bindings with the underlying real-time process (**Raw Data**).  **Data binding** is responsible for coupling  **Raw Data** representing the current state/behavior of the underlying real-time process with the appropriate nodes in the address space. **Data Binding** is, therefore, the process that establishes a connection between the `Variable` node and selected data source. The `Value` attribute of the `Variable` node reflects changes when made. It can also mean that when the `Value` attribute of the `Variable` is changed, the underlying data will reflect that change.

Optionally the **Address Space Management** activity may also provide systems integration services supporting the process of bringing together the component external applications (not OPC **UA Server Services** aware) into one system and ensuring that the applications function together as a whole. It includes but is not limited to linking together different software applications physically or functionally on the basis of common data and metadata compliant with the applied *OPC UA Information Models*. These services should provide the following functions:

* Selection of the *Variable* nodes (integration data) that are to be made available to the external applications           
* Selection of the data exchange channels, i.e. network links, databases, etc.
* Data security protection services
* Data discovery support services   

Optionally the **Address Space Management** could expose supplementary **UA Information Model** aimed to control remotely the data exchange channels by an *OPC UA Client* customized to be compliant with this model.

The underlying process **Raw Data** may also be processed by any **Application** simultaneously. This kind of activity is outside the presented concept scope.

### UA Client

It is an embedded part of the **OPC UA Client**. **OPC UA Client** shall be compliant with Part 4. It consumes all not optional services required by Part 4 of the specification and must be compliant with the selected existing profile. To get data from the *OPC UA Server* it must establish the session.

Optionally, the **OPC UA Client** activity may also provide systems integration services supporting the process of bringing together the component external applications (not OPC UA Server Services aware) into one system and ensuring that the applications function together as a whole. It includes but is not limited to linking together different software applications physically or functionally on the basis of common data and metadata compliant with the applied OPC UA Information Models.  These services should provide the following functions:

* Selection of the Variables (integration data) that are to be made available to the external applications
* Selection of the data exchange channels, i.e. network links, databases, etc.
* Data security protection services
* Data discovery support services

Optionally the **OPC UA Client** may also support importing and exporting services of the data to external semantics (**Raw Data**).

# OPC UA Semantic Data Deployment

Meaningful processing of the *OPC UA Data* by variety of applications is an important part of a broader concept described in the document [OPC Unified Architecture - Object Oriented Internet](../README.md).

A detailed description of this solution is captured in the document [Content Description](./Readme.SemanticDataDeplyment.MD). The solution contains projects supporting deployment of the concept presented in this document.

The code illustrating the **UA Semantic Data** paradigm implementation can be found in the project [DataManagement](./DataManagement).
