
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

    #region constructors
    public CustomNodeManager()
    {
      ValueBoolean = new ProducerBindingMonitoredValue<Boolean>(nameof(ValueBoolean), BuiltInType.Boolean) { MonitoredValue = false };
      AddBinding(ValueBoolean, nameof(ValueBoolean), () => ValueBoolean.MonitoredValue = Inc(ValueBoolean.MonitoredValue));

      ValueByte = new ProducerBindingMonitoredValue<Byte>(nameof(ValueByte), BuiltInType.Byte) { MonitoredValue = Byte.MinValue };
      AddBinding(ValueByte, nameof(ValueByte), () => ValueByte.MonitoredValue = Inc(ValueByte.MonitoredValue));

      ValueDateTime = new ProducerBindingMonitoredValue<DateTime>(nameof(ValueDateTime), BuiltInType.DateTime) { MonitoredValue = DateTime.Now };
      AddBinding(ValueDateTime, nameof(ValueDateTime), () => ValueDateTime.MonitoredValue = Inc(ValueDateTime.MonitoredValue));

      ValueDouble = new ProducerBindingMonitoredValue<double>(nameof(ValueDouble), BuiltInType.Double) { MonitoredValue = 0 };
      AddBinding(ValueDouble, nameof(ValueDouble), () => ValueDouble.MonitoredValue = Inc(ValueDouble.MonitoredValue));

      ValueFloat = new ProducerBindingMonitoredValue<float>(nameof(ValueFloat), BuiltInType.Float) { MonitoredValue = float.MinValue };
      AddBinding(ValueFloat, nameof(ValueFloat), () => ValueFloat.MonitoredValue = Inc(ValueFloat.MonitoredValue));

      ValueGuid = new ProducerBindingMonitoredValue<Guid>(nameof(ValueGuid), BuiltInType.Guid) { MonitoredValue = Guid.NewGuid() };
      AddBinding(ValueGuid, nameof(ValueGuid), () => ValueGuid.MonitoredValue = Inc(ValueGuid.MonitoredValue));

      ValueInt16 = new ProducerBindingMonitoredValue<Int16>(nameof(ValueInt16), BuiltInType.Int16) { MonitoredValue = Int16.MinValue };
      AddBinding(ValueInt16, nameof(ValueInt16), () => ValueInt16.MonitoredValue = Inc(ValueInt16.MonitoredValue));

      ValueInt32 = new ProducerBindingMonitoredValue<Int32>(nameof(ValueInt32), BuiltInType.Int32) { MonitoredValue = Int32.MinValue };
      AddBinding(ValueInt32, nameof(ValueInt32), () => ValueInt32.MonitoredValue = Inc(ValueInt32.MonitoredValue));

      ValueInt64 = new ProducerBindingMonitoredValue<Int64>(nameof(ValueInt64), BuiltInType.Int64) { MonitoredValue = Int64.MinValue };
      AddBinding(ValueInt64, nameof(ValueInt64), () => ValueInt64.MonitoredValue = Inc(ValueInt64.MonitoredValue));

      ValueSByte = new ProducerBindingMonitoredValue<SByte>(nameof(ValueSByte), BuiltInType.SByte) { MonitoredValue = SByte.MinValue };
      AddBinding(ValueSByte, nameof(ValueSByte), () => ValueSByte.MonitoredValue = Inc(ValueSByte.MonitoredValue));

      ValueString = new ProducerBindingMonitoredValue<String>(nameof(ValueString), BuiltInType.String) { MonitoredValue = String.Empty };
      AddBinding(ValueString, nameof(ValueString), () => ValueString.MonitoredValue = Inc(ValueString.MonitoredValue));

      ValueUInt16 = new ProducerBindingMonitoredValue<UInt16>(nameof(ValueUInt16), BuiltInType.UInt16) { MonitoredValue = UInt16.MinValue };
      AddBinding(ValueUInt16, nameof(ValueUInt16), () => ValueUInt16.MonitoredValue = Inc(ValueUInt16.MonitoredValue));

      ValueUInt32 = new ProducerBindingMonitoredValue<UInt32>(nameof(ValueUInt32), BuiltInType.UInt32) { MonitoredValue = UInt32.MinValue };
      AddBinding(ValueUInt32, nameof(ValueUInt32), () => ValueUInt32.MonitoredValue = Inc(ValueUInt32.MonitoredValue));

      ValueUInt64 = new ProducerBindingMonitoredValue<UInt32>(nameof(ValueUInt64), BuiltInType.UInt64) { MonitoredValue = UInt32.MinValue };
      AddBinding(ValueUInt64, nameof(ValueUInt64), () => ValueUInt64.MonitoredValue = Inc(ValueUInt64.MonitoredValue));
    }
    #endregion

    #region API
    /// <summary>
    /// Run this instance - stuarts pumping the data.
    /// </summary>
    internal void Run()
    {
      m_Timer = new Timer(TimerCallback, null, 0, 500);
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
    public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
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
    public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentNullException("repositoryGroup");
      if (variableName == m_Variable1Name)
      {
        return ValueDateTime;
      }
      else if (variableName == m_Variable2Name)
      {
        return ValueDouble;
      }
      else
        throw new ArgumentOutOfRangeException("variableName");
    }
    #endregion

    #region IEncodingFactory
    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">repositoryGroup</exception>
    void IEncodingFactory.UpdateValueConverter(IBinding converter, string repositoryGroup, BuiltInType sourceEncoding)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentOutOfRangeException("repositoryGroup");
      Debug.Assert(sourceEncoding == converter.Encoding);
    }
    /// <summary>
    /// Gets the ua decoder.
    /// </summary>
    /// <value>The ua decoder.</value>
    IUADecoder IEncodingFactory.UADecoder
    {
      get { return null; }
    }
    public IUAEncoder UAEncoder
    {
      get { return m_IUAEncoder; }
    }
    #endregion

    #region private
    //types
    /// <summary>
    /// Class UABinaryEncoderImplementation - limited implementation of the <see cref="UABinaryEncoder"/> for the testing purpose only.
    /// </summary>
    private class UABinaryEncoderImplementation : UABinaryEncoder
    {
      public override void Write(IBinaryEncoder encoder, byte[] value)
      {
        throw new NotImplementedException();
      }
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
    //simulator vars
    private ProducerBindingMonitoredValue<DateTime> ValueDateTime { get; set; }
    private ProducerBindingMonitoredValue<bool> ValueBoolean { get; set; }
    private ProducerBindingMonitoredValue<byte> ValueByte { get; set; }
    private ProducerBindingMonitoredValue<float> ValueFloat { get; set; }
    private ProducerBindingMonitoredValue<double> ValueDouble { get; set; }
    private ProducerBindingMonitoredValue<Guid> ValueGuid { get; set; }
    private ProducerBindingMonitoredValue<Int16> ValueInt16 { get; set; }
    private ProducerBindingMonitoredValue<Int32> ValueInt32 { get; set; }
    private ProducerBindingMonitoredValue<Int64> ValueInt64 { get; set; }
    private ProducerBindingMonitoredValue<SByte> ValueSByte { get; set; }
    private ProducerBindingMonitoredValue<String> ValueString { get; set; }
    private ProducerBindingMonitoredValue<ushort> ValueUInt16 { get; set; }
    private ProducerBindingMonitoredValue<uint> ValueUInt32 { get; set; }
    private ProducerBindingMonitoredValue<uint> ValueUInt64 { get; set; }
    //vars
    private const string m_Variable1Name = "Value1";
    private const string m_Variable2Name = "Value2";
    private const string m_RepositoryGroup = "repositoryGroup";
    private Timer m_Timer;
    private readonly IUAEncoder m_IUAEncoder = new UABinaryEncoderImplementation();
    private event EventHandler m_TimeEven;
    private Dictionary<string, IProducerBinding> m_NodesDictionary = new Dictionary<string, IProducerBinding>();

    //methods
    #region Inc methods
    private static string Inc(string monitoredValue)
    {
      return $"Hello World; Here now is: {DateTime.Now.ToShortDateString()}";
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
    private static bool Inc(bool monitoredValue)
    {
      return !monitoredValue;
    }
    #endregion    

    private void AddBinding(IProducerBinding binding, string key, Action increment)
    {
      m_NodesDictionary.Add(key, binding);
      m_TimeEven += (x, y) => increment();
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
      //if (ValueDateTime.HandlerState == HandlerState.Operational)
      //  ValueDateTime.MonitoredValue = DateTime.Now;
      //if (Value2.HandlerState == HandlerState.Operational)
      //  Value2.MonitoredValue = m_Random.NextDouble();
    }
    #endregion

  }

}
