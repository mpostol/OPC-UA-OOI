
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class ProducerAssociation - implements the association for the producer side.
  /// </summary>
  internal class ProducerAssociation : Association, IDisposable
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerAssociation"/> class.
    /// </summary>
    /// <param name="data">The semantic data description.</param>
    /// <param name="aliasName">Name of the alias - .</param>
    /// <param name="dataSet">The data set configuration.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    internal ProducerAssociation(ISemanticData data, string aliasName, DataSetConfiguration dataSet, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
      : base(data, dataSet.AssociationName)
    {
      m_ConfigurationVersion = dataSet.ConfigurationVersion;
      m_DataSetBindings =
        dataSet.DataSet.Select<FieldMetaData, IProducerBinding>
        ((x) =>
        {
          IProducerBinding _ret = x.GetProducerBinding4DataMember(dataSet.RepositoryGroup, bindingFactory, encodingFactory);
          _ret.PropertyChanged += ProducerBinding_PropertyChanged;
          return _ret;
        }).ToArray<IProducerBinding>();
      m_Timer = new Timer(1000) { AutoReset = true };
      m_Timer.Elapsed += M_Timer_Elapsed;
      m_Timer.Start();
    }
    #endregion

    #region public API
    /// <summary>
    /// Adds the message writer.
    /// </summary>
    /// <param name="messageWriter">The message writer.</param>
    /// <exception cref="System.ArgumentNullException">messageReader</exception>
    public void AddMessageWriter(IMessageWriter messageWriter)
    {
      if (messageWriter == null)
        throw new ArgumentNullException("messageReader");
      if (m_MessageWriter.Exists(x => x.Equals(messageWriter)))
        return;
      m_Modified = true;
      m_MessageWriter.Add(messageWriter);
    }
    /// <summary>
    /// Removes the message writer.
    /// </summary>
    /// <param name="messageWriter">The message writer.</param>
    /// <exception cref="System.ArgumentNullException">messageReader</exception>
    public void RemoveMessageWriter(IMessageWriter messageWriter)
    {
      if (messageWriter == null)
        throw new ArgumentNullException("messageReader");
      if (m_MessageWriter.Exists(x => x.Equals(messageWriter)))
        m_MessageWriter.Add(messageWriter);
    }
    #endregion

    #region private
    //vars
    private Timer m_Timer;
    private List<IMessageWriter> m_MessageWriter = new List<IMessageWriter>();
    private IProducerBinding[] m_DataSetBindings;
    private object mLockObject = new object();
    private bool m_Modified = true;
    private Object m_lock = new object();
    private ushort m_MessageSequenceNumber = 0;
    private FieldEncodingEnum m_Encoding;
    //TODO Handle Configuration Version  #140 at: https://github.com/mpostol/OPC-UA-OOI/issues/140
    private ConfigurationVersionDataType m_ConfigurationVersion = null;
    //methods
    protected override void InitializeCommunication()
    {
      //Do nothing;
    }
    protected override void OnEnabling()
    {
      foreach (IProducerBinding _pbx in m_DataSetBindings)
        _pbx.OnEnabling();
    }
    protected override void OnDisabling()
    {
      foreach (IProducerBinding _pbx in m_DataSetBindings)
        _pbx.OnDisabling();
    }
    protected internal override void AddMessageHandler(IMessageHandler messageHandler, AssociationConfiguration configuration)
    {
      base.AddMessageHandler(messageHandler, configuration);
      ProducerAssociationConfiguration _configuration = (ProducerAssociationConfiguration)configuration;
      m_Encoding = _configuration.FieldEncoding;
      AddMessageWriter(messageHandler as IMessageWriter);
    }
    private void ProducerBinding_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      lock (m_lock)
      {
        m_Modified = true;
      }
    }
    private void M_Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      lock (m_lock)
      {
        if (!m_Modified)
          return;
        m_Modified = false;
      }
      Send();
    }
    private void Send()
    {
      foreach (IMessageWriter _mwx in m_MessageWriter)
        lock (mLockObject)
          _mwx.Send(x => m_DataSetBindings[x], Convert.ToUInt16(m_DataSetBindings.Length), UInt64.MaxValue, m_Encoding, DataSetId, m_MessageSequenceNumber, DateTime.UtcNow, m_ConfigurationVersion);
      m_MessageSequenceNumber = m_MessageSequenceNumber.IncRollOver();
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
          m_Timer.Dispose();
        disposedValue = true;
      }
    }
    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~ProducerAssociation() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }
    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion

    #endregion

  }

}
