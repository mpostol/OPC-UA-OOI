
rem https://msdn.microsoft.com/en-us/library/aa347733(v=vs.110).aspx
Svcutil ConfigurationData.xsd /N:http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/Serialization.xsd,UAOOI.SemanticData.UANetworking.Configuration.Serialization /o:ConfigurationData.!.cs /dconly /s /serializer:DataContractSerializer /importXmlTypes
