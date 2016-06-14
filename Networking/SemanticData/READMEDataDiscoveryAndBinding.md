# Data management and binding

The diagram below provides very generic overview of the responsibilities that must be implemented by the UA Application:

![Architecture] (../../CommonResources/Media/UADataNetworking.DataManagementBinding.DomainModel.png)

* `UA Data Networking` - represents a software exchanging the data using protocol compliant with the OPC UA Part 14 Pub/Sub and binding the data with the underlaying process.
* `MessageHandler` - represents a selected transport protocol supporting unsolicited data distribution or a middle-ware supporting publication/subscription message exchange communication pattern.
* `DataSet` - represents a preselected collection of process data items transmitted by the `MessageHandler` as one whole and information required to bind with the underlaying process.
* `MessageReader` - captures functionality necessary to filter out unwanted messages and decode the data according to provided meta-data
* `MessageWriter` - captures functionality necessary to address the message (provide globally unique identifier) and encode the data using provided meta-data
* `Association` - provides:
  1. one to one association between an entity instance derived from `MessageHandler` and `DataSet`
  2. required addressing information as the `PublisherId`/`DataSetWriterId` couple
  3. meta-data information used to encode/decode the process data into the messages
