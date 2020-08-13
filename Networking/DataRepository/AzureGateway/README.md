# Azure Gateway DataRepository Implementation

## Subject

This project presents an implementation of the Azure gateway. The article  [Reactive Networking of Semantic-Data Library](../../../Networking/SemanticData/README.MD) covers a description of an application (`OOI Reactive Application`) architecture supporting the reactive communication designed based on the library `UAOOI.Networking.SemanticData`. The `OOI Reactive Application` is a collection of `Producer` and `Consumer` entities. They must provide interconnection to real-time process data, hence they are recognized as an extension of the `DataRepository` class. To implement the `DataRepository` dedicated implementation of the [`IBinding`](http://www.commsvr.com/download/OPC-UA-OOI/html/T-UAOOI.Networking.SemanticData.DataRepository.IBinding.htm) interface shall be provided to create a bridge between real-word data `Row Data` represented by the `LocalResources` class.

## Key words

Azure, Cloud Computing, Object-Oriented Internet, OPC Unified Architecture, Machine to Machine Communication, Internet of Things

## Goal 

It is proof of the concept that out-of-band communication can be implemented based on the `DataRepository` concept. This workout will be described in the article: [OOI/Azure Interoperability](https://it-p-lodz-pl.github.io/OOI.Gateway2Azure.Article/README.html).

## Scope

- Architecture
- Bootstrapping
- Protocol selection and mapping
- Configuration
- Binding
- Testing 

## Related work

The prototyping is conducted in the:

- [CrossHMI](https://github.com/Drutol/CrossHMI#crosshmi) - Thesis: Object-Oriented Internet - reactive visualization of asynchronous data using AZURE

## See also

- Postół M. (2020) Object-Oriented Internet Reactive Interoperability. In: Krzhizhanovskaya V. et al. (eds) Computational Science – ICCS 2020. ICCS 2020. Lecture Notes in Computer Science, vol 12141. Springer, Cham; [DOI: https://doi.org/10.1007/978-3-030-50426-7_31](https://doi.org/10.1007%2F978-3-030-50426-7_31)

  - Postół M. (2020) [Object-Oriented Internet Reactive Interoperability](https://www.researchgate.net/publication/341882427_Object-Oriented_Internet_Reactive_Interoperability), presentation, DOI: 10.13140/RG.2.2.33984.56323

- Mariusz Postol, [Machine to Machine Semantic-Data Based Communication: Comprehensive Survey](https://www.researchgate.net/publication/341165347_Machine_to_Machine_Semantic-Data_Based_Communication_Comprehensive_Survey) chapter in book [Computer Game Innovations 2018](https://www.researchgate.net/publication/335524620_Computer_Game_Innovations_2018), Publisher: Lodz University of Technology Press; ISBN: 978-83-7283-999-2

- Mariusz Postol, [Object Oriented Internet](https://ieeexplore.ieee.org/abstract/document/7321562), [3rd International Conference on Innovative Network Systems and Applications](https://fedcsis.org/2015/inetsapp), 2015, [IEEE Xplore Digital Library](https://ieeexplore.ieee.org/abstract/document/7321562) [![DOI](https://img.shields.io/badge/DOI-10.15439%2F2015F160-blue)](https://fedcsis.org/proceedings/2015/pliks/160.pdf)

- [Object Oriented Internet - online ebook][OOIBook]

[OOIBook]:https://commsvr.gitbook.io/ooi/readme

