
using UAOOI.DataDiscovery.DiscoveryServices.Models;

namespace CUAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData
{
  internal static class RootDomainDescriptorFactory
  {
    internal static DomainDescriptor GetRootDomainDescriptor()
    {
      return new DomainDescriptor()
      {
        Description = "Starting point for discovery process with the purpose of resolving Uri and get DomainModel record",
        NextStepRecordType = RecordType.DomainDescriptor,
        UrlPattern = "http://localhost/root.zone/#authority#/DomainDescriptor.xml" //"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/DomainDescriptor.xml"
      };
    }
  }
}
