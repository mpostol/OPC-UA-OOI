# Object-Oriented Internet

## Preface

### Object-Oriented Internet - Machines to Machine Meaningful Interoperability

It is said that we are or soon will be citizens of a global village - a world considered as a single community linked by telecommunications. All applications designed atop of network communication can be grouped as follows:

- **human-centric** - applications where the information origin or information destination is an operator
- **machine-centric** - applications where information creation, consumption, networking, and processing are achieved entirely without human interaction

A typical **human-centric** approach is web-service supporting, for example, online bank account management. In this case, it is essential that any uncertainty and necessity to make a decision can be relaxed by human interaction. Coordination of multi-robot behavior in a work-cell or autonomous cars entering a service area fulfills the **machine-centric** scenario. It is crucial that, in this case, any human interaction is impractical or even impossible. **This interoperability scenario requires a machine to machine communication (M2M) demanding multi-vendor devices integration**.

The human-centric global village is almost done. However, the machine-centric global village still needs design and development effort. Information and Communication Technology (ICT) has provided society with a vast variety of distributed machine-oriented applications including the meaningful Machine to Machine (M2M) communication targeting distributed mobile applications in the context of new emerging disciplines, i.e. Industry 4.0 (I40) and Internet of Things (IoT). However, it is a real challenge if the mentioned machines are provided by a vast variety of vendors. The real challenge we are facing is how to produce independently smart things (i.e. machines, devices, appliances, assets, etc.) to guarantee that they are plug and produce ready. There are no doubts, it requires standardization. I believe that while producing the machines in compliance with the OPC Unified Architecture this issue is relaxed by applying the following OPC UA standardized concepts:

- **Information Model** - all about how to design a formal but mutually meaningful and shareable description of the considered process
- **Address Space** - all about how to instantiate and expose to the network a life replica of the process providing real-time data according to the above-mentioned formal description

The standardization process may be "paper-driven" or "community-driven". In both cases, standardization is indispensable but not sufficient. Let me recall that the foundation for the human-centric global village is just the Internet Protocol defined in 1981 and derived from the academic abstract knowledge and practitioners' concrete experience. It is worth stressing that it was published as an open-access document (RFC 791) and it has not been deprecated yet.

This umbrella project targets multi-vendor plug-and-produce machines interoperability scenarios targeting all aspects of the machine-centric global village concept aimed at providing reusable deliverables, training, best practice rules, prototyping, compliance testing and dissemination of valuable results.

I am a researcher who is passionate about applying knowledge and experience in building a machine-centric global village. Let's build it with you and for you. To join our effort and create an organization context I have launched the [**Object-Oriented Internet Partnership Program**](https://github.commsvr.com/AboutPartnershipProgram.md.html).

