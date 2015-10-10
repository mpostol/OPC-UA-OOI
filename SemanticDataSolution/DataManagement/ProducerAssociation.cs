
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

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
    /// <param name="data">The semanti data description.</param>
    /// <param name="aliasName">Name of the alias - .</param>
    /// <param name="dataSetConfiguration">The data set configuration.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    internal ProducerAssociation(ISemanticData data, string aliasName, DataSetConfiguration dataSetConfiguration, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
      : base(data, aliasName)
    {
      IProducerBinding[] _pb =
        dataSetConfiguration.Members.Select<DataMemberConfiguration, IProducerBinding>
        ( (x) =>
        {
          IProducerBinding _ret = x.GetProducerBinding4DataMember(dataSetConfiguration, bindingFactory, encodingFactory);
          _ret.PropertyChanged += ProcessDataBindings_CollectionChanged;
          return _ret;
        }).ToArray<IProducerBinding>();
      m_ProcessDataBindings = new ObservableCollection<IProducerBinding>(m_ProcessDataBindings.AsEnumerable<IProducerBinding>());
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
      if (! m_MessageWriter.Exists(x => x.Equals(messageWriter)))
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
    protected override void InitializeCommunication()
    {
      //Do nothing;
    }
    protected override void OnEnabling()
    {
      foreach (IProducerBinding _pbx in m_ProcessDataBindings)
        _pbx.OnEnabling();
    }
    protected override void OnDisabling()
    {
      foreach (IProducerBinding _pbx in m_ProcessDataBindings)
        _pbx.OnDisabling();
    }
    protected internal override void AddMessageHandler(IMessageHandler messageHandler)
    {
      AddMessageWriter(messageHandler as IMessageWriter);
    }
    private List<IMessageWriter> m_MessageWriter = new List<IMessageWriter>();
    private ObservableCollection<IProducerBinding> m_ProcessDataBindings = new ObservableCollection<IProducerBinding>();
    private void ProcessDataBindings_CollectionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      foreach (IMessageWriter _mwx in m_MessageWriter)
        _mwx.Send(x => m_ProcessDataBindings[x]);
    }

    #endregion

  }

}
