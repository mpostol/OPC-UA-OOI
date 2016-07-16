
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
            new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/commsvr.com/ua/Examples/BoilersSet/DomainDescriptor.xml"),
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
    //TODO test consistency accusing local files
    //[TestMethod]
    //public void RecursiveResolveDomainDescriptionTestMethod()
    //{
    //  Task<DomainDescriptor> _DomainDescriptorTask = ResolveDomainDescriptionAsync<DomainDescriptor>(m_RootUrl);
    //  _DomainDescriptorTask.Wait(TimeSpan.FromSeconds(10));
    //  DomainDescriptor _tc = _DomainDescriptorTask.Result;
    //  Uri _resolution = _tc.ResolveUri(m_ModelUri);

    //  _DomainDescriptorTask = ResolveDomainDescriptionAsync<DomainDescriptor>(_resolution);
    //  _DomainDescriptorTask.Wait(TimeSpan.FromSeconds(10));
    //  _tc = _DomainDescriptorTask.Result;
    //  Assert.IsNotNull(_tc);
    //  Assert.IsTrue(_tc.Description.Contains("commsvr.com"));
    //  Assert.AreEqual<RecordType>(RecordType.DomainDescriptor, _tc.NextStepRecordType);
    //  Assert.AreEqual<string>("http://localhost/opc/#authority#/#path#/DomainDescriptor.xml", _tc.UrlPattern);

    //  _resolution = _tc.ResolveUri(m_ModelUri);
    //  _DomainDescriptorTask = ResolveDomainDescriptionAsync<DomainDescriptor>(_resolution);
    //  _DomainDescriptorTask.Wait(TimeSpan.FromSeconds(10));
    //  _tc = _DomainDescriptorTask.Result;
    //  Assert.IsNotNull(_tc);
    //  Assert.IsTrue(_tc.Description.Contains("Boiler test domain model"));
    //  Assert.AreEqual<RecordType>(RecordType.DomainModel, _tc.NextStepRecordType);
    //  Assert.AreEqual<string>("http://localhost/opc/#authority#/#path#/DomainModel.xml", _tc.UrlPattern);

    //  _resolution = _tc.ResolveUri(m_ModelUri);
    //  Task<DomainModel> _DomainModelTask = ResolveDomainDescriptionAsync<DomainModel>(_resolution);
    //  _DomainModelTask.Wait(TimeSpan.FromSeconds(10));
    //  DomainModel _tm = _DomainModelTask.Result;
    //  Assert.IsNotNull(_tm);
    //}

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
    private Uri m_RootUrl = new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/DomainDescriptor.xml");
    private Uri m_ModelUri = new Uri(@"http://commsvr.com/UA/Examples/BoilersSet");
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
        debugLog($"Finished {nameof(AreEqualsDomainDescriptors)} succesfully", TraceEventType.Verbose, Priority.None);
      }

    }


  }
}
