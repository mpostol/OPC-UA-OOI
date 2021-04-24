//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UAOOI.DataDiscovery.DiscoveryServices.Models;
using UAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData;

namespace UAOOI.DataDiscovery.DiscoveryServices.UnitTest
{
  [TestClass]
  public class DataDiscoveryServicesUnitTest
  {
    [TestMethod]
    public void RGetHTTPResponseAsyncTestMethod()
    {
      int _messages = 0;
      using (LocalDataDiscoveryServices _service = new LocalDataDiscoveryServices())
      {
        _service.AreEqualsDomainDescriptors
          (
            DomainDescriptorFactory.GetRootDomainDescriptor(),
            new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/DomainDescriptor.xml"),
            (x, y, z) => { _messages++; DebugLog(x, y, z); }
          );
        _service.AreEqualsDomainDescriptors
          (
            DomainDescriptorFactory.Iteration1DomainDescriptor(),
            new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/commsvr.com/DomainDescriptor.xml"),
            (x, y, z) => { _messages++; DebugLog(x, y, z); }
          );
        _service.AreEqualsDomainDescriptors
          (
            DomainDescriptorFactory.Iteration2DomainDescriptor(),
            new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/commsvr.com/UA/Examples/BoilersSet/DomainDescriptor.xml"),
            (x, y, z) => { _messages++; DebugLog(x, y, z); }
          );
        Assert.AreEqual<int>(3, _messages);
      }
    }

    [TestMethod]
    [ExpectedException(typeof(System.AggregateException))]
    public void GetHTTPResponseAsyncRetryCountErrorTestMethod()
    {
      using (LocalDataDiscoveryServices _service = new LocalDataDiscoveryServices())
      {
        DomainDescriptor _tc = null;
        _service.GetHTTPResponse<DomainDescriptor>(new Uri("http://localhost/alfa.bravo.xml"), DebugLog, x => _tc = x);
      }
    }

    [TestMethod]
    public void ResolveDomainModelAsyncTestMethod()
    {
      using (LocalDataDiscoveryServices _service = new LocalDataDiscoveryServices())
      {
        Task<DomainModel> _DomainModelTask = _service.ResolveDomainModelAsync(m_ModelUri, m_RootUrl, DebugLog);
        _DomainModelTask.Wait(TimeSpan.FromSeconds(10));
        DomainModel _model = _DomainModelTask.Result;
        Assert.IsNotNull(_model);
      }
    }

    //tests instrumentation
    private readonly Uri m_RootUrl = new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/DomainDescriptor.xml");

    private readonly Uri m_ModelUri = new Uri(@"http://commsvr.com/UA/Examples/BoilersSet");

    private static void DebugLog(string message, TraceEventType eventType, Priority priority)
    {
      Debug.WriteLine($"ResolveDomainModelAsync log: message: {message}, level: {eventType}, priority: {priority}");
    }

    private class LocalDataDiscoveryServices : DataDiscoveryServices
    {
      internal void AreEqualsDomainDescriptors(DomainDescriptor _rootDomainDescriptor, Uri address, Action<string, TraceEventType, Priority> debugLog)
      {
        DomainDescriptor _tc = null;
        GetHTTPResponse<DomainDescriptor>(address, DebugLog, x => _tc = x);
        Assert.IsNotNull(_tc);
        Assert.AreEqual<string>(_rootDomainDescriptor.Description, _tc.Description);
        Assert.AreEqual<RecordType>(_rootDomainDescriptor.NextStepRecordType, _tc.NextStepRecordType);
        Assert.AreEqual<String>(_rootDomainDescriptor.UrlPattern, _tc.UrlPattern);
        debugLog($"Finished {nameof(AreEqualsDomainDescriptors)} successfully", TraceEventType.Verbose, Priority.None);
      }
    }
  }
}