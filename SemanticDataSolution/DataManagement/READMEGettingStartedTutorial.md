# Getting Started Tutorial

The topics contained in this section are intended to give you quick exposure to the **UA Data Application** network based data exchange programming experience. They are designed to be completed in the order of the list at the bottom of this topic. Working through this tutorial gives you an introductory understanding of the steps required to create **UA Data Application** producer and consumer applications.

## How to: Implement consumer role for **UA Data Application**

This section provides hints how to implement the consumer role of any **UA Data Application** processing data received in messages sent over the network by a data producer. For example the following applications are good candidate to support this role:
* HMI device - displaying incoming data on the screen;
* Supervisory Control and Data Acquisition (SCADA) - equipped with driver compliant with the standard
* PLC - updating the internal registers using data recovered from the incoming messages.

The class `ConsumerDeviceSimulator` is an implementation of an application like that. It consumes the data sent by the testing method and updates properties in the class `ScreeViewModel`. The class `ScreeViewModel` demonstrates how to create bindings to the properties that are holders of values in the [Model View ViewModel (on MZDN)](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx) pattern.

## How to: Implement producer role for OPC UA server

The class `OPCUAServerProducerSimulator` captures implementation of an interface between the library and an object supporting  **Address Space Management** functionality.
