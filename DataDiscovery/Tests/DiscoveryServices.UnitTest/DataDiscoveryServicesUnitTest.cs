
using CUAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UAOOI.DataDiscovery.DiscoveryServices.Models;

namespace UAOOI.DataDiscovery.DiscoveryServices.UnitTest
{
  [TestClass]
  public class DataDiscoveryServicesUnitTest
  {

    [TestMethod]
    public void RGetHTTPResponseAsyncTestMethod()
    {
      DomainDescriptor _tc = null;
      DataDiscoveryServices.GetHTTPResponse<DomainDescriptor>(m_RootUrl, DebugLog, x => _tc = x);
      Assert.IsNotNull(_tc);
      Assert.IsTrue(_tc.Description.Contains("Starting point"));
      Assert.AreEqual<RecordType>(RecordType.DomainDescriptor, _tc.NextStepRecordType);
      Assert.AreEqual<string>("http://localhost/root.zone/#authority#/DomainDescriptor.xml", _tc.UrlPattern);
      Uri _resolution = _tc.ResolveUri(m_ModelUri);
      Assert.AreEqual<string>("http://localhost/root.zone/commsvr.com/DomainDescriptor.xml", _resolution.ToString());
    }
    [TestMethod]
    [ExpectedException(typeof(System.AggregateException))]
    public void GetHTTPResponseAsyncRetryCountErrorTestMethod()
    {
      DomainDescriptor _tc = null;
      DataDiscoveryServices.GetHTTPResponse<DomainDescriptor>(new Uri("http://localhost/alfa.bravo.xml"), DebugLog, x => _tc = x);
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
    //
    //[TestMethod]
    //TODO test consistency accusing local files
    //public void ResolveDomainDescriptionServiceTestMethod()
    //{
    //  DomainDescriptor _lastDomainDescriptor = new DomainDescriptor() { NextStepRecordType = RecordType.DomainDescriptor };
    //  Uri _nextUri = m_RootUrl;
    //  int _iteration = 0;
    //  do
    //  {
    //    _iteration++;
    //    if (_iteration > 16)
    //      throw new InvalidOperationException("Too many iteration in the resolve process.");
    //    Task<DomainDescriptor> _DomainDescriptorTask = DataDiscoveryServices.ResolveDomainModelAsync(_nextUri, (x, y, z) => { });
    //    _DomainDescriptorTask.Wait(TimeSpan.FromSeconds(10));
    //    _lastDomainDescriptor = _DomainDescriptorTask.Result;
    //    _nextUri = _lastDomainDescriptor.ResolveUri(m_ModelUri);
    //  } while (_lastDomainDescriptor.NextStepRecordType == RecordType.DomainDescriptor);
    //  Task<DomainModel> _DomainModelTask = DataDiscoveryServices. ResolveDomainDescriptionAsync<DomainModel>(_nextUri);
    //  _DomainModelTask.Wait(TimeSpan.FromSeconds(10));
    //  DomainModel _tm = _DomainModelTask.Result;
    //  Assert.IsNotNull(_tm);
    //}
    [TestMethod]
    public void ResolveDomainModelAsyncTestMethod()
    {
      Task<DomainModel> _DomainModelTask = DataDiscoveryServices.ResolveDomainModelAsync(m_ModelUri, m_RootUrl, DebugLog);
      _DomainModelTask.Wait(TimeSpan.FromSeconds(10));
      DomainModel _model = _DomainModelTask.Result;
      Assert.IsNotNull(_model);
    }

    //tests instrumentation
    private Uri m_RootUrl = new Uri(@"https://raw.githubusercontent.com/mpostol/OPC-UA-OOI/master/DataDiscovery/Tests/DiscoveryServices.UnitTest/TestData/root.zone/DomainDescriptor.xml");
    private Uri m_ModelUri = new Uri(@"http://commsvr.com/UA/Examples/BoilersSet");
    private string m_ExpectedFirsRoundUrl = @"http://localhost/opc/commsvr.com/DomainDescriptor.xml";
    private void DebugLog(string message, TraceEventType eventType, Priority priority)
    {
      Debug.WriteLine($"ResolveDomainModelAsync log: message: {message}, level: {eventType}, priority: {priority}");
    }

  }
}
