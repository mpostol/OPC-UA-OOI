# Address Space Prototyping Tool (asp.exe)

## Getting Started

The **OPC UA Address Space Prototyping** (asp.exe) is an engineering tool and can be utilized in several ways:

- creates `UA Address Space` populated using input XML files compliant with the `UANodeSet` schema defined in Part 6 Annex F.
- validates an instance of the `UA Address Space` against the OPC UA specification 1.04.
- exports XML file compliant with the `ModelDesign` schema that may be used as the input for the [OPC UA Information Model Compiler][OPC.UA.ModelCompiler].

These instructions will get you a copy of the software up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

## Syntax

```txt
asp { filePath } [(-e | --export=) filePath] [ (-s | --stylesheet=)*stylesheetName* ] (-n | --namespace=) ns [--nologo]

asp [--help] [--version]

asp [help] [version]

```

### Argument

| Argument   | Description                                                                                                                                                                                                         |
| ---------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| *filePath* | Specifies the input file to populate the internal `UA Address Space`. At least one file containing `UA Address Space` model compliant with `UANodeSet` schema must be specified. Many files can be entered at once. |

### General Options

| Option    | Description                                                                                                                                                                                                                                                                                         |
| --------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `help`    | Displays command syntax and options for the tool. It halts processing and displays the help screen. The help screen is also displayed when the parsing process fails, along with the clear and explicit description of every error encountered. The switch or the built-in `help` verb can be used. |
| `version` | Prints version information. The switch or the built-in `version` verb can be used.                                                                                                                                                                                                                  |
| `nologo`  | Suppresses the banner.                                                                                                                                                                                                                                                                              |

### ModelDesign Options

| Option                              | Description                                                                                                                                                  |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `e`[ `xport` ] *path*               | Specifies the output file containing the ModelDesign XML document.                                                                                           |
| `s`[ `tylesheet` ] *stylesheetName* | Name of the stylesheet document (XSLT - eXtensible Stylesheet Language Transformations). With XSLT you can transform an XML document into any text document. |
| `n`[ `amespace` ] *ns*              | Specifies the namespace of the model to be processed.                                                                                                        |

### Command-line Syntax

The following table describes the notation used to indicate command-line syntax.

| Notation                        | Description                                        |
| ------------------------------- | -------------------------------------------------- |
| Text without brackets or braces | Items you must type as shown                       |
| Text Italic                     | Placeholder for which you must supply a value      |
| [Text inside square brackets]   | Optional items                                     |
| {Text inside braces}            | Items that can be repeated                         |
| \| Vertical bar                 | Separator for mutually exclusive items; choose one |

## Deployment

The current binary release containing ready to use application is available on the repository [Releases][OOI.Releases] page. Download and run the file to unzip the content to the selected folder. Now you may run the application file `asp.exe`. The section `Examples` cover details on how to get started using attached examples.

The application is located on the GitHub at [OPC-UA-OOI][OPC-UA-OOI] and maintained as the project

- [`OPC-UA-OOI/SemanticData/AddressSpaceComplianceTestTool/`](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticData/AddressSpaceComplianceTestTool).

## Examples

### Common Task Scrips

There are the following Windows Command shell scripts attached to the software and located in the main folder where the downloaded file has been unzipped:

- `DoDisplayHelp.cmd` -  displays a help screen and version information
- `DoValidate.cmd` - validates the `UA Address Space` content populated using the `XMLModels\DataTypeTest.NodeSet2.xml` XML document
- `DoExport.cmd` - validates the `UA Address Space` populated as above and exports the model to the `XMLModels\DataTypeTest.ModelDesign.xml`

> NOTE: The folder `XMLModels` also contains an example `DataTypeTest.NodeSet2.xml` XML document compliant with `UANodeSet` used by above-mentioned scripts. For your convenience, the folder also contains relevant schema files.

### How to Display Help

Run the script `DoDisplayHelp.cmd` or enter the following command

``` C#
asp --help --version
```

It allows you to display a help screen and version information. A similar screen is used to report syntax errors to the end user.

### How to validate `UA Address Space` consistency

