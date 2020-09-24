# Azure Gateway DataRepository Implementation

## Preface

This project shows an example implementation of an OPC UA PubSub to Azure gateway. This gateway is implemented as a composable part of the Reactive Networking Application (`RxNetworking App`). The article  [Reactive Networking of Semantic-Data Library](../../../Networking/SemanticData/README.MD) covers a description of the architecture supporting the reactive communication design pattern.  The `RxNetworking App` is an aggregation of `Producer` and `Consumer` entities derived from `DataRepository`. They must provide interconnection to real-time process data, hence they are recognized as an extension of the `DataRepository` class. `AzureGateway` part fulfills the `Consumer` role and uses out-of-band communication to push process data to the cloud.

Working through this tutorial gives you an introductory understanding of the steps required to implement the `Consumer` role of `OOI Reactive Application`. The `ReferenceApplication` is an example application of `Semantic-Data` reactive networking based on [OPC UA PubSub][OPC.UA.PubSub] specification. The document [OPC UA PubSub Main Technology Features][PubSubMTF] covers a description of selected fetuses relevant for this specification.

It is proof of the concept that out-of-band communication for OPC UA PubSub can be implemented based on the `DataRepository` concept. This workout will be described in the article: [OOI/Azure Interoperability](https://it-p-lodz-pl.github.io/OOI.Gateway2Azure.Article/README.html).

### Key words

Azure, Cloud Computing, Object-Oriented Internet, OPC Unified Architecture, Reactive Networking (RxNetworking), Machine to Machine Communication, Internet of Things

## Acknowledgment

I would like to acknowledge the [CrossHMI](https://github.com/Drutol/CrossHMI#crosshmi) project from which the `AzureGateway` implementation of the `DataRepository` was derived. I would like to thank [Piotr Szymczak](https://github.com/Drutol) for their inputs/inspirations, feedback, and cooperation in this respect.

## Implementation walk through

## Executive Summary

Here are steps undertook to implement the `Consumer` role in the application:

1. `DataManagementSetup`: this class has been overridden by the `PartDataManagementSetup` class and it initializes the communication and binds data fields recovered form messages to local resources.
1. `IEncodingFactory` and `IMessageHandlerFactory`: has been implemented in external common libraries and `Consumer` doesn't depend on this implementation - current implementation of the interfaces is localized as services using an instance of the `IServiceLocator` interface.
1. `IBindingFactory`: has been implemented in the class  `PartBindingFactory` that is responsible to gather the data recovered from the `Message` instances pulled from the `Distribution Channel`. The received data is driven to the Azure services using configured out-of-band protocol.
1. `IConfigurationFactory`: the class `PartConfigurationFactory` implements this interface to be used for the configuration file opening.

### `DataManagementSetup` implementation

The `PartDataManagementSetup` constructor initializes all properties, which are injection points of all parts composing this role.

```C#

  public sealed class PartDataManagementSetup : DataManagementSetup, IProducerDataManagementSetup
  {

    public PartDataManagementSetup()
    {
      _Logger.EnteringMethodPart(nameof(PartDataManagementSetup));
      //Compose external parts
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      //string _configurationFileName = _serviceLocator.GetInstance<string>(CompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<ProducerViewModel>();
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      _Logger.Composed(nameof(EncodingFactory), EncodingFactory.GetType().FullName);
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
      _Logger.Composed(nameof(MessageHandlerFactory), MessageHandlerFactory.GetType().FullName);
      //compose internal parts
      ConfigurationFactory = new PartConfigurationFactory(ConfigurationFilePath);
      PartBindingFactory pbf = new PartBindingFactory();
      _DTOProvider = pbf;
      BindingFactory = pbf;
    }

    ....

  }

```

In this example, it is assumed that [`ServiceLocator`](https://www.nuget.org/packages/CommonServiceLocator) is implemented to resolve references to any external services.

Finally the `DataManagementSetup.Start()` method is called to initialize the infrastructure, enable all associations and start pumping the data.

### `IBindingFactory` implementation

Implementation of this interface is a basic step to implement `Consumer` functionality. The `DataRepository` represents data holding assets in the `RxNetworking App` and, following the proposed architecture, the `IBindingFactory` interface is implemented by this external part. It captures functionality responsible for accessing the process data represented by the `LocalResources`. The `LocalResources` represents the external part that has a very broad usage purpose. For example, it may be any kind of process data source/destination, and to name a few `Raw Data`,  `OPC UA Address Space Management`, and `Azure` services in this case.

The `AzureGateway` functional package has been implemented based on the `Consumer` concept. This particular `Consumer` implements `IBindingFactory` interface to gather the data recovered from the `Message` instances pulled from the `Distribution Channel`. The received data is driven to the Azure services using configured out-of-band' protocol. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used by the `Consumer` to forward the data retrieved from `NetworkMessag` received over the wire to Azure services.

The proposed implementation of the Azure gateway proves that the `DataRepository` and associated entities, i.e. `Local Resources`, `Consumer`, `Producer` can be implemented as external parts, and consequently, the application scope may cover practically any concern that can be separated from the core PubSub communication engine implementation.

### `IConfigurationFactory` implementation

Implementation of this interface is straightforward and based entirely on the library [`UAOOI.Configuration.Networking`](../../../Configuration/Networking/README.MD). In a typical scenario, this implementation should not be considered for further modification. The only open question is how to provide the name of the file containing the configuration of this role. This role uses independent configuration file:

- `ConfigurationDataConsumer.BoilersSet.xml` 

attached to the project.

## Current release

> Note; This library is not considered to be published as the NuGet package.

## Contributing

Please read CONTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [Semantic Versioning 2.0.0](http://semver.org/) for versioning. For the versions available, see the [Releases](https://github.com/mpostol/OPC-UA-OOI/releases) page of the project.

## Authors

- [Mariusz Postol](https://github.com/mpostol) - main contributor of this project.

See also the list of contributors who participated in this project and the `Acknowledgment` section.

## See also

- Postół M. (2020) Object-Oriented Internet Reactive Interoperability. In: Krzhizhanovskaya V. et al. (eds) Computational Science – ICCS 2020. ICCS 2020. Lecture Notes in Computer Science, vol 12141. Springer, Cham; [DOI: https://doi.org/10.1007/978-3-030-50426-7_31](https://doi.org/10.1007%2F978-3-030-50426-7_31)

  - Postół M. (2020) [Object-Oriented Internet Reactive Interoperability](https://www.researchgate.net/publication/341882427_Object-Oriented_Internet_Reactive_Interoperability), presentation, DOI: 10.13140/RG.2.2.33984.56323

- Mariusz Postol, [Machine to Machine Semantic-Data Based Communication: Comprehensive Survey](https://www.researchgate.net/publication/341165347_Machine_to_Machine_Semantic-Data_Based_Communication_Comprehensive_Survey) chapter in book [Computer Game Innovations 2018](https://www.researchgate.net/publication/335524620_Computer_Game_Innovations_2018), Publisher: Lodz University of Technology Press; ISBN: 978-83-7283-999-2
- Mariusz Postol, [Object Oriented Internet](https://ieeexplore.ieee.org/abstract/document/7321562), [3rd International Conference on Innovative Network Systems and Applications](https://fedcsis.org/2015/inetsapp), 2015, [IEEE Xplore Digital Library](https://ieeexplore.ieee.org/abstract/document/7321562) [![DOI](https://img.shields.io/badge/DOI-10.15439%2F2015F160-blue)](https://fedcsis.org/proceedings/2015/pliks/160.pdf)
- [Reactive HMI Android application example](https://github.com/Drutol/CrossHMI#crosshmi)
- [Object Oriented Internet - online ebook][OOIBook]
- API Browser: the preliminary code help documentation - [available for sponsors- consider joining](https://github.commsvr.com/AboutPartnershipProgram.md.html)
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]
- [OPC UA PubSub Main Technology Features][PubSubMTF]

[PubSubMTF]:../../../Networking/SemanticData/README.PubSubMTF.md
[OPC.UA.PubSub]: https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[wordpress.OPCUACD]:https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible
[OOIBook]:https://commsvr.gitbook.io/ooi/readme
