# `ReferenceApplication` Producer - Interoperability Test Data Generator

## Common Tasks

Working through this tutorial gives you an introductory understanding of the steps required to implement `Producer` role of `OOI Reactive Application`. `SimulatorInteroperabilityTest` is a sample of the `Producer` part of the `ReferenceApplication`, which is an example of `Semantic-Data` reactive networking based on  [OPC UA Part 14 Pub/Sub](../../Networking/SemanticData/README.PubSubMTF.md) specification.
The `Producer` role serves as a data generator to be used for testing purpose aimed at supporting the interoperability tests planned by the OPC Foundation. In the production environment, you may replace this library by a custom one supporting more realistic process data acquisition scenario.

Here are steps undertook to implement the `Producer` role in the application:

1. `DataManagementSetup`: this class has been inherited by the `SimulatorDataManagementSetup` class. It initializes the communication and binds the fields used to populate the messages and local resources.
1. `IEncodingFactory` and `IMessageHandlerFactory`: both has been implemented in the external common libraries and `Producer` doesn't depend on the implementation - the instance of this interfaces are localized as services using the `IServiceLocator` interface implementation.
1. `IBindingFactory`: has been implemented in the class `DataGenerator` responsible to synchronize the values of the local data repository properties and messages sent over the wire.
1. `IConfigurationFactory`: the class `ProducerConfigurationFactory` implements this interface to be used for configuration opening.

## How to: Implement `DataManagementSetup`

The `SimulatorDataManagementSetup` constructor initializes all properties, which are injection points of all parts composing this role.

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

    `....`

  }
```

In this example, it is assumed that [`ServiceLocator`](https://www.nuget.org/packages/CommonServiceLocator) is implemented to resolve references to any external services.

Finally the `DataManagementSetup.Start()` method is called to initialize the infrastructure, enable all associations and start pumping the data.

## How to: Implement IBindingFactory

Implementation of this interface is a basic step to implement `Producer` functionality. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used to read or generate (simulator case) from the local data repository.

This section provides hints on how to implement the `Producer` role responsible for:

- generating stream of process data
- packing the data into the messages
- and sending the data over the network to all interested parties

The class `DataGenerator` captures implementation of a generator of testing data aimed at accomplishing interoperability tests defined by the OPC Foundation for PubSub applications. The example contains properties implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling state machine that a new value is available.

## How to: Implement `IConfigurationFactory`

### Implementation

Implementation of this interface is straightforward and based entirely on the library [`UAOOI.Configuration.Networking`](../../Configuration/Networking/README.MD). In a typical scenario, this implementation should not be considered for further modification.  The only open question is how to provide a path to the file containing the configuration of this role. In proposed solution the file path is provided by a service defined by the application entry point and localized using `IServiceLocator` in the class `SimulatorDataManagementSetup`:

```C#
  string _configurationFileName = _serviceLocator.GetInstance<string>(SimulatorCompositionSettings.ConfigurationFileNameContract);
```

This role uses independent configuration file `ConfigurationDataProducer.xml` attached to the project.

### Generated Data

This `Producer` sends out the following datasets:

#### DataSet 1 (Simple)

Parameter Name |Type|Behavior
-|-|-
BoolToggle    | Boolean| Toggles every 3 seconds
Int32          | Int32        | Counts (1 per second) from 0 to 10.000 and then resets
Int32Fast     | Int32        | Counts (100 per second) from 0 to 10.000 and then resets
DateTime      | DateTime    | Current time refreshed with every packet sent

#### DataSet 2 (AllTypes)

ParameterName    |Type    |Behavior
-|-|-
BoolToggle |Boolean |toggle every second and send one per second
Byte    |Byte   |Counts (1 per second) from 0 to type-max and then resets
Int16 |Int16| Counts (1 per second) from 0 to type-max and then resets
Int32 |Int32| Counts (1 per second) from 0 to type-max and then resets
Int64 |Int64| Counts (1 per second) from 0 to type-max and then resets
SByte |SByte| Counts (1 per second) from 0 to type-max and then resets
UInt16|UInt16| Counts (1 per second) from 0 to type-max and then resets
UInt32|UInt32| Counts (1 per second) from 0 to type-max and then resets
UInt64|UInt64| Counts (1 per second) from 0 to type-max and then resets
Float  |Float| Counts (1 per second) from 0 to type-max and then resets
Double|Double| Counts (1 per second) from 0 to type-max and then resets
String|String| Spells the aviation alphabet (Alpha, Bravo �) (1 per second)
ByteString    |ByteString| 1 new random ByteString per second
Guid|Guid |1 new random Guid per second
DateTime |DateTime | Current time refreshed with every packet sent
UInt32Array |UInt32[10] | Length=10 Counts (1 per second on every element) from 0 to type-max and then resets. The count starting point for each value should differ

#### DataSet 3 (MassTest)

Parameter    |Type    |Behavior
-|-|-
Mass_[n0]    | UInt32    | 100 single parameters, each counting from 0 to type-max and then resets back (each with an offset of 100).
Mass_1       | UInt32
`...`        | UInt32
Mass_99      | UInt32

#### UPD Settings

DataSet |DataSetWriterId |Message Length |Message Encoding
-|-|-|-
DataSet 1 |1 |2 byte  |Variant
DataSet 1 |11 |1 byte |Compressed
DataSet 1 |12 |2 byte |DataValue
DataSet 2 |2 |2 byte  |Variant
DataSet 2 |21 |2 byte |Compressed
DataSet 2 |22 |2 byte |DataValue
DataSet 3 |3 |2 byte  |Variant
DataSet 3 |31 |2 byte |Compressed
DataSet 3 |32 |2 byte |DataValue

## Current release

> Notes:
>
> - This library is not considered to be published as the NuGet package.
> - Current configuration may not support all test cases described above.

## See also

- [API Browser][API Browser]: the preliminary code help documentation.
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]

[API Browser]: http://www.commsvr.com/download/OPC-UA-OOI/?topic=html/N-UAOOI.Networking.SemanticData.htm
[wordpress.OPCUACD]: https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible
