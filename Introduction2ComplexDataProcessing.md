# Introduction to Complex Data Processing 

From the definition, the Industrial IT domain is an integrated set of ICT systems. System integration means the necessity of the information exchange between them (the nodes of a common domain). ICT systems are recognized as a typical measure of processing information (Section [Semantic-Data Processing Architecture]). The main challenge of deploying an Industrial IT solution is that information is abstract – it is knowledge describing a situation in the selected environment, e.g. temperature in a boiler, a car speed, an account balance, a robot manipulator position, etc. Unfortunately, machines cannot be used to process abstraction. It is also impossible to transfer abstraction from one place to another.

Fortunately, there is a very simple solution to address that impossibility, namely the information must be represented as binary data. In consequence, we can usually use both ones (ie. data and information) as interchangeable terms while talking about ICT systems. Unfortunately, these terms must be distinguished in the context of further discussion on the complex data, because before stepping forward we must be aware of the fact that the same information could have many different but equivalent representations – different binary patterns. For example, having interconnected system A and system B, system A can use one representation, but system B another one. Moreover, to integrate them, the transferred stream of bits may not resemble any of the previous ones. It should be nothing new for us, as it is obvious that the same information is written as a text in regional newspapers in English, German, Polish, etc. does not resemble one another.

To understand a newspaper we must learn the appropriate language. To understand the binary data we must have defined a data type – a description of how to create an appropriate bits pattern. Simplifying, the data type determines a set of valid values and rules needed to assign the information (understand the data) to a selected bits pattern. Therefore, to make two systems interoperable, apart from communication, they should be prepared – integrated to be able to consume data from each other, and so communication is only a prerequisite for interoperability.

The type is usually not enough to make the data meaningful. Referring to the above example the newspaper name (i.e. the location where the information came from) and timestamp (a single point in time when the information was valid) are properties of the text that is a representation of the information.

To have a similar ability to add common properties to the representations of many information entities at the same time the complex data types must be used. Complex in this context means that the data type must additionally define a relationship between the components of the binary data, i.e. how to selectively get a component of the complex data.

There are two well-known and widely used relationships:

- Arrays – components are indexed and all components must have a common data type
- Structures – components are named and components may have different data types

Anyway, indexes and names must be unambiguous, and a complex data type has the responsibility to provide a precise definition of them, i.e. selectors of the components.

The complex data has a very important feature, namely, all components are considered to be consistent with one another. For example, if we need to represent time at last three components (integers) must be distinguished: hour, minute, and second. In this case, even if there is no need to add any property to the binary data it must be consistent, i.e. it has to represent information in a single point in time. Other criteria for describing the data consistency could also be applied.

On the other hand using complex data simplifies data integrity if there is a need to store or transfer it. If intermediaries are present, the initial data creator and the ultimate consumer need to trust those intermediaries to help provide end-to-end data integrity, because each hop is processed separately. Thus, using complex data it can be processed and transferred as one item what finally mitigates any risk of integrity compromising.

Using the data type definitions to describe information exposed by a server allows:

- Development against a type definition.
- Unambiguous association of the information with the data.

Having defined types in advance, clients may provide dedicated functionality, e.g. displaying the information in the context of specific graphics.

Typical scenarios can be recognized when we can define appropriate complex data types in advance. The OPC UA offers a variety of standard types ready to be used in common cases (Section [Standard Information Model]). If this out of the box set is not capable of fulfilling more demanding needs users may define custom data types. The OPC UA allows servers to provide data type definitions. The type definitions may be abstract and may be inherited by new types to reflect polymorphism. They may be of generic use or they may be application domain specific. Custom types must have a globally unique identifier, which can be used to identify the authoring organization responsible for that type definition.
If the data publisher – an OPC UA server is not running in an environment capable of creating the complex data there must be taken special precaution to fabricate it if required. An example of this scenario is a standalone OPC UA server pooling data from plant floor devices using a custom protocol, e.g. MODBUS. If that is the case the protocol used to gather process data is usually not data complex aware. Reading and writing the data is accomplished using REQUEST/RESPONSE frame pairs. Moreover, one request can be used to read a set of values that has the same simple type only. The fabrication is an operation that uses a group of requests to gather components and embeds them into a single value of a selected complex data type. It is an optional server capability.

See also

- [Semantic-Data Processing Architecture]
- [Standard Information Model]

[Semantic-Data Processing Architecture]:SemanticData/README.MD
[Standard Information Model]:SemanticData/StandardInformationModel.md