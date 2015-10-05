# Introduction #

This project i aimed to provide a prototype library for Part 14 using the concept described in the [
OPC UA Data Processing Outside the Server](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution#opc-ua-data-processing-outside-the-server)

The code is tested using the Unit Test located in the project: https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/Tests/DataManagementUnitTest


The project is in development stage -  major changes are expected.

#Architecture#

The library is designed to be a foundation for developing application programs that are parties of message centric communication pattern. The diagram presents relationship between the `DataMangement` library and other communication parties where the library is expanded to provide custom functionality.

![Architecture] (https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/Media/DataManagementGeneralization.png)

#Asumptions#

* It is assumed that the data consumer is lightweight and may support limited encoding/decoding functionality. The functionality will be  provided as a plug-in library injected at run time. EncodingDecoding is recognizable using a pair of: name recovered from the Type object and OPC UA DataType represented by its URI.
* Data binding - it is assumed that the binding of Local repositories/variables and messages content items is provided by the configuration. It could be also provided at runtime by updating the configuration using any external mechanism.

# Remarks #
## Bindings and encodings implementation ##

Binding between the local repository (e.g. HMI Screen, Server Address Space) and the message content items is provided by the `IBinding` interface and its basic implementation `Binding` class. This class is responsible to decode the data from the format used to construct the message to the local type. See definition for details:
[IBinding](https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/DataManagement/IBinding.cs).
It is expected that the value conversion (decoding) is provided by an instance of the IValueConverter class. If it is not applicable the [Adapter patern](http://www.dofactory.com/net/adapter-design-pattern) must be used.
* See how it works in the unir test class: [BindingUnitTest](https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/Tests/DataManagementUnitTest/BindingUnitTest.cs).
