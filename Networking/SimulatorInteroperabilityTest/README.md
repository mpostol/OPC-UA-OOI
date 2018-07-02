# `ReferenceApplication` Producer - Interoperability Test Data Generator

## Getting Started

### Getting Started Tutorial

<!--The topics contained in this section are intended to give you quick exposure to the `OOI Reactive Application` network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create `OOI Reactive Application` producer and consumer applications.

Both roles uses configuration managed using the `UAOOI.SemanticDataUANetworkingConfiguration` component. The remote host name/port number and the consumer port number are provided by the application configuration and may be changed using the UI. The configuration location may be opened using the menu. Using menu the configuration files may be opened in a default external editor.-->

<!--### How to: Implement producer role for OPC UA server

The class `CustomNodeManager` captures implementation of an interface between the library and an object supporting  **Address Space Management** (described in  [OOI Semantic Data Processing Architecture](../../SemanticData/README.MD)) functionality in the typical OPC UA server. The **Address Space Management** instantiates the server address space, i.e. creates the nodes and binds the nodes with underlying external behavior. The example contains properties implemented as an instance of class `ProducerBindingMonitoredValue`. Modification of the `ProducerBindingMonitoredValue<type>.MonitoredValue` provides notification to the message handling state machine that a new value is available.

In the current software version the class `CustomNodeManager` simulates underlying process using random numbers and current time. Arrays length is incremented periodically.

[OOI.Releases]:https://github.com/mpostol/OPC-UA-OOI/releases
-->

`Producer` implementation of a data generator to be used for testing purpose.

Semantic Data Reactive Networking based on OPC UA Part 14 Pub/Sub library.

Main purpose of this release is to support implementation of the interoperability tests defined by the OPC Foundation. In the production environment, you may replace this library by a custom one providing unlimited encoding functionality.

## Current release

> Note; This library is not considered to be published as the NuGet package.

Each role uses independent configuration file as follows:

* Producer: `ConfigurationDataProducer.xml`
* Consumer: `ConfigurationDataConsumer.xml`

<!--# TBD 

> NOTE The rest of document is just template

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
Give examples
```

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
-->
