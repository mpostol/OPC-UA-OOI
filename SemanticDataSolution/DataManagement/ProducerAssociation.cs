
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  internal class ProducerAssociation : Association
  {
    internal ProducerAssociation(ISemanticData data, string aliasName, DataSetConfiguration dataSetConfiguration, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
      : base(data, aliasName)
    {
      IProducerBinding[] _pb =
        dataSetConfiguration.Members.Select<DataMemberConfiguration, IProducerBinding>(x => x.GetProducerBinding4DataMember(dataSetConfiguration, bindingFactory, encodingFactory)).ToArray<IProducerBinding>();
      m_ProcessDataBindings = new ObservableCollection<IProducerBinding>(m_ProcessDataBindings.AsEnumerable<IProducerBinding>());
      m_ProcessDataBindings.CollectionChanged += ProcessDataBindings_CollectionChanged;

    }
    void ProcessDataBindings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      foreach (IMessageWriter _mwx in m_MessageWriter)
        _mwx.Send(x => m_ProcessDataBindings[x]);
    }
    internal void AddMessageWriter(IMessageWriter messageWriter)
    {
      if (messageWriter == null)
        throw new ArgumentNullException("messageReader");
      if (m_MessageWriter.Exists(x => x.Equals(messageWriter)))
        m_MessageWriter.Add(messageWriter);
    }
    void RemoveMessageWriter(IMessageWriter messageWriter)
    {
      if (messageWriter == null)
        throw new ArgumentNullException("messageReader");
      if (m_MessageWriter.Exists(x => x.Equals(messageWriter)))
        m_MessageWriter.Add(messageWriter);
    }
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

  }
}
