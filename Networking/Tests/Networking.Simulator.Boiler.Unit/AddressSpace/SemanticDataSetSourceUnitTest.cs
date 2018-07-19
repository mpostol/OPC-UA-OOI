//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Upgrade;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.AddressSpace
{
  [TestClass]
  public class SemanticDataSetSourceUnitTest
  {

    [TestMethod]
    public void ConstructorTest()
    {
      using (StateFixture _object = new StateFixture())
      {
        SemanticDataSetSource _register1 = new SemanticDataSetSource(_object, nameof(SemanticDataSetSource));
        Assert.AreEqual<int>(3, _register1.Count);
        Assert.IsTrue(_register1.ContainsKey("Property0"));
        Assert.IsTrue(_register1.ContainsKey("Property1"));
        Assert.IsTrue(_register1.ContainsKey("Property2"));
        Assert.AreEqual<string>(nameof(SemanticDataSetSource), _register1.SemanticDataSetRootBrowseName);
      }
    }
    [TestMethod]
    [DeploymentItem("ConfigurationDataProducer.xml", @"\")]
    public void CreateConfigurationTest()
    {
      TraceSourceFixture _log = new TraceSourceFixture();
      using (BoilerState _boilerState = new BoilerState(null, "browseName"))
      {
        const string _inFileName = "ConfigurationDataProducer.xml";
        FileInfo _inFile = new FileInfo(_inFileName);
        Assert.IsTrue(_inFile.Exists, $"File not exist {_inFile.FullName}");
        string _outFileName = $"new.{_inFileName}";
        _boilerState.Logger = _log;
        ISemanticDataSetSource _dataSource = new SemanticDataSetSource(_boilerState, nameof(SemanticDataSetSource));
        CreateConfiguration(_dataSource, "Simple", _inFileName, _outFileName);
      }
    }
    private class StateFixture : BaseInstanceState
    {
      public StateFixture() : base(null, NodeClass.Object_1, "BaseObjectStateFixture")
      {
        new PropertyState<int>(this, "Property0");
        new PropertyState<int>(this, "Property1");
        new PropertyState<int>(this, "Property2");
      }
    }
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

    private void CreateConfiguration(ISemanticDataSetSource dataSource, string associationName, string inFileName, string outFileName)
    {
      ITraceSource _traceSource = new TraceSourceFixture();
      List<FieldMetaData> _lf = new List<FieldMetaData>();
      foreach (KeyValuePair<string, IVariable> _item in dataSource)
      {
        FieldMetaData _field = new FieldMetaData()
        {
          ProcessValueName = _item.Key,
          SymbolicName = _item.Key,
          TypeInformation = _item.Value.ValueType
        };
        _lf.Add(_field);
      }
      ConfigurationManagement.UpdateDataSetFields(_lf.ToArray(), associationName, inFileName, outFileName, _traceSource);
    }

  }

}
