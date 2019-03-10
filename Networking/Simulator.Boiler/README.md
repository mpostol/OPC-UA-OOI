# OPC UA Information Model Centric Boilers Set Simulator

## Key words

Boiler, Simulator, Reactive Networking, Object Oriented Internet, OPC UA, CommServer.

## Subject

### Introduction

Simulator of a set of boilers used to produce steam for a turbine. 

By design it is composable component of the `ReferenceApplication`. This component may be integrated with the `ReferenceApplication` as the `Producer`.

### Custom Model - Boiler

![Figure 1 Boiler diagram.](../../CommonResources/Media/Boiler/image001.png)

*Figure 1 Boiler diagram*

This example considers a real process in a boiler producing steam from water. The piping and instrumentation diagram (P&ID) in Figure 1 shows the piping and process equipment together with the instrumentation and control devices. It consists of an input pipe feeding water, a boiler drum producing steam that is carried away by an output pipe. To meet the process requirements, flow and level controllers use a valve on the input pipe to control water flow in the feedback loop.

One purpose of this example is to illustrate modeling against OPC UA type definitions. A simplified model of the presented process is illustrated in Figure 2 showing part of an OPC UA Information Model where the `BoilerType` type is defined.

![Figure 2 Boiler simplified model](../../CommonResources/Media/Boiler/image003.png)

*Figure 2 Boiler simplified model*

Objects of this type are complex and consist of the following components: `InputPipe`, `Drum`, `OutputPipe`, `FlowController`, `LevelController`, `CustomController`. For all of these objects corresponding types are defined.

To reflect the process behavior, a `FlowTo` reference type is used to interconnect relevant objects and provide clients short browsing paths. It is derived from `NonHierarchicalReferences` (Figure 3) what is exposed in the Address Space of the server. It is good illustration how the requirements that server should expose the OPC UA Information Model are realized in the practice, i.e. the server exposes the types as nodes using predefined layout merging all selected Information Model domains. It is also worth noting that we can find the same type definition in many places in the Address Space (e.g. Figure 2 , Figure 3 , and Figure 4 ).

The `BoilerType` can be instantiated every time a new boiler process is to be represented. As a result of instantiation of this type, all mandatory node chains referenced consecutively by the `HierarchicalReferences` in the forward direction (i.e. all components defined in Figure 2 and all their subcomponents) are instantiated as well.

Analyzing the whole process model is impractical here. To illustrate the design practice using this model, we will focus only on one selected brand of type definition inheritance hierarchy (see Figure 4). The whole model is available as a sample solution attached to the Address Space Model Designer [\[1\]][CAS.OPCUAIMD] [\[ 2\]][CAS.ASMD] and can be used for further examination.

![Figure 3  New FlowTo reference type definition](../../CommonResources/Media/Boiler/image005.png)

*Figure 3  New FlowTo reference type definition*

The model of the `BoilerInputPipeType` consists of two mandatory object components: `FlowTransmitter1`(`FTC001`) and `Valve`(`Valve001`) (Figure 5). After parent type instantiation, they are also created as components of that type and, therefore, called instance declaration. The newly created nodes have the same browse names (`FTC001`, `Valve001`) and display names (`FlowTransmitter1`, `Valve`) as in the type definition. Since browse names shall be unique in the context of the parent type definition, new nodes may be created without any fear of breaking the browse path uniqueness rules. A graphical element programmed against the `BoilerType` may need to display the value of the `Valve`. If the main graphical element is called `Boiler1` (an instance of `BoilerType`) it will need to refer to the target using the browse path: `Boiler1.Pipe001.Valve001`. This browse path is always unique, because the browse name of the created main object should be unique in the context it is located in and all instance declarations should have unique browse names in the context of types they are defined by.

![Figure 4 Model of the BoilerInputPipeType inheritance hierarchy](../../CommonResources/Media/Boiler/image007.png)

*Figure 4 Model of the BoilerInputPipeType inheritance hierarchy*

`FlowTransmiter1` is of `FlowTransmitterType` type, which indirectly inherits from `GenericSensorType`, based finally on the standard `BaseObjectType`. `GenericSensorType` has a component an - `Output` variable of the
standard `AnalogItemType`, which has three properties: `EURange`, `InstrumentRange` and `EngineeringUnits`,_but only `EURange` is mandatory. `InstrumentRange` and `EngineeringUnits` are optional, therefore should be created if needed. In the case of optional instance declaration, clients are responsible for examining the exposed Address Space to check if the predefined nodes are instantiated.

![Figure 5 Boiler Object exposed by the server](../../CommonResources/Media/Boiler/image009.png)

*Figure 5 Boiler Object exposed by the server*

After instantiation of the `BoilerType` and adding reference to it in the `Objects.Boilers` folder, we obtain the Address Space presented in Figure 5 exposed by the server to clients. It should be noted that in Figure 5 both objects, `FlowTransmitter1` and `Valve,` have names other than in the definition. It is because each node in the Information Model has `DisplayName` attribute that contains the localized name of the node. Clients should use this attribute if they want to display the node name to the user. They should not use the browse name for this purpose. In this example only mandatory nodes have been instantiated.

## Goal 

The main goal of this proof of concept is to demonstrate the feasibility of process data generation and publication using Pub/Sub reactive networking concept against selected OPC UA Information Model. It also demonstrates how to design the pluggable software module dedicated to implementing the Producer role that is developed solely on top of the library  [Reactive Networking of SemanticData Library](../../Networking/SemanticData/README.MD)

## Getting Started

> TBD

## See also

> - [ 1] [Mariusz Postol. OPC UA Information Model Deployment][CAS.OPCUAIMD]. 2016. Version 1.2, DOI: 10.5281/zenodo.2586616 [![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.2586616.svg)](https://doi.org/10.5281/zenodo.2586616)
> - [ 2] [OPC UA Address Space Model Designer, 2019][CAS.ASMD]

[CAS.ASMD]: http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx
[CAS.OPCUAIMD]: http://www.commsvr.com/InternetDSL/commserver/P_DowloadCenter/P_Publications/20140301E_DeploymentInformationModel.pdf