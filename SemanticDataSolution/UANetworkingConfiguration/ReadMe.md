# UA Data Networking Configuration

This library contains types that supports the configuration management. The configuration may be read to or write from the xml or json file.

Open issues are gruped by the milestone: [UANetworkingConfiguration - development](https://github.com/mpostol/OPC-UA-OOI/issues?q=is%3Aopen+is%3Aissue+milestone%3A%22UANetworkingConfiguration+-+development%22)

By design it is a plug-in or using modern terminology application composition part.

It is good opportunity to review your requirements (if you have any) against the proposed solution. I hope the interface is ready to fulfill the following design time scenarios allowing for configuration in context of selected Information Model/Address Space:

1. Plug in to a modeler (UA Information Model design tool) as the configuration editor tool (preferred for me) - now it is compatible with the [Address Space Model Designer](http://www.commsvr.com/Products/OPCUA/UAModelDesigner.aspx)
2. OPC UA Server as an local editor – using server local GUI, Address Space and the UANetworkingConfiguration plug-in (in this case OPC UA server is just modeler)
3. OPC UA Client as an editor  - using client local GUI/UANetworkingConfiguration on the client side, and remote access to the Address Space
4. OPC UA Server as a remote editor – using custom configuration Information Model/UANetworkingConfiguration plug-in on the server side, and  generic configuration OPC UA Clint as the remote configuration tool,
5. OPC UA Server as a remote dedicated editor – using PubSub Information Model/UANetworkingConfiguration plug-in on the server side, and  dedicated (configuration editor GUI) PubSub configuration OPC UA Clint as the remote configuration tool (let me stress it is the only scenario using PubSub Information Model) – it is rather run time approach.

It is my opinion, but option 4 and 5 are only theoretically possible, because in real installations OPC server is untouchable artefact, but in lab it is just Modeler and cannot be used as run time configuration tool.

Examples how to use the library you can find in the Unit Tests aimed to test selected scenarios. 
At run time it can be used as the primary source of configuration for any OPC UA Data Application and OPC UA Server supporting PubSub role. In this case the application configuration may:
* be derived from the proposed one
* be expanded by the proposed one

Let me know if it works for you and if not how to converge proposed solution and your expectations. 
The project seems to be very important prototyping workspace to answer the question how far we can go with the configuration (design time approach) in context of the following problems we have:

1. Message content definition, i.e. items selected to be distributed (or fields, variables, values, etc.  – nor sure about terminology)
2. Metadata distribution – preparation of the consumer to be able to decode the messages, 
3. Privileges/Permissions management in context of the distributed data as the primary resource (subject) for any OPC UA data processing application and in context of the message handlers as the data access communication channels (infrastructure).
4. Security tokens distribution to support all scenarios mentioned in 3


