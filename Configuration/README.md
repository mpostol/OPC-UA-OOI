# Configuration - Executive Summary

## Introduction

This library contains types that support the configuration management of application implementing the paradigm described in [Semantic-Data Processing Architecture](../SemanticData/README.MD#ooi-reactive-application). By design, this library may be used to support a variety of application types at design and run time.

The project is to be a prototyping workspace to answer the question how far we can go with the configuration (design time approach) in context of the following problems we have:

1. Message content definition, i.e. items selected to be distributed (or fields, variables, values, etc.  – not sure about terminology)
2. Metadata distribution – preparation of the consumer to be able to decode the messages
3. Privileges/Permissions management in context of the distributed data as the primary resource (subject) for any OPC UA data processing application and in context of the message handlers as the data access communication channels (infrastructure).
4. Security artefacts distribution to support all scenarios mentioned in 3 above

## Architecture

The relationship of the set of assemblies is illustrated in the following figure.

![Configuration Architecture](../CommonResources/Media/Configuration/ConfigurationArchitecture.png)

>WHERE:
>
>- "DataBindings" - This project is aimed at implementing an editor of the `OOI Reactive Application` configuration file.
>- "Networking" - This library contains types that support the configuration management of the `OOI Reactive Application`
>- Core - The library contains core definitions for "DataBindings" to promote late binding and loosely coupled components> interaction

## Features

By design libraries in this namespace
- allow saving the configuration in the **XML** or **JSON** formats
- promotes dependency injection pattern
- enable dynamic configuration to reload after modification and discovery functionality

The dependency injection allows the composition of the hosting application using a late-binding approach and as a result replacing the used parts after deploying the main library. It requires loosely coupled parts.

## Prerequisites

By design, the Configuration libraries depend on a logger functionality implementing the interface `UAOOI.Common.Infrastructure.Diagnostic.ITraceSource` defined in the `UAOOI.Common.Infrastructure` package. It is used to trace the behavior of libraries at run-time. To get an instance implementing this interface the `CommonServiceLocator` is used. The functionality required by the `CommonServiceLocator` has to be provided by the hosting application. Usually, it is provided by the composition container that is built at the application bootstrap stage. If the `CommonServiceLocator` is not available a default logger (do nothing) is used. To get more visit the `CommonServiceLocator` library home page.

## How to guide

### How to use the library by the configuration editor tool

The following code snippet demonstrates how to use this library set by a configuration editor tool

```C#

public class ConfigurationEditorBase : IConfigurationEditor { ... }
public class InstanceConfigurationFactory : IInstanceConfigurationFactory { ... }

```

Implementation of this scenario is covered by a full-featured example managed in an independent repository at: [Object Oriented Internet Reactive Networking Configuration Editor](https://github.com/mpostol/OPC-UA-OOI.ConfigEditor). Implement of the above-mentioned interface is supported by the libraries that provide a vast variety of helper classes.

### How to use the library by the RxNetworking (OPC UA PubSub) communication application

An example illustrating this scenario for the XML format is in the `UAOOI.Networking.DataLogger.ConsumerConfigurationFactory` and the following code snippet demonstrates this case

```C#
internal class ConsumerConfigurationFactory : ConfigurationFactoryBase<ConfigurationData>
{
  ...

  public ConsumerConfigurationFactory(string configurationFileName)
  {
    Loader = LoadConfig;
    m_ConfigurationFileName = configurationFileName;
  }

  private ConfigurationData LoadConfig()
  {
    FileInfo _configurationFile = new FileInfo(m_ConfigurationFileName);
    return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
  }
  ...
}

```

## See Also

- [API Browser][API Browser]: the preliminary code help documentation.
- [OPC UA Address Space Model Designer (ASMD) - GitHub repository with related work][ASMD]

[API Browser]:http://www.commsvr.com/download/OPC-UA-OOI/index.html
[ASMD]:https://github.com/mpostol/ASMD
