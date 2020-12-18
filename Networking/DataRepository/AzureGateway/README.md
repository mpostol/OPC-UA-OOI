# Azure Gateway DataRepository

## Key words

Azure, Cloud Computing, Object-Oriented Internet, OPC Unified Architecture, Reactive Networking (RxNetworking), Machine to Machine Communication, Internet of Things

## Executive Summary

This project shows an example implementation of an OPC UA PubSub to Azure embedded gateway. It is implemented as a composable part of the Reactive Networking Application (`RxNetworking App`). The article  [Reactive Networking of Semantic-Data Library](../../../Networking/SemanticData/README.MD) covers a description of the architecture supporting the reactive communication design pattern.  The `RxNetworking App` is an aggregation of `Producer` and `Consumer` entities derived from `DataRepository`. They must provide interconnection to real-time process data, hence they are recognized as an extension of the `DataRepository` class. `AzureGateway` part fulfills the `Consumer` role and uses out-of-band communication to push telemetry data to the cloud.

Working through this tutorial gives you an introductory understanding of the steps required to implement the `Consumer` role of the `RxNetworking` application. It is an example of `Semantic-Data` reactive networking based on [OPC UA PubSub][OPC.UA.PubSub] specification. The document [OPC UA PubSub Main Technology Features][PubSubMTF] covers a description of selected fetuses relevant to this specification.

This project is proof of concept that out-of-band communication for OPC UA PubSub can be implemented based on the `DataRepository` concept. This workout will be described in an independent article. To get the full story and your copy check out the preprint from [Research Gateway: Object-Oriented Internet - Azure Interoperability](https://www.researchgate.net/publication/346563454_Object-Oriented_Internet_-_Azure_Interoperability). Main purpose of this preprint it to enable an early community review. We will consider your contribution to be applied to the final version of the article.

## Conclusion

The obtained results prove that the **embedded gateway** archetype implementation is possible based on the existing standalone framework supporting reactive interoperability atop of the M2M communication compliant with the [OPC UA PubSub standard][OPC.UA.PubSub]. It is worth stressing that **there is no dependency on the Client/Server session-oriented relationship**. In contrast to the architecture described in the OPC UA Part 1 specification, the publisher/consumer roles are not tightly coupled with the **Address Space** of the OPC UA Server embedded component. In the proposed approach, the cloud interoperability is supported by a dedicated part employing out-of-band communication only without dependency on the OPC UA functionality. In contrast to the middleware concept, the gateway functionality is implemented as a part - **composable to the whole without programming skills**. It makes it possible to modify its functionality later after releasing the library or even deploying the application program in the production environment because the part is composed at the runtime.

Concluding, the paper describes a proof of the concept that it is possible to integrate selected cloud services (e.g. **Azure**) with the **Cyber-physical network** interconnected as one whole atop of the OPC UA PubSub applying the proposed architecture and deployment scenario. It is in contrast to interconnecting cloud services with an **Address Space** exposed by a selected OPC UA server limiting the PubSub role to data exporter transferring the data out of the OPC UA ecosystem.

## Acknowledgment

I would like to acknowledge the [CrossHMI](https://github.com/Drutol/CrossHMI#crosshmi) project from which the `AzureGateway` implementation of the `DataRepository` was derived. I would like to thank [Piotr Szymczak](https://github.com/Drutol) for his inputs/inspirations, feedback, and cooperation in this respect.

## Implementation walk through

### Executive Summary

Here are steps undertook to implement the `Consumer` role in the application:

1. `DataManagementSetup`: this class has been overridden by the `PartDataManagementSetup` class and it initializes the communication and binds data fields recovered form messages to local resources.
1. `IEncodingFactory` and `IMessageHandlerFactory`: have been implemented in external common libraries and `Consumer` doesn't depend on this implementation - current implementation of the interfaces is localized as services using an instance of the [CommonServiceLocator.IServiceLocator][Locator] interface.
1. `IBindingFactory`: has been implemented in the class  `PartBindingFactory` that is responsible to gather the data recovered from the `Message` instances pulled from the `Distribution Channel`. The received data is driven to the Azure services using configured out-of-band protocol.
1. `IConfigurationFactory`: the class `PartConfigurationFactory` implements this interface to be used for the configuration file opening.

### `DataManagementSetup` implementation

The `PartDataManagementSetup` constructor initializes all properties, which are injection points of all parts composing this role.

```C#

  [Export(typeof(PartDataManagementSetup))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public sealed class PartDataManagementSetup : DataManagementSetup
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

In this example, it is assumed that [IServiceLocator][Locator] is implemented to resolve references to any external services.

Finally the `DataManagementSetup.Start()` method is called to initialize the infrastructure, enable all associations and start pumping the data.

### `IBindingFactory` implementation

Implementation of this interface is a basic step to implement `Consumer` functionality. The `DataRepository` represents data holding assets in the `RxNetworking App` and, following the proposed approach, the `IBindingFactory` interface is implemented by an external part. It captures functionality responsible for accessing the process data represented by the `LocalResources`. The `LocalResources` represents the external part that has a very broad usage purpose. For example, it may be any kind of process data source/destination, and to name a few `Raw Data`,  `OPC UA Address Space Management`, `Azure` cloud-based front-end, etc.

The `AzureGateway` functional package has been implemented based on the `Consumer` concept. This particular `Consumer` (`PartBindingFactory`) implements the `IBindingFactory` interface to gather the data recovered from the `Message` instances pulled from the `Distribution Channel`. The received data is driven to the Azure services using configured out-of-band' protocol. An instance of the `IBindingFactory` is responsible to create objects implementing `IBinding` that can be used by the `Consumer` to forward the data retrieved from `NetworkMessag` received over the wire to Azure services.

The proposed implementation of the Azure gateway proves that the `DataRepository` and associated entities, i.e. `Local Resources`, `Consumer`, `Producer` can be implemented as external parts, and consequently, the application scope may cover practically any concern that can be separated from the core OPC UA PubSub communication engine implementation.

### `IConfigurationFactory` implementation

the library [`UAOOI.Configuration.Networking`](../../../Configuration/Networking/README.MD). In a typical scenario, this implementation should not be considered for further modification. The only open question is how to provide the name of the file containing the configuration of this role. This role uses an independent configuration file:

- `ConfigurationDataConsumer.BoilersSet.xml`

attached to the project.

## Current release

> Note; This library is not considered to be published as the NuGet package.

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
- [Object Oriented Internet - on-line ebook][OOIBook]
- API Browser: the preliminary code help documentation - [available for sponsors- consider joining](https://github.commsvr.com/AboutPartnershipProgram.md.html)
- [OPC UA Makes Complex Data Processing Possible][wordpress.OPCUACD]
- [OPC Unified Architecture Specification Part 14: PubSub Release 1.04 February 06, 2018][OPC.UA.PubSub]
- [OPC UA PubSub Main Technology Features][PubSubMTF]
- [CommonServiceLocator NuGet package][Locator]

[PubSubMTF]:../../../Networking/SemanticData/README.PubSubMTF.md
[OPC.UA.PubSub]: https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/
[wordpress.OPCUACD]:https://mpostol.wordpress.com/2014/05/08/opc-ua-makes-complex-data-access-possible
[OOIBook]:https://commsvr.gitbook.io/ooi/readme
[Locator]:https://www.nuget.org/packages/CommonServiceLocator
