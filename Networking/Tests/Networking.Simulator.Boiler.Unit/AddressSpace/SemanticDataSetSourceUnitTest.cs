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
using System.Xml;
using BoilerType = tempuri.org.UA.Examples.BoilerType;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Upgrade;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using BoilersSet = Commsvr.UA.Examples.BoilersSet;
using System;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.AddressSpace
{
  [TestClass]
  [DeploymentItem("*.xml", @"\")]
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
    public void ReplaceDataSetFieldsTest()
    {
      TraceSourceFixture _log = new TraceSourceFixture();
      using (BoilerType.BoilerState _boilerState = new BoilerType.BoilerState(null, "browseName"))
      {
        const string _inFileName = "ConfigurationDataProducer.xml";
        FileInfo _inFile = new FileInfo(_inFileName);
        Assert.IsTrue(_inFile.Exists, $"File not exist {_inFile.FullName}");
        string _outFileName = $"new.{_inFileName}";
        _boilerState.Logger = _log;
        ISemanticDataSetSource _dataSource = new SemanticDataSetSource(_boilerState, nameof(SemanticDataSetSource));
        ReplaceDataSetFields(_dataSource, "Simple", _inFileName, _outFileName);
      }
    }
    [TestMethod]
    public void CreateConfigurationTest()
    {
      TraceSourceFixture _log = new TraceSourceFixture();
      const string _associationName = "BoilersArea_BoilerAlpha";
      using (BoilerType.BoilerState _boilerState = new BoilerType.BoilerState(null, _associationName))
      {
        const string _inFileName = "EmptyProducerConfiguration.xml";
        FileInfo _inFile = new FileInfo(_inFileName);
        Assert.IsTrue(_inFile.Exists, $"File not exist {_inFile.FullName}");
        string _outFileName = $"new.{_inFileName}";
        _boilerState.Logger = _log;
        ISemanticDataSetSource _dataSource = new SemanticDataSetSource(_boilerState, nameof(SemanticDataSetSource));
        //ReplaceDataSetFields(_dataSource, "Simple", _inFileName, _outFileName);
        ITraceSource _traceSource = new TraceSourceFixture();
        List<FieldMetaData> _lf = new List<FieldMetaData>();
        foreach (KeyValuePair<string, IVariable> _item in _dataSource)
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
        DataSetConfiguration _newDataSetConfiguration = new DataSetConfiguration()
        {
          AssociationName = _associationName,
          AssociationRole = AssociationRole.Producer,
          ConfigurationGuid = System.Guid.NewGuid(),
          ConfigurationVersion = new ConfigurationVersionDataType() { MajorVersion = 1, MinorVersion = 0 },
          Id = System.Guid.NewGuid(),
          InformationModelURI = BoilersSet.Namespaces.BoilersSet,
          DataSet = _lf.ToArray(),
          DataSymbolicName = _associationName,
          MaxBufferTime = 1000,
          PublishingInterval = 100,
          RepositoryGroup = _associationName,
          Root = new NodeDescriptor()
          {
            BindingDescription = "Binding Description",
            DataType = new XmlQualifiedName(BoilerType.BrowseNames.BoilerType, BoilerType.Namespaces.BoilerType) { },
            InstanceDeclaration = false,
            NodeClass = InstanceNodeClassesEnum.Object,
            NodeIdentifier = new XmlQualifiedName(_associationName, BoilersSet.Namespaces.BoilersSet)
          }
        };
        ConfigurationManagement.AddDataSetConfiguration(_newDataSetConfiguration, new Tuple<string, ushort, System.Guid>("UDP", 1, ProducerId), _inFileName, _outFileName, _traceSource);
        //ConfigurationManagement.ReplaceDataSetFields(_lf.ToArray(), associationName, inFileName, outFileName, _traceSource);

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
