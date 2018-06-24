
# OPC Unified Architecture - Object Oriented Internet

This project is aimed to workout deliverables supporting strong typed Process Data handling over the Internet. It contains example applications and class libraries.

This framework class library is a library of classes, interfaces, and value types that provide access to supported OPC UA Data processing functionality. The documentation of the library code is available on-line at:

* [API Browser](http://www.commsvr.com/download/OPC-UA-OOI/index.html)

The namespaces in the class library are as follows and documented in detail in this release notes.

* `UAOOI.SemanticData`
* `UAOOI.DataBindings`

## Library UAOOI.SemanticData

### Introduction

Main aim of this namespace is to support engineering of data processing in context of the data semantics at all the stages of this process to manipulate the data meaningfully outside the OPC UA Server.

### Deliverables

#### UA Data Example Application

The description of this application is available at: [UA Data Example Application](../../Networking/ReferenceApplication).

The installation package is available at: http://www.commsvr.com/COInstal/UANetworking/uand.html

This release contains modifications required to start interoperability testing against PubSub protocol version 1.10.

#### The library

The most important projects for this library release are as follows:

* [OPC UA DataManagement Library](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticData/DataManagement#opc-ua-datamanagement-library)
* [UA Data Networking Configuration](https://github.com/mpostol/OPC-UA-OOI/tree/master/Configuration/Networkingn#ua-data-networking-configuration)
* [OPC UA NodeSet Validation](https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticData/UANodeSetValidation/README.MD#opc-ua-nodeset-validation)

The Nuget packages are available at:

* not yet released.

### Versions

Current release:

* Assembly Product:       Object Oriented Internet
* Assembly Version:       4.0.0

# Library UAOOI.DataBindings

### Introduction

Data binding library contains classes, interfaces, and value types that may be used to define how the process data relates to the real world.

### Deliverables

The most important projects for this library release are as follows:

* [Data Bindings Solution Library](https://github.com/mpostol/OPC-UA-OOI/tree/master/DataBindingsSolution)

The NuGet package is available at:

* [OPC UA DataBindings Library 1.0.15](https://www.nuget.org/packages/UAOOI.DataBindings/)

### Versions

* Assembly Product:       Object Oriented Internet
* Assembly Version:       4.0.0

## Contact

For assistance, contact:

Mariusz Postol
[Contact](http://www.commsvr.com/tabid/85/language/en-US/Default.aspx)
http://www.commsvr.com/

Copyright (C) 2016, commsvr.com LODZ POLAND

