# OPC UA Object Model Working Notes

## Address Space Concept Executive Summary

The primary objective of the OPC UA application is to expose information that can be used by other OPC UA applications aimed at managing an underlying process with the main challenge of integrating systems into one homogeneous foundation. It requires an exchange of information over a computer network as bitstreams. To make the information available for further processing by computer systems it must be assured that it is:

- **transferable** – there must exist mechanisms to transfer the data over the network
- **meaningful** – there must exist rules (unambiguous for all interoperating parties) on how to map the meaning and bitstreams (data)
- **addressable** – there must exist services to selectively access the data

To promote interoperability in the multi-vendor environment the services fulfilling these functionality must be standardized.

The discussion related to the data transfer is outside the scope of this document.

Based on the role humans take while using OPC UA applications they can be grouped as follows:

- **human-centric** - information origin or ultimate information destination is an operator,
- **machine-centric** - information creation, consumption, networking, and processing are achieved entirely without human interaction.

A typical **human-centric** approach is a web-service supporting, for example, a web user interface (UI) to monitor conditions and manage millions of devices in a typical cloud-based IoT approach. It is essential in this case that any uncertainty and necessity to make a decision can be relaxed by human interaction. Coordination of robot behaviors in a work-cell (automation islands) is a **machine-centric** example. In this case, any human interaction must be recognized as impractical or even impossible. This interconnection scenario requires the machine to machine communication (M2M) demanding the integration of multi-vendor devices.

To leverage the **meaningful** data distribution, the OPC UA engages rules derived from the object-oriented programming concept. Following this approach types are commonly used to describe the data semantics (to assign meaning to the bitstreams). For example, using Int32 we are dealing with a set of numbers that can be represented as bitstreams 32 bits long. Unfortunately, sometimes it is not enough. Let assume that we are going to use these numbers to express the age in a personal record. In the  **human-centric** environment, we can use the appropriate names derived from the native language of the data holders called variables. For the **machine-centric** case, the multi-vendor environment must be considered. A typical approach to deal with this environment is the usage of names defined by a commonly acceptable standardization body. To make the name unambiguous 9to avoid name collision) for all vendors it must be globally unique.

Generally speaking, to select a particular target piece of complex data we have two options: **random access** or **browsing**. **Random-access** requires that the target item must have been assigned a unique address known in advance by a selection operation. The browsing approach means that the data consumer walks down available paths from an entity to an entity that builds up the structure of compound data - a data graph - using references interconnecting entities. It is necessary if we need to represent a relationship between data components. As an example, consider a family tree containing a graph of personal records. The browsing process is costly because instead of jumping to a target, we need to traverse the graph step by step using references. The main advantage of this approach is that the data consumer do not need any prior knowledge of the data structure. To minimize the cost, after having found the target as the result of browsing the graph, every operation targeting it can use direct access. Random access is possible only if the browsing path is convertible to a unique direct address or selected targets have well know addresses assigned by a standardization body.

It seems that, despite the access method, we have to assign an identification to all of the accessible entities in the representation of the process data structure. In this concept, this atomic identifiable entity is called a node. Each node is a collection of attributes (value-holders) that have values accessible locally in the context of the node. To enable browsing the internal structure of the nodes graph (relationship information), nodes are interconnected by references (address-holders of coupled nodes). Taking into consideration that the browse mechanism is based on the incremental and relative passage along the path of interconnected nodes, we can easily find out that each path must have a defined entry point, so we must address the question of where to start.

The collection of these nodes is called the **address space**. The OPC UA Address Space concept is all about exposing the process data in a standard way. The main goal of exposing a graph of nodes as one whole is to create a meaningful context for the underlying process data. To create the Address Space, we need to instantiate nodes and interconnect them by references.

