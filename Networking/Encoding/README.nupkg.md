# Encoding Library

## Getting Started

This library is a loosely coupled part of the `ReferenceApplication` described in the document [Walk-through ReferenceApplication](https://commsvr.gitbook.io/ooi/reactive-communication/referenceapplication). The main purpose of this library is the interoperability testing and diagnostic. Check out the `Source repository` to get detailed description.

It provides `UAOOI.Networking.SemanticData.IEncodingFactory` implementation in the class `EncodingFactoryBinarySimple`.  Using this implementation the library can encode/decode only simple data types. The `ReferenceApplication` uses implementation of this class for late binding to inject dependency on the encoding functionality.  

The implementation of the `UAOOI.Networking.SemanticData.Encoding.IUADecoder` interface is provided by the `UABinaryDecoderImplementation`. The implementation of the `UAOOI.Networking.SemanticData.Encoding.IUAEncoder` interface is provided in the `UABinaryEncoderImplementation`;

This `UpdateValueConverter` method is responsible to lookup a dictionary containing value converters and if any assigns it to `IBinding.Converter` property.

This library may be easily replaced by a custom one - change the composition contract in:

- `UAOOI.Networking.DataLogger.LoggerManagementSetup`
- `UAOOI.Networking.SimulatorInteroperabilityTest.SimulatorDataManagementSetup`

