# Getting Started Tutorial

The topics contained in this section are intended to give you quick exposure to the **UA Data Application** network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create **UA Data Application** producer and consumer applications.

Here are steps to create a successful the **UA Data Application**:

- Implement 'IEncodingFactory' - provides functionality to lookup a dictionary containing value converters.
- Implement `IMessageHandlerFactory` interface -  creates objects supporting messages handling over the wire.
- Implement `IBindingFactory` - interface creating objects implementing `IBinding` that can be used to synchronize the values of the local data repository properties and messages received/send over the wire.
- Implement `IConfigurationFactory` - providing access to the selected role configuration.
- Derive from `DataManagementSetup` - it is place holder to gather all external injection points used to initialize the communication and bind to local resources.

> Notes:
> - It is expected that the encoding/decoding functionality is provided outside in a custom library. The interface is used for late binding to inject dependency on the external library. 
>- `Producer` and `Consumer` roles may use independent configurations.


## How to: Implement `Consumer` role for the **UA Data Application**

This section provides hints how to implement the consumer role of any **UA Data Application** processing data received in messages sent over the network by a data producer. For example the following applications are good candidate to support this role:

* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.
* A [data logger](..\DataLogger\README.md) or data recorder - recording data over time; 

The class `UAOOI.Networking.DataLogger.DataConsumer` is an implementation of the application like that. It consumes the data sent by the testing method and updates properties in the class `UAOOI.Networking.DataLogger.ConsumerViewModel`. The class `DataConsumer` demonstrates how to create bindings to the properties that are holders of values in the [Model View ViewModel (on MZDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern.

## How to: Implement producer role for OPC UA server

The class `OPCUAServerProducerSimulator` captures implementation of an interface between the library and an object supporting  **Address Space Management** functionality.
The **Address Space Management** is represented by the class `CustomNodeManager` that instantiates the server address space, i.e. creates the nodes and binds the nodes with underlying external behavior. The example contains two `Value` attributes implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling machine state that new value is available.
