
# Content Description

## Tools

## AddressSpaceComplianceTestTool

It is command line application aimed to validate XML files compliant with the [UANodeSet schema](https://opcfoundation.org/UA/2011/03/UANodeSet.xsd) defined in Part 6 of the OPC UA Specification. The description of this schema is captured in the document [Address Space Interchange XML](http://goo.gl/LE64MA)

## Libraries

### UANodeSetValidation library

The [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) project is aimed to validate UANodeSet xml files.
To validate a new model add the code to the [USNodeSetValidationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/USNodeSetValidationUnitTestProject)
and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved.
In this case add issue or modify the code on your fork and add purge request after finishing.

**Public API**:
 The public API for this project is defined in by the interface:
*IAddressSpaceContext*

This interface may be used for dependency injection.

## InformationModelFactory library

TBD

## Tests projects

TBD

## Resources

### Media

This folder contains files referred to by the documentation.
