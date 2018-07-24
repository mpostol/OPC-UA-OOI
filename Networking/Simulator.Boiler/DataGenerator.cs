//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;

namespace UAOOI.Networking.Simulator.Boiler
{

  /// <summary>
  /// Class DataGenerator - it is simulator producing data to be sent over the wire using message centric communication provided 
  /// by the UAOOI.Networking.SemanticData framework.
  /// The data is generated according to the Boiler model
  /// </summary>
  internal class DataGenerator : IBindingFactory, IDisposable
  {

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="DataGenerator" /> class that generates the data to be used for interoperability testing.
    /// </summary>
    public DataGenerator() : this(BoilersSet.Factory) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="DataGenerator"/> class.
    /// </summary>
    /// <param name="semanticDataSource">The boilers set.</param>
    internal DataGenerator(ISemanticDataSource semanticDataSource)
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      m_TraceSource = _serviceLocator.GetInstance<ITraceSource>();
      m_TraceSource.TraceData(TraceEventType.Information, 43, $"Starting {nameof(DataGenerator)} with the data source {semanticDataSource.GetType().FullName}");
      m_SemanticDataSource = semanticDataSource;
      m_SemanticDataSource.GetSemanticDataSources(RegisterVariable);
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <remarks>It is called by the <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>.</remarks>
    public void Dispose()
    {
      m_SemanticDataSource.Dispose();
    }
    #endregion

    #region IBindingFactory
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belong to the same group are handled according to the same profile.</param>
    /// <param name="processValueName">The name of a variable that is the ultimate destination of the values recovered from messages.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" />.</param>
    /// <param name="fieldTypeInfo">The field metadata definition represented as an object of <see cref="T:UAOOI.Configuration.Networking.Serialization.UATypeInfo" />.</param>
    /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    IConsumerBinding IBindingFactory.GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      m_TraceSource.TraceData(TraceEventType.Error, 60, $"Starting {nameof(IBindingFactory.GetConsumerBinding)} for the process variable {repositoryGroup}_{processValueName}");
      throw new NotImplementedException();
    }
    /// <summary>
    /// Gets the producer binding.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="processValueName">The name of a variable that is the source of the values forwarded by a message over the network.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" /></param>
    /// <param name="fieldTypeInfo">The <see cref="T:UAOOI.Configuration.Networking.Serialization.BuiltInType" />of the message field encoding.</param>
    /// <returns>IProducerBinding.</returns>
    /// <exception cref="System.ArgumentNullException">repositoryGroup</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">encoding</exception>
    IProducerBinding IBindingFactory.GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      string _name = CreateKey(repositoryGroup, processValueName);
      m_TraceSource.TraceData(TraceEventType.Information, 60, $"Starting {nameof(IBindingFactory.GetProducerBinding)} for the process variable {_name}");
      IProducerBinding _return = null;
      switch (fieldTypeInfo.BuiltInType)
      {
        case BuiltInType.Boolean:
          _return = AddBinding<Boolean>(_name, fieldTypeInfo);
          break;
        case BuiltInType.SByte:
          _return = AddBinding<SByte>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Byte:
          _return = AddBinding<Byte>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Int16:
          _return = AddBinding<Int16>(_name, fieldTypeInfo);
          break;
        case BuiltInType.UInt16:
          _return = AddBinding<UInt16>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Int32:
          _return = AddBinding<Int32>(_name, fieldTypeInfo);
          break;
        case BuiltInType.UInt32:
          _return = AddBinding<UInt32>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Int64:
          _return = AddBinding<Int64>(_name, fieldTypeInfo);
          break;
        case BuiltInType.UInt64:
          _return = AddBinding<UInt64>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Float:
          _return = AddBinding<float>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Double:
          _return = AddBinding<Double>(_name, fieldTypeInfo);
          break;
        case BuiltInType.String:
          _return = AddBinding<String>(_name, fieldTypeInfo);
          break;
        case BuiltInType.DateTime:
          _return = AddBinding<DateTime>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Guid:
          _return = AddBinding<Guid>(_name, fieldTypeInfo);
          break;
        case BuiltInType.ByteString:
          _return = AddBinding<byte[]>(_name, fieldTypeInfo);
          break;
        case BuiltInType.Null:
        case BuiltInType.XmlElement:
        case BuiltInType.NodeId:
        case BuiltInType.ExpandedNodeId:
        case BuiltInType.StatusCode:
        case BuiltInType.QualifiedName:
        case BuiltInType.LocalizedText:
        case BuiltInType.ExtensionObject:
        case BuiltInType.DataValue:
        case BuiltInType.Variant:
        case BuiltInType.DiagnosticInfo:
        case BuiltInType.Enumeration:
        default:
          {
            m_TraceSource.TraceData(TraceEventType.Error, 60, $"Cannot get binding for {_name}");
            throw new ArgumentOutOfRangeException($"{_name}");
          }
      }
      m_TraceSource.TraceData(TraceEventType.Information, 60, $"Created binding for the process variable {_name}");
      return _return;
    }
    #endregion

    #region private
    //vars
    private ISemanticDataSource m_SemanticDataSource = null;
    private Dictionary<string, IVariable> m_NodesDictionary = new Dictionary<string, IVariable>();
    private ITraceSource m_TraceSource = null;
    //methods
    private void RegisterVariable(string repositoryGroup, string processValueName, IVariable variable)
    {
      string _name = CreateKey(repositoryGroup, processValueName);
      m_TraceSource.TraceData(TraceEventType.Information, 60, $"Registering next process variable {_name}");
      m_NodesDictionary.Add(_name, variable);
    }
    private string CreateKey(string repositoryGroup, string processValueName)
    {
      return $"{repositoryGroup}.{ processValueName}";
    }
    //methods
    private IProducerBinding AddBinding<type>(string key, UATypeInfo typeInfo)
    {
      IVariable _variable = m_NodesDictionary[key];
      Type _expectedType = typeof(type);
      if (!_expectedType.GetUATypeInfo().IsEqual(_variable.ValueType))
        throw new ArgumentOutOfRangeException($"Wrong argument type: {_expectedType.GetUATypeInfo()} but expected {_variable.ValueType}");
      ProducerBindingMonitoredValue<type> _binding = new ProducerBindingMonitoredValue<type>(key, typeInfo) { MonitoredValue = default(type) };
      _variable.OnStateChanged += (context, node, changes) =>
      {
        if (changes == NodeStateChangeMasks.Value)
          _binding.MonitoredValue = (type)_variable.Value;
      };
      return _binding;
    }
    #endregion

  }

}
