#OPC Unified Architecture - Object Oriented Internet
##CHARTER
###What is Object Oriented Internet

In this project C# deliverables supporting a new Machine To Machine (M2M) communication architecture is to be reserched with the goal to provide a generic solution for publishing and updating information in a context that can be used to describe and discover it by software applications. It is implemented based on the [OPC Unified Architecture](http://goo.gl/y4EHUn) - a new emerging industrial integration standard that fulfills the proposed architecture requirements.

This project is aimed to workout deliverables supporting Process Data handling over Internet including but not limiting to:

•	Data Edition – UI allowing display and edition of any custom data

•	Data serialization and deserialization - see whitepaper: [Address Space Interchange XML](http://goo.gl/LE64MA)

•	Data binding – to define how the process data relate to the real world

•	Data Validation - see project [DataSerializationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/DataSerializationUnitTestProject)

•	Data Prototyping  - methods and tools to design custom data types

•	Data Discovery – methods and tools to find the data over the network.

In scope there are also deliverables supporting:

•	Exposition of the Process Data in the context of Metadata [OPC UA Address Space Model Designer](http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx)

•	Browsing (using the sematic) of the Metadata to selectively access requested Process Data

•	Modeling and representation of the Metadata - see whitepaper: [OPC UA Information Model Deployment] (http://goo.gl/HqYjvy)

•	Validation semantic and consistency of the Metadata - see project [UANodeSetValidationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/USNodeSetValidationUnitTestProject)

The presented approach is a real proposal for new technology wave based on the existing Internet infrastructure because it allows vendors to provide generic off-the-shelf products tested independently for interoperability.

###Out of scope

Out of scope is any work on exchanging the Process Data and Metadata over the network. The hope is that the interoperability can be gained as the result of usage of the OPC Unified Architecture international standard. 

###Conclusion

I hope it is a good place to prototype and converge the OPC UA communication technology with Semantic Data, Internet Of Things, Plug and Play, Global Data Discovery, Selective Availability, etc. concepts. My goal is to bridge a gap between OPC UA technology and Industrial IT Application Domains. 

Redd more:

The paper **Object Oriented Internet** will be presented on the conference [3rd International Conference on Innovative Network Systems and Applications](https://fedcsis.org/inetsapp)

[WIKI of this project](https://github.com/mpostol/OPC-UA-OOI/wiki)

[My Blog: About enablers of future solutions](http://wwww.mpostol.wordpress.com/)

[About me on LinkedIn](https://pl.linkedin.com/in/mpostol)

[OPC Foundation](https://opcfoundation.org/)

[Sponsored by commsvr.com](http://www.commsvr.com/)
