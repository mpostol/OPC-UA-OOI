# OPC UA Object Model Working Notes

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

## Inheritance

This approach is commonly used in object-oriented programming languages in which the variables of a class are defined as instances of other classes. When the class is instantiated, each variable is also instantiated, but with the default values (constructor values) defined for the containing class. That is, typically, the constructor for the component class runs first, followed by the constructor for the containing class. The constructor for the containing class may override component values set by the component class.

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