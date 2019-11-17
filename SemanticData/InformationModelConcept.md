# UA Information Model - Concept

To make systems interoperable, i.e. empower common processing of information by variety of computer systems, the data transfer mechanism must be associated with a consistent information representation model. OPC UA uses an object as a fundamental notion to represent data and behavior of an underlying system. The objects are placeholders of variables, events and methods and are interconnected by references. This concept is similar to well-known object-oriented programming (OOP) that is a programming paradigm using "objects" – data structures consisting of fields, events and methods – and their interactions to design computer programs. The OPC UA Information Model [\[1\]][CAS.EBOOK], [2], [\[3\]][OPC.UA.Part5] provides features such as data abstraction, encapsulation, polymorphism, and inheritance.

For the purpose of unification of the information representation the producers (servers) and consumers (clients) use the type notion. The OPC UA object model allows servers to provide type definitions for objects and their components. Type definitions may be abstract, and may be inherited by new types to reflect polymorphism. They may also be common or they may be system-specific. Object types may be defined by standardization organizations, vendors or end-users. Each type must have a globally unique identifier that can be used to provide description of the data semantics from the defining body or organization. Using the type definitions to describe the information exposed by the server allows for:

- Development against type definition
- Unambiguous assignment of the semantics to the data expected by the client

Having defined types in advance, clients may provide dedicated functionality, for example: displaying the information in the context of specific graphics. It greatly improves reusability as a result of the possibility of designing a unique context for typical real-time processes. As an example, the section [Adopting Companion Standard Models - Analyzer Devices Integration][3] presents a case of unification of the model for chemical analyzers.

The OPC UA information modeling concept is based on domains. A domain is a named self-contained collection of definitions. Any domain name must be globally unique - it is an identification string that defines a realm of administrative autonomy and authority of responsibility. Type definition from one domain may be inherited from type definitions located in other domains. To avoid circular references domains should be organized in layers, which expand step by step the basic model provided by the OPC UA Specification.

Type definitions are exposed in the OPC UA Address Space using the specialized `NodeClass`es: `ObjectType`, `DataType`, `ReferenceType`, `VariableType` (Section [Address Space and Address Space Model][ASASM]). The main role of the types represented by the above `NodeClass`es is to provide a description of the Address Space structure and to allow clients to use this knowledge to navigate to desired information in the Address Space exposed by the server. In other words, this way the clients obtain the definition of the data (meta-data) using the following two concepts:

1. `NodeClass` – as a formal description of the node defining the allowed attributes and references.
2. Type – as a formal description of the node defining values of the allowed attributes and references.

The OPC UA Information Model concept provides a set of predefined types and rules that must be applied to expand it. Even though the OPC UA specification contains a rich set of predefined types, the type concept allows designers to freely define types according to the application needs. New types are derived from the existing ones. The derived types inherit all features from the base types but can include modifications to make the new types more appropriate for information the designers are representing. To expand the standard model, independent domains must be defined. This new information model covered by the domain may be the subject of a companion specification or proprietary release. In any case the definitions must be uniquely named and self-contained except for external type references. All not predefined types (not belonging to the standard domain) must be exposed in the Address Space.

Types are called meta-data since they describe the data structure and not the actual data values. Simplifying, we can say that a `NodeClass` plays a role similar to the shape of a puzzle piece and the represented information is similar to the picture on the piece.  Both are needed to enable us to see the final picture. In the above simplification we have lost that the OPC UA Address Space is capable of displaying movies, and not just static pictures.

From the above discussion we learn that before nodes making up the Address Space can be instantiated by the server, that Address Space must be designed first. Model designing is a process aimed at defining a set of types and their associations and, next, creating an Address Space representation in a format appropriate for implementation. More detailed description of this topic is captured in the section [*Address Space Model Life-cycle*][ASMLC].

The Address Space concept based on types can be a foundation for exposing any information that is required. Clients understand the Address Space concept and have a browse service to navigate the Address Space. Since browsing is based on the incremental and relative passage among nodes it is apparent that each path must have an entry point defined, so we must address the question as to “where to start". To meet this requirement, the Information Model includes definition to create a predefined structure containing well defined nodes that can be used as anchors from which a client can discover the Address Space. Thus to design an Address Space instance using predefined new types, we must derive them from the existing ones. At the very beginning the only existing types are the standard ones defined by the OPC Foundation [\[1\]][CAS.EBOOK], [\[3\]][OPC.UA.Part5]. The available standard types are briefly described in the sections [*Standard Information Model*][SIM].

## See also

- \[1\] [OPC Unified Architecture e-book, 2010][CAS.EBOOK]
- \[2\] Jürgen Lange, Frank Iwanitz, Thomas J. Burke. Von Data Access bis Unified Architecture. Hüthig Fachverlag, 2009.
- \[3\] [OPC Unified Architecture Specification Part 5: Information Model, OPC Foundation, Rel. 1.04, 2017-11-22][OPC.UA.Part5]
- \[4\] [Address Space Model Life-cycle][ASMLC]
- \[5\] [Address Space and Address Space Model][ASASM]
- \[6\] [Standard Information Model][SIM]

[SIM]:StandardInformationModel.md
[ASASM]:AddressSpaceAddressSpaceModel.md
[OPC.UA.Part5]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-5-information-model/
[ASMLC]:InformationModelLifecycle.md
[CAS.EBOOK]:http://www.commsvr.com/UAModelDesigner/
[3]:AdoptingCompanionStandardADI.md