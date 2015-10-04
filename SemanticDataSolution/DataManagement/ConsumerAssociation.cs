using System;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  public class ConsumerAssociation : Association
  {
    public ConsumerAssociation
      (ISemanticData data, DataSetConfiguration members, string aliasName, IBindingFactory bindingFactory, IEncodingFactory encodingFactory, ISemanticDataItemConfiguration itemConfiguration) :
      base(data, aliasName)
    {
      if (itemConfiguration == null)
        throw new NullReferenceException("itemConfiguration argument must not be null");
      m_SemanticDataItemConfiguration = itemConfiguration;
      if (members == null)
        throw new NullReferenceException("members argument must not be null");
      m_ProcessDataBindings = members.Members.Select<DataMemberConfiguration, IBinding>(x => x.GetBinding4DataMember(members, bindingFactory, encodingFactory)).ToArray<IBinding>();
    }
    internal IProducerConfiguration Configuration { get; private set; }
    internal void AddMessageReader(IMessageReader messageWriter, Func<IMessageHandler> messageHandler)
    {
      throw new NotImplementedException();
    }
    internal void RemoveMessageReader(IMessageHandler messageHandler)
    {
      throw new NotImplementedException();
    }
    public override IEndPointConfiguration Address
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    //private
    private IBinding[] m_ProcessDataBindings = null;
    private ISemanticDataItemConfiguration m_SemanticDataItemConfiguration;

    protected override ISemanticDataItemConfiguration GetDefaultConfiguration()
    {
      return m_SemanticDataItemConfiguration;
    }

    protected override void InitializeCommunication()
    {
      throw new NotImplementedException();
    }

    protected override void OnEnabling()
    {
      throw new NotImplementedException();
    }

    protected override void OnDisabling()
    {
      throw new NotImplementedException();
    }
  }
}