To instantiate the **address space** we need to deal with naming, addressing, and meaning of the nodes. Appropriate naming is helpful in the **human-centric** environment, especially at the design-time. Proper addressing is essential for **machine-centric** environment, especially at the run-time. Designing appropriate rules applied to make the **address space** meaningful is necessary for both and must be addressed by the information model design process. All mentioned above aspects are tightly coupled and contribute to the design process.  The design process can be backed by:

- design conventions - contributing to design best practice rules
- OPC UA concepts - as a foundation of AS deployment addressing a selected process requirements
- design tool - to author reusable in the multi-vendor market comprehensive information model

The following section covers a detailed description of the design conventions to improve reusability, comprehensiveness and minimize the deployment costs in the production environment.

OPC UA engages the following concepts supporting the mentioned above topics, namely naming, addressing, and meaning associations:

- `BrowseName` attribute - to support browsing and meaning association  
- `DisplayName` attribute - to enable comprehensive description using native languages
- `NodeId` attribute - to implement the nodes direct addressing
- `Reference` - to apply nodes relationship information
- `Type` concept - to provide metadata used as a meaningful context for the process data.

To create the address space exposed by an OPC UA Application it must instantiate all nodes and interconnect them through references at the bootstrap process. Before the address space can be instantiated by an OPC UA application it must be designed first. To promote reusability of the address space design process a Domain Specific Language (DSL) is required. A detailed description of this process is covered by the document [Address Space Model Life-cycle](https://commsvr.gitbook.io/ooi/semantic-data-processing/informationmodelsdevelopment/informationmodellifecycle). A mandatory option - coined as `NodeSet` model - of the DSL is described in OPC UA Specification [Part 6: Mappings][Opc.UA.Part6]. By design, it minimizes the required effort spent by the OPC UA applications to instantiate the address space because it requires a detailed description of all implementation details enabling to avoid the necessity to resolve inheritance chains, type definitions, encodings, direct addressing, defaults, etc. A detailed description of this DSL is covered by the document [OPC UA Address Space Interchange XML][InterchangeXML]. As a result, it is expected that all OPC UA applications and design tools must be compliant with this language somehow.

This standard additionaly introduces the term

- `SymbolicName` - an identifier that uniquely identifies a specific entity in a program or procedure.

## Naming Conventions for Nodes

### General Rules for BrowseName Attribute

OPC UA defines two attributes containing naming information about an OPC UA Node, the `BrowseName` and the `DisplayName`. The `NodeSet` DSL additionally introduces `SymbolicName`.

The `BrowseName` is of the `QualifiedName` type. Each complex value of this type contains `namespaceIndex` and `name` fields. The `namespaceIndex` field is an index that identifies the namespace (a set of unique names) that defines the context of the name. This index is a  selector of that namespace in an array of namespace entries in concern. This array may be used to access the actual value of the namespace selected by the `namespaceIndex`.

Namespace concept is used by OPC UA to create unique identifiers across different naming authorities. The attributes NodeId and BrowseName are identifiers. A Node in the UA AddressSpace is unambiguously identified using a NodeId. Unlike the NodeId identifier, the BrowseName cannot be used to unambiguously identify a Node. Different Nodes may have the same `BrowseName`.

The `BrowseName` are used:

- to build a browse path between Nodes
- define a globaly unique meaning of an entity, e.g. properties, types, etc

In case the `BrowseName` is applied to build a browse path its uniqueness is resolved in the context of a parent node. Unless the `BrowseName` is assigned a globally specific meaning defined independently the `namespaceIndex` shall be the same as assigned to `NodeId` attribute of the hosting Node. It is recommended that Nodes defined in any custom model (including but not limited to Companion Specification) should use a Namespace of the model for their NodeId and BrowseName attributes.

If `BrowseName` is associated with a globally unique meaning it shall be defined in the context of a dedicated namespace. For this purpose, standardization organizations (naming authorities) shall define independent globally unique identifiers (e.g. URI) that must be added to the local namespaces array and to be indexed by the `BrowseName` value.

For the `name` part of the `BrowseName` attribute, the following naming conventions apply.

The `name` field value should be the upper camel case (also known as PascalCase), that is, all words are written without spaces (concatenated), and the first character of each word is the upper case letter, the other characters are lower case or digits. Examples: ReferenceType, BaseObjectType, Int32.

If an acronym or abbreviation is used, upper camel case should also be used. Examples: PortMacAddress (where MAC is an acronym for Media Access Control), NodeId (where ID is an
abbreviation for identyfier), UInt32 (where U is an abbreviation for unsigned). In general, it is recommended to only use letters, digits or the underscore (‘_’) as characters for the `BrowseName`.  unless it is explicitly defined like “<” and “>” for optional placeholders.

> Remark: If special chars like “&”, “<”, etc. are used, the `NodeSet` document should define the `SymbolicName` attribute for that Node as well. This can then be used for code generation.

There is no recommendation on the use of prefixes. Companion Specifications may use a prefix because it suits their model. For example, if the Vision companion specification were to define types based on generic concepts (say a state machine), then using the prefix “Vision” may make sense (as in “VisionStateMachineType”).

#### Parametrysation

Special characters may be used for parametryzation of the BrowseName to create several copies of the same node. In this case the `BrowseName` amy be used as a pattern of the valuess assigned to new instances created this way.

> What is the impact on the `SymbolicName` ?

#### Syntax

Regex first attempt:

```TXT
/^(\s+)?((\d{1,}+):)?([^:\s][\w\S]+)/gm
^(\s+)?((\d+):)?([a-zA-z0-9_]+)
/^(\s+)?\d+:[a-zA-z0-9_]+$|^(\s+)?[a-zA-z0-9_]+$/gm
/^(\s+)?\d+:[a-zA-z0-9_]+$|^(\s+)?[a-zA-z0-9_]+$|\S+/gm
```

```C#
using System;
using System.Text.RegularExpressions;

public class Example
{
    public static void Main()
    {
        string pattern = @"^(\s+)?((\d{1,}+):)?([^:\s][\w\S]+)";
        string input = @"       123456:_ab_98AS   
       :_ab_98AS
    1111112@#hdshdskk8878787*&&&*) 32322\/?\:";
        RegexOptions options = RegexOptions.Multiline;
        
        foreach (Match m in Regex.Matches(input, pattern, options))
        {
            Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
        }
    }
}
```

### General Rules for DisplayName Attribute

### General Rules for NodeId Attribute

  <xs:simpleType name="NodeId">
    <xs:restriction base="xs:string" />
  </xs:simpleType>


### General Rules for SymbolicName Attribute

Accordin to the specyfication it can be used as a class/field name in auto generated code. It should only be specified if the `BrowseName` cannot be used for this purpose.

This xml attribute does not appear in the AddressSpace and is intended for use by design tools. Only letters, digits or the underscore (‘_’) are permitted. The detailed syntax definition is as follows by the type `SymbolicName`

```XML
  <xs:simpleType name="SymbolicName">
    <xs:list>
      <xs:simpleType>
        <xs:restriction base="xs:string">
          <xs:pattern value="[A-Za-z][A-Za-z0-9_]*" />
        </xs:restriction>
      </xs:simpleType>
    </xs:list>
  </xs:simpleType>
```

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

- [P4 5.8.4 TranslateBrowsePathsToNodeIds](https://reference.opcfoundation.org/v104/Core/docs/Part4/5.8.4/)
- [Postół M. (2016) OPC UA Address Space Interchange XML; Technical Report; DOI: 10.13140/RG.2.2.12228.37768][InterchangeXML]
- [OPC Unified Architecture Specification Part 6: Mappings, OPC Foundation, Rel. 1.04, 2017-11-22][OPC.UA.Part6]

[InterchangeXML]: https://www.researchgate.net/publication/334259707_OPC_UA_Address_Space_Interchange_XML
[Opc.UA.Part6]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-6-mappings/
[InterchangeXML]: https://www.researchgate.net/publication/334259707_OPC_UA_Address_Space_Interchange_XML