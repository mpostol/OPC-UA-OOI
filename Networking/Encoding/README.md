# Encoding Library

## Getting Started

This library is a loosely coupled part of the `ReferenceApplication` described in the section [Reference Application Graphical User Interface](../ReferenceApplication/README.MD). The main purpose of this library is the interoperability testing and diagnostic.

It provides `UAOOI.Networking.SemanticData.IEncodingFactory` limited implementation in the class `EncodingFactoryBinarySimple`.  Using this implementation the library can encode/decode only simple data types. The `ReferenceApplication` uses implementation of this class for late binding to inject dependency on the encoding functionality.  

The implementation of the `UAOOI.Networking.SemanticData.Encoding.IUADecoder` interface is provided by the `UABinaryDecoderImplementation`. The implementation of the `UAOOI.Networking.SemanticData.Encoding.IUAEncoder` interface is provided in the `UABinaryEncoderImplementation`;

This `UpdateValueConverter` method is responsible to lookup a dictionary containing value converters and if any assigns it to `IBinding.Converter` property. This method doesn't support this functionality and left the property unassigned.

This library may be easily replaced by a custom one - change the composition contract in:

- `UAOOI.Networking.DataLogger.LoggerManagementSetup`
- `UAOOI.Networking.SimulatorInteroperabilityTest.SimulatorDataManagementSetup`

## Current release

This library has been released as the NuGet package [UAOOI.Networking.Encoding](https://www.nuget.org/packages/UAOOI.Networking.Encoding).

Main purpose of this release is to support implementation of the interoperability tests defined by the OPC Foundation. In the production environment, you may simply replace this library by a custom one providing unlimited encoding functionality.

## Contributing

Please read CONTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.

Consider expanding functionality in this library by implementing the methods in classes: `UABinaryDecoderImplementation` and `UABinaryEncoderImplementation`.

## Running tests

The library is not Unit Tested.

## See also

- [Reference Application Graphical User Interface](../ReferenceApplication/README.MD)
- [Reference Application Consumer - Data Logger](../DataLogger/README.md)
- [Reference Application Producer - Interoperability Test Data Generator](../SimulatorInteroperabilityTest/README.md)