> Consider joining as a sponsor, contributor or end-user. Details are covered by the section [**How to be involved**](https://github.commsvr.com/AboutPartnershipProgram.md.html#how-to-be-involved)
>
> Your participation is needed to make sure the work will continue as expected.

### What is Object Oriented Internet

In this project, C\# deliverables supporting a new Machine To Machine \(M2M\) communication approach based on the **Data-Oriented Architecture** (DOA) paradigm is to be researched. The goal is to provide a generic solution for publishing and updating information in a context that can be used to describe and discover it by software applications. It is implemented based on the [OPC Unified Architecture][wordpress.OPCUAMTF] (OPC UA)- an industrial integration standard that fulfills the proposed architecture requirements.

The [Object Oriented Internet](https://fedcsis.org/proceedings/2015/pliks/160.pdf) article published in **Proceedings of the Federated Conference on Computer Science and Information Systems** captures description of this idea.

|                                                                  Go To                                                                  | Description                                                                                                                                               |
| :-------------------------------------------------------------------------------------------------------------------------------------: | --------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [![Release](https://img.shields.io/github/release/mpostol/OPC-UA-OOI.svg?style=popout)](https://github.com/mpostol/OPC-UA-OOI/releases) | latest release                                                                                                                                            |
|              [![ebook](https://img.shields.io/badge/OOI-read_on_Gitbook-brightgreen.svg)](https://commsvr.gitbook.io/ooi)               | the ebook **Object Oriented Internet** contains description of this project - it is auto-generated content gathered from *.md files from this repository. |
|                [![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.1198852.svg)](https://doi.org/10.5281/zenodo.1198852)                | you can cite all versions by using the DOI 10.5281/zenodo.1198852. This DOI represents all versions, and will always resolve to the latest one.           |

> Note: the DOI number is a unique identifying number associated with the repository version. Visit the section `How to cite the software and associated documentation files` to learn how to create derived works and cite the origin.

### Keywords

> *OPC, OPC UA, M2M communication, Client-Server, Publisher-Subscriber, Data Oriented Architecture, DOA, AMQP, MQTT, PubSub, OPC UA PubSub, Semantic-Data, Industry 4.0, I4.0, Internet Of Things, IoT, IIoT, Global Data Discovery, Security*

### Releases

We use [Semantic Versioning](http://semver.org/) for versioning. For the versions available, see the [releases on this repository](https://github.com/mpostol/OPC-UA-OOI/releases). For your convenience, the versions of the repository, tools and NuGet packages has been listed in the next subsections.

#### Repository

|   Version   | Milestone                                                                                                                                                                                                             |                                                    DOI                                                    |     Date     |
| :---------: | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------: | :----------: |
|    6.1.2    | Azure Gateway Implementation 1.0                                                                                                                                                                                      | [![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.4361640.svg)](https://doi.org/10.5281/zenodo.4361640) | Dec 18, 2020 |
|    5.1.0    | Semantic-Data ModelDesign Export 5.1.0                                                                                                                                                                                |                                          10.5281/zenodo.3345043                                           | Jul 21, 2019 |
| 5.0.2-Alpha | Updated the UA Address Space build against OPC UA Specification 1.04; updated UANodeSet schema; added export to ModelDesign [OPC UA Address Space Prototyping](SemanticData/AddressSpaceComplianceTestTool/README.md) |                                          10.5281/zenodo.2636426                                           | Apr 11, 2019 |
|    4.0.1    | .NET Standard implementation, documentation improved, new code help documentation available, tested against Xamarin.Android                                                                                           |                                          10.5281/zenodo.2555407                                           | Feb 1, 2019  |
|    3.1.0    | New version of the UAOOI.Networking.ReferenceApplication, documentation has been improved.                                                                                                                            |                                          10.5281/zenodo.1291549                                           | Jun 17, 2018 |
|    3.0.0    | DOI creation for the publication purpose.                                                                                                                                                                             |                                          10.5281/zenodo.1198853                                           | Mar 14, 2018 |

#### Tools

The table below lists the latest versions of the published Tools.

| Description                                                                                | Version |
| ------------------------------------------------------------------------------------------ | :-----: |
| [OPC UA Address Space Prototyping](SemanticData/AddressSpaceComplianceTestTool/README.md)  |  5.1.0  |
| [Semantic-Data Processing ReferenceApplication](Networking/ReferenceApplication/README.MD) | 4.00.01 |

#### NuGet packages

The table below lists the latest versions of the published NuGet packages.

| Id                                                                                                                       | Version | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| ------------------------------------------------------------------------------------------------------------------------ | :-----: | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [UAOOI.Common.Infrastructure](https://www.nuget.org/packages/UAOOI.Common.Infrastructure)                                |  4.0.1  | This library provides API for common infrastructure management functionality aimed at Object Oriented Internet application deployment.)                                                                                                                                                                                                                                                                                                                                                                                          |
| [UAOOI.Configuration.Core](https://www.nuget.org/packages/UAOOI.Configuration.Core)                                      |  4.0.1  | The library contains a shared interfaces for server configuration which OPC UA applications can reference. The library provides an abstraction over any OPC UA Server configuration plug-in. Using the library allows an application to indirectly access the server configuration attributes without relying on hard references. The hope is that using this library, third-party applications and frameworks can begin to leverage server configuration management without tying themselves down to a specific implementation. |
| [UAOOI.Configuration.DataBindings](https://www.nuget.org/packages/UAOOI.Configuration.DataBindings)                      |  4.0.1  | OPC UA Object Oriented Internet (UAOOI) DataBindings library is dedicated to create a plug-in aimed at editing of the configuration of any application based on OPC UA Information Model. It allows also to add data binding to the configuration.                                                                                                                                                                                                                                                                               |
| [UAOOI.Configuration.Networking](https://www.nuget.org/packages/UAOOI.Configuration.Networking)                          |  4.0.1  | OPC UA Object Oriented Internet (UAOOI) UAOOI.Configuration.Networking library is dedicated to create a plug-in aimed at provisioning configuration of any application based on OPC UA Information Model.                                                                                                                                                                                                                                                                                                                        |
| [UAOOI.Networking.Core](https://www.nuget.org/packages/UAOOI.Networking.Core)                                            |  4.0.2  | The core functionality of `Semantic-Data` reactive networking library based on OPC UA Part 14 Pub/Sub.                                                                                                                                                                                                                                                                                                                                                                                                                           |
| [UAOOI.Networking.Encoding](https://www.nuget.org/packages/UAOOI.Networking.Encoding)                                    |  4.0.2  | This package provides functionality to lookup a dictionary containing value converters. The interface is used for late binding to inject dependency on the external library.  This library provides IEncodingFactory functionality limited to encoding simple data types only for the testing purpose only.                                                                                                                                                                                                                      |
| [UAOOI.Networking.SemanticData](https://www.nuget.org/packages/UAOOI.Networking.SemanticData)                            |  4.0.2  | SemanticData Reactive Networking library based on OPC UA Part 14 Pub/Sub.                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| [UAOOI.Networking.UDPMessageHandler](https://www.nuget.org/packages/UAOOI.Networking.UDPMessageHandler)                  |  4.0.2  | OOI Reactive Networking: UDP IMessageHandlerFactory Implementation                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| [UAOOI.SemanticData.InformationModelFactory](https://www.nuget.org/packages/UAOOI.SemanticData.InformationModelFactory/) |  5.1.0  | OPC UA Information Model Factory Library                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| [UAOOI.SemanticData.UANodeSetValidation](https://www.nuget.org/packages/UAOOI.SemanticData.UANodeSetValidation/)         |  5.1.0  | This project supports validation of the OPC UA Address Space captured in the XML file against the OPC UA Specification. It is assumed that the Address Space is represented as an XML file compliant with the NodeSet schema. This library is dedicated to creating a plug-in aimed at importing models based on the OPC UA Information Model. It is part of the project supporting OPC UA Object Oriented Internet paradigm.                                                                                                    |
| [UAOOI.SemanticData.UAModelDesignExport](https://www.nuget.org/packages/UAOOI.SemanticData.UAModelDesignExport/)         |  5.1.0  | This library provides an implementation of the export functionality of an XML file compliant with the UAModelDeign schema. By design, the UAModelDeign schema describes the syntax of an XML document representing the OPC UA Information Model.                                                                                                                                                                                                                                                                                 |
| [UAOOI.SemanticData.BuildingErrorsHandling](https://www.nuget.org/packages/UAOOI.SemanticData.BuildingErrorsHandling/)   |  5.1.0  | It provides descriptions of building errors to be used for OPC UA Address Space consistency validation purpose.                                                                                                                                                                                                                                                                                                                                                                                                                  |

## Content

### Architecture

The repository workspace are organized as it is illustrated in the Figure below.

![Figure 1. Packages relationship](CommonResources/Media/Packages.png)

Next sections describe the content of the packages in the repository. The packages are loosely coupled. Each package contains applications (tools), libraries, and unit tests. All common resources are collected in the `CommonResources`.

### Common

This workspace provides API for common infrastructure management functionality aimed at Object Oriented Internet application deployment.

### Configuration

This workspace is aimed at implementing an editor of the `OOI Reactive Application` configuration file. It contains types that supports the configuration management of application implementing the paradigm described in [Semantic-Data Processing Architecture](SemanticData/README.MD). The configuration may be read to or write from the xml or json files. By design this library may be used to support variety kinds of applications at design and run time. It supports also the data binding mechanism to define how the process data relate to the real world.

### DataDiscovery

This workspace contains libraries and tools to find the data over the network. In the proposed approach the URI of the OPC UA **Information Model** is to be used as a unique key to browse the **Global Data Discovery System** \(GDDS ֠an expanded version of GDS\) to find recursively the destination `OPC UA Server`  or  `UA Data Application` exposing the requested data.

To get more details visit the section [Global Data Discovery](./DataDiscovery/README.MD)

### Networking

This workspace contains library empowering the Industrial Internet of Things \(IIoT\) and Industry 4.0. as the result of using reactive networking paradigm. Intentionally it is designed on top of OPC UA Part 14 Pub/Sub protocol supporting interoperability with any product compliant with this specification. Seamless integration with AMQP, MQTT, etc. allows meaningful data transfer in the context of semantics defined using **OPC UA Information Model**.

To get more details visit the section [Internet of Things (IoT) Communication](./Networking/README.MD)

### SemanticData

#### Introduction

The `SemanticData` folder contains projects related to support the [OOI Semantic-Data Processing Architecture](./SemanticData/README.MD). Processing of the OPC UA Data Outside of the Server context is based on the **Semantic-Data** concept. This project is aimed to workout deliverables supporting Process Data handling over Internet including but not limiting to:

- Data edition ֠UI allowing display and edition of any custom data
- Data serialization and deserialization - see white-paper [Address Space Interchange XML][InterchangeXML]
- Data prototyping - methods and tools to design custom data types
- Exposition of the process data in the context of metadata [OPC UA Address Space Model Designer](https://github.com/mpostol/ASMD)
- Browsing of the Metadata to selectively access requested Process Data
- Modeling and representation of the metadata - detailed description is covered by the section [OPC UA Information Model Deployment](SemanticData/InformationModelsDevelopment.md)
- Validation of the semantics and consistency of the metadata - see project [USNodeSetValidationUnitTestProject](./SemanticData/UANodeSetValidation/README.MD)
- Data Oriented Architecture (DOA)

#### Project `AddressSpaceComplianceTestTool`

It is a command-line application aimed at the validation of the XML files compliant with the [UANodeSet schema](https://opcfoundation.org/UA/2011/03/UANodeSet.xsd) defined in Part 6 of the OPC UA Specification. The description of this schema is captured by the document [Address Space Interchange XML][InterchangeXML]

#### Project `UANodeSetValidation`

The [UANodeSetValidation](./SemanticData/UANodeSetValidation/README.MD) project is a library aimed at validate `UANodeSet` xml files.

To validate a new model add the code to the [USNodeSetValidationUnitTestProject](./SemanticData/Tests/USNodeSetValidationUnitTestProject/README.md) and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved.

In this case add issue or modify the code on your fork and add pull request after finishing.

**Public API**:
 The public API for this project is defined by the interface:

- `IAddressSpaceContext`

This interface may be used for dependency injection where validation of the input data conforming to the `UANodeSet` schema is processed.

#### Project `InformationModelFactory`

The project is dedicated to develop the OPC UA Information Model Factory Library. It is a library used as the dependency injection to produce OPC UA **Information Model** by a selected importer. The abstract API must be implemented by a classes providing functionality of **Information Model** creation.

Detailed description is covered by the document [InformationModelFactory Library](./SemanticData/InformationModelFactory/README.md)

## Related work

### OPC UA Address Space Model Designer (`ASMD`)

The main challenge of the project [OPC UA Address Space Model Designer (ASMD)][ASMD] is to offer the designers a user-friendly tool supporting all aspects of the OPC UA Address Space model designing process. It brings together: designing, learning, and deploying. Description of the main features of the tool is covered by the section [Design and Deployment Support][DDSupport].

The OPC UA Address Space Model Designer ([`ASMD`][ASMD]) implements conceptual containers called solutions and projects to apply their settings. Any solution contains one or more projects and it manages the way the designer configures, builds, and deploys sets of related projects. Any project includes source files containing the model representation and related metadata such as properties and references to other projects. The designer Integrated Development Environment (IDE) provides tools that help you edit and manipulate models, namespaces, and add references to external resources.

### Process-Observer (`PO`)

Process-Observer (`PO`) is an archetype that allows creation consistent, homogeneous real-time representation of the underlying process. This representation is a kind of a process state and behavior replica, which exposes real-time process data to the network using standardized interfaces like OPC Classic, OPC Unified Architecture, OPC PubSub, AMQP, MQTT, etc. In other words, it supports **Machine to Sensors Connectivity (M2S)**, i.e. it allows an open, uniform, secure and standards-based communication solution between sensors, actuators, controllers and the upper layer applications.

A detailed description of this concept is covered by the article [Object Oriented Internet][OOI.ieeexplore]. By design this concept supports

- **Process Devices Interconnection** - synchronization of the process replica with the process state
- **Process Simulation** - simulation of the process behavior to recover unavailable data and ensure a safe testing environment
- **Resource Monitoring** - allowing to add information processing and networking infrastructure to be exposed consistently aggregated with the process replica
- **Server to Server Interactions** - supports a scenario in which `PO` is the Client of a Server

The `PO` concept has been implemented as a generic communication engine used by the CAS CommServer Classic and Unified Architecture servers. This implementation is optimized for highly distributed applications. This implementation is maintained in the [mpostol/ProcessObserver][OOI.PO] repository.

### CommServer

[CommServer][commserver] is a package of software to manage data transfer using OPC standards. Built-in technologies and algorithms support Machine to Machine (M2M) meaningful interoperability. The [CommServer][commserver] was written by CAS Lodz Poland.

The GitHub repository [commsvr-com/migration2os][migration2os] is aimed at the `CommServer` software migration from on-premise subversion repository to GitHub and publishing it as the Open-Source Software (OSS). The members of the `CommServer` family are to be converged with the Object-Oriented Internet paradigms and integrated with the solutions maintained in this repository.

### Object Oriented Internet Reactive Networking Configuration Editor

OPC-UA-OOI.ConfigEditor is a package of software to manage the configuration of the [Reactive Communication][OOIBookToC].  The `OPC-UA-OOI.ConfigEditor` was written by CAS Lodz Poland. The [mpostol/OPC-UA-OOI.ConfigEditor][ConfigEditor] repository is aimed at the software migration from on-premise subversion repository to GitHub and publishing it as the Open-Source Software (OSS).  After migration, this repository will be used to manage all activities addressing the software maintenance process as a plug-in of the [ASMD][ASMD].

## How to cite the software and associated documentation files

To be compliant with the license of the repository the below copyright notice shall be included in all copies or substantial portions of the software and associated documentation files (the "Software").

Copyright (c) 2020 Mariusz Postol

In this section, you will learn how to cite the "Software" using the DOI number. A DOI number is a unique identifying number for the Software version. Because this repository has a DOI, use the DOI in your citation for the article or any derived work, like this:

> Mariusz Postol, Object Oriented Internet: [Target Part Name], `https://github.com/mpostol/OPC-UA-OOI`, [year]
DOI: [10.5281/zenodo.1198852](http://doi.org/10.5281/zenodo.1198852).

or

> Mariusz Postol, Object Oriented Internet: [Target Part Name], `https://github.com/mpostol/OPC-UA-OOI`, [year]
DOI: [http://doi.org/10.5281/zenodo.1198852](http://doi.org/10.5281/zenodo.1198852).

Replace [year] with the current year and [Target Part Name] with the name (or names) of the files you are referring to.

## How to follow up?

GitHub offers `Discussions` as a space to connect with other members of the community. I hope that using the `Discussion` space you:

- ask questions you’re wondering about
- share ideas
- engage with other community members
- welcome others and are open-minded; remember that this is a community we build together

I have activated the [Discussion][Discussion] space for this repository. Follow the  [Discussion][Discussion] to be in touch.

To follow any activity in the repository, switch on the `Watch` functionality. If you find the project interesting, please star the repository. Starring a repository also shows appreciation to the repository maintainer for their work. You can star repositories and topics to keep track of projects you find interesting and discover related content in your news feed. Check out [Saving repositories with stars](https://docs.github.com/en/get-started/exploring-projects-on-github/saving-repositories-with-stars) to get more.

## Conclusion

I hope it is a good place to prototype and converge the OPC UA communication technology with `Semantic-Data`, Industry 4.0, Internet Of Things, Data Oriented Architecture, Plug and Play, Global Data Discovery, Selective Availability, etc. concepts. My goal is to bridge a gap between OPC UA technology and Industrial IT Application Domains.

The presented approach is a real proposal for a new technology wave based on the existing Internet infrastructure because it allows vendors to provide generic off-the-shelf products tested independently for interoperability.

## Partnership Program

I am a researcher and University associate who is passionate about applying knowledge and experience in building a Machine to Machine (M2M) meaningful interoperability based on OPC UA. Let's build it with you and for you. To join our effort and create an organizational context I have launched an open-access **Object-Oriented Internet Partnership Program**. Hence, maintenance of this repository and further development of the OPC UA Information Model Domain-Specific Language will be carried out under a broader concept described in the following article

[Object-Oriented Internet Partnership Program][Sponsorship]

**Consider joining**. Visit the section [How to be involved][SponsorshipToBeInvolved] to get more. I hope that thanks to this partnership program we will establish long-term mutually beneficial cooperation. Your participation is needed to make sure that the work will continue as expected. As a rule of thumb, the work priority is derived from community feedback.

I strongly encourage community participation and contribution to this project. First, please fork the repository and commit your changes there. Once happy with your changes you can generate a 'pull request'.

When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

Please note we have a code of conduct, please follow it in all your interactions with the project.

## See Also

### Publications

- [Object-Oriented Internet](https://youtube.com/playlist?list=PLC7zPvgw-YbyWss-0j_waddacgroLFTzi) This playlist on YouTube addresses research results on the systematic approach to the design of the meaningful Machine to Machine (M2M) communication targeting distributed mobile applications in the context of new emerging disciplines, i.e. Industry 4.0 and Internet of Things.
- Postół M., Szymczak P. (2021) Object-Oriented Internet Cloud Interoperability. In: Paszynski M., Kranzlmüller D., Krzhizhanovskaya V.V., Dongarra J.J., Sloot P.M. (eds) Computational Science – ICCS 2021. ICCS 2021. Lecture Notes in Computer Science, vol 12745. Springer, Cham. <https://doi.org/10.1007/978-3-030-77970-2_43>
  - Available on [ResearchGate](https://www.researchgate.net/publication/352289895_Object-Oriented_Internet_Cloud_Interoperability)
  - ICCS 2021: INTERNATIONAL CONFERENCE ON COMPUTATIONAL Presentation is available on [YouTube](https://youtu.be/yXH09wuWEcA)
- Postół M. (2020) Object-Oriented Internet Reactive Interoperability. In: Krzhizhanovskaya V. et al. (eds) Computational Science – ICCS 2020. ICCS 2020. Lecture Notes in Computer Science, vol 12141. Springer, Cham; [DOI: https://doi.org/10.1007/978-3-030-50426-7_31](https://doi.org/10.1007/978-3-030-50426-7_31)
  - Postół M. (2020) [Object-Oriented Internet Reactive Interoperability](https://www.researchgate.net/publication/341882427_Object-Oriented_Internet_Reactive_Interoperability), presentation, DOI: 10.13140/RG.2.2.33984.56323
- Mariusz Postol, [Machine to Machine Semantic-Data Based Communication: Comprehensive Survey](https://www.researchgate.net/publication/341165347_Machine_to_Machine_Semantic-Data_Based_Communication_Comprehensive_Survey) chapter in book [Computer Game Innovations 2018](https://www.researchgate.net/publication/335524620_Computer_Game_Innovations_2018), Publisher: Lodz University of Technology Press; ISBN: 978-83-7283-999-2
- ebook **Object Oriented Internet** contains description of this project - it is auto-generated content gathered from *.md files [![ebook](https://img.shields.io/badge/Gitbook-Release-brightgreen.svg)](https://commsvr.gitbook.io/ooi/tableofcontent)
- Mariusz Postol, [Object Oriented Internet][OOI.ieeexplore], [3rd International Conference on Innovative Network Systems and Applications](https://fedcsis.org/2015/inetsapp), 2015, [IEEE Xplore Digital Library][OOI.ieeexplore] [![DOI](https://img.shields.io/badge/DOI-10.15439%2F015F160-blue)](https://fedcsis.org/proceedings/2015/pliks/160.pdf)

### Related documents

- [Object-Oriented Internet Partnership Program][Sponsorship]
- [How to be involved][SponsorshipToBeInvolved]
- [WIKI of this project](https://github.com/mpostol/OPC-UA-OOI/wiki)
- [API Browser (is available for sponsors - consider joining)](https://github.commsvr.com/AboutPartnershipProgram.md.html)
- [OPC UA Address Space Model Designer (ASMD)][ASMD]
- [My Blog: About enablers of future solutions](http://wwww.mpostol.wordpress.com/)
- [OPC Unified Architecture – Main Technological Features][wordpress.OPCUAMTF]
- [About me on LinkedIn](https://pl.linkedin.com/in/mpostol)
- [References](REFERENCES.md) - this section contains links to selected internal and external resources.

### Related websites

- [OPC Foundation](https://opcfoundation.org/)
- [Sponsored by commsvr.com](http://www.commsvr.com/)

[Sponsorship]: https://github.commsvr.com/AboutPartnershipProgram.md.html
[SponsorshipToBeInvolved]: https://github.commsvr.com/AboutPartnershipProgram.md.html#how-to-be-involved
[wordpress.OPCUAMTF]: https://mpostol.wordpress.com/2013/08/04/opc-unified-architecture-main-technological-features/
[ASMD]:https://github.com/mpostol/ASMD
[DDSupport]:SemanticData/DesignDeploymentSupport.md
[InterchangeXML]: https://www.researchgate.net/publication/334259707_OPC_UA_Address_Space_Interchange_XML
[commserver]: http://www.commsvr.com/
[migration2os]: https://github.com/commsvr-com/migration2os
[migration2osProj]: https://github.com/orgs/commsvr-com/projects
[ConfigEditor]: https://github.com/mpostol/OPC-UA-OOI.ConfigEditor
[OOIBookToC]: TableOfContent.md#reactive-communication
[OOI.ieeexplore]:https://ieeexplore.ieee.org/abstract/document/7321562
[OOI.PO]:https://github.com/mpostol/ProcessObserver#process-observer
[Discussion]: https://github.com/mpostol/OPC-UA-OOI/discussions
