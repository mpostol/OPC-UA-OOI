# OPC UA Address Space Prototyping

The OPC UA Address Space Prototyping (asp.exe) tool

- creates OPC UA Address Space using input XML files compliant with the `UANodeSet` schema.
- validates the  OPC UA Address Space against the OPC UA specification 1.04.
- generates XML file compliant with the `ModelDesign` schema that is input standard for the [OPCFoundation/UA-ModelCompiler](https://github.com/OPCFoundation/UA-ModelCompiler).

## Syntax

> asp \{ *filePath* \} [(-e | --export=)*filePath*] [ (-s | --stylesheet=)*stylesheetName* ] [ (-n | --namespace=)*ns* ] [ --nologo ]
>
> asp [ --help ] [ --version ]
> 
> asp [ help ] [ version ]

## Argument

|Argument|Description|
|--------|-----------|
|*filePath*| Specifies the input file to convert. At least one file containing Address Space definition compliant with UANodeSet schema must be specified. Many files may be entered at once.

## General Options

|Option|Description|
|--------|-----------|
|`help`|Displays command syntax and options for the tool. It halts processing and displays the help screen. The help screen is also displayed when the parsing process fails, along with the clear and explicit description of every error encountered.|
|`version`|Prints version information using this switch or the built-in version verb.|
|`nologo`|Suppresses the banner.|

## ModelDesign Options

|Option|Description|
|--------|-----------|
|`e[xport]`*ModelDesign.xml*| Specifies the output file containing the ModelDesign XML document.|
|`s[tylesheet]`*\<stylesheetName\>* |Name of the stylesheet document (XSLT - eXtensible Stylesheet Language Transformations). With XSLT you can transform an XML document into any text document.|
|`n[amespace]`*ns*| Specifies the namespace for the generated types. If not specified last imported model is used for export. 

## Remarks

> Version limitation
> 
> - Current version of the tool exports only types to ModelDesign XML file.

