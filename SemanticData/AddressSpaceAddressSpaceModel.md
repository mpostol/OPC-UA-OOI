# Address Space and Address Space Model

## Address Space

The primary objective of the OPC UA server is to expose information that can be used by clients to manage an underlying real-time process and the entire enterprise as a large whole with the main challenge of integrating systems and management resources into one homogeneous environment. Information describes the state and behavior of the process and the server must be able to transfer it in both directions. The main challenge of the OPC UA Address Space is to support this transfer in a unique and transparent way in spite of the process complexity and roles of clients in the enterprise management hierarchy.

To make the data available for further processing by computer systems it must be assured that the data is:

- **transferable** – there must exist mechanisms to transfer the data over the network,
- **addressable** – there must exist services to selectively access the data,
- **meaningful** – there must exist rules (unambiguous for all interoperating parties) how to apply the semantics to bit patterns.

OPC UA Address Space concept is all about exposing the data in a standard way, so it must address the above mentioned issues, but the description of mechanisms involved in the data transfer is outside this section scope.

Generally speaking, to select a particular target piece of complex data we have two options: random access or browsing. Random access requires that the target item must have been assigned a globally unique address and the clients must know it in advance. We call them well-known addresses. The browsing approach means that the clients walk down available paths from entity to entity that build up the structure of data - a data graph - using references interconnecting entities. This process is costly because instead of jumping to a target, we need to discover the graph step by step using references. The main advantage of this approach is that the clients do not need any prior knowledge of the data structure – the clients of this type are called generic clients. To minimize the cost, after having found the target, every operation targeting it can use direct access. Random access is possible since the browsing path is convertible to a globally unique direct address.

It seems that, in spite of the access method, we have to assign an address to all of the accessible items in the representation of the data structure. We therefore call the collection of these items the Address Space [\[1\]][CAS.ASMD], [2], [\[3\]][Opc.UA.Part3]. 

> In this concept this atomic addressable item is called a node.  Each node is a collection of attributes (value-holders) that have values accessible locally in context of the node. To enable browsing, i.e. to represent information about the internal structure of the nodes graph, nodes are interconnected by references (address-holders of coupled nodes).

## Address Space Meta-Model

The main goal of exposing a graph of nodes to clients is to create a meaningful context for the underlying process data. To create the Address Space, we need to instantiate nodes and interconnect them by references. Instantiating nodes requires assigning appropriate values to attributes. To make information internally consistent as a large whole, we need rules governing the creation and modification processes, i.e. Address Space Meta-Model. According to the model, the roles of nodes in the graph are well defined as a result of the definition of a set of types called  `NodeClass`. In other words, the node is an instance of the selected `NodeClass`. Available `NodeClass` types are predefined, i.e. the Address Space Meta-Model provides a strictly defined and non-extensible set of `NodeClass` types.  Each one is assigned a dedicated function to represent well-defined information at run-time. `NodeClass` is a formal description of the node defining the allowed attributes and references. Each node must be an instance of the selected `NodeClass`.

![Figure 1. Address Space Model](../CommonResources/Media/InformationModelClassDisgram.png)

The Address Space Model defines the following set of `NodeClass` types (Figure 1):

- `View`: defines a subset of nodes in the Address Space.
- `ObjectType`: provides definition for objects.
- `Object`:  is used to represent systems, system components, real-world objects and software objects.
- `ReferenceType`: is used to define the meaning of the nodes relationship.
- `DataType`: is used to define simple and complex data types of the Variable values.
- `VariableType`: is used to provide type definition for variables.
- `Variable`: is used as real-time process data holders, i.e. it provides a value.
- `Method`: is a lightweight function, whose scope is bounded by an owning object.

All the presented in above figure types derive from common Root `NodeClass`.

Each View `NodeClass` represents a subset of the nodes in the Address Space. The entire Address Space is the default view. Each node in a view may contain only a subset of its references, as defined by the creator of the view. The View instance acts as the root for the nodes in the represented view.

`ObjectType` provides definition for objects. `Object` instance are used to represent systems, system components, real-world objects and software objects.

It is worth noting that the `ReferenceType` are visible in the Address Space. In contrast, a reference instance of this type is an inherent part of a node and no `NodeClass` is used to represent reference instances. In other words, any node is a collection of references, so there is no need to instantiate an additional object as reference with the role of a nodes couple.

The `Variable` node is used to be a holder of the process data – it has a `Value` attribute. To be used as the real-time process state representation, the value of the `Value` attribute must be bound to a real data source, e.g. a sensor or actuator. The `Method` node represents functions that can be called by the clients connected to the server. In this case the real-time process bindings are responsible for conveying the parameters current values, invoking the represented function and returning the execution result. Both classes are the main building blocks that allow the server to couple the exposed address space` with the current state and behavior of the underlying process.

The `DataType` is used to define simple and structured data types. Data types are used to describe the bits pattern of the `Value` attribute of `Variable` and their `VariableType` nodes. Therefore each `Variable` and `VariableType` refers to an instance of the `DataType` `NodeClass`.

Address Space Meta-Model is an intermediate language used by the Information Model to formally describe the content of the Address Space instance. Detailed description is covered in the section [UA Information Model - Concept]. 

Accessing information by clients is the first aspect of controlling the information stream between the clients and underling process. Another one is creating and maintaining the Address Space in real-time. This activity includes also creation of data bindings with the underlying real-time process. This topic is described in more details in the section [*Address Space Model Life-cycle*][ASMLC].

## See also

- [1] [OPC UA Address Space Model Designer, 2019][CAS.ASMD]
- [2] Wolfgang Mahnke, Stefan Helmut Leitner, Matthias Damm. OPC Unified Architecture. Berlin: Springer, 2009.
- [3] [OPC Unified Architecture Specification Part 3: Address Space Model, OPC Foundation, Rel. 1.04, 2017-11-22][Opc.UA.Part3]
- [4] [Address Space Model Life-cycle][ASMLC]
- [5] [UA Information Model - Concept]

[Opc.UA.Part3]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-3-address-space-model/
[CAS.ASMD]: http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx
[UA Information Model - Concept]:InformationModelConcept.md
[ASMLC]:InformationModelLifecycle.md

