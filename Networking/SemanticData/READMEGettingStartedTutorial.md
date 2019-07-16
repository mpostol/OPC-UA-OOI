# Getting Started Tutorial

## Common Tasks

The topics contained in this section are intended to give you quick exposure to the `OOI Reactive Application` network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create `OOI Reactive Application` producer and consumer applications using the library `UAOOI.Networking.SemanticData`. Current release of the NuGet package is available at:

[UAOOI.Networking.SemanticData](https://www.nuget.org/packages/UAOOI.Networking.SemanticData/)

Here are steps to create a successful `OOI Reactive Application`:

1. Derive from `DataManagementSetup` - it is place holder to gather all external injection points used to initialize the communication and bind to local resources.
1. Implement 'IEncodingFactory' - provides functionality to lookup a dictionary containing value converters.
1. Implement `IMessageHandlerFactory` interface -  creates objects supporting messages handling over the wire.
1. Implement `IBindingFactory` - interface creating objects implementing `IBinding` that can be used to synchronize the values of the local data repository properties and messages received/send over the wire.
1. Implement `IConfigurationFactory` - providing access to the selected role configuration.

> Notes:
> - It is expected that the encoding/decoding functionality is provided outside in a custom library. The interface 'IEncodingFactory' is used for late binding to inject dependency on the external library. 
>- `Producer` and `Consumer` roles may use independent configurations.

## How to: Implement `DataManagementSetup`

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

## How to: Implement `IMessageHandlerFactory`

An instance implementing `IMessageHandlerFactory` creates objects supporting messages handling over the wire services:
- `IMessageReader` - provides functionality supporting reading the messages from the wire.
- `IMessageWriter` - provides functionality supporting sending the messages over the wire.

The communicating party can be interconnected using any transparent messages transport infrastructure. For the broker-less transport layer the network infrastructure routes datagram-based messages and the services should implement `UDP`, `AMQP` or `ETHERNET` protocol. Applying the broker-based approach a core component of the transport layer is a message broker and in this case the services should implement `AMQP` or `MQTT` protocol.

It is expected that implementation of the `IMessageHandlerFactory` ans as the result selected messages handling  services will be provided as an external part. An example implementation of the messages handling  services conforming to UTP standard may found in `UAOOI.Networking.UDPMessageHandler` project described in the document [Transport over UDP](../../Networking/UDPMessageHandler/README.md).

## How to: Implement IBindingFactory

### Introduction

Implementation of this interface is a basic step to implement `Consumer` and/or `Producer` functionality. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used:
- by the `Consumer` to save the data received over the wire in the local data repository.
- by the `Producer` to read from the local data repository.

### `Consumer` Role Implementation

This section provides hints on how to implement the `Consumer` role of the `OOI Reactive Application` processing data received in messages sent over the network by a data producer.

The class `UAOOI.Networking.DataLogger.DataConsumer` is an example implementation of a [data logger](./../DataLogger/README.md) functionality recording data over time. It consumes the testing data sent and updates properties in the class `UAOOI.Networking.DataLogger.ConsumerViewModel` implementing ViewModel layer in the [Model View ViewModel (on MSDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx). The class `DataConsumer` demonstrates how to create bindings interconnecting the data received over the wire and the properties that are the ultimate destination of the data.

### `Producer` Role Implementation 

This section provides hints on how to implement the `Producer` role responsible for:
- reading process data from a local repository,
- packing the data into the messages,
- and sending the data over the network to all interested parties. 

The class `UAOOI.Networking.SimulatorInteroperabilityTest.DataGenerator` captures implementation of a [testing data generator](../../Networking/SimulatorInteroperabilityTest/README.md) aimed at accomplishing interoperability tests defined by the OPC Foundation for PuSub applications.

## How to: Implement `IConfigurationFactory`

Definition of the interface `UAOOI.Configuration.Networking.IConfigurationFactory` is located in the [`UAOOI.Configuration.Networking`](../../Configuration/Networking/README.MD) library. This library also contains the class `UAOOI.Configuration.Networking.ConfigurationFactoryBase` that is a base implementation of this interface. This class must be overridden by a custom class designed according to the user application custom requirements.

Example implementations of this class are in [`Producer`](../../Networking/SimulatorInteroperabilityTest/README.md) and [`Consumer`](./../../Networking/DataLogger/README.md).

Both are parts of the example implementation [`ReferenceApplication`](../../Networking/ReferenceApplication/README.MD).

## See also

- [API Browser][API Browser]: the preliminary code help documentation.
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]

[API Browser]:http://www.commsvr.com/download/OPC-UA-OOI/index.html
[wordpress.OPCUACD]: https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/
