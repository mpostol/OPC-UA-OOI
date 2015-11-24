
using System;
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
      Value1 = new ProducerBindingMonitoredValue<DateTime>(m_Variable1Name, BuiltInType.DateTime);
      Value2 = new ProducerBindingMonitoredValue<double>(m_Variable2Name, BuiltInType.Double);
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
        return Value1;
      }
      else if (variableName == m_Variable2Name)
      {
        return Value2;
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
      public override void WriteByteString(IBinaryEncoder encoder, byte[] value)
      {
        throw new NotImplementedException();
      }
      public override void WriteDataValue(IBinaryEncoder encoder, IDataValue value)
      {
        throw new NotImplementedException();
      }
      public override void WriteDiagnosticInfo(IBinaryEncoder encoder, IDiagnosticInfo value)
      {
        throw new NotImplementedException();
      }
      public override void WriteExpandedNodeId(IBinaryEncoder encoder, IExpandedNodeId value)
      {
        throw new NotImplementedException();
      }
      public override void WriteExtensionObject(IBinaryEncoder encoder, IExtensionObject value)
      {
        throw new NotImplementedException();
      }
      public override void WriteGuid(IBinaryEncoder encoder, Guid value)
      {
        throw new NotImplementedException();
      }
      public override void WriteLocalizedText(IBinaryEncoder encoder, ILocalizedText value)
      {
        throw new NotImplementedException();
      }
      public override void WriteNodeId(IBinaryEncoder encoder, INodeId value)
      {
        throw new NotImplementedException();
      }
      public override void WriteQualifiedName(IBinaryEncoder encoder, IQualifiedName value)
      {
        throw new NotImplementedException();
      }
      public override void WriteStatusCode(IBinaryEncoder encoder, IStatusCode value)
      {
        throw new NotImplementedException();
      }
      public override void WriteXmlElement(IBinaryEncoder encoder, XmlElement value)
      {
        throw new NotImplementedException();
      }
    }
    //simulator vars
    private ProducerBindingMonitoredValue<DateTime> Value1 { get; set; }
    private ProducerBindingMonitoredValue<double> Value2 { get; set; }
    //vars
    private const string m_Variable1Name = "Value1";
    private const string m_Variable2Name = "Value2";
    private const string m_RepositoryGroup = "repositoryGroup";
    private Timer m_Timer;
    private Random m_Random = new Random();
    private readonly IUAEncoder m_IUAEncoder = new UABinaryEncoderImplementation();
    //methods
    private void TimerCallback(object state)
    {
      if (Value1.HandlerState == HandlerState.Operational)
        Value1.MonitoredValue = DateTime.Now;
      if (Value2.HandlerState == HandlerState.Operational)
        Value2.MonitoredValue = m_Random.NextDouble();
    }
    #endregion

  }

}
