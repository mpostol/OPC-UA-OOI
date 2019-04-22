# Companion Specification - Information Model for Analyzers

In 2008 the OPC Foundation announced support for Analyzer Devices Integration into the OPC Unified Architecture and created a working group composed of end-users and vendors with the main goal of developing a common method for data exchange and an analyzer data model for process and laboratory analyzers. In 2009 the OPC Unified Architecture Companion Specification for Analyzer Devices was released [[1]][OPC.UA.ADI]. To prove the concept a reference implementation was developed containing an ADI compliant server and simple client using the Software Development Kid released by the OPC Foundation.

The model described in the specification [[1]][OPC.UA.ADI] is intended to provide a unified view of analyzers irrespective of the underlying device. This Information Model is also referred to as the ADI Information Model. As it was mentioned, analyzers can be further refined into various groups, but the specification defines an Information Model that can be applied to all the groups of analyzers.

The ADI Information Model is located above the DI Information Model [[9]][OPC.UA.DI]. It means that the ADI model refers to definitions provided by the DI model, but the reverse is not true. To expand the ADI Information Model, the next layers shall be provided.

Analyzing in detail the whole ADI Information Model is impractical here. Hence, the discussion below will be focused only on selected types defined in this specification to illustrate the design practice of the model adoption.

The object model that describes analyzers is separated into definitions of the types representing the main parts of the device, namely: `AnalyserDeviceType`, `AnalyserChannelType`, `StreamType`, `AccessoryType` and `AccessorySlotType`.

In general terms `AnalyserDeviceType` represents the instrument as a whole. Each object of the `AnalyserDeviceType` has at least one component of the `AnalyserChannelType` and may have components of the `AccessorySlotType`. Similarly, each object of `AnalyserChannelType` may have `AccessorySlotType` components.

`AnalyserDeviceType` is an abstract type which shall be subtyped for different types of analyzer devices. In the specification [[1]][OPC.UA.ADI] there are defined the following subtypes of the `AnalyserDeviceType`: `SpectrometerDeviceType`, `AcousticSpectrometerDeviceType`, `MassSpectrometerDeviceType`, `ParticleSizeMonitorDeviceType`, `ChromatographDeviceType`, `NMRDeviceType`. Each of these types may be further subtyped by device vendors to converge the Information Model and the underlying process.

## See also

- [[1] OPC Unified Architecture for Analyzer, OPC Foundation, Rel. 1.1a, 2015-01-09][OPC.UA.ADI]
- [[2] Part 100: Device Information Model, OPC Foundation, 1.02, 2019-04-19][OPC.UA.DI]

[OPC.UA.DI]:https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-100-device-information-model/
[OPC.UA.ADI]:https://opcfoundation.org/developer-tools/specifications-opc-ua-information-models/opc-unified-architecture-for-analyzer-devices-adi/
