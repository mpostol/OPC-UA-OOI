# Information Processing

## Data - Information Representation

By definition, the industrial IT domain is an integrated set of ICT systems. System integration means the necessity of the information exchange between them (the nodes of a common domain). ICT systems are recognized as a typical measure of information processing. The main challenge of deploying an industrial IT solution is that information is abstract – it is knowledge describing a situation in the selected environment, e.g. temperature in a boiler, car speed, account balance, etc.

>Unfortunately, machines cannot be used to process abstraction. It is also impossible to transfer abstraction from one place to another over the network.

Fortunately, there is a very simple solution to address that impossibility, namely the information must be represented as a binary stream. In consequence, the terms information and data can usually be used interchangeably while talking about the ICT systems. On the other hand, they must be distinguished in the context of further discussion on information processing, because before stepping forward we must be aware of the fact that the same information could have many different but equivalent representations – different binary patterns. For example, having interconnected system A and system B, system A can use one representation, but system B another one. Moreover, to integrate them, the transferred stream of bits may not resemble any of the previous ones. It should be nothing new for us, as it is obvious that the same information represented as a text in regional newspapers in English, German, Polish, etc. does not resemble one another.

To understand a newspaper we must learn an appropriate language. To understand binary data a data type must have been defined – a description of how to create an appropriate bits pattern. Simplifying, the data type determines a set of valid values and rules needed to assign the information to the selected bits pattern (understand data). Therefore, to make two systems interoperable, apart from communication, they should be prepared (integrated) to be able to consume data from each other, and so communication accessibility is only a prerequisite for interoperability.

The type is usually not enough to make the data meaningful. Referring to the above example, the newspaper name (i.e. the location where the information came from) and timestamp (a single point in time when the information was valid) are attributes of the text that is a representation of the information.

## Complex Data

To have a similar ability to add common attributes to the representations of many information entities at the same time the complex data types must be used. In this context, the term complex means that the data type must additionally define a relationship between the components of the binary data and operation returning a selected component of the complex data. Software engineering offers two well-known and widely used relationships:

- **Arrays** – parts of the array are indexed and all of them must have a common data type
- **Structures** – components are named and they may have different data types.

Anyway, indexes and names must be unambiguous, and a complex data type has a responsibility to provide a precise definition of them, i.e. selectors of the components.

The complex data has a very important feature, namely, all components are considered to be consistent with one another. For example, if we need to represent time at least three components must be distinguished: hour, minute, and second. In this case, even if there is no need to add any common attribute to the binary data it must be consistent, i.e. it has to represent information in a single point in time. Other criteria for describing the data consistency could also be applied.

Using complex data simplifies data integrity if there is a need to store or transfer it. If intermediaries are present, the initial data creator and the ultimate consumer need to trust those intermediaries to help provide end-to-end data integrity, because each hop is processed separately. Thus, using complex data means that the data is processed and transferred as one item what finally mitigates any risk of integrity compromising.

Using the data type definitions to describe the exposed information allows for:

•    Development against a type definition.
•    Unambiguous association of the information with the data.

Having defined types in advance, clients may provide dedicated functionality, e.g. displaying the information in the context of specific graphics. Typical scenarios occur when we can define appropriate complex data types in advance. Usually, the design environment offers a variety of standard types ready to be used in common cases. If the out-of-the-box set is not capable of fulfilling more demanding needs users may define custom data types. They may be of generic use or they may be application domain specific.

Representing the information processed as one whole sequence of bits could be impossible or impractical for some application domains. If the information comes from a real-time process, for example, a boiler or a chemical analyzer, we use an independent sensor to measure values, e.g. pressure, temperature, flow. The measuring process is independent, but pieces of information are related to each other as they describe the same physical process. If the data publisher (e.g. an OPC UA server) is not running in an environment capable of creating complex data there must be taken special measures to fabricate it if required. An example of this scenario is a software application pooling data from plant floor devices using a custom protocol, e.g. MODBUS. If that is the case the protocol used to gather process data is usually not data complex aware. Reading and writing the data is accomplished using REQUEST/RESPONSE frame pairs. Moreover, one request can be used to read a set of values that has the same simple type only.

> Fabrication is an operation that uses a group of requests to gather components and embed them into a single value of a selected complex data type.

## Data Graph

Fabrication of complex data is comparable to using reverse engineering for recovering a big picture from details. Additionally, as it was pointed out, fabrication of complex data from pieces (i.e. composing it using building-blocks) is possible but it needs additional effort. Because processing and transferring the data over the network are not for free this approach must be well-founded. If the data volume grows paying this cost could be groundless or even impossible and then we need an alternative solution, i.e. the possibility to process and transfer the data piece by piece. In such a case the consistency could be achieved by timestamps associated with each piece separately and partial data processing is possible if pieces can be accessed selectively. The proposed selection mechanisms of components for the complex data are rather static, i.e. they limit the internal structure and meaning (semantics) of the relations, but still can be successfully used for that purpose. Hence, to overcome those limits the reference concept could be introduced. Reference links two elements together, where the source and target roles are distinguished in this couple. Reference could also represent information. Adding randomly specific references to particular pieces of data we can create a graph. For example, lets try to describe a car. We need partial information about the main car body and four references to the tires as its components, but for the spare tire we need different reference kind, say a spare component to point out a different relationship for this case. Following the reference concept, we actually introduced a new selection mechanism, namely browsing. Nowadays, as a consequence of using references, we are able to replace a static newspaper with a dynamic website, where information is represented using hypertext instead of using text.

The concepts and terms presented above are well known and widely used by programmers and website authors. As there are people working on processing and exposing information professionally, a question arises why we are bothering about it. There is one simple reason: the offered services are unsatisfactory. There are two issues that can be recognized. Programmers offer dedicated solutions with the goal of meeting precisely defined requirements of selected stakeholders. The webmasters offer the possibility for freely exposing any information you need, but the representation is hard to be processed by other programs because the references are described (has meaning) in the native language.

In contrast to the offer of programmers and webmasters we face the biggest challenge of providing a generic solution that allows us to:

- expose any mentioned above data, i.e. simple, complex, and graph
- transport it over the network
- process it finally

Additionally, it must be assumed that all these three operations can be done by independent parties.  In this context generic means that only out of the box products and existing infrastructure are acceptable. Independent parties mean no need for special agreements made to guarantee interoperability case by case. In other words, common rules must be observed instead of case-specific agreements. The rules must be valid now, in the future, and for all application domains called industrial IT. Having an adequate rules specification in hands we will be able to develop products fulfilling these requirements and finally obtaining a universal, flexible enough solution based on best practice.

To meet the requirements presented above it is proposed to select OPC Unified Architecture specification as a foundation for further work. One of the main goals of the OPC Unified Architecture (OPC UA) is to provide a consistent mechanism for the integration of process control and business management systems. It is assumed that it should be robust and the implementation should be platform independent. In the next section, I will examine technologies and paradigms used as a framework for the development of the OPC UA standard and discuss their impact on the final result.
