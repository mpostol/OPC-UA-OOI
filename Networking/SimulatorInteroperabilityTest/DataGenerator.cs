
using System;
using System.Collections.Generic;
using System.Threading;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.SimulatorInteroperabilityTest
{

  /// <summary>
  /// Class DataGenerator - it is simulator producing data to be sent over the wire using message centric communication provided 
  /// by the UAOOI.Networking.SemanticData framework.
  /// 
  /// The data is generated according to the principles defined by the OPCF to proceed interoperability testing.
  /// </summary>
  internal class DataGenerator : IBindingFactory, IDisposable
  {

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="DataGenerator" /> class that generates the data to be used for interoperability testing.
    /// </summary>
    public DataGenerator()
    {
      m_Timer = new Timer(TimerCallback, null, 1000, 1000);
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <remarks>It is called by the <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>.</remarks>
    public void Dispose()
    {
      if (m_Timer == null)
        return;
      m_Timer.Change(Timeout.Infinite, Timeout.Infinite);
      m_Timer.Dispose();
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
      if (repositoryGroup != Encoding.EncodingCompositionSettings.ConfigurationRepositoryGroup)
        throw new ArgumentNullException("repositoryGroup");
      string _name = $"{repositoryGroup}.{ processValueName}";
      IProducerBinding _return = null;
      if (m_NodesDictionary.ContainsKey(processValueName))
        _return = m_NodesDictionary[processValueName];
      else
        switch (fieldTypeInfo.BuiltInType)
        {
          case BuiltInType.Boolean:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Boolean>(_name, Inc, false, fieldTypeInfo) : AddBinding<Boolean[]>(_name, x => Inc(y => (y & 1) == 0, x.Length), new bool[] { false }, fieldTypeInfo);
            break;
          case BuiltInType.SByte:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<SByte>(_name, Inc, sbyte.MinValue, fieldTypeInfo) : AddBinding<sbyte[]>(_name, x => Inc(y => (sbyte)y, x.Length), new sbyte[] { sbyte.MinValue }, fieldTypeInfo);
            break;
          case BuiltInType.Byte:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Byte>(_name, Inc, Byte.MinValue, fieldTypeInfo) : AddBinding<Byte[]>(_name, x => Inc(y => (Byte)y, x.Length), new Byte[] { Byte.MinValue }, fieldTypeInfo);
            break;
          case BuiltInType.Int16:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Int16>(_name, Inc, Int16.MinValue, fieldTypeInfo) : AddBinding<Int16[]>(_name, x => Inc(y => (Int16)y, x.Length), new Int16[] { Int16.MinValue }, fieldTypeInfo);
            break;
          case BuiltInType.UInt16:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<UInt16>(_name, Inc, UInt16.MinValue, fieldTypeInfo) : AddBinding<UInt16[]>(_name, x => Inc(y => (UInt16)y, x.Length), new UInt16[] { UInt16.MinValue }, fieldTypeInfo);
            break;
          case BuiltInType.Int32:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Int32>(_name, Inc, Int32.MinValue, fieldTypeInfo) : AddBinding<Int32[]>(_name, x => Inc(y => (Int32)y, x.Length), new Int32[] { Int32.MinValue }, fieldTypeInfo);
            break;
          case BuiltInType.UInt32:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<UInt32>(_name, Inc, UInt32.MinValue, fieldTypeInfo) : AddBinding<UInt32[]>(_name, x => Inc(y => (UInt32)y, x.Length), new UInt32[] { UInt32.MinValue }, fieldTypeInfo);
            break;
          case BuiltInType.Int64:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Int64>(_name, Inc, Int64.MinValue, fieldTypeInfo) : AddBinding<Int64[]>(_name, x => Inc(y => (Int64)y, x.Length), new Int64[] { 0 }, fieldTypeInfo);
            break;
          case BuiltInType.UInt64:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<UInt64>(_name, Inc, UInt64.MinValue, fieldTypeInfo) : AddBinding<UInt64[]>(_name, x => Inc(y => (UInt64)y, x.Length), new UInt64[] { 0 }, fieldTypeInfo);
            break;
          case BuiltInType.Float:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<float>(_name, Inc, -10.12345678f, fieldTypeInfo) : AddBinding<float[]>(_name, x => Inc(y => (float)y, x.Length), new float[] { -10.12345678f }, fieldTypeInfo);
            break;
          case BuiltInType.Double:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Double>(_name, Inc, -10.12345678, fieldTypeInfo) : AddBinding<Double[]>(_name, x => Inc(y => (Double)y, x.Length), new Double[] { 0 }, fieldTypeInfo);
            break;
          case BuiltInType.String:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<String>(_name, Inc, String.Empty, fieldTypeInfo) : AddBinding<String[]>(_name, x => Inc(y => $"#{y}", x.Length), new String[] { }, fieldTypeInfo);
            break;
          case BuiltInType.DateTime:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<DateTime>(_name, Inc, DateTime.Now, fieldTypeInfo) : AddBinding<DateTime[]>(_name, x => Inc(y => DateTime.Now, x.Length), new DateTime[] { }, fieldTypeInfo);
            break;
          case BuiltInType.Guid:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<Guid>(_name, Inc, Guid.NewGuid(), fieldTypeInfo) : AddBinding<Guid[]>(_name, x => Inc(y => Guid.NewGuid(), x.Length), new Guid[] { }, fieldTypeInfo);
            break;
          case BuiltInType.ByteString:
            _return = fieldTypeInfo.ValueRank < 0 ? AddBinding<byte[]>(_name, Inc, new byte[] { 0 }, fieldTypeInfo) : AddBinding<byte[][]>(_name, x => Inc(y => new byte[] { 0, 1, 2, 3, 4 }, x.Length), new byte[][] { new byte[] { } }, fieldTypeInfo);
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

    #region private
    //vars
    private Timer m_Timer;
    private event EventHandler m_TimeEvent;
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
    private static type[] Inc<type>(Func<byte, type> incrementItem, int previousLength)
    {
      type[] _ret = new type[Math.Min(previousLength + 1, 254)];
      for (byte i = 0; i < _ret.Length; i++)
        _ret[i] = incrementItem(i);
      return _ret;
    }
    #endregion
    private IProducerBinding AddBinding<type>(string key, Func<type, type> increment, type defaultValue, UATypeInfo typeInfo)
    {
      ProducerBindingMonitoredValue<type> binding = new ProducerBindingMonitoredValue<type>(key, typeInfo) { MonitoredValue = defaultValue };
      m_NodesDictionary.Add(key, binding);
      m_TimeEvent += (x, y) => binding.MonitoredValue = increment(binding.MonitoredValue);
      return binding;
    }
    private void TimerCallback(object state)
    {
      try
      {
        m_TimeEvent?.Invoke(this, EventArgs.Empty);
      }
      catch (Exception) { }
    }
    #endregion

  }

}
