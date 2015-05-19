# Description

The [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) project is aimed to validate UANodeSet xml files. To validate a new model add the code to the [USNodeSetValidationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/USNodeSetValidationUnitTestProject) and XML file to the XMLModels in this project. If the validation test does not recognize an error the code in the UANodeSetValidation must be improved. In this case add issue or modify the code on your fork and add purge request after finishing.

# Public API
Public API of the [UANodeSetValidation](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/UANodeSetValidation) is described by the interface:
*IAddressSpaceContext*

This interface may be used for dependency injection. 
