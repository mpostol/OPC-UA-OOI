# OPC UA Object Model Working Notes

## Address Space Concept Executive Summary

The primary objective of the OPC UA application is to expose information that can be used by other OPC UA applications aimed at managing an underlying process with the main challenge of integrating systems into one homogeneous foundation. It requires an exchange of information over a computer network as bitstreams. To make the information available for further processing by computer systems it must be assured that it is:

- **transferable** – there must exist mechanisms to transfer the data over the network
- **meaningful** – there must exist rules (unambiguous for all interoperating parties) on how to map the meaning and bitstreams (data)
- **addressable** – there must exist services to selectively access the data

To promote interoperability in the multi-vendor environment the services fulfilling appropriate functionality must be standardized.

The discussion related to the data transfer is outside the scope of this document and will be skipped.

Based on the role humans take while using OPC UA applications they can be grouped as follows:

- **human-centric** - information origin or ultimate information destination is an operator,
- **machine-centric** - information creation, consumption, networking, and processing are achieved entirely without human interaction.

A typical **human-centric** approach is a web-service supporting, for example, a web user interface (UI) to monitor conditions and manage millions of devices and their data in a typical cloud-based IoT approach. In this case, it is characteristic that any uncertainty and necessity to make a decision can be relaxed by human interaction. Coordination of robot behaviors in a work-cell (automation islands) is a **machine-centric** example. In this case, any human interaction must be recognized as impractical or even impossible. This interconnection scenario requires machine to machine communication (M2M) demanding the integration of multi-vendor devices.

To leverage the **meaningful** data distribution, the OPC UA engages rules derived from the object-oriented programming concept. Following this approach types are commonly used to describe the data semantics (to assign meaning to the bitstreams). For example, using Int32 we are dealing with a set of numbers that can be represented as bitstreams 32 bits long. Unfortunately, sometimes it is not enough. Let assume that we are going to use these numbers to express the age in a personal record. In the  **human-centric** environment, we can use the appropriate names derived from the native language of the data placeholders called variables. For the **machine-centric** case, the multi-vendor environment must be considered. A typical approach to deal with this environment is the usage of names defined by a commonly acceptable standardization body. To make the name unambiguous for all vendors it must be globally unique.

Generally speaking, to select a particular target piece of complex data we have two options: **random access** or **browsing**. **Random-access** requires that the target item must have been assigned a unique address that must be known in advance by a selection operation. The browsing approach means that the data consumer walks down available paths from an entity to an entity that builds up the structure of compound data - a data graph - using references interconnecting entities. It is necessary if we need to represent a relationship between data components. As an example, consider a family tree containing a graph of personal records. The browsing process is costly because instead of jumping to a target, we need to traverse the graph step by step using references. The main advantage of this approach is that the data consumer do not need any prior knowledge of the data structure. To minimize the cost, after having found the target as the result of browsing the graph, every operation targeting it can use direct access. Random access is possible only if the browsing path is convertible to a unique direct address or selected targets have well know addresses assigned by a standardization body.

It seems that, despite the access method, we have to assign an address to all of the accessible entities in the representation of the process data structure. In this concept, this atomic addressable entity is called a node. Each node is a collection of attributes (value-holders) that have values accessible locally in the context of the node. To enable browsing the internal structure of the nodes graph (relationship information), nodes are interconnected by references (address-holders of coupled nodes). Taking into consideration that the browse mechanism is based on the incremental and relative passage along the path of a node, we can easily find out that each path must have a defined entry point, so we must address the question of where to start.

The collection of these nodes is called the Address Space. OPC UA Address Space concept is all about exposing the process data in a standard way. The main goal of exposing a graph of nodes as one whole is to create a meaningful context for the underlying process data. To create the Address Space, we need to instantiate nodes and interconnect them by references.

## Naming Conventions for Nodes

### Introduction

OPC UA defines two attributes containing naming information about an OPC UA Node, the BrowseName and the DisplayName.

Recommendations for DisplayName will be given in a later version of this document.

The BrowseName is of DataType QualifiedName, containing a NamespaceIndex and a String. Unless
the BrowseName is defined in some other Namespace or there is some specific handling for the BrowseName, the Namespace for the BrowseName should be the one the Node is defined in (i.e. the same Namespace as the NodeId). Nodes defined in a Companion Specification should use the Namespace of the Companion Specification for their NodeIds and BrowseNames.
For the string-part the following naming conventions apply:

### General Rules for BrowseNames

All BrowseNames should be upper camel case (also known as PascalCase), that is, all words written without spaces, and the first character of each word is upper case, the other characters are lower case. Examples: ReferenceType, BaseObjectType, Int32.

If an acronym or abbreviation is used, upper camel case should also be used. Examples: PortMacAddress (where MAC is an acronym for Media Access Control), NodeId (where ID is an
abbreviation for identification), UInt32 (where U is an abbreviation for unsigned). In general, it is recommended to only use letters, digits or the underscore (‘_’) as characters for the BrowseName for TypeDefinitions (ObjectTypes, VariableTypes, DataTypes, ReferenceTypes and InstanceDeclarations), unless it is explicitly defined like “<” and “>” for optional placeholders.

> Remark: If special chars like “&”, “<”, etc. are used, the NodeSet-File should define the optional SymbolicName for that Node. This can then be used for code generation.

There is no recommendation on the use of prefixes. Companion specifications may use a prefix because it suits their model. For example, if the Vision companion specification were to define types based on generic concepts (say a state machine), then using the prefix “Vision” may make sense (as in “VisionStateMachineType”).

## UANodeSet validation

### XML Import validation

- text syntax against the XML rules
- XML syntax against the selected schema (UANodeSet in this case)
- XML syntax against the OPC UA spec (e.g. QualifiedName, NodeId, etc) if the schema is not prcise enough

