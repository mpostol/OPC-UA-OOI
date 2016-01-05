
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using UAOOI.SemanticData.DataManagement;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{

  /// <summary>
  /// Class CustomNodeManager - it is simulator producing data to be sent over the wire using message centric communication provided 
  /// by the UAOOI.SemanticData.DataManagement framework.
  /// </summary>
  internal class CustomNodeManager : IBindingFactory, IEncodingFactory, IDisposable
  {

    #region API
    /// <summary>
    /// Run this instance - stuarts pumping the data.
    /// </summary>
    internal void Run()
    {
      m_Timer = new Timer(TimerCallback, null, 0, 1000);
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
      if (m_Timer != null)
        m_Timer.Change(Timeout.Infinite, Timeout.Infinite);
    }
    #endregion    

    #region IBindingFactory
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belong to the same group are handled according to the same profile.</param>
    /// <param name="variableName">The name of a variable that is the ultimate destination of the values recovered from messages. Must be unique in the context of the repositories group.
    /// is updated periodically by a data produced - user of the <see cref="IBinding" /> object.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    IConsumerBinding IBindingFactory.GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Gets the producer binding.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>IProducerBinding.</returns>
    /// <exception cref="System.ArgumentNullException">repositoryGroup</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    /// <exception cref="System.NotImplementedException"></exception>
    IProducerBinding IBindingFactory.GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentNullException("repositoryGroup");
      string _name = $"{ repositoryGroup}.{ processValueName}";
      IProducerBinding _return = null;
      if (m_NodesDictionary.ContainsKey(processValueName))
        _return = m_NodesDictionary[processValueName];
      else
        switch (fieldTypeInfo.BuiltInType)
        {
          case BuiltInType.Boolean:
            _return = AddBinding<Boolean>(_name, Inc, false, BuiltInType.Boolean);
            break;
          case BuiltInType.SByte:
            _return = AddBinding<SByte>(_name, Inc, sbyte.MinValue, BuiltInType.SByte);
            break;
          case BuiltInType.Byte:
            _return = AddBinding<Byte>(_name, Inc, Byte.MinValue, BuiltInType.Byte);
            break;
          case BuiltInType.Int16:
            _return = AddBinding<Int16>(_name, Inc, Int16.MinValue, BuiltInType.Int16);
            break;
          case BuiltInType.UInt16:
            _return = AddBinding<UInt16>(_name, Inc, UInt16.MinValue, BuiltInType.UInt16);
            break;
          case BuiltInType.Int32:
            _return = AddBinding<Int32>(_name, Inc, Int32.MinValue, BuiltInType.Int32);
            break;
          case BuiltInType.UInt32:
            _return = AddBinding<UInt32>(_name, Inc, UInt32.MinValue, BuiltInType.UInt32);
            break;
          case BuiltInType.Int64:
            _return = AddBinding<Int64>(_name, Inc, Int64.MinValue, BuiltInType.Int64);
            break;
          case BuiltInType.UInt64:
            _return = AddBinding<UInt64>(_name, Inc, UInt64.MinValue, BuiltInType.UInt64);
            break;
          case BuiltInType.Float:
            _return = AddBinding<float>(_name, Inc, -10.12345678f, BuiltInType.Float);
            break;
          case BuiltInType.Double:
            _return = AddBinding<Double>(_name, Inc, -10.12345678, BuiltInType.Double);
            break;
          case BuiltInType.String:
            _return = AddBinding<String>(_name, Inc, String.Empty, BuiltInType.String);
            break;
          case BuiltInType.DateTime:
            _return = AddBinding<DateTime>(_name, Inc, DateTime.Now, BuiltInType.DateTime);
            break;
          case BuiltInType.Guid:
            _return = AddBinding<Guid>(_name, Inc, Guid.NewGuid(), BuiltInType.Guid);
            break;
          case BuiltInType.ByteString:
            _return = AddBinding<byte[]>(_name, Inc, new byte[] { 0 }, BuiltInType.ByteString);
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
            throw new ArgumentOutOfRangeException("encoding");
        }
      return _return;
    }
    #endregion

    #region IEncodingFactory
    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="binding">An object responsible to transfer the value between the message and ultimate destination in the repository.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">repositoryGroup</exception>
    void IEncodingFactory.UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentOutOfRangeException("repositoryGroup");
      Debug.Assert(sourceEncoding.BuiltInType == binding.Encoding);
    }
    /// <summary>
    /// Gets the ua decoder.
    /// </summary>
    /// <value>The ua decoder.</value>
    public IUAEncoder UAEncoder
    {
      get { return m_IUAEncoder; }
    }
    /// <summary>
    /// Gets the decoder that provides methods to be used to decode OPC UA Built-in types.
    /// </summary>
    /// <value>The object implementing <see cref="T:UAOOI.SemanticData.DataManagement.Encoding.IUADecoder" /> interface.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public IUADecoder UADecoder
    {
      get
      {
        throw new NotImplementedException();
      }
    }
    #endregion

    #region private
    //types
    /// <summary>
    /// Class UABinaryEncoderImplementation - limited implementation of the <see cref="UABinaryEncoder"/> for the testing purpose only.
    /// </summary>
    private class UABinaryEncoderImplementation : UABinaryEncoder
    {
      public override void Write(IBinaryEncoder encoder, IDataValue value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, IDiagnosticInfo value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, IExpandedNodeId value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, IExtensionObject value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, ILocalizedText value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, INodeId value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, IQualifiedName value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, IStatusCode value)
      {
        throw new NotImplementedException();
      }
      public override void Write(IBinaryEncoder encoder, XmlElement value)
      {
        throw new NotImplementedException();
      }
    }
    //vars
    private const string m_RepositoryGroup = "repositoryGroup";
    private Timer m_Timer;
    private readonly IUAEncoder m_IUAEncoder = new UABinaryEncoderImplementation();
    private event EventHandler m_TimeEven;
    private Dictionary<string, IProducerBinding> m_NodesDictionary = new Dictionary<string, IProducerBinding>();
    //methods
    #region Inc methods
    private static string Inc(string monitoredValue)
    {
      return $"Hello World; Now is: {DateTime.Now.ToLongTimeString()}";
    }
    private static Guid Inc(Guid monitoredValue)
    {
      return Guid.NewGuid();
    }
    private static SByte Inc(SByte monitoredValue)
    {
      return monitoredValue == SByte.MaxValue ? SByte.MinValue : (SByte)(monitoredValue + 1);
    }
    private static UInt16 Inc(UInt16 monitoredValue)
    {
      return monitoredValue == UInt16.MaxValue ? UInt16.MinValue : (UInt16)(monitoredValue + 1);
    }
    private static UInt32 Inc(UInt32 monitoredValue)
    {
      return monitoredValue == UInt32.MaxValue ? UInt32.MinValue : (UInt32)(monitoredValue + 1);
    }
    private static UInt64 Inc(UInt64 monitoredValue)
    {
      return monitoredValue == UInt64.MaxValue ? UInt64.MinValue : (UInt64)(monitoredValue + 1);
    }
    private static Int64 Inc(Int64 monitoredValue)
    {
      return monitoredValue == Int64.MaxValue ? Int64.MinValue : (Int64)(monitoredValue + 1);
    }
    private static Int32 Inc(Int32 monitoredValue)
    {
      return monitoredValue == Int32.MaxValue ? Int32.MinValue : (Int32)(monitoredValue + 1);
    }
    private static Int16 Inc(Int16 monitoredValue)
    {
      return monitoredValue == Int16.MaxValue ? Int16.MinValue : (Int16)(monitoredValue + 1);
    }
    private static float Inc(float monitoredValue)
    {
      return monitoredValue + 1;
    }
    private static double Inc(double monitoredValue)
    {
      return monitoredValue + 1;
    }
    private static DateTime Inc(DateTime monitoredValue)
    {
      return DateTime.UtcNow;
    }
    private static byte Inc(byte monitoredValue)
    {
      return monitoredValue == byte.MaxValue ? byte.MinValue : (byte)(monitoredValue + 1);
    }
    private static byte[] Inc(byte[] monitoredValue)
    {
      byte[] _ret = new byte[Math.Min(monitoredValue.Length + 1, 254)];
      for (byte i = 0; i < _ret.Length; i++)
        _ret[i] = i;
      return _ret;
    }
    private static bool Inc(bool monitoredValue)
    {
      return !monitoredValue;
    }
    #endregion    
    private IProducerBinding AddBinding<type>(string key, Func<type, type> increment, type defaultValue, BuiltInType builtInType)
    {
      ProducerBindingMonitoredValue<type> binding = new ProducerBindingMonitoredValue<type>(key, builtInType) { MonitoredValue = defaultValue };
      m_NodesDictionary.Add(key, binding);
      m_TimeEven += (x, y) => binding.MonitoredValue = increment(binding.MonitoredValue);
      return binding;
    }
    private void TimerCallback(object state)
    {
      try
      {
        m_TimeEven?.Invoke(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
      }
    }
    #endregion

  }

}
