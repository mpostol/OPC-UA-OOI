# OPC UA DataBinding library

## Introduction

This project is aimed at implementing an editor of the **UA Data Application** configuration file. For more extensive examples, see the [OPC UA Data Processing Outside the Server](../../SemanticDataSolution#opc-ua-data-processing-outside-the-server).

The schema of the configuration files is available at:  [ConfigurationData.xsd](../../SemanticDataSolution/UANetworkingConfiguration/Serialization/ConfigurationData.xsd) and detailed description of the configuration is captured in the document [UA Data Networking Configuration](../../SemanticDataSolution/UANetworkingConfiguration#ua-data-networking-configuration).

## Version History

Assembly Version:       1.00.15
Assembly Date:          Saturday, January 9, 2016

The main changes and new functionalities are listed below:

1. Published Nuget package to decouple dependent projects: Address Space Model Designer, UAOOI.SemanticData.
The Nuget package is available here: https://www.nuget.org/packages/UAOOI.Configuration.DataBindings/


## Getting Started Tutorial

The topics contained in this section are intended to give you quick exposure to the **UA Data Application** network based data exchange programming experience. Working through this tutorial gives you an introductory understanding of the steps required to create **UA Data Application** configuration custom editor. The editor is to be used by a universal tool supporting OPC UA Information Model design process. For mor information on deploying OPC UA Information Model read the document: [OPC UA Information Model Deployment](http://www.cas.internetdsl.pl/commserver/P_DowloadCenter/P_Publications/20140301EN_DeploymentInformationModel.pdf)

The configuration files are managed using the `UAOOI.SemanticDataUANetworkingConfiguration` component.

### How to implement the custom editor

This section provides hints how to implement a custom editor of the of any **UA Data Application** processing data. For example the following applications are good candidate to support this role:

* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.

## Further work

All planned tasks related to the projects are groped in the [milestone: UA Networking Reference Application](https://github.com/mpostol/OPC-UA-OOI/milestones/UA%20Networking%20Reference%20Application)
