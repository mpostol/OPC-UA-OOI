
# Using OPC UA Data Ouside of the Server

## Domain Model

Main challenge that must be faced up by engineering of the data processing process is preservation of the data semantics at all the stages of this process. This diagram is a domain model, which presents a high level conceptual framework of the **UA Semantic Data** supporting contextual data processing outside of the *OPC UA Server*. It presents all the major contributors processing the data in context of **UA Information Model**.

**UA Semantic Data** is directly used by the following classes of actors:
* **Address Space Management** -  part of the *OPC UA Server* optionally supporting integration services   
* **UA Client** -  **OPC UA Client** supporting integration services    
* **UA Data Application**  -  an application program processing the data in context of the OPC UA metadata without the *OPC UA Server* session.  

Integration services are responsible for exchanging the data and metadata with other parties using:
* Network communication
* Database access   

![Domain Model] (https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/Media/UADataIntegrationServices.Domain.png)

## UA Semantic Data

The OPC **UA Semantic Data** represents the following triple:

* Globally Unique Identifier   
* Data Type as an Entity of the OPC UA Information Model   
* Data Object in Context of OPC UA Address Space   

Any data object is always just a stream of bits. To understand the binary data we must have defined a data type – a description how to create an appropriate bits pattern. Simplifying, the data type determines a set of valid values and rules needed to assign the information (understand the data) to a selected bits pattern.

OPC UA specification provides additional possibility to define meaning of the data, i.e. exposing the data in the context of metadata. In this case the data is made available as the *Value* attribute of the *Variable* node and surrounded by other nodes creating part of relationship.

Unique identifier and both methods of data meaning definition makeup a concept of OPC UA Semantic Data. Again this concept can be implemented using the OPC UA Information Model to define types and OPC UA Address Space Model to expose the data in the context of metadata.

To keep all benefits of the OPC UA Semantic Data concept the remote end users (UA Data Application) are interested to process the data using relevant part of the OPC UA Address Space.

Some scenarios where the OPC UA Semantic Data is used requires that the data is protected against:

* Unauthorized Access  
* Non-repudiation
* Integrity

Each node in the OPC UA Address Space is a root of a tree structure created by nodes recursively pointed out by HierarchicalReferences in forward direction. Because each node in this hierarchy is globally recognizable it may be protected as a resource by any independent protection system (for example any Kerberos protocol implementation) supporting authentication and authorization operation and responsible to distribute security tokens used to protect the data as stated above. Hierarchical relationship supports permissions inheritance, i.e. permissions propagation to the children nodes.
## Interoperable parties

### UA Data Application

OPC **UA Data** Centric **Application** Program (shortened to **UA Data Application**) is any software program designed to perform a specific function directly for the user or for another application programs. Application programs processes (consume and produce) the OPC UA Data according to selected algorithm to meet the user requirements.

The **UA Data Application** gets access to the **UA Semantic Data** as:
* Networked computer device;
* Client of DataBase Management System;

### Address Space Management

TBD

### UA Client

TBD

## UA Information Model

The description of the OPC UA Information Model is captured in detail in:
* [OPC Unified Architecture e-book]([http://www.commsvr.com/UAModelDesigner/Index.aspx)
* [OPC UA Information Model Deployment]( http://www.cas.internetdsl.pl/commserver/P_DowloadCenter/P_Publications/20140301EN_DeploymentInformationModel.pdf)

# Projects Description

## UANodeSetValidation

### Description

The [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) project is aimed to validate UANodeSet xml files.
To validate a new model add the code to the [USNodeSetValidationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/USNodeSetValidationUnitTestProject)
and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved.
In this case add issue or modify the code on your fork and add purge request after finishing.

### Public API

Public API of the [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) is described by the interface:
*IAddressSpaceContext*

This interface may be used for dependency injection.
