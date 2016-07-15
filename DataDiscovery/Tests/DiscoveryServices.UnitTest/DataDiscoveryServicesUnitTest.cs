
using CUAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
    public void ResolveUriTestMethod()
    {
      DomainDescriptor _rootDomainDescriptor = RootDomainDescriptorFactory.GetRootDomainDescriptor();
      Uri _resolution = _rootDomainDescriptor.ResolveUri(m_ModelUri);
      Assert.AreEqual<string>(m_ExpectedFirsRoundUrl, _resolution.ToString());
      string _fn = "RootDomainDescriptor.xml";
      FileInfo _fi = new FileInfo($@"TestData\{_fn}");
      using (Stream _outputStream = _fi.Create())
      {
        XmlSerializer _serializer = new XmlSerializer(typeof(DomainDescriptor));
        _serializer.Serialize(_outputStream, _rootDomainDescriptor);
      }
      _fi.Refresh();
      Assert.IsTrue(_fi.Exists);
      Assert.IsTrue(_fi.Length > 0);
      DomainDescriptor _tc;
      using (Stream _descriptionStream = _fi.OpenRead())
      {
        XmlSerializer _serializer = new XmlSerializer(typeof(DomainDescriptor));
        _tc = (DomainDescriptor)_serializer.Deserialize(_descriptionStream);
        Assert.IsNotNull(_tc);
      }
      Assert.IsTrue(_tc.Description.Contains("Starting point"));
      Assert.AreEqual<RecordType>(RecordType.DomainDescriptor, _tc.NextStepRecordType);
      Assert.AreEqual<string>("http://localhost/opc/#authority#/DomainDescriptor.xml", _tc.UrlPattern);
      _resolution = _tc.ResolveUri(m_ModelUri);
      Assert.AreEqual<string>(m_ExpectedFirsRoundUrl, _resolution.ToString());
    }
    //[TestMethod]
    //public void ResolveDomainDescriptionAsyncTestMethod()
    //{
    //  Task<DomainDescriptor> _task = ResolveDomainDescriptionAsync<DomainDescriptor>(m_RootUrl);
    //  _task.Wait(TimeSpan.FromSeconds(10));
    //  DomainDescriptor _tc = _task.Result;
    //  Assert.IsNotNull(_tc);
    //  Assert.IsTrue(_tc.Description.Contains("Starting point"));
    //  Assert.AreEqual<RecordType>(RecordType.DomainDescriptor, _tc.NextStepRecordType);
    //  Assert.AreEqual<string>("http://localhost/opc/#authority#/DomainDescriptor.xml", _tc.UrlPattern);
    //  Uri _resolution = _tc.ResolveUri(m_ModelUri);
    //  Assert.AreEqual<string>(m_ExpectedFirsRoundUrl, _resolution.ToString());
    //}
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
      Task<DomainModel> _DomainModelTask = DataDiscoveryServices.ResolveDomainModelAsync
        ( m_ModelUri, 
          (x, y, z) => { System.Diagnostics.Debug.WriteLine($"ResolveDomainModelAsync log: message: {x}, level: {y}, priority: {z}"); }
        );
      _DomainModelTask.Wait(TimeSpan.FromSeconds(10));
      DomainModel _model = _DomainModelTask.Result;
      Assert.IsNotNull(_model);
    }

    Uri m_RootUrl = new Uri(@"http://localhost/opc/DomainDescriptor.xml");
    Uri m_ModelUri = new Uri(@"http://commsvr.com/UA/Examples/BoilersSet");
    string m_ExpectedFirsRoundUrl = @"http://localhost/opc/commsvr.com/DomainDescriptor.xml";

  }
}
