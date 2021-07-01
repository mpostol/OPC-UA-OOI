# UA Model Design Semantics

## `ModelDesign`

The root element for the information model. This scheme allows information model designers to define UA types and instances in a machine readable form. This definition can be used to generate code and documentation.

The file is expected to contain a number of types and their instance declarations. Objects which are unique in the address space can also be defined.

A validator is available verify consistency of the model generator and to create suitable values or optional information. Once the design is validated it can be passed to a generator which creates different types of code or documentation.

This XSD File contains comments describing the available scheme for defining models. The comments provide an explanation of the scheme, they do not explain the concept that is being model. It is assumed that the designer is familiar with these concepts via the UA specifications.

A XML file used for generating a model must start with a  Model definition. The below list is a list of the valid constructs where each construct map to a model concept in UA definition using these construct must be assigned to a UA namespace, by the use of the TargetNamespace attribute, they can further be assigned to an XML namespace and have a default Locale assigned.

## `NamespaceTable`

This defines the namespaces used in the model. Each namespace listed should also have a namespace prefix defined in the `xs:schema` element. The order of the namespaces is significant and used to assigned a numeric index to namespaces when they are used in BrowsePaths specified in the ModelDesign.

## `Namespace` element

Defines a single namespace along with identifiers for the namespace. The following table contains an explanation of the attributes.

| Attribute         | description |
| ---------         | ----------- |
|`Name`             | A symbolic name for the namespace that can be used as a variable name. The Name is used to create a program constant for the URL.|
|`Prefix`           | The .NET namespace used for the classes produced by the generator. The `Prefix` is the C# namespace that qualifies the generated types.
|`InternalPrefix`   | The .NET namespace used for classes that are only used within a server application. The `InternalPrefix` is an optional C# namespace that qualifies the generated types used only by the server.
|`XmlNamespace`     | The URI for the XML namespace to which the data types belong if it is different from the URI for the model namespace.
|`XmlPrefix`        | The prefix to be used in the XML file for the XML namespace to which the data types belong. Used only XmlNamespace is set.
|`FilePath`         | The path to the file containing the design file for the namespace.
| `Version`         | no description
| `PublicationDate` | no description

## NodeDesign

The base type of all node designs.

## BrowseName

The BrowseName is the name used in the information model. The validator will create the BrowseName automatically from the SymbolicName. The BrowseName is qualified by the namespace used for the SymbolicName.

## DisplayName

The DisplayName human readable name for the Node. This element includes an optional key that can be used to look up the display name for other locales in a resource DB. The validator automatically creates the DisplayName from the BrowseName.

## Description

The Description the value of the Description attribute for the Node. This element includes an optional key that can be used to look up the Description for other locales in a resource DB. The validator automatically creates a generic Description from the BrowseName and NodeClass.

## Children

The Children are the Properties or Components of a Node.

## References

The References specify additional references from the Node. These references may refer to other children of the same Node or children of other Nodes defined in the ModelDesign.

## SymbolicName

The SymbolicName identifies the Node within the the ModelDesign or within the containing Node. The SymbolicName should always be specified. It is used to create the BrowseName and SymbolicId if they are not specified.

## SymbolicId

The SymbolicId is a globally unique identifier for the Node. The validator will create the SymbolicId automatically from the SymbolicName if it is not specified.

## IsDeclaration

The `IsDeclaration` flag indicates that the Node is defined elsewhere and no code will be generated. Nodes that are declarations do not need to be completely defined. They only need to have the information required to generate code for nodes that reference it (e.g. the BaseType).

## NumericId

The `NumericId` specifies the unique numeric id for the Node. It is filled in automatically by reading a CSV file containing the `SymbolicIds` and an associated UInt32. The validator will automatically assign a unique id if no CSV input is provided.

The `NumericId` or `StringId` are combined with the `Namespace` used for the SymbolicId to create the well known UA NodeId for the Node. The generator will create programmatic constants that can be used to reference the Node in code.

## StringId

The StringId is an alternate unique identifer for the node. It is used instead of the NumericId if it is specified in the CSV input file.

## WriteAccess

The bit mask which indicates which attributes are writeable.

## PartNo

The part that defines the node.

## Category

A coma separated list of categories assigned to the node (e.g. Part4/Services or Part5/StateMachines).

## TypeDesign

A base type for all Type Nodes (ObjectType, VariableType, DataType and ReferenceType).

### ClassName

This is the name for the instance of the type. If not specified the validator creates it by removing the 'Type' suffix from the SymbolicName for the Node.

### BaseType

The SymbolicId for the BaseType.

### IsAbstract

Whether the Type is abstract.

### NoClassGeneration

Whether to suppress class generation for the type.

## ObjectTypeDesign

`ObjectTypes` define structure of an Object in the information model.

## VariableTypeDesign

VariableTypes define structure of a Variable in the information model.

## DataTypeDesign

DataTypes define structure of a Value for Variables in the information model.

## ReferenceTypeDesign

ReferenceType define typed references between Nodes.

## InstanceDesign

A base type for all Instance Nodes (Object, Variable, and Method).

## ViewDesign

A View Node.

### SupportsEvents

Whether the View generates events.

### ContainsNoLoops

Specifies that the View contains a non-looping hierarchy.

## ObjectDesign

Defines the structure of an Object in the information model.

## VariableDesign

Defines the structure of a Variable in the information model.

## MethodDesign

Defines the a Method in the information model.

## PropertyDesign

Defines a Variable which is a Property for a Node.

## EncodingDesign

Defines an `Object` which is a `DataTypeEncoding` for a `DataType`.

## DictionaryDesign

Defines an Variable which is a DataTypeDictionary.

## Reference

Defines a reference between two nodes.

The SourceId is the `SymbolicId` of the Node that contains the Reference.<html:br />

The SourcePath and TargetPath are RelativePaths specified using the syntax defined in Part 4. The order of the Namespaces defined in the Namespaces element is used to determine the namespace index used in the RelativePaths.

## Parameter

Defines a Field in a DataType or Argument of a Method.