### XML Semantic validation

- XML semantics against the OPC UA spec

## Model

The version information is also provided as part of the ModelTableEntry in the UANodeSet XML file. The UANodeSet XML schema is defined in OPC 10000-6.

The NamespaceUri for all NodeIds defined in this document is defined in Annex A. The NamespaceIndex for this NamespaceUri is vendor-specific and depends on the position of the NamespaceUri in the server namespace table.

Namespaces are used by OPC UA to create unique identifiers across different naming authorities. The Attributes NodeId and BrowseName are identifiers. A Node in the UA AddressSpace is unambiguously identified using a NodeId. Unlike NodeIds, the BrowseName cannot be used to unambiguously identify a Node. Different Nodes may have the same BrowseName. They are used to build a browse path between two Nodes or to define a standard Property.

## AS graph

`HasSubtype` References are used to define subtypes of `ReferenceTypes`. It is not required to provide the HasSubtype Reference for the supertype. 

If a `ReferenceType` specifies some constraints this is inherited and can only be refined (be more restrictive).

ObjectType is the base ObjectType and all other ObjectTypes shall either directly or indirectly inherit from it. However, it might not be possible for Servers to provide all `HasSubtype` References from this `ObjectType` to its subtypes, and therefore it is not required to provide this information.

## Instance Declaration

### [P3 4.5 TypeDefinitionNode](https://reference.opcfoundation.org/v104/Core/docs/Part3/4.5.1/)

### [7.10 HasSubtype ReferenceType](https://reference.opcfoundation.org/v104/Core/docs/Part3/7.10/)

The semantic of this ReferenceType is to express a subtype relationship of types. It is used to span the ReferenceType hierarchy, whose semantic is specified in 5.3.3.3; a DataType hierarchy is specified in 5.8.3, and other subtype hierarchies are specified in Clause 6.

[5.3.3.3 HasSubtype References](https://reference.opcfoundation.org/v104/Core/docs/Part3/5.3.3/#5.3.3.3) HasSubtype References are used to define subtypes of ReferenceTypes. It is not required to provide the HasSubtype Reference for the supertype, but it is required that the subtype provides the inverse Reference to its supertype. The following rules for subtyping apply.

1. The semantic of a ReferenceType (e.g. “spans a hierarchy”) is inherited to its subtypes and can be refined there (e.g. “spans a special hierarchy”). The DisplayName, and also the InverseName for non-symmetric ReferenceTypes, reflect the specialization.
1. If a ReferenceType specifies some constraints (e.g. “allow no loops”) this is inherited and can only be refined (e.g. inheriting “no loops” could be refined as “shall be a tree – only one parent”) but not lowered (e.g. “allow loops”).
1. The constraints concerning which NodeClasses can be referenced are also inherited and can only be further restricted. That is, if a ReferenceType “A” is not allowed to relate an 1. 1. Object with an ObjectType, this is also true for its subtypes.
1. A ReferenceType shall have exactly one supertype, except for the References ReferenceType defined in 7.2 as the root type of the ReferenceType hierarchy. The ReferenceType hierarchy does not support multiple inheritances.

[5.8.3 DataType NodeClass](https://reference.opcfoundation.org/v104/Core/docs/Part3/5.8.3/) HasSubtype References may be used to expose a data type hierarchy in the AddressSpace. The semantic of subtyping is only defined to the point, that a Server may provide instances of the subtype instead of the DataType. Clients should not make any assumptions about any other semantic with that information. For example, it might not be possible to cast a value of one data type to its base data type. Servers need not provide HasSubtype References, even if their DataTypes span a type hierarchy. Some restrictions apply for subtyping enumeration DataTypes as defined in 8.14.

[6.3 Subtyping of ObjectTypes and VariableTypes](https://reference.opcfoundation.org/v104/Core/docs/Part3/6.3.1/) 

The HasSubtype ReferenceType defines subtypes of types. Subtyping can only occur between Nodes of the same NodeClass. Rules for subtyping ReferenceTypes are described in 5.3.3.3. There is no common definition for subtyping DataTypes, as described in 5.8.3. The remainder of 6.3 specify subtyping rules for single inheritance on ObjectTypes and VariableTypes.

Subtypes inherit the parent type’s Attribute values, except for the NodeId. Inherited Attribute values may be overridden by the subtype, the BrowseName and DisplayName values should be overridden. Special rules apply for some Attributes of VariableTypes as defined in 6.2.7. Optional Attributes, not provided by the parent type, may be added to the subtype.

As long as those InstanceDeclarations are not overridden they are not referenced by the subtype. InstanceDeclarations can be overridden by adding References, changing References to reference different Nodes, changing References to be subtypes of the original ReferenceType, changing values of the Attributes or adding optional Attributes. In order to get the full information about a subtype, the inherited InstanceDeclarations have to be collected from all types that can be found by recursively following the inverse HasSubtype References from the subtype. This collection of InstanceDeclarations is called the fully-inherited InstanceDeclarationHierarchy of a subtype.

As long as those InstanceDeclarations are not overridden they are not referenced by the subtype. InstanceDeclarations can be overridden by adding References, changing References to reference different Nodes, changing References to be subtypes of the original ReferenceType, changing values of the Attributes or adding optional Attributes. In order to get the full information about a subtype, the inherited InstanceDeclarations have to be collected from all types that can be found by recursively following the inverse HasSubtype References from the subtype. This collection of InstanceDeclarations is called the fully-inherited InstanceDeclarationHierarchy of a subtype.

### [P4 5.8.4 TranslateBrowsePathsToNodeIds](https://reference.opcfoundation.org/v104/Core/docs/Part4/5.8.4/)