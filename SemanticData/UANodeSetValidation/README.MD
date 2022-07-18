# Address Space Management Implementation

This library project implements functionality of the `Address Space Management` domain model class described in the section [Semantic-Data Processing Architecture](../README.md). This library supports the following functionality:

- creates `Address Space` instance populated using input XML NodeSet files, which conforms to the schema `UANodeSet` defined in Part 6 Annex F
- validates consistency of the `Address Space` instance against the OPC UA specifications set 1.04
- supports the OPC UA Information Model export, for example XML file compliant with the `ModelDesign` schema that is input format for the [UA-ModelCompiler][UA-ModelCompiler].

> **Note**: The OPC Foundation Model Compiler generates C# and ANSI C source code from XML files which include the UA Services, data-types, error codes, etc.; and numerous CSV files that contain NodeIds, error codes, and attributes etc.

## See also

- [M.Postol, Address Space Interchange XML](https://www.researchgate.net/publication/334259707_OPC_UA_Address_Space_Interchange_XML); DOI: DOI: 10.13140/RG.2.2.12228.37768
- [OPC Unified Architecture eBook](http://goo.gl/y4EHUn)

[UA-ModelCompiler]:https://github.com/mpostol/UA-ModelCompiler
