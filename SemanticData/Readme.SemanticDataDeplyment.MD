
# GitHub Repository Content

## Introduction

The [Object Oriented Internet](https://github.com/mpostol/OPC-UA-OOI) repository contains deliverables supporting a new Machine To Machine (M2M) communication architecture is to be researched. The goal is to provide a generic solution for publishing and updating information in a context that can be used to describe and discover it by software applications.

## Networking

The [Networking](https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/SemanticData) folder contains projects related to support the [OOI Semantic Data Processing Architecture](./README.MD).  Processing of the OPC UA Data Outside of the Server context is based on the OPC **UA Semantic Data** concept.

The **Tools** section describes projects with output type console application. These deliverables may be used as utilities or as examples illustrating how to use the libraries.
The **Libraries** offer functionally supporting creation and validation of the OPC **UA Semantic Data**.

## SemanticData

### AddressSpaceComplianceTestTool

It is command line application aimed to validate XML files compliant with the [UANodeSet schema](https://opcfoundation.org/UA/2011/03/UANodeSet.xsd) defined in Part 6 of the OPC UA Specification. The description of this schema is captured in the document [Address Space Interchange XML](http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/P-150101E-AddressSpaceInterchangeXML.pdf)

### UANodeSetValidation

The [UANodeSetValidation](./UANodeSetValidation/README.MD) project is aimed at validate `UANodeSet` xml files.

To validate a new model add the code to the [USNodeSetValidationUnitTestProject](./Tests/USNodeSetValidationUnitTestProject/README.md) and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved.

In this case add issue or modify the code on your fork and add pull request after finishing.

**Public API**:
 The public API for this project is defined in by the interface:

- `IAddressSpaceContext`

This interface may be used for dependency injection where validation of the input data conforming to the 'UANodeSet' schema is processed.

### InformationModelFactory

the folder is dedicated to develop the OPC UA Information Model Factory Library. It is a library used as the dependency injection to produce **OPC UA Information Model** by a selected importer. The abstract API Must be implemented by a classes providing functionality of Information Model creation.

Detailed description is covered by the document [InformationModelFactory Library](../SemanticData/InformationModelFactory/README.md)