Run the script `DoValidate.cmd` or enter the following command

``` txt
asp "XMLModels\DataTypeTest.NodeSet2.xml"
```

The screen will contain a verbose listing of diagnostic messages related to the processing of the input file `DataTypeTest.NodeSet2.xml` The messages can be examined to improve the files used to populate the `UA Address Space` and remove any inconsistency against the OPC UA Specification 1.04.

### How to Export Selected Model to ModelDesign

Run the script `DoExport.cmd` or enter a similar command to the following one

``` txt
asp "XMLModels\DataTypeTest.NodeSet2.xml" -e "XMLModels\DataTypeTest.ModelDesign.xml" -s XMLstylesheet
```

The screen will contain a verbose listing of diagnostic messages related to the processing of the input file - `DataTypeTest.NodeSet2.xml` in this example. All inconsistency problems will be fixed on a best-effort attempt basis. Finally, the model will be exported to the file `XMLModels\DataTypeTest.ModelDesign.xml`.  

The model contains the XSLT instruction

```XML
<?xml-stylesheet type="text/xsl" href="XMLstylesheet"?>
```

to transform XML documents into other formats (like transforming XML into HTML, PDF, mark-down, etc).

- XSL (eXtensible Stylesheet Language) is a styling language for XML.
- XSLT stands for XSL Transformations.

Have a look at W3C documentation [XSLT Introduction][XSLT Introduction] to get more about XML transformation

### Remarks

- **Versioning**

We use [Semantic Versioning 2.0.0](http://semver.org/) for versioning. For the versions available, see the [releases][OOI.Releases] page of the project.

- **Related work**

The `ModelDesign` exporter has been derived from the [CAS Address Space Model Designer][CAS.ASMD] component.

- **Contributing**

Please read [CONTRIBUTING.md][CONTRIBUTING.md] for details, and the process for submitting pull requests to us. Let me know in case any problems related to documentation, application asp.exe or the libraries is encountered.

- **License**

This software is licensed under the MIT License - see the [LICENSE.md][LICENSE.md] file for details.

- **Version limitations**

## See also

- [Mariusz Postol. OPC UA Information Model Deployment. 2016. Version 1.2][CAS.OPCUAIMD] [![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.2586616.svg)](https://doi.org/10.5281/zenodo.2586616)
- [![ebook](https://img.shields.io/badge/OOI-read_on_Gitbook-brightgreen.svg)](https://commsvr.gitbook.io/ooi) - the ebook **Object Oriented Internet** contains description of this project.
- [OPC-UA-OOI Home Page][OPC-UA-OOI]
- [OPC Unified Architecture][wordpress.opc-ua]
- [OPC Unified Architecture - Main Technological Features][wordpress.OPCUAMTF]
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]
- [OPC UA Address Space Model Designer][CAS.ASMD]
- [XSLT Introduction][XSLT Introduction]
- [OPC UA Information Model Compiler][OPC.UA.ModelCompiler]
- [How to contribute][CONTRIBUTING.md]

[CAS.OPCUAIMD]: https://zenodo.org/record/2586616#.XdAT5FdKiUk
[wordpress.opc-ua]: https://mpostol.wordpress.com/opc-ua/
[wordpress.OPCUAMTF]: https://mpostol.wordpress.com/2013/08/04/opc-unified-architecture-main-technological-features/
[wordpress.OPCUACD]: https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/
[LICENSE.md]: https://github.com/mpostol/OPC-UA-OOI/blob/master/license.md
[CONTRIBUTING.md]: https://github.com/mpostol/OPC-UA-OOI/blob/master/CONTRIBUTING.md
[OPC-UA-OOI]: https://github.com/mpostol/OPC-UA-OOI
[OOI.Releases]: https://github.com/mpostol/OPC-UA-OOI/releases
[CAS.ASMD]: https://github.com/mpostol/ASMD
[OPC.UA.ModelCompiler]: https://github.com/mpostol/UA-ModelCompiler#opc-ua-information-model-compiler-
[XSLT Introduction]: https://www.w3schools.com/xml/xsl_intro.asp
