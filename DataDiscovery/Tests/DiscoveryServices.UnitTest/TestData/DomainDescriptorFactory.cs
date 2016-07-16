
using UAOOI.DataDiscovery.DiscoveryServices.Models;

namespace CUAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData
{

  internal static class DomainDescriptorFactory
  {
    internal static DomainDescriptor GetRootDomainDescriptor()
    {
      return new DomainDescriptor()
      {
        Description = "Starting point for discovery process with the purpose of resolving Uri and get DomainDescriptor record",
        NextStepRecordType = RecordType.DomainDescriptor,
        UrlPattern = "https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/#authority#/DomainDescriptor.xml"
      };
    }
    internal static DomainDescriptor Iteration1DomainDescriptor()
    {
      return new DomainDescriptor()
      {
        Description = "Iteration #1 DomainDescriptor record - root entry for commsvr.com.",
        NextStepRecordType = RecordType.DomainDescriptor,
        UrlPattern = "https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/#authority#/#path#/DomainDescriptor.xml"
      };
    }
    internal static DomainDescriptor Iteration2DomainDescriptor()
    {
      return new DomainDescriptor()
      {
        Description = "Iteration #2 DomainDescriptor record - Boiler test domain model.",
        NextStepRecordType = RecordType.DomainModel,
        UrlPattern = "https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/#authority#/#path#/DomainModel.xml"
      };
    }

  }
}
