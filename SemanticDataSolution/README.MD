
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

The OPC **UA Semantic Data** represents the following triple:

* Uniform Resource Identifier (URI)
* Type as an entity of the *OPC UA Information Model*
* A set of related nodes in the context of *OPC UA Address Space*

 Using the URI as a standard for global identifiers allows for a worldwide reference for any data. This means that we can tell when any two applications anywhere in the world are referring to the same data. It could answer the question when is a data in one location the same data as a data in another location? Using URI, therefore, we can introduce the notion of the global data identity. The data identity allows creation of variety of dictionaries collecting supplementary information independently and outside of the server Address Space context.

 Value of the data can change in time. It is worth nothing that in the presented concept if two instances of the data has the same URI it does not mean that both have the same value, i.e. the same bits pattern. For example if we are measuring the temperature in the output pipe of the selected boiler with the 1 second interval, we are getting a new value every second. The URI cannot be used to distinguish instances among each other, but still it could be used to use common key and sign each value, and finally send it over the wire to a destination application. The destination application can use recognizable key (indexed by the URI) retrieved from a dictionary and check the data against the not-repudiation security scenario.

To associate the type and the data the URI must be defined unambiguously in context of the selected *OPC UA Information Model*. 


TBD #47: Define Globally Unique Identifier using terms: NamespaceUri, BrowseName, BrowsePath, SymbolicName.

TBD Using the URI as a standard for global identifiers allows for a worldwide reference for any symbol. This means that we can tell when any two people anywhere in the world are referring to the same thing.


Any data object is always just a stream of bits. To understand the binary data we must have defined a data type – a description how to create an appropriate bits pattern. Simplifying, the data type determines a set of valid values and rules needed to assign the information (understand the data) to a selected bits pattern.

OPC UA specification provides additional possibility to define meaning of the data, i.e. exposing the data in the context of metadata. In this case the data is made available as the *Value* attribute of the *Variable* node and surrounded by other nodes creating part of relationship.

Unique identifier and both methods of data meaning definition makeup a concept of OPC **UA Semantic Data**. Again this concept can be implemented using the *OPC UA Information Model* to define types and *OPC UA Address Space Model* to expose the data in the context of metadata.

To keep all benefits of the OPC **UA Semantic Data** concept the remote end users (UA Data Application) are interested to process the data using relevant part of the *OPC UA Address Space*.

Some scenarios where the OPC **UA Semantic Data** is used requires that the data is protected against:

* Unauthorized Access  
* Non-repudiation
* Integrity

Each node in the *OPC UA Address Space* is a root of a tree structure created by nodes recursively pointed out by HierarchicalReferences in forward direction. Because each node in this hierarchy is globally recognizable it may be protected as a resource by any independent protection system (for example any Kerberos protocol implementation) supporting authentication and authorization operation and responsible to distribute security tokens used to protect the data as stated above. Hierarchical relationship supports permissions inheritance, i.e. permissions propagation to the children nodes.

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

TBD #47:

# OPC UA Semantic Data Deployment

TBD Whht is OPC UA Semantic Data Deployment

The detailed description of the solution content is captured in the document [Content Description](./Readme.SemanticDataDeplyment.MD)
