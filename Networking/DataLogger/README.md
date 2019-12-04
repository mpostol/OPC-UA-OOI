# `ReferenceApplication` Consumer - Data Logger

## Common Tasks

Working through this tutorial gives you an introductory understanding of the steps required to implement `Consumer` role of `OOI Reactive Application`. `DataLogger` is a sample implementation of the `Consumer` part of the `ReferenceApplication`, which is an example application of `Semantic-Data` reactive networking based on [OPC UA Part 14 Pub/Sub](../../Networking/SemanticData/README.PubSubMTF.md) specification.

Here are steps undertook to implement the `Consumer` role in the application:

1. `DataManagementSetup`: this class has been overridden by the `LoggerManagementSetup`class and it initializes the communication and binds data fields recovered form messages to local resources.
1. `IEncodingFactory` and `IMessageHandlerFactory`: has been implemented in external common libraries and `Consumer` doesn't depend on this implementation - current implementation of the interfaces is localized as services using an instance of the `IServiceLocator` interface.
1. `IBindingFactory`: has been implemented in the class  `DataConsumer` that is responsible to synchronize the values of the local data repository properties and messages received over the wire.
1. `IConfigurationFactory`: the class `ConsumerConfigurationFactory` implements this interface to be used for the configuration file opening.

## How to Guide

### How to: Implement `DataManagementSetup`

The `LoggerManagementSetup` constructor initializes all properties, which are injection points of all parts composing this role.

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

Finally the `DataManagementSetup.Start()` method is called to initialize the infrastructure, enable all associations and start pumping the data.

### How to: Implement IBindingFactory

Implementation of this interface is a basic step to implement `Consumer` functionality. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used by the `Consumer` to save the data received over the wire in the local data repository.

The class `DataConsumer` is a sample implementation of a data logger functionality recording data over time. It consumes the testing data sent and updates properties in the class `ConsumerViewModel` implementing *ViewModel* layer in the *[Model View ViewModel (on MSDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx)* (*MVVM pattern*). The class `DataConsumer` demonstrates how to create bindings interconnecting the data received over the wire and the properties that are the ultimate destination of the data. The user interface provided by the *View* layer implemented in the `UAOOI.Networking.ReferenceApplication.MainWindow` class is dynamically bounded at run time with the `ConsumerViewModel`. To implement the *ViewModel* layer in the *MVVM pattern* the helper generic class `UAOOI.Networking.SemanticData.DataRepository.ProducerBindingMonitoredValue<type>` is used.

### How to: Implement `IConfigurationFactory`

Implementation of this interface is straightforward and based entirely on the library [`UAOOI.Configuration.Networking`](../../Configuration/Networking/README.MD). In a typical scenario, this implementation should not be considered for further modification. The only open question is how to provide the name of the file containing the configuration of this role. In proposed solution the name is provided by a service defined by  the application entry point part and localized using `IServiceLocator` in the class `LoggerManagementSetup` - see code snipped below:

```C#

string _ConsumerConfigurationFileName = _serviceLocator.GetInstance<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);

```

This role uses independent configuration file `ConfigurationDataConsumer.xml` attached to the project.
This file contains a mirror configuration of the [`Producer`](../../Networking/SimulatorInteroperabilityTest/README.md) role configuration to log all the generated data.

## Current release

> Note; This library is not considered to be published as the NuGet package.

## See also

- [API Browser][API Browser]: the preliminary code help documentation.
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]

[API Browser]:http://www.commsvr.com/download/OPC-UA-OOI/index.html
[wordpress.OPCUACD]:https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible
