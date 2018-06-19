
# Content Description

The `SemanticData` folder contains projects related to support the [OPC UA Data Processing Outside of the Server](./README.MD).  Processing of the OPC UA Data Outside of the Server context is based on the OPC **UA Semantic Data** concept.

The **Tools** section describes projects with output type console application. These deliverables may be used as utilities or as examples illustrating how to use the libraries.
The **Libraries** offer functionally supporting creation and validation of the OPC **UA Semantic Data**.

## Tools

## AddressSpaceComplianceTestTool

It is command line application aimed to validate XML files compliant with the [UANodeSet schema](https://opcfoundation.org/UA/2011/03/UANodeSet.xsd) defined in Part 6 of the OPC UA Specification. The description of this schema is captured in the document [Address Space Interchange XML](http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/P-150101E-AddressSpaceInterchangeXML.pdf)

## Libraries

### UANodeSetValidation

The [UANodeSetValidation](./UANodeSetValidation/README.MD) project is aimed to validate `UANodeSet` xml files.

To validate a new model add the code to the [USNodeSetValidationUnitTestProject](./Tests/USNodeSetValidationUnitTestProject/README.md) and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved.

In this case add issue or modify the code on your fork and add purge request after finishing.

**Public API**:
 The public API for this project is defined in by the interface:
*IAddressSpaceContext*

This interface may be used for dependency injection where validation of the input data conforming to the 'UANodeSet' schema is processed.

### InformationModelFactory

TBD

### OPCUAUserInterfaceTypeEditors

This projects shall provide a custom design-time experience for OPC UA types by implementing a user interface (UI) type editor. All works related to development of XAML User Controls with the goal to edit OPC UA values represented as objects of custom type. The build in data types are described in the document:[Address Space Interchange XML](http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/P-150101E-AddressSpaceInterchangeXML.pdf).
The MVVM is to be used as the deign pattern in this project.

Detailed description of this project is captured in the document: [OPCUAUserInterfaceTypeEditors/README.MD](./OPCUAUserInterfaceTypeEditors/README.MD)

## Tests projects

### DataSerializationUnitTestProject
TBD

### USNodeSetValidationUnitTestProject
TBD


