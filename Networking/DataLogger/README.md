# `ReferenceApplication` Consumer - Data Logger

## Getting Started

### Getting Started Tutorial

<!--The topics contained in this section are intended to give you quick exposure to the `OOI Reactive Application` network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create `OOI Reactive Application` producer and consumer applications.

Both roles uses configuration managed using the `UAOOI.SemanticDataUANetworkingConfiguration` component. The remote host name/port number and the consumer port number are provided by the application configuration and may be changed using the UI. The configuration location may be opened using the menu. Using menu the configuration files may be opened in a default external editor.-->

<!--### How to implement the consumer role for WPF application

This section provides hints how to implement the consumer role of any `OOI Reactive Application` processing data received in messages sent over the network by a data producer. For example the following applications are good candidate to support this role:

* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.

The class `ConsumerDataManagementSetup` contains code composing an application like that. This part of application consumes the data sent over the network and updates properties in the class `MainWindowViewModel`. The class `MainWindowViewModel` demonstrates how to create bindings to the properties that are holders of values in the [Model View ViewModel (on MSDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern. The user interface View in the `MainWindow` class is dynamically bounded at run time with the `MainWindowViewModel`. To implement the ViewModel layer in the MVVM pattern the helper generic class `ConsumerBindingMonitoredValue` is used.

The Model layer in the MVVM pattern is implemented by classes located in the `Consumer` folder.-->


`DataLogger` is an example of the `Consumer` part of the `ReferenceApplication`. 

Semantic Data Reactive Networking based on OPC UA Part 14 Pub/Sub library.

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