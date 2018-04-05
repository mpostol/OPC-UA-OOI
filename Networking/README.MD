bject Oriented Internet Reactive Networking

## Introduction

This folder contains library empowering the Industrial Internet of Things \(IIOT\) and Industrie 4.0. as the result of using reactive   
networking paradigm. Intentionally it is designed on top of OPC UA Part 14 Pub/Sub protocol supporting interoperability with any product compliant with this specification. Seamless integration with AMQP, MQTT, etc. allows meaningful data transfer in context of semantics defined using OPC UA Information Model.

## Goals

Main goals of this document are:

* description of the IoT and main difference between OPC UA clint server model and Object Oriented Internet Reactive Networking implemented in this folder. 
* description how this project addresses the IoT paradigms,
* instruction hot to build commercial products based on this stuff
* instruction how to integrate proposed solution with other existing on the marked, i.e. AMQP, MQTT, etc. -  

## Projects

This folder contains the projects in concern.

## IoT paradigms description

Starting point for this description is available at:  
[IoT versus SCADA/DCS Data Acquisition Patterns](https://mpostol.wordpress.com/2017/09/19/iot-versus-scadadcs/)

## Scope

* description of **IoT/IIoT** paradigms.
* Object Oriented Internet Networking is empowering the Industrial Internet of Things \(IIOT\) and Industrie 4.0.
* The role of MQTT and AMQP- How to get them together in a consistent meaningful way.

## How to get started

In folder [OPC-UA-OOI/Networking/ReferenceApplication/Release.bin/](https://github.com/mpostol/OPC-UA-OOI/tree/master/Networking/ReferenceApplication/Release.bin) I have added binary release of the `ReferenceApplication` containing ready to use binaries. Unzip and run exe file to get started. Nuget package will be created after getting more feedback from MQTT and AMQP testing, which are in progress.

