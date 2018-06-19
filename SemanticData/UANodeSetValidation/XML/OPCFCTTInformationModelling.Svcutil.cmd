
rem https://msdn.microsoft.com/en-us/library/aa347733(v=vs.110).aspx
Svcutil OPCFCTTInformationModelling.xsd /N:http://commsvr.com/UAOOI/SemanticData/UANodeSetValidation/OPCFCTTInformationModelling.xsd,UAOOI.SemanticData.UANodeSetValidation.XML /o:OPCFCTTInformationModelling.cs /dconly /s /serializer:DataContractSerializer /internal
