# Object Oriented Internet

[![Join the chat at https://gitter.im/mpostol/OPC-UA-OOI](https://badges.gitter.im/mpostol/OPC-UA-OOI.svg)](https://gitter.im/mpostol/OPC-UA-OOI?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![DOI](https://zenodo.org/badge/33917970.svg)](https://zenodo.org/badge/latestdoi/33917970)

## CHARTER

### What is Object Oriented Internet

In this project, C\# deliverables supporting a new Machine To Machine \(M2M\) communication architecture is to be researched with the goal to provide a generic solution for publishing and updating information in a context that can be used to describe and discover it by software applications. It is implemented based on the [OPC Unified Architecture](http://goo.gl/y4EHUn) - a new emerging industrial integration standard that fulfills the proposed architecture requirements.

The [Object Oriented Internet](https://fedcsis.org/proceedings/2015/pliks/160.pdf) article published in **Proceedings of the Federated Conference on  
Computer Science and Information Systems** captures description of this idea.

## Content

The `SemanticDataSolution` folder contains projects related to support the [OPC UA Data Processing Outside of the Server](./SemanticDataSolution#opc-ua-data-processing-outside-the-server). Processing of the OPC UA Data Outside of the Server context is based on the OPC **UA Semantic Data** concept.

This project is aimed to workout deliverables supporting Process Data handling over Internet including but not limiting to:

•    Data Edition – UI allowing display and edition of any custom data

•    Data serialization and deserialization - see whitepaper: [Address Space Interchange XML](http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/P-150101E-AddressSpaceInterchangeXML.pdf)

•    Data binding – to define how the process data relate to the real world

•    Data Validation - see project description [OPC UA NodeSet Validation](./SemanticDataSolution/UANodeSetValidation)

•    Data Prototyping  - methods and tools to design custom data types

•    [Data Discovery – methods and tools to find the data over the network](./DataDiscovery)

In scope there are also deliverables supporting:

•    Exposition of the Process Data in the context of Metadata [OPC UA Address Space Model Designer](http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx)

•    Browsing \(using the sematics\) of the Metadata to selectively access requested Process Data

•    Modeling and representation of the Metadata - see whitepaper: [OPC UA Information Model Deployment](http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/20140301E_DeploymentInformationModel.pdf)

•    Validation of the semantics and consistency of the Metadata - see project [USNodeSetValidationUnitTestProject](./SemanticDataSolution/Tests/USNodeSetValidationUnitTestProject)

The presented approach is a real proposal for new technology wave based on the existing Internet infrastructure because it allows vendors to provide generic off-the-shelf products tested independently for interoperability.

### Out of scope

Out of scope is any work on exchanging the Process Data and Metadata over the network. The hope is that the interoperability can be gained as the result of usage of the OPC Unified Architecture international standard.

### Conclusion

I hope it is a good place to prototype and converge the OPC UA communication technology with Semantic Data, Semantc Web, Internet Of Things, Plug and Play, Global Data Discovery, Selective Availability, etc. concepts. My goal is to bridge a gap between OPC UA technology and Industrial IT Application Domains.

## Read more

The paper [**Object Oriented Internet**](https://fedcsis.org/proceedings/2015/pliks/160.pdf) has been presented on the conference [3rd International Conference on Innovative Network Systems and Applications](https://fedcsis.org/2015/inetsapp).

[WIKI of this project](https://github.com/mpostol/OPC-UA-OOI/wiki)

[My Blog: About enablers of future solutions](http://wwww.mpostol.wordpress.com/)

[About me on LinkedIn](https://pl.linkedin.com/in/mpostol)

[OPC Foundation](https://opcfoundation.org/)

[Sponsored by commsvr.com](http://www.commsvr.com/)

