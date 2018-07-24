//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Upgrade;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using BoilersSet = Commsvr.UA.Examples.BoilersSet;
using BoilerType = tempuri.org.UA.Examples.BoilerType;

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
        SemanticDataSetSource _register1 = new SemanticDataSetSource(_object);
        Assert.AreEqual<int>(3, _register1.Count);
        Assert.IsTrue(_register1.ContainsKey("Property0"));
        Assert.IsTrue(_register1.ContainsKey("Property1"));
        Assert.IsTrue(_register1.ContainsKey("Property2"));
        Assert.AreEqual<string>(_object.BrowseName.ToString(), _register1.SemanticDataSetRootBrowseName);
      }
    }
    [TestMethod]
    [DeploymentItem(@"CommonServiceLocatorInstrumentation\ConfigurationDataProducer.xml", "CommonServiceLocatorInstrumentation")]
    public void ReplaceDataSetFieldsTest()
    {
      TraceSourceFixture _log = new TraceSourceFixture();
      using (BoilerType.BoilerState _boilerState = new BoilerType.BoilerState(null, "browseName"))
      {
        const string _inFileName = @"CommonServiceLocatorInstrumentation\ConfigurationDataProducer.xml";
        FileInfo _inFile = new FileInfo(_inFileName);
        Assert.IsTrue(_inFile.Exists, $"File not exist {_inFile.FullName}");
        string _outFileName = @"CommonServiceLocatorInstrumentation\new.ConfigurationDataProducer.xml";
        _boilerState.Logger = _log;
        ISemanticDataSetSource _dataSource = new SemanticDataSetSource(_boilerState);
        ReplaceDataSetFields(_dataSource, "Simple", _inFileName, _outFileName);
      }
    }
    [TestMethod]
    [DeploymentItem(@"Deploy\", @"Deploy")]
    public void CreateConfigurationTest()
    {
      TraceSourceFixture _log = new TraceSourceFixture();
      string _inFileName = $@"Deploy\Producer.tml.xml";
      FileInfo _inFile = new FileInfo(_inFileName);
      Assert.IsTrue(_inFile.Exists, $"File not exist {_inFile.FullName}");
      CreateConfiguration(_log, 1, "BoilersArea_Boiler #1", "BoilersArea_BoilerAlpha", _inFileName);
      CreateConfiguration(_log, 2, "BoilersArea_Boiler #2", "BoilersArea_BoilerBravo", _inFileName);
      CreateConfiguration(_log, 3, "BoilersArea_Boiler #3", "BoilersArea_BoilerBravo", _inFileName);
      CreateConfiguration(_log, 4, "BoilersArea_Boiler #4", "BoilersArea_BoilerBravo", _inFileName);
      //Assert.Fail($"{Environment.CurrentDirectory}");
    }

    private void CreateConfiguration(TraceSourceFixture _log, ushort writerId, string _associationName, string symbolicName, string _inFileName)
    {
      using (BoilerType.BoilerState _boilerState = new BoilerType.BoilerState(null, _associationName))
      {
        _boilerState.Logger = _log;
        SemanticDataSetSource _dataSource = new SemanticDataSetSource(_boilerState);
        XmlQualifiedName _type = new XmlQualifiedName(BoilerType.BrowseNames.BoilerType, BoilerType.Namespaces.BoilerType);
        XmlQualifiedName _instanceSymbolicName = new XmlQualifiedName(symbolicName, BoilersSet.Namespaces.BoilersSet);
        _dataSource.CreateConfiguration(_type, _associationName, _instanceSymbolicName, _inFileName, Tuple.Create("UDP", writerId, ProducerId), _log);
      }
    }

    #region instrumentation
    private readonly System.Guid ProducerId = new System.Guid("d80d81dd-96e6-4560-850e-154f9181307c");
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
    private void ReplaceDataSetFields(ISemanticDataSetSource dataSource, string associationName, string inFileName, string outFileName)
    {
      ITraceSource _traceSource = new TraceSourceFixture();
      List<FieldMetaData> _lf = new List<FieldMetaData>();
      foreach (KeyValuePair<string, IVariable> _item in dataSource)
      {
        if (_item.Value.ValueType.BuiltInType == BuiltInType.Null)
          continue;
        FieldMetaData _field = new FieldMetaData()
        {
          ProcessValueName = _item.Key,
          SymbolicName = _item.Key,
          TypeInformation = _item.Value.ValueType
        };
        _lf.Add(_field);
      }
      ConfigurationManagement.ReplaceDataSetFields(_lf.ToArray(), associationName, inFileName, outFileName, _traceSource);
    }
    #endregion

  }

}
