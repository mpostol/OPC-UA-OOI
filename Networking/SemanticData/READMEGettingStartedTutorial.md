# Getting Started Tutorial

## Common Tasks

The topics contained in this section are intended to give you quick exposure to the `OOI Reactive Application` network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create `OOI Reactive Application` producer and consumer applications using the library `UAOOI.Networking.SemanticData`. Current release of the NuGet package is available at:

[UAOOI.Networking.SemanticData](https://www.nuget.org/packages/UAOOI.Networking.SemanticData/)

Here are steps to create a successful `OOI Reactive Application`:

1. derive from `DataManagementSetup` - it is place holder to gather all external injection points used to initialize the communication and bind to local resources
1. implement 'IEncodingFactory' interface - to provide functionality to lookup a dictionary containing value converters
1. implement `IMessageHandlerFactory` interface - to create objects supporting messages handling over the wire
1. implement `IBindingFactory` interface - to create objects implementing `IBinding` that can be used to synchronize the values of the local data repository properties and messages received/send over the wire
1. implement `IConfigurationFactory` interface - to provide access to the selected role configuration
1. register the library `EventSource` to support common logging infrastructure

> Notes:
>
> - It is expected that the encoding/decoding functionality is provided outside in a custom library. The interface 'IEncodingFactory' is used for late binding to inject dependency on the external library.
>- `Producer` and `Consumer` roles may use independent configurations.

## How to Guide

### How to: Implement `DataManagementSetup`

Any application engaging the library is composed using the dependency injection pattern. The `DataManagementSetup`class is a placeholder to gather all external injection points used to compose the application, initialize the communication and bind to local resources. The class declares the following properties that must be initialized by the application to provide specific functionality.

```C#
  #region Injection points
  /// <summary>
  /// Gets or sets the binding factory.
  /// </summary>
  /// <value>The binding factory.</value>
  public IBindingFactory BindingFactory { get; set; }
  /// <summary>
  /// Gets or sets the encoding factory.
  /// </summary>
  /// <value>The encoding factory.</value>
  public IEncodingFactory EncodingFactory { get; set; }
  /// <summary>
  /// Gets or sets the message handler factory.
  /// </summary>
  /// <value>The message handler factory.</value>
  public IMessageHandlerFactory MessageHandlerFactory { get; set; }
  /// <summary>
  /// Gets or sets the configuration factory.
  /// </summary>
  /// <value>The configuration factory.</value>
  public IConfigurationFactory ConfigurationFactory { get; set; }
  #endregion

```

Create new class that derives from `DataManagementSetup` and initialize all mentioned above properties.

```C#
 public sealed class LoggerManagementSetup : DataManagementSetup
 {
  public LoggerManagementSetup()
  {
   IServiceLocator _serviceLocator = ServiceLocator.Current;
   string _ConsumerConfigurationFileName = _serviceLocator.GetInstance<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);
   m_ViewModel = _serviceLocator.GetInstance<ConsumerViewModel>(ConsumerCompositionSettings.ViewModelContract);
   ConfigurationFactory = new ConsumerConfigurationFactory(_ConsumerConfigurationFileName);
   EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
   BindingFactory = new DataConsumer(m_ViewModel);
   MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
  }

  ....

 }
```

In this example, it is assumed that [`ServiceLocator`](https://www.nuget.org/packages/CommonServiceLocator) is implemented to resolve references to any external services.

Finally call the `DataManagementSetup.Start()` methods responsible to initialize the infrastructure, enable all associations and start pumping the data.

## How to: Implement `IEncodingFactory`

Encoding means that data is represented as a stream of bits according to selected data type, for example long, float, string, structure, etc. Visit the [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD] article to get moore.

It is expected that the encoding/decoding functionality is provided as an external part in a custom library. The interface `IEncodingFactory` is used for late binding to inject dependency on the external library.

To implement encoding the following steps must be accomplished:

- implement the `UAOOI.Networking.SemanticData.IEncodingFactory` interface;
- implement the `UAOOI.Networking.SemanticData.Encoding.IUADecoder` interface;
- implement the `UAOOI.Networking.SemanticData.Encoding.IUAEncoder` interface;

This library has been released as the NuGet package [UAOOI.Networking.Encoding](https://www.nuget.org/packages/UAOOI.Networking.Encoding).

Main purpose of this release is to support implementation of the interoperability tests defined by the OPC Foundation. In the production environment, you may simply replace this library by a custom one providing unlimited encoding functionality.

### How to: Implement `IMessageHandlerFactory`

An instance implementing `IMessageHandlerFactory` creates objects supporting messages handling over the wire services:

- `IMessageReader` - provides functionality supporting reading the messages from the wire.
- `IMessageWriter` - provides functionality supporting sending the messages over the wire.

The communicating party can be interconnected using any transparent messages transport infrastructure. For the broker-less transport layer the network infrastructure routes datagram-based messages and the services should implement `UDP`, `AMQP` or `ETHERNET` protocol. Applying the broker-based approach a core component of the transport layer is a message broker and in this case the services should implement `AMQP` or `MQTT` protocol.

It is expected that implementation of the `IMessageHandlerFactory` and as the result messages handling services will be provided as an external part. An example implementation of the messages handling services conforming to UTP standard may be found in `UAOOI.Networking.UDPMessageHandler` project described in the document [Transport over UDP](../../Networking/UDPMessageHandler/README.md).

### How to: Implement `IBindingFactory`

#### Introduction

Implementation of this interface is a basic step to implement `Consumer` and/or `Producer` functionality. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used by:

- `Consumer` to save the data received over the network in the local data repository.
- `Producer` to read from the local data repository and send it over the network.

Depending on the role, the `IBinding` objects are returned from the following procedures of this interface:

``` C#
IConsumerBinding IBindingFactory.GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo);
IProducerBinding IBindingFactory.GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo);
```

where:

- `repositoryGroup` - is the name of a repository group profiling the configuration behavior, e.g. encoders selection. The configuration of the repositories belonging to the same group is handled according to the same profile. For example, the `repositoryGroup` may be used to represent a browse path in the OPC UA Address Space. In this case browse path aggregates all variables belonging to the same object (e.g. a boiler), which has to be handled consistently on the screen. This name is determined by the `DataSetConfiguration.RepositoryGroup` in the application configuration (section [Reactive Networking Configuration](../../Configuration/Networking/README.MD).
- `processValueName` - is the name of a variable that is the ultimate destination/source of the message values. The value of `processValueName` must be unique in the context of the group named by `repositoryGroup`.
- `fieldTypeInfo` - the field metadata definition represented as an object of 'UATypeInfo`.

#### `Consumer` Role Implementation

This section provides hints on how to implement the `Consumer` role of the `OOI Reactive Application` processing data received in messages sent over the network by a data `Producer`.

The `Consumer` role implementation is captured by the `Networking.DataLogger` project where `DataManagementSetup` is implemented by derived class `UAOOI.Networking.DataLogger.LoggerManagementSetup`.

The class `UAOOI.Networking.DataLogger.DataConsumer` is an example implementation of a [data logger](./../DataLogger/README.md). This functionality is aimed at recording data over time. It consumes the testing data sent over the wire and updates properties in the class `UAOOI.Networking.DataLogger.ConsumerViewModel` implementing ViewModel layer in the [Model View ViewModel (on MSDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx). The class `DataConsumer` demonstrates how to create bindings interconnecting the data received over the wire and the properties that are the ultimate destination of the data. Because there is only one group of variables the `GetConsumerBinding` method doesn't use the `repositoryGroup` and the `GetProducerBinding` is intentionally not implemented.

#### `Producer` Role Implementation

This section provides hints on how to implement the `Producer` role responsible for:

- reading process data from a local repository
- packing the data into the messages
- sending the data over the network to all interested parties

There are two examples of this role implementation:

- `Networking.SimulatorInteroperabilityTest` - in this project the data expected by a [data logger](./../DataLogger/README.md) is generated and send over the network
- `Networking.Simulator.Boiler` - in this project a set of boilers is simulated. It is a part of the proof of concept with the aim of verifying that the reactive communication implemented using the `Networking.SemanticData` library is well suited to deploy the Internet of Things (IoT) paradigm for highly distributed applications

In the `Networking.SimulatorInteroperabilityTest` project, the `DataManagementSetup` class is implemented by derived class `UAOOI.Networking.SimulatorInteroperabilityTest.SimulatorDataManagementSetup`. The class `UAOOI.Networking.SimulatorInteroperabilityTest.DataGenerator` captures implementation of a [testing data generator](../../Networking/SimulatorInteroperabilityTest/README.md) aimed at accomplishing interoperability tests defined by the OPC Foundation for PubSub applications. Because there is only one group of variables the `GetProducerBinding` method doesn't use the `repositoryGroup` and the `GetConsumerBinding` is intentionally not implemented.

In the `Networking.Simulator.Boiler` project `DataManagementSetup` class is implemented by derived class `UAOOI.Networking.Simulator.Boiler.SimulatorDataManagementSetup`. The class `UAOOI.Networking.Simulator.Boiler.DataGenerator` captures the implementation of a simulator generating data for a set of boilers. In this case, the variables representing the state of one boiler are grouped by the `GetProducerBinding` method using the `repositoryGroup` parameter. Because it is the `Producer` role implementation the `GetConsumerBinding` method is intentionally not implemented and should not be called.

### How to: Implement `IConfigurationFactory`

Definition of the interface `UAOOI.Configuration.Networking.IConfigurationFactory` is located in the [`UAOOI.Configuration.Networking`](../../Configuration/Networking/README.MD) library. This library also contains the class `UAOOI.Configuration.Networking.ConfigurationFactoryBase` that is a base implementation of this interface. This class must be overridden by a custom class designed according to the user application custom requirements.

Example implementations of this class are in [`Producer`](../../Networking/SimulatorInteroperabilityTest/README.md) and [`Consumer`](./../../Networking/DataLogger/README.md).

Both are parts of the example implementation [`ReferenceApplication`](../../Networking/ReferenceApplication/README.MD).

### How to: Register the `EventSource`

Using the following contract create an instance of the `NetworkingEventSourceProvider` and call `GetPartEventSource` to get the local instance of the `EventSource` that is used locally for the semantic logging purpose.

```C#
[Export(typeof(INetworkingEventSourceProvider))]
public class NetworkingEventSourceProvider : INetworkingEventSourceProvider
```
The `EventSourceBootstrapper` in `Networking.ReferenceApplication` project is an example on how to register all `EventSource` instances to support common logging infrastructure.

## See also

- [API Browser][API Browser]: the preliminary code help documentation.
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]

[API Browser]:http://www.commsvr.com/download/OPC-UA-OOI/index.html
[wordpress.OPCUACD]: https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/
