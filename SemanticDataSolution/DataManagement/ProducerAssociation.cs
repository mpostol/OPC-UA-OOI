
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class ProducerAssociation - implements the association for the producer side.
  /// </summary>
  internal class ProducerAssociation : Association
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
      m_DataSetBindings =
        dataSet.DataSet.Select<FieldMetaData, IProducerBinding>
        ((x) =>
        {
          IProducerBinding _ret = x.GetProducerBinding4DataMember(dataSet.RepositoryGroup, bindingFactory, encodingFactory);
          _ret.PropertyChanged += ProducerBinding_PropertyChanged;
          return _ret;
        }).ToArray<IProducerBinding>();
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
      if (!m_MessageWriter.Exists(x => x.Equals(messageWriter)))
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
    private List<IMessageWriter> m_MessageWriter = new List<IMessageWriter>();
    private IProducerBinding[] m_DataSetBindings;
    private object mLockObject = new object();
    private bool m_Busy = false;
    private ushort m_MessageSequenceNumber = 0;
    //TODO Handle Configuration Version  #140 at: https://github.com/mpostol/OPC-UA-OOI/issues/140
    private MessageHeader.ConfigurationVersionDataType m_ConfigurationVersion = new MessageHeader.ConfigurationVersionDataType() { MajorVersion = 0, MinorVersion = 0 };
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
    protected internal override void AddMessageHandler(IMessageHandler messageHandler)
    {
      AddMessageWriter(messageHandler as IMessageWriter);
    }
    private void ProducerBinding_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (m_Busy)
        return;
      m_Busy = true;
      foreach (IMessageWriter _mwx in m_MessageWriter)
        lock (mLockObject)
          _mwx.Send(x => m_DataSetBindings[x], Convert.ToUInt16(m_DataSetBindings.Length), UInt64.MaxValue, DataDescriptor, m_MessageSequenceNumber, DateTime.UtcNow, m_ConfigurationVersion);
      m_Busy = false;
      m_MessageSequenceNumber.IncRollOver();
    }
    #endregion

  }

}
