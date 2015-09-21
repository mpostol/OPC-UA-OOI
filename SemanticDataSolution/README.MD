
# Using OPC UA Data Ouside of the Server
## Domain Model

![Domain Model](\Media\UADataIntegrationServices.Domain.png)

## UA Semantic Data

The OPC UA Semantic Data represents the following triple:

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

Each node in the OPC UA Address Space is a root of a tree structure created by nodes recursively pointed out by HierarchicalReferences in forward direction. Because each node in this hierarchy is globally recognizable it may be protected as a resource by any independent protection system (for example any Kerberos protocol implementation) supporting authentication and authorization operation and responsible to distribute security tokens used to protect the data as stated above. Hierarchical relationship supports permissions inheritance, i.e. propagation to the children nodes.

# Projects Description

## UANodeSetValidation


The [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) project is aimed to validate UANodeSet xml files.
To validate a new model add the code to the [USNodeSetValidationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/USNodeSetValidationUnitTestProject)
and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved.
In this case add issue or modify the code on your fork and add purge request after finishing.

# Public API

Public API of the [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) is described by the interface:
*IAddressSpaceContext*

This interface may be used for dependency injection.
