# Domain Model of the Global Data Discovery

## Main goals
* To promote systems integration against data type definitions.
* To promote separation of concerns.

The following diagram presents domain model of the **Data Discovery** concept.

![Design Time Activities](https://github.com/mpostol/OPC-UA-OOI/blob/master/CommonResources/Media/DataDiscovery.DomainModel.png) .

# Separation of Concerns:
* process model design - *Control Agency* (e.g. Polution Control Agency) prepares a generic information model to meet monitoring requirements.
* asset model - *Asset Vendor* (e.g. boilers vendor) adopts the model to meet the product requirements with the purpose of continuous development and real time control.
* maintenance model - *Engineering* organization (e.g. boiler maintenace service provider) according to the local needs adopts the information model to meet maintenance requirements on behalf of the *Asset User* (e.g. Heat and Power Plant).
* **Data Domain** model - *Asset User* is ultimate owner of the data. Engineering on behalf of the user prepares the **Data Domain** model that could be uniquely identified in the global scope.
* subscriber configuration - *Cloud* or *IIoT Hub* configures subscriber using prepared in advance **Data Domain** model to gather selected data.
