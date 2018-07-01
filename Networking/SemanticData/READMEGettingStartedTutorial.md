# Getting Started Tutorial

The topics contained in this section are intended to give you quick exposure to the `OOI Reactive Application` network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create `OOI Reactive Application` producer and consumer applications.

Here are steps to create a successful the `OOI Reactive Application`:

- Implement 'IEncodingFactory' - provides functionality to lookup a dictionary containing value converters.
- Implement `IMessageHandlerFactory` interface -  creates objects supporting messages handling over the wire.
- Implement `IBindingFactory` - interface creating objects implementing `IBinding` that can be used to synchronize the values of the local data repository properties and messages received/send over the wire.
- Implement `IConfigurationFactory` - providing access to the selected role configuration.
- Derive from `DataManagementSetup` - it is place holder to gather all external injection points used to initialize the communication and bind to local resources.

> Notes:
> - It is expected that the encoding/decoding functionality is provided outside in a custom library. The interface is used for late binding to inject dependency on the external library. 
>- `Producer` and `Consumer` roles may use independent configurations.

## How to: Implement `IEncodingFactory`

Encoding means that data is represented as a stream of bits according to selected data type, for example long, float, string, structure, etc. 

The UADP message mapping uses optimized UA Binary encoding. The following encodings may be supported.

- The data fields are encoded as `Variant` The Variant can contain a `StatusCode` instead of the expected `DataType` if the status of the field is `Bad`. The `Variant` can contain a `DataValue` with the value and the statusCode if the status of the field is `Uncertain`.

- RawData Field Encoding: The data fields are encoded in the `DataTypes` specified in the DataSetMetaData for the DataSet. The encoding is handled like a Structure DataType where the DataSet fields are handled like Structure fields and fields with Structure DataType are handled like nested structures. All restrictions for the encoding of Structure DataTypes also apply to the RawData Field Encoding.

- DataValue Field Encoding: The DataSet fields are encoded as DataValue. This option is set if the DataSet is configured to send more than the Value.

To implement encoding the following steps must be accomplished:

- implement the `UAOOI.Networking.SemanticData.IEncodingFactory` interface;
- implement the `UAOOI.Networking.SemanticData.Encoding.IUADecoder` interface;
- implement the `UAOOI.Networking.SemanticData.Encoding.IUAEncoder` interface;

## How to: Implement `Consumer` role for the `OOI Reactive Application`

This section provides hints how to implement the consumer role of any `OOI Reactive Application` processing data received in messages sent over the network by a data producer. For example the following applications are good candidate to support this role:

* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.
* A [data logger](./../DataLogger/README.md) or data recorder - recording data over time; 

The class `UAOOI.Networking.DataLogger.DataConsumer` is an implementation of the application like that. It consumes the data sent by the testing method and updates properties in the class `UAOOI.Networking.DataLogger.ConsumerViewModel`. The class `DataConsumer` demonstrates how to create bindings to the properties that are holders of values in the [Model View ViewModel (on MZDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern.

## How to: Implement producer role for OPC UA server

The class `OPCUAServerProducerSimulator` captures implementation of an interface between the library and an object supporting  **Address Space Management** functionality.
The **Address Space Management** is represented by the class `CustomNodeManager` that instantiates the server address space, i.e. creates the nodes and binds the nodes with underlying external behavior. The example contains two `Value` attributes implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling machine state that new value is available.




