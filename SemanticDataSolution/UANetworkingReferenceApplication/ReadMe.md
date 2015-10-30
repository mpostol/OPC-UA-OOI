# UA Data Example Application

## Introduction

This project implements an example of the **UA Data Application** supporting producer and consumer roles simultaneously. Both roles are implemented as independent threads that have common user interface for diagnostic and configuration purpose. In this pre-release version the application uses the hard-coded configuration. The producer sends current time and a random floating-point number between 0.0 and 1.0 contained in messages. Because of OPC UA encoding applied the time is converted to UTC.

By the user interface (UI) remote host name and port numbers can be modified. Use the update buttons to start communication with the new settings. The user interface provides also some diagnostic information.

## Application installation

**TBD** after releasing the application will be available as installation package.

## Getting Started Tutorial

The topics contained in this section are intended to give you quick exposure to the **UA Data Application** network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create **UA Data Application** producer and consumer applications.

Both roles uses the same data set configuration provided by the `Configuration` class. After changing the configuration the code must be rebuilt. The remote host name and the consumer host name are provided by the application configuration and may be changed using the UI.

### How to implement the consumer role for WPF application

This section provides hints how to implement the consumer role of any **UA Data Application** processing data received in messages sent over the network by a data producer. For example the following applications are good candidate to support this role:
* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.

The class `ConsumerDataManagementSetup` contains code composing an application like that. This part of application consumes the data sent over the network and updates properties in the class `MainWindowViewModel`. The class `MainWindowViewModel` demonstrates how to create bindings to the properties that are holders of values in the [Model View ViewModel (on MSDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern. The user interface View in the `MainWindow` class is dynamically bounded at run time with the `MainWindowViewModel`. To implement the ViewModel layer in the MVVM pattern the helper generic class `ConsumerBindingMonitoredValue` is used.

The Model layer in the MVVM pattern is implemented by classes located in the `Consumer` folder.

### How to: Implement producer role for OPC UA server

The class `CustomNodeManager` captures implementation of an interface between the library and an object supporting  **Address Space Management** (described in  [OPC UA Data Processing Outside the Server](../#concept)) functionality in the typical OPC UA server. The **Address Space Management** instantiates the server address space, i.e. creates the nodes and binds the nodes with underlying external behavior. The example contains two `Value` attributes implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling machine state that new value is available.

In the pre-release version the class `CustomNodeManager` simulates underlying process using random numbers and current time.

## Further work

All planned tasks related to this project are groped in the [milestone: UA Networking Reference Application](https://github.com/mpostol/OPC-UA-OOI/milestones/UA%20Networking%20Reference%20Application)
