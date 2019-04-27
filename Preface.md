# Introduction to Object-Oriented Internet

## Preface

The industrial IT applications domain is an integrated set of ICT (Information and Communication Technologies) systems. System integration means the necessity of the information exchange between them (the nodes of a common domain). 

The main aim of this project is to present a new emerging engineering discipline as a synergy between systematic design methodology and available tools. Bothering about information processing is usually a subject recognized as research and development activity. Engaging R&D activity to provide information processing solutions has many drawbacks. It requires distinct skills and, in consequence, solving a problem and deploying the solution must be carried out as two independent phases. It is not efficient and, therefore, very expensive and risky. The Object-Oriented Internet concept addresses this problem, namely it proposes an architecture, services, tools, and information modeling consistent concepts with the goal to allow vendors to release out-of-the-box products ready to be used by engineers. The above-mentioned issues could be overcome by reusability and unification.

The main challenge of this project is to converge the methodology and tools development to eliminate research and programming needs.

This project is dedicated to process architects and software developers to help them deploy the real-time process state and behavior description as a ready to use solution in a real production environment and use this description to integrate the process as a consistent part of a selected Industrial IT application domain.

To comply with the Industry 4.0 communication criterion, even the lowest category requires the product to integrate at least the OPC Unified Architecture Information Model. As a result, any product being advertised as Industry 4.0-enabled must be OPC UA-capable somehow. The OPC Unified Architecture (UA) technology (Section [OPC UA Main Technology Features]) is selected because:

- it is Internet-based technology
- it supports the client-server and publisher-subscriber communications relationship
- it is a platform neutral standard allowing easy implementation on any system including embedded systems
- it is designed to support complex data types and object models (structural data)
- it is designed to achieve high-speed data transfers using efficient binary data encoding/decoding
- it is scalable from embedded applications up to the process control and enterprise management/operation systems
- it has broad industry support and is being used in support of other industry standards such as ISA S95, ISA S88, EDDL, MIMOSA, OAGiS, etc.

Considering the above-defined requirements and OPC UA main technology features (Section [OPC UA Main Technology Features]) as a starting point for further discussion a generic architecture is proposed (Section [Semantic-Data Processing Architecture]) that allows designing robust real-time globally scoped distributed systems against the cyber-physical systems paradigm called Industry 4.0.

To deploy the Industry 4.0 paradigm additionally the data holder mobility behavior must be incorporated. This way we have entered the domain of Internet applications coined as the Internet of Things (IoT) (Section [Internet of Things (IoT) Communication]). One of the arguments for an Internet of Things is allowing distributed yet interlinked devices, machines, and objects (data holders) to interact with each other without relying on human intervention to set-up and commission the embedded intelligence. In this context, the IoT is all about:

- mobile data fetching - how to gather data from mobile things (data holders)
- mobile data distribution - how to transfer the data over the Internet to a place where it could be processed
- mobile data processing - how to integrate consistently the partial data into a selected application as one whole to improve process behavioral performance

On the foundation of this model, selected interoperability deployment issues and available solutions are being researched in the project. The discussion is focused on the issues related to:

- meaningful data transfer based on reactive and interactive parties relationship
- data oriented architecture
- underlying process data binding
- data discovery
- data security

> OPC Unified Architecture is a suit of standardization documents so more research is required to achieve a synergy between systematic design methodology and available tools compliant with this specification.

## See also

- [OPC UA Main Technology Features]
- [Semantic-Data Processing Architecture]
- [Internet of Things (IoT) Communication]

[Internet of Things (IoT) Communication]:Networking/README.MD
[Semantic-Data Processing Architecture]:SemanticData/README.MD 
[Address Space and Address Space Model]:SemanticData/AddressSpaceAddressSpaceModel.md
[UA Information Model - Concept]:SemanticData/InformationModelConcept.md
[OPC UA Main Technology Features]:OPCUAMainTechnologyFeatures.md
[References]:REFERENCES.md




