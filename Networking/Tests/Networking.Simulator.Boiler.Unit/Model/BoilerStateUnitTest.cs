
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
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
    public void GetChildrenTest()
    {
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        List<BaseInstanceState> _children = new List<BaseInstanceState>();
        _boilerState.GetChildren(_children);
        Assert.AreEqual<int>(7, _children.Count);
      }
    }
    [TestMethod]
    public void RegisterVariableTest()
    {
      TraceSourceFixture _log = new TraceSourceFixture();
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        _boilerState.Logger = _log;
        Dictionary<string, BaseInstanceState> _vars = new Dictionary<string, BaseInstanceState>();
        _boilerState.RegisterVariable(new List<BaseInstanceState>(), (x, y) => _vars.Add(String.Join("_", y), x));
        foreach (KeyValuePair<string, BaseInstanceState> _item in _vars)
        {
          BaseVariableState _var = _item.Value as BaseVariableState;
          Assert.IsNotNull(_var);
          string _type = _var.Value == null ? "not set" : _var.Value.GetType().Name;
          Debug.WriteLine($"{_item} {_type}");
        }
        Assert.AreEqual<int>(20, _vars.Count);
      }
      Assert.IsTrue(_log.TraceLog.Count == 0);
      Assert.IsTrue(_log.ErrorTraceLog.Count == 0);
    }

    [TestMethod]
    public void StartSimulationTest()
    {
      ISystemContext _context = new SystemContextFixture();
      TraceSourceFixture _log = new TraceSourceFixture();
      List<Tuple<string, object>> _callBackCount = new List<Tuple<string, object>>();
      Range _startRange = null;
      int _valueChangeCount = 0;
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        _boilerState.RegisterVariable(new List<BaseInstanceState>(), (x, y) => { _callBackCount.Add(Tuple.Create<string, object>(String.Join("_", y), ((BaseVariableState)x).Value)); x.OnStateChanged += (q, w, e) => _valueChangeCount++; });
        Range _level = _boilerState.Drum.LevelIndicator.Output.EURange.Value;
        _startRange = ModelExtensions.CreateRange(_level.High, _level.Low);
        double _startSetPoint = _boilerState.LevelController.SetPoint.Value;
        _boilerState.Logger = _log;
        _boilerState.ClearChangeMasks(_context, true);
        _boilerState.StartSimulation();
        _boilerState.OnStateChanged = (x, y, z) => Assert.Fail();
        System.Threading.Thread.Sleep(10000);
        Assert.AreEqual<int>(20, _callBackCount.Count);
        _boilerState.ClearChangeMasks(_context, true);
        Assert.AreEqual<Range>(_level, _boilerState.Drum.LevelIndicator.Output.EURange.Value);
        Assert.AreEqual<double>(_startSetPoint, _boilerState.LevelController.SetPoint.Value);
      }
      Assert.IsTrue(_log.TraceLog.Count > 10);
      Assert.IsTrue(_log.ErrorTraceLog.Count == 0);
      Assert.AreEqual<int>(20, _callBackCount.Count);
      Assert.IsTrue(600 < _valueChangeCount, $"_valueChangeCount = {_valueChangeCount}");
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

