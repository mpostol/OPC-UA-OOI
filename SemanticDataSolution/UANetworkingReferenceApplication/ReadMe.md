# UA Data Example Application

## Introduction

This project is aimed at implementing an example of the **UA Data Application** supporting producer and consumer roles simultaneously. The purpose of the **Example Application** is to demonstrate the concepts and syntax of the PubSub implementation, rather than to necessarily provide a realistic scenario for its use. For more extensive examples, see the [OPC UA Data Processing Outside the Server](../../SemanticDataSolution#opc-ua-data-processing-outside-the-server).

Both roles are implemented as independent threads that have common user interface for diagnostic and configuration purpose. The producer sends messages containing the field defined in the configuration file. The values are simulated. To correctly decode the messages the consumer must have configuration defining the same set of fields. The data is encoded using Variant.

Each role uses independent configuration file as follows:

* Producer: `ConfigurationDataProducer.xml`
* Consumer: `ConfigurationDataConsumer.xml`

The schema of the configuration files is available at:  [ConfigurationData.xsd](../../SemanticDataSolution/UANetworkingConfiguration/Serialization/ConfigurationData.xsd) and detailed description of the configuration is captured in the document [UA Data Networking Configuration](../../SemanticDataSolution/UANetworkingConfiguration#ua-data-networking-configuration).

By the user interface (UI) remote host name and port numbers can be modified. The IP address is resolved using DNS. Use the update buttons to start communication to apply new settings. The user interface provides also some diagnostic information. All received values are logged and displayed as the list with the time prefix. Two first fields are displayed in the text boxes and they must not be removed from configuration.

## Version History

Assembly Version:       1.00.10
Assembly Date:          2015-12-15

The main changes and new functionalities are listed below:
1. It supports the protocol version 1.10
2. It is ready for IOP

Assembly Version:       1.00.09

Assembly Date:          2015-11-02

The main changes and new functionalities are listed below:

1. Initial release
2. It support the protocol version 1.09

## Application installation

The application installation package is available here:
[CAS.UAOOI.UANetworkingDemo](http://www.commsvr.com/COInstal/UANetworking/uand.html)

## Getting Started Tutorial

The topics contained in this section are intended to give you quick exposure to the **UA Data Application** network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create **UA Data Application** producer and consumer applications.

Both roles uses configuration managed using the `UAOOI.SemanticDataUANetworkingConfiguration` component. The remote host name/port number and the consumer port number are provided by the application configuration and may be changed using the UI. The configuration location you may be opened using the menu. Using menu the configuration files may be opened in a default external editor.

### How to implement the consumer role for WPF application

This section provides hints how to implement the consumer role of any **UA Data Application** processing data received in messages sent over the network by a data producer. For example the following applications are good candidate to support this role:

* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.

The class `ConsumerDataManagementSetup` contains code composing an application like that. This part of application consumes the data sent over the network and updates properties in the class `MainWindowViewModel`. The class `MainWindowViewModel` demonstrates how to create bindings to the properties that are holders of values in the [Model View ViewModel (on MSDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern. The user interface View in the `MainWindow` class is dynamically bounded at run time with the `MainWindowViewModel`. To implement the ViewModel layer in the MVVM pattern the helper generic class `ConsumerBindingMonitoredValue` is used.

The Model layer in the MVVM pattern is implemented by classes located in the `Consumer` folder.

### How to: Implement producer role for OPC UA server

The class `CustomNodeManager` captures implementation of an interface between the library and an object supporting  **Address Space Management** (described in  [OPC UA Data Processing Outside the Server](../#concept)) functionality in the typical OPC UA server. The **Address Space Management** instantiates the server address space, i.e. creates the nodes and binds the nodes with underlying external behavior. The example contains two `Value` properties implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling state machine that a new value is available.

In the pre-release version the class `CustomNodeManager` simulates underlying process using random numbers and current time.

## Further work

All planned tasks related to this project are groped in the [milestone: UA Networking Reference Application](https://github.com/mpostol/OPC-UA-OOI/milestones/UA%20Networking%20Reference%20Application)
