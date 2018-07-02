# `ReferenceApplication` Producer - Interoperability Test Data Generator

## Common Tasks

Working through this tutorial gives you an introductory understanding of the steps required to implement `Producer` role of `OOI Reactive Application`. `SimulatorInteroperabilityTest` is a sample of the `Producer` part of the `ReferenceApplication`, which is an example of `SemanticData` reactive networking based on  [OPC UA Part 14 Pub/Sub](../../Networking/SemanticData/README.PubSubMTF.md) specification. 

The `Producer` role serves as a data generator to be used for testing purpose. Main purpose of this library is to support implementation of the interoperability tests defined by the OPC Foundation. In the production environment, you may replace this library by a custom one supporting more realistic process data acquisition scenario.

Here are steps undertaken to implement the `Producer` role of the application:

1. `DataManagementSetup`: this class has been overridden in the `SimulatorDataManagementSetup` class. It initializes the communication and bind the fields populating the messages to local resources.
1. 'IEncodingFactory' and `IMessageHandlerFactory`: has been implemented in the an external common libraries and `Producer` doesn't depend on the implementation - this interfaces are localized as services using an instance of the `IServiceLocator` interface.
1. `IBindingFactory`: has been implemented in the class `DataGenerator` responsible to synchronize the values of the local data repository properties and messages sent over the wire.
1. `IConfigurationFactory`: the class `ProducerConfigurationFactory` implements this interface to be used for configuration opening.

## How to: Implement `DataManagementSetup`

The `LoggerManagementSetup` constructor initializes all properties, which are injection points of all parties composing this role.
```C#
  public sealed class SimulatorDataManagementSetup : DataManagementSetup
  {

    public SimulatorDataManagementSetup()
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      string _configurationFileName = _serviceLocator.GetInstance<string>(SimulatorCompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<SimulatorViewModel>();
      ConfigurationFactory = new ProducerConfigurationFactory(_configurationFileName);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      BindingFactory = m_DataGenerator = new DataGenerator();
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
    }

    ....

  }
```

In this example, it is assumed that [`ServiceLocator`](https://www.nuget.org/packages/CommonServiceLocator) is implemented to resolve references to any external services.

Finally the `DataManagementSetup.Start()` method is called to initialize the infrastructure, enable all associations and start pumping the data.

## How to: Implement IBindingFactory

Implementation of this interface is a basic step to implement `Producer` functionality. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used to read or generate (simulator case) from the local data repository.

This section provides hints on how to implement the `Producer` role responsible for:
- generating stream of process data,
- packing the data into the messages,
- and sending the data over the network to all interested parties. 

The class `DataGenerator` captures implementation of a [testing data generator](../../Networking/SimulatorInteroperabilityTest/README.md) aimed at accomplishing interoperability tests defined by the OPC Foundation for PuSub applications. The example contains properties implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling state machine that a new value is available.

## How to: Implement `IConfigurationFactory`

Implementation of this interface is straightforward and based entirely on the library [`UAOOI.Configuration.Networking`](../../Configuration/Networking/README.MD). In a typical scenario, this implementation should not be considered for further modification.  The only open question is how to provide the file path of the file containing the configuration of this role. In proposed solution the file path is provided by a service defined by the configuration maintained by the application entry point part and localized using `IServiceLocator` in the class `SimulatorDataManagementSetup`: 

```C#
  string _configurationFileName = _serviceLocator.GetInstance<string>(SimulatorCompositionSettings.ConfigurationFileNameContract);
```
This role uses independent configuration file `ConfigurationDataProducer.xml` attached to the project.

## Current release

> Note; This library is not considered to be published as the NuGet package.

## See also

- [API Browser][API Browser]: the preliminary code help documentation.
 
[API Browser]: http://www.commsvr.com/download/OPC-UA-OOI/?topic=html/N-UAOOI.Networking.SemanticData.htm

- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]

[wordpress.OPCUACD]: https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible/


