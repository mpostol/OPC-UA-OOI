# OPC UA Object Model Working Notes <!-- omit in toc -->

## Table of Content <!-- omit in toc -->

- [Address Space Concept Executive Summary](#address-space-concept-executive-summary)
- [Naming Conventions for Nodes](#naming-conventions-for-nodes)
  - [BrowseName Attribute](#browsename-attribute)
    - [General Naming Rules](#general-naming-rules)
    - [Requirements against the specification](#requirements-against-the-specification)
  - [General Rules for DisplayName Attribute](#general-rules-for-displayname-attribute)
  - [General Rules for NodeId Attribute](#general-rules-for-nodeid-attribute)
  - [General Rules for SymbolicName Attribute](#general-rules-for-symbolicname-attribute)
- [UANodeSet validation](#uanodeset-validation)
  - [XML Import validation](#xml-import-validation)
  - [XML Semantic validation](#xml-semantic-validation)
- [Model](#model)
- [AS graph](#as-graph)
- [Instance Declaration](#instance-declaration)
  - [P3 4.5 TypeDefinitionNode](#p3-45-typedefinitionnode)
  - [7.10 HasSubtype ReferenceType](#710-hassubtype-referencetype)

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

To leverage the **meaningful** data distribution, the OPC UA engages rules derived from the object-oriented programming concept. Following this approach types are commonly used to describe the data semantics (to assign meaning to the bitstreams). For example, using Int32 we are dealing with a set of numbers that can be represented as bitstreams 32 bits long. Unfortunately, sometimes it is not enough. Let assume that we are going to use these numbers to express the age in a personal record. In the  **human-centric** environment, we can use the appropriate names derived from the native language of the data holders called variables. For the **machine-centric** case, the multi-vendor environment must be considered. A typical approach to deal with this environment is the usage of names defined by a commonly acceptable standardization body. To make the name unambiguous (to avoid name collision) for all vendors it must be globally unique.

Generally speaking, to select a particular target piece of complex data we have two options: **random access** or **browsing**. **Random-access** requires that the target item must have been assigned a unique address known in advance by a selection operation. The browsing approach means that the data consumer walks down available paths from an entity to an entity that builds up the structure of compound data - a data graph - using references interconnecting entities. It is necessary if we need to represent a relationship between data components. As an example, consider a family tree containing a graph of personal records. The browsing process is costly because instead of jumping to a target, we need to traverse the graph step by step using references. The main advantage of this approach is that the data consumer do not need any prior knowledge of the data structure. To minimize the cost, after having found the target as the result of browsing the graph, every operation targeting it can use direct access. Random access is possible only if the browsing path is convertible to a unique direct address or selected targets have well know addresses assigned by a standardization body.

It seems that, despite the access method, we have to assign an identification to all of the accessible entities in the representation of the process data structure. In this concept, this atomic identifiable entity is called a node. Each node is a collection of attributes (value-holders) that have values accessible locally in the context of the node. To enable browsing the internal structure of the nodes graph (relationship information), nodes are interconnected by references (address-holders of coupled nodes). Taking into consideration that the browse mechanism is based on the incremental and relative passage along the path of interconnected nodes, we can easily find out that each path must have a defined entry point, so we must address the question of where to start.

The collection of these nodes is called the **address space**. The OPC UA Address Space (AS) concept is all about exposing the process data in a standard way. The main goal of exposing a graph of nodes as one whole is to create a meaningful context for the underlying process data. To create the AS, we need to instantiate nodes and interconnect them by references.

To instantiate the AS we need to deal with naming, addressing, and meaning of the nodes. Appropriate naming is helpful in the **human-centric** environment, especially at the design-time. Proper addressing is essential for **machine-centric** environment, especially at the run-time. Designing appropriate rules applied to make the AS meaningful is necessary for both and must be addressed by the information model design process. All mentioned above aspects are tightly coupled and contribute to the design process.  The design process can be backed by:

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

To create the AS exposed by an OPC UA Application it must instantiate all nodes and interconnect them through references at the bootstrap process. Before the AS can be instantiated by an OPC UA application it must be designed first. To promote reusability of the AS design process a Domain Specific Language (DSL) is required. A detailed description of this process is covered by the document [Address Space Model Life-cycle](https://commsvr.gitbook.io/ooi/semantic-data-processing/informationmodelsdevelopment/informationmodellifecycle). A mandatory option - coined as `NodeSet` model - of the DSL is described in OPC UA Specification [Part 6: Mappings][Opc.UA.Part6]. By design, it minimizes the required effort spent by the OPC UA applications to instantiate the AS because it requires a detailed description of all implementation details enabling to avoid the necessity to resolve inheritance chains, type definitions, encodings, direct addressing, defaults, etc. A detailed description of this DSL is covered by the document [OPC UA Address Space Interchange XML][InterchangeXML]. As a result, it is expected that all OPC UA applications and design tools must be compliant with this language somehow.

This standard additionaly introduces the term

- `SymbolicName` - an identifier that uniquely identifies a specific entity (node) in a program or procedure.

## Naming Conventions for Nodes

### BrowseName Attribute

#### General Naming Rules

OPC UA defines two attributes containing naming information about an OPC UA Node, the `BrowseName` and the `DisplayName`. The `NodeSet` DSL additionally introduces `SymbolicName`.

The `BrowseName` is of the `QualifiedName` type. Each complex value of this type contains `namespaceIndex` and `name` fields. The `namespaceIndex` field is an index that identifies the namespace (a set of unique names) that defines the context of the name. This index is a  selector of that namespace in an array of namespace entries in concern. This array may be used to access the actual value of the namespace selected by the `namespaceIndex`.

Namespace concept is used by OPC UA to create unique identifiers across different naming authorities. The attributes NodeId and BrowseName are identifiers. A Node in the UA AddressSpace is unambiguously identified using a NodeId. Unlike the NodeId identifier, the BrowseName cannot be used to unambiguously identify a Node. Different Nodes may have the same `BrowseName`.

The `BrowseName` are used:

- to build a browse path between Nodes
- define a globally unique meaning of an entity, e.g. properties, types, etc

In case the `BrowseName` is applied to build a browse path its uniqueness is resolved in the context of a parent node. Unless the `BrowseName` is assigned a globally specific meaning defined independently the `namespaceIndex` shall be the same as assigned to `NodeId` attribute of the hosting Node. It is recommended that Nodes defined in any custom model (including but not limited to Companion Specification) should use a Namespace of the model for their NodeId and BrowseName attributes.

If `BrowseName` is associated with a globally unique meaning it shall be defined in the context of a dedicated namespace. For this purpose, standardization organizations (naming authorities) shall define independent globally unique identifiers (e.g. URI) that must be added to the local namespaces array and to be indexed by the `BrowseName` value.

For the `name` part of the `BrowseName` attribute, the following naming conventions apply.

The `name` field value should be the upper camel case (also known as PascalCase), that is, all words are written without spaces (concatenated), and the first character of each word is the upper case letter, the other characters are lower case or digits. Examples: ReferenceType, BaseObjectType, Int32.

If an acronym or abbreviation is used, upper camel case should also be used. Examples: PortMacAddress (where MAC is an acronym for Media Access Control), NodeId (where ID is an
abbreviation for identifier), UInt32 (where U is an abbreviation for unsigned). In general, it is recommended to only use letters, digits or the underscore (‘_’) as characters for the `BrowseName`.  unless it is explicitly defined like “<” and “>” for optional placeholders.

> Remark: If special chars like “&”, “<”, etc. are used, the `NodeSet` document should define the `SymbolicName` attribute for that Node as well. This can then be used for code generation.

There is no recommendation on the use of prefixes. Companion Specifications may use a prefix because it suits their model. For example, if the Vision companion specification were to define types based on generic concepts (say a state machine), then using the prefix “Vision” may make sense (as in “VisionStateMachineType”).

Special characters may be used for parametrization of the `BrowseName` to create several copies of the same node. In this case the `BrowseName` amy be used as a pattern of the values assigned to new instances created this way.

> What is the impact on the `SymbolicName` ?

#### Requirements against the specification


**P03-03030200XX  Conventions for defining NodeClasses** - this standard defines Properties, but Properties can be defined by other standard organizations or vendors and Nodes can have Properties that are not standardised. Properties defined in this standard are defined by their name, which is mapped to the `BrowseName` having the NamespaceIndex 0, which represents the Namespace for OPC UA.

**P03-04040200XX Properties** - to prevent recursion, Properties are not allowed to have Properties defined for them. To easily identify Properties, the `BrowseName` of a Property shall be unique in the context of the Node containing the Properties (see 5.6.3 for details).

**P03-040504 Instantiation of complex TypeDefinitionNodes** - the instantiation of complex TypeDefinitionNodes depends on the ModellingRules defined in 6.4.4. However, the intention is that instances of a type definition will reflect the structure defined by the TypeDefinitionNode. Figure 7 shows an instance of the TypeDefinitionNode “AI_BLK_TYPE”, where the ModellingRule Mandatory, defined in 6.4.4.5.2, was applied for its containing Variable. Thus, an instance of “AI_BLK_TYPE”, called AI_BLK_1”, has a HasTypeDefinition Reference to “AI_BLK_TYPE”. It also contains a Variable “SP” having the same `BrowseName` as the Variable “SP” used by the TypeDefinitionNode and thereby reflects the structure defined by the TypeDefinitionNode.

There are several constraints related to programming against the TypeDefinitionNode. A TypeDefinitionNode or an InstanceDeclaration shall never reference two Nodes having the same `BrowseName` using forward hierarchical References. Instances based on InstanceDeclarations shall always keep the same `BrowseName` as the InstanceDeclaration they are derived from. A special Service defined in Part 4 called TranslateBrowsePathsToNodeIds may be used to identify the instances based on the InstanceDeclarations. Using the simple Browse Service might not be sufficient since the uniqueness of the `BrowseName` is only required for TypeDefinitionNodes and InstanceDeclarations, not for other instances. Thus, “AI_BLK_1” may have another Variable with the `BrowseName` “SP”, although this one would not be derived from an InstanceDeclaration of the TypeDefinitionNode.

**P03-040802 Well Known Roles** - all Servers should support the well-known Roles which are defined in Table 2. The NodeIds for the well-known Roles are defined in Part 6.

>MP NOTE: The table contains `BrowseNames` instead of `NodeIds`.

**P03-050204 BrowseName** - nodes have a `BrowseName` Attribute that is used as a non-localised human-readable name when browsing the AddressSpace to create paths out of `BrowseNames`. The TranslateBrowsePathsToNodeIds Service defined in Part 4 can be used to follow a path constructed of `BrowseNames`.

A `BrowseName` should never be used to display the name of a Node. The DisplayName should be used instead for this purpose.

Unlike NodeIds, the BrowseName cannot be used to unambiguously identify a Node. Different Nodes may have the same `BrowseName`.

Subclause 8.3 defines the structure of the `BrowseName`. It contains a namespace and a string. The namespace is provided to make the `BrowseName` unique in some cases in the context of a Node (e.g. Properties of a Node) although not unique in the context of the Server. If different organizations define `BrowseNames` for Properties, the namespace of the `BrowseName` provided by the organization makes the BrowseName unique, although different organizations may use the same string having a slightly different meaning.

Servers may often choose to use the same namespace for the NodeId and the `BrowseName`. However, if they want to provide a standard Property, its `BrowseName` shall have the namespace of the standards body although the namespace of the NodeId reflects something else, for example the local Server.

It is recommended that standard bodies defining standard type definitions use their namespace for the NodeId of the TypeDefinitionNode as well as for the `BrowseName` of the TypeDefinitionNode.

The string-part of the `BrowseName` is case sensitive. That is, Clients shall consider them case sensitive. Servers are allowed to handle `BrowseNames` passed in Service requests as case insensitive. Examples are the TranslateBrowsePathsToNodeIds Service or Event filter.

**P03-050205 DisplayName** - the DisplayName Attribute contains the localised name of the Node. Clients should use this Attribute if they want to display the name of the Node to the user. They should not use the BrowseName for this purpose. The Server may maintain one or more localised representations for each `DisplayName`. Clients negotiate the locale to be returned when they open a session with the Server. Refer to Part 4 for a description of session establishment and locales. Subclause 8.5 defines the structure of the `DisplayName`. The string part of the `DisplayName` is restricted to 512 characters.

**P03-050302 Attributes** The ReferenceType NodeClass inherits the base Attributes from the Base NodeClass defined in 5.2. The inherited `BrowseName` Attribute is used to specify the meaning of the ReferenceType as seen from the SourceNode. For example, the ReferenceType with the BrowseName “Contains” is used in References that specify that the SourceNode contains the TargetNode. The inherited DisplayName Attribute contains a translation of the `BrowseName`.

The `BrowseName` of a ReferenceType shall be unique in a Server. It is not allowed that two different ReferenceTypes have the same `BrowseName`.

Figure 9 provides examples of symmetric and non-symmetric References and the use of the `BrowseName` and the InverseName.

**P03-050501 Object NodeClass** If the Object is used as an InstanceDeclaration (see 4.5) then all Nodes referenced with forward hierarchical References direction shall have unique `BrowseNames` in the context of this Object.

If the Object is created based on an InstanceDeclaration then it shall have the same `BrowseName` as its InstanceDeclaration.

**P03-050502 ObjectType** NodeClass All Nodes referenced with forward hierarchical References shall have unique `BrowseNames` in the context of an ObjectType (see 4.5).

**P03-050504 Client-side creation of Objects of an ObjectType** - in addition to the AddNodes Service ObjectTypes may have a special Method with the `BrowseName` “Create”. This Method is used to create an Object of this ObjectType. This Method may be useful for the creation of Objects where the semantic of the creation should differ from the default behaviour expected in the context of the AddNodes Service. For example, the values should directly differ from the default values or additional 1 should be added, etc. The input and output arguments of this Method depend on the ObjectType; the only commonality is the `BrowseName` identifying that this Method will create an Object based on the ObjectType. Servers should not provide a Method on an ObjectType with the `BrowseName` “Create” for any other purpose than creating Objects of the ObjectType.

**P03-050602 Variable NodeClass** - if the Variable is created based on an InstanceDeclaration (see 4.5) it shall have the same `BrowseName` as its InstanceDeclaration.

**P03-050603 Properties** The HasTypeDefinition Reference points to the VariableType of the Property. Since Properties are uniquely identified by their `BrowseName`, all Properties shall point to the PropertyType defined in Part 5.

The `BrowseName` of a Property is always unique in the context of a Node. It is not permitted for a Node to refer to two Variables using HasProperty References having the same `BrowseName`.

**P03-050604 DataVariable** - if the DataVariable is used as InstanceDeclaration (see 4.5) all Nodes referenced with forward hierarchical References shall have unique `BrowseNames` in the context of this DataVariable.

**P03-050605 VariableType NodeClass** All Nodes referenced with forward hierarchical References shall have unique `BrowseNames` in the context of the VariableType (see 4.5).

**P03-0507 Method NodeClass** - if the Method is used as InstanceDeclaration (see 4.5) all Nodes referenced with forward hierarchical References shall have unique `BrowseNames` in the context of this Method.

**P03-050803 DataType NodeClass** - each concrete Structured DataType shall point to at least one DataTypeEncoding Object with the `BrowseName` “Default Binary” or “Default XML” having the NamespaceIndex 0. The `BrowseName` of the DataTypeEncoding Objects shall be unique in the context of a DataType, i.e. a DataType shall not point to two DataTypeEncodings having the same `BrowseName`.

**P03-0509 Summary of Attributes of the NodeClasses** - BrowseName is the mandatory attribute for all NodeClasses.

**P03-060204 Similar Node of InstanceDeclaration** - a similar Node of an InstanceDeclaration is a Node that has the same BrowseName and NodeClass as the InstanceDeclaration and in cases of Variables and Objects the same TypeDefinitionNode or a subtype of it.

**P03-060205 BrowsePath** - all targets of forward hierarchical References from a TypeDefinitionNode shall have a `BrowseName` that is unique within the TypeDefinitionNode. The same restriction applies to the targets of forward hierarchical References from any InstanceDeclaration. This means that any InstanceDeclaration within the InstanceDeclarationHierarchy can be uniquely identified by a sequence of `BrowseNames`. This sequence of `BrowseNames` is called a BrowsePath.

**P03- 060206 Attribute Handling of InstanceDeclarations** - some restrictions exist regarding the Attributes of InstanceDeclarations when the InstanceDeclaration is overridden or instantiated. The `BrowseName` and the NodeClass shall never change and always be the same as the original InstanceDeclaration.

**P03-060302 Attributes** - Subtypes inherit the parent type’s Attribute values, except for the NodeId. Inherited Attribute values may be overridden by the subtype, the `BrowseName` and DisplayName values should be overridden. Special rules apply for some Attributes of VariableTypes as defined in 6.2.7. Optional Attributes, not provided by the parent type, may be added to the subtype.

**P03-06030303 - overriding InstanceDeclarations** - a subtype overrides an InstanceDeclaration by specifying an InstanceDeclaration with the same BrowsePath. An overridden InstanceDeclaration shall have the same NodeClass and `BrowseName`. The TypeDefinitionNode of the overridden InstanceDeclaration shall be the same or a subtype of the TypeDefinitionNode specified in the supertype.

The overriding Node may specify new values for the Node Attributes other than the NodeClass or `BrowseName`, however, the restrictions on Attributes specified in 6.2.6 apply. Any Attribute provided by the overridden InstanceDeclaration has to be provided by the overriding InstanceDeclaration, additional optional Attributes may be added.

**P03-060401 Overview** any Instance of a TypeDefinitionNode will be the root of a hierarchy which mirrors the InstanceDeclarationHierarchy for the TypeDefinitionNode. Each Node in the hierarchy of the Instance will have a BrowsePath which may be the same as the BrowsePath for one of the InstanceDeclarations in the hierarchy of the TypeDefinitionNode. The InstanceDeclaration with the same BrowsePath is called the InstanceDeclaration for the Node. If a Node has an InstanceDeclaration then it shall have the same `BrowseName` and NodeClass as the InstanceDeclaration and, in cases of Variables and Objects, the same TypeDefinitionNode or a subtype of it.

**P03-060402 Creating an Instance** - when a Server creates an instance of a TypeDefinitionNode it shall create the same hierarchy of Nodes beneath the new Object or Variable depending on the ModellingRule of each InstanceDeclaration. Standard ModellingRules are defined in 6.4.4.5. The Nodes within the newly created hierarchy may be copies of the InstanceDeclarations, the InstanceDeclaration itself or another Node in the AddressSpace that has the same TypeDefinitionNode and `BrowseName`. If new copies are created, then the Attribute values of the InstanceDeclarations are used as the initial values.

Figure 15 provides a simple example of a TypeDefinitionNode and an Instance. Nodes referenced by the TypeDefinitionNode without a ModellingRule do not appear in the instance. Instances may have children with duplicate `BrowseNames`; however, only one of those children will correspond to the InstanceDeclaration.

A Client can use the information of TypeDefinitionNodes to access Nodes which are in the hierarchy of the instance. It shall pass the NodeId of the instance and the BrowsePath of the child Nodes based on the TypeDefinitionNode to the TranslateBrowsePathsToNodeIds service (see Part 4). This Service returns the NodeId for each of the child Nodes. If a child Node exists then the `BrowseName` and NodeClass shall match the InstanceDeclaration. In the case of Objects or Variables, also the TypeDefinitionNode shall either match or be a subtype of the original TypeDefinitionNode.

**P03-0604040201 NamingRule** If an InstanceDeclaration has a ModellingRule using the NamingRule Constraint it identifies that the `BrowseName` of the InstanceDeclaration is of no significance but other semantic is defined with the ModellingRule. The TranslateBrowsePathsToNodeIds Service (see Part 4) can typically not be used to access instances based on those InstanceDeclarations.

**P03-0604040503 Optional** In Figure 20 an example using the ModellingRules Optional and Mandatory is shown. The example contains an ObjectType Type_A and all valid combinations of instances named A1 to A13. Note that if the optional B is provided, the mandatory E has to be provided as well, otherwise not. F is referenced by C and D. On the instance, this can be the same Node or two different Nodes with the same `BrowseName` (similar Node to InstanceDeclaration F). Not considered in the example is if the instances have ModellingRules or not. It is assumed that each F is similar to the InstanceDeclaration F, etc.

**P03-0604040504 ExposesItsArray** Figure 21 gives an example. A is an instance of Type_A having two entries in its value array. Therefore it references two instances of the same type as the InstanceDeclaration ArrayExpose. The `BrowseNames` of those instances are not defined by the ModellingRule. In general, it is not possible to get a Variable representing a specific entry in the array (e.g. the second). Clients will typically either get the array or access the Variables directly, so there is no need to provide that information.

**P03-0604040505 OptionalPlaceholder** - for Object and Variable the intention of the ModellingRule OptionalPlaceholder is to expose the information that a complex TypeDefinition expects from instances of the TypeDefinition to add instances with specific References without defining `BrowseNames` for the instances. For example, a Device might have a Folder for DeviceParameters, and the DeviceParameters should be connected with a HasComponent Reference. However, the names of the DeviceParameters are specific to the instances. The example is shown in Figure 23, where an
instance Device A adds two DeviceParameters in the Folder.

It is recommended that the `BrowseName` and the DisplayName of InstanceDeclarations having the OptionalPlaceholder ModellingRule should be enclosed within angle brackets.

When overriding the InstanceDeclaration, the ModellingRule shall remain OptionalPlaceholder. For Methods, the ModellingRule OptionalPlaceholder is used to define the `BrowseName` where subtypes and instances provide more information. The Method definition with the OptionalPlaceholder only defines the `BrowseName`. An instance or subtype defines the InputArguments and OutputArguments. A subtype shall also change the ModellingRule to Optional or Mandatory. The Method is optional for instances. For example, a Device might have a Method to perform calibration however the specific arguments for the Method depend on the instance of the Device. In this example Device A does not implement the Method, Device B implements the Method with no arguments and Device C implements the Method accepting a mode argument to select how the calibration is to be performed. The example is shown in Figure 24.

**P03-0604040506 MandatoryPlaceholder** for example, when the DeviceType requires that at least one DeviceParameter shall exist without specifying the `BrowseName` for it, it uses MandatoryPlaceholder as shown in Figure 25. Device A is a valid instance as it has the required DeviceParameter. Device B is not valid as it uses the wrong ReferenceType to reference a DeviceParameter (Organizes instead of HasComponent) and Device C is not valid because it does not provide a DeviceParameter at all.

The ModellingRule MandatoryPlaceholder requires that each instance provides at least one instance with the TypeDefinition of the InstanceDeclaration or a subtype, and is referenced with
the same ReferenceType or a subtype as the InstanceDeclaration. It does not require a specific `BrowseName` and thus cannot be used for the TranslateBrowsePathsToNodeIds Service (see Part 4).

It is recommended that the `BrowseName` and the DisplayName of InstanceDeclarations having the MandatoryPlaceholder ModellingRule should be enclosed within angle brackets.

For Methods, the ModellingRule MandatoryPlaceholder is used to define the `BrowseName` where subtypes and instances provide more information. The Method definition with the MandatoryPlaceholder only defines the `BrowseName`. An instance or subtype defines the InputArguments and OutputArguments. A subtype shall also change the ModellingRule to Mandatory. The Method is mandatory for instances.

**P03-0803 QualifiedName** - this Built-in DataType contains a qualified name. It is, for example, used as `BrowseName`. Its elements are defined in Table 25. The name part of the QualifiedName is restricted to 512 characters.

The  QualifiedName structure syntax

| Name           | Type   | Description                            |
| -------------- | ------ | -------------------------------------- |
| namespaceIndex | UInt16 | see description below                  |
| name           | String | The text portion of the QualifiedName. |

namespaceIndex description

- Index that identifies the namespace that defines the name.
- This index is the index of that namespace in the local Server’s NamespaceArray.
- The Client may read the NamespaceArray Variable to access the string value of the namespace.

The regular expression pattern to match.

```TXT
\b((\d{1,}):)?(.+)
```

**P03-0851 StructureField** StructureFields can be exposed as DataVariables that are children of the Variable that contains the Structure Value. In this case the `BrowseName` of the DataVariable shall be the same as the StructureField name and the NamespaceIndex of the `BrowseName` shall be the same as the Structure DataType Node NamespaceIndex.

**P03-A0402 Properties or DataVariables** - besides the semantic differences of Properties and DataVariables described in Clause 4 there are also syntactical differences. A Property is identified by its `BrowseName`, that is, if Properties having the same semantic are used several times, they should always have the same `BrowseName`. The same semantic of DataVariables is captured in the VariableType.

**P03-C0203 Extended Notation** - the `BrowseName` contains the NamespaceIndex and a String. Such a structure can be exposed as \[\<NamespaceIndex\>:\]\<String\> where the NamespaceIndex is optional. For example, a `BrowseName` can be “1:MyName”. Instead of that, “MyName” can also be used. This rule applies whenever a `BrowseName` is shown, including the text used in the graphical representation of a Node.

**P05-0303 Conventions for Node descriptions** - References are defined by providing the ReferenceType name, the `BrowseName` of the TargetNode and its NodeClass.

Nodes of all other NodeClasses cannot be defined in the same table; therefore only the used ReferenceType, their NodeClass and their `BrowseName` are specified. A reference to another part of this document points to their definition.

**P05-0401 NodeIds** - the symbolic name of each Node defined in this standard is its `BrowseName`, or, when it is part of another Node, the `BrowseName` of the other Node, a “.”, and the BrowseNa`me of itself. In this case “part of” means that the whole has a HasProperty or HasComponent Reference to its part. Since all Nodes not being part of another Node have a unique name in this standard, the symbolic name is unique. For example, the ServerType defined in 6.3.1 has the symbolic name “ServerType”. One of its InstanceDeclarations would be identified as “ServerType.ServerCapabilities”. Since this Object is complex, another InstanceDeclaration of the ServerType is “ServerType.ServerCapabilities.MinSupportedSampleRate”. The Server Object defined in 8.3.2 is based on the ServerType and has the symbolic name “Server”. Therefore, the instance based on the InstanceDeclaration described above has the symbolic name “Server.ServerCapabilities.MinSupportedSampleRate”.

**P05-0402 BrowseNames** - the text part of the `BrowseNames` for all Nodes defined in this standard is specified in the tables defining the Nodes. The NamespaceIndex for all `BrowseNames` defined in this standard is 0.

**P05-0501 General** the DisplayName is a LocalizedText. Each server shall provide the DisplayName identical to the `BrowseName` of the Node for the LocaleId “en”. Whether the server provides translated names for other LocaleIds is server-specific.

The NodeId is described by `BrowseNames` as defined in 4.1 and defined in Part 6.

**P05-060304 SessionsDiagnosticsSummaryType** - for each session of the Server, this Object also provides an Object representing the session, indicated by <ClientName>. The BrowseName could be derived from the sessionName defined in the CreateSession Service (Part 4) or some other server-specific mechanisms. It is of the ObjectType SessionDiagnosticsObjectType, as defined in 6.3.5.

**P05-060313 NamespaceMetadataType** - the `BrowseName` of instances of this type shall be derived from the represented namespace. This can, for example, be done by using the index of the namespace in the NamespaceArray as namespaceIndex of the QualifiedName and the namespace URI as name of the QualifiedName.

**P05-060314 NamespacesType** - the ObjectType contains a list of NamespaceMetadataType Objects representing the namespaces in the Server. The `BrowseName` of an Object shall be derived from the namespace represented by the Object. This can, for example, be done by using the index of the namespace in the NamespaceArray as namespaceIndex of the QualifiedName and the namespace URI as name of the QualifiedName. Clients should not assume that all namespaces provided by a Server are present in this list as a namespace may not provide the information necessary to fill all mandatory Properties of the NamespaceMetadataType.

**P05-060402 BaseEventType** Server does not have a description, it shall return the string part of the `BrowseName` of the Node associated with the Event.

**P05-060431 BaseModelChangeEventType** - this EventType inherits all Properties of the BaseEventType. Their semantic is defined in 6.4.2. There are no additional Properties defined for this EventType. The SourceNode Property for Events of this type shall be the Node of the View that gives the context of the changes. If the whole AddressSpace is the context, the SourceNode Property is set to the NodeId of the Server Object. The SourceName for Events of this type shall be the String part of the `BrowseName` of the View; for the whole AddressSpace it shall be “Server”.

**P05-060433 SemanticChangeEventType** - this EventType inherits all Properties of the BaseEventType. Their semantic is defined in 6.4.2. There are no additional Properties defined for this EventType. The SourceNode Property for Events of this type shall be the Node of the View that gives the context of the changes. If the whole AddressSpace is the context, the SourceNode Property is set to the NodeId of the Server Object. The SourceName for Events of this type shall be the String part of the `BrowseName` of the View, for the whole AddressSpace it shall be “Server”.

**P05-0703 PropertyType** - the PropertyType is a subtype of the BaseVariableType. It is used as the type definition for all Properties. Properties are defined by their `BrowseName` and therefore they do not need a specialised type definition. It is not allowed to subtype this VariableType.

**P05-0709 SamplingIntervalDiagnosticsArrayType** - this complex VariableType is used for diagnostic information. For each entry of the array, instances of this type will provide a Variable of the SamplingIntervalDiagnosticsType VariableType having the sampling rate as `BrowseName`.

**P05-B0405 FiniteStateMachineType** - The States of the machine are represented with instances of the StateType ObjectType. Each State shall have a `BrowseName` which is unique within the StateMachine and shall have a StateNumber which shall also be unique across all States defined in the StateMachine. Be aware that States in a SubStateMachine may have the same StateNumber or `BrowseName` as States in the parent machine. A concrete subtype of FiniteStateMachineType shall define at least one State.

The Transitions that may occur are represented with instances of the TransitionType. Each Transition shall have a `BrowseName` which is unique within the StateMachine and may have a TransitionNumber which shall also be unique across all Transitions defined in the StateMachine.

**P05-B0406 FiniteStateVariableType** - the Name Property is inherited from StateVariableType. Its Value shall be the `BrowseName` of one of the State Objects of the FiniteStateMachineType.

**P05-C0302** FileSystem Object The support of file directory structures is declared by aggregating an instance of the FileDirectoryType with the `BrowseName` FileSystem as illustrated in Figure C.1.

The Object representing the root of a file directory structure shall have the `BrowseName` FileSystem. An OPC UA Server may have different FileSystem Objects in the AddressSpace.
HasComponent is used to reference a FileSystem from aggregating Objects like the Objects Folder or the Object representing a device.


### General Rules for DisplayName Attribute

### General Rules for NodeId Attribute

  <xs:simpleType name="NodeId">
    <xs:restriction base="xs:string" />
  </xs:simpleType>

### General Rules for SymbolicName Attribute

According to the specification it can be used as a class/field name in auto generated code. It should only be specified if the `BrowseName` cannot be used for this purpose.

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