# OPC UA Address Space Prototyping

The OPC `UA Address Space Prototyping` (UAAddressSpacePrototyping.exe) tool

- creates OPC UA Address Space using input XML files compliant with the `UANodeSet` schema.
- validates the  OPC UA Address Space against the OPC UA specification 1.04.
- generates XML files compliant with the `ModelDesign` schema that is input standard for the [OPCFoundation/UA-ModelCompiler](https://github.com/OPCFoundation/UA-ModelCompiler).

## Syntax

```
UAAddressSpacePrototyping {file.xml} {-eModelDesign.xml}
UAAddressSpacePrototyping [--help] [--version]
UAAddressSpacePrototyping [help] [version]
```

## Argument

|Argument|Description|
|--------|-----------|
|*file.xml*| Specifies the input file to convert. Many files may be entered at once.

## General Options

|Option|Description|
|--------|-----------|
|*--help*|Displays command syntax and options for the tool. It halts processing and displays the help screen. The help screen is also displayed when the parsing process fails, along with the clear and explicit description of every error encountered.|
|*--version*|Prints version information using this switch or the built-in version verb.|

## ModelDesign Options

|Option|Description|
|--------|-----------|
|-eModelDesign.xml| Specifies the output file containing the ModelDesign XML document.|

## Remarks

> Version limitation
> 
> - Current version of the tool exports only types.

