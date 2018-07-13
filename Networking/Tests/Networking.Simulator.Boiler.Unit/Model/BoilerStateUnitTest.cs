
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{
  [TestClass]
  public class BoilerStateUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void ConstructorTest()
    {
      using (BoilerState _boilerState = new BoilerState(null))
      {
      }
        
    }
    [TestMethod]
    public void Constructor2Test()
    {
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        Assert.IsNotNull(_boilerState.BrowseName);
        Assert.AreEqual<string>("browseName", _boilerState.BrowseName.Name);
        Assert.IsFalse(_boilerState.BrowseName.NamespaceIndexSpecified);
        Assert.IsNotNull(_boilerState.CustomController);
        Assert.IsNotNull(_boilerState.Drum);
        Assert.IsNotNull(_boilerState.FlowController);
        Assert.IsNotNull(_boilerState.InputPipe);
        Assert.IsNotNull(_boilerState.LevelController);
        Assert.IsNotNull(_boilerState.OutputPipe);
        Assert.IsNull(_boilerState.Parent);
        Assert.IsNotNull(_boilerState.Simulation);
        Assert.AreEqual<NodeClass>(NodeClass.Object_1, _boilerState.NodeClass);
        Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Children, _boilerState.ChangeMasks);
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.CustomController.BrowseName }, 0));
      }
    }
    [TestMethod]
    public void FindChildTest()
    {
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.CustomController.BrowseName }, 0));
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.FlowController.BrowseName }, 0));
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.Drum.BrowseName }, 0));
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.InputPipe.BrowseName }, 0));
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.LevelController.BrowseName }, 0));
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.OutputPipe.BrowseName }, 0));
        Assert.IsNotNull(_boilerState.FindChild(null, new List<QualifiedName>() { _boilerState.Simulation.BrowseName }, 0));
      }
    }
    [TestMethod]
    public void StartSimulationTest()
    {
      ISystemContext _context = new SystemContextFixture();
      TraceSourceFixture _log = new TraceSourceFixture();
      int _callBackCount = 0;
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        _boilerState.Logger = _log;
        _boilerState.ClearChangeMasks(_context, true);
        _boilerState.StartSimulation();
        _boilerState.OnStateChanged = (x, y, z) => _callBackCount++;
        System.Threading.Thread.Sleep(10000);
        Assert.AreEqual<int>(0, _callBackCount);
        _boilerState.ClearChangeMasks(_context, true);
      }
      Assert.IsTrue(_log.TraceLog.Count > 10);
      Assert.IsTrue(_log.ErrorTraceLog.Count == 0);
      Assert.Inconclusive("RegisterVariable must be implemented to agregate all events.");
      Assert.AreEqual<int>(5, _callBackCount);
    }
    private class SystemContextFixture : ISystemContext { }
    private class TraceSourceFixture : ITraceSource
    {
      public void TraceData(TraceEventType eventType, int id, object data)
      {
        string _message = $"{eventType} at {id}: {data}";
        switch (eventType)
        {
          case TraceEventType.Critical:
          case TraceEventType.Error:
          case TraceEventType.Warning:
            ErrorTraceLog.Add(_message);
            break;
          case TraceEventType.Information:
          case TraceEventType.Verbose:
          case TraceEventType.Start:
          case TraceEventType.Stop:
          case TraceEventType.Suspend:
          case TraceEventType.Resume:
          case TraceEventType.Transfer:
            TraceLog.Add(_message);
            break;
        }
        Debug.WriteLine(_message);
      }
      internal List<string> TraceLog { get; private set; } = new List<string>();
      internal List<string> ErrorTraceLog { get; private set; } = new List<string>();
    }
  }
}

