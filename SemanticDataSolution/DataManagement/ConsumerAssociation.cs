
using System;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  public class ConsumerAssociation : Association
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerAssociation"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="aliasName">Name of the alias.</param>
    /// <param name="members">The members.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <param name="itemConfiguration">The item configuration.</param>
    /// <exception cref="System.NullReferenceException">
    /// itemConfiguration argument must not be null
    /// or
    /// members argument must not be null
    /// </exception>
    internal ConsumerAssociation
      (ISemanticData data, string aliasName, DataSetConfiguration members, IBindingFactory bindingFactory, IEncodingFactory encodingFactory, ISemanticDataItemConfiguration itemConfiguration) :
      base(data, aliasName)
    {
      if (itemConfiguration == null)
        throw new NullReferenceException("itemConfiguration argument must not be null");
      if (itemConfiguration == null)
        throw new NullReferenceException("itemConfiguration argument must not be null");
      m_SemanticDataItemConfiguration = itemConfiguration;
      if (members == null)
        throw new NullReferenceException("members argument must not be null");
      m_ProcessDataBindings = members.Members.Select<DataMemberConfiguration, IBinding>(x => x.GetBinding4DataMember(members, bindingFactory, encodingFactory)).ToArray<IBinding>();
    }
    internal IProducerConfiguration Configuration { get; private set; }
    internal void AddMessageReader(IMessageReader messageReader)
    {
      messageReader.messageHandlerStatusChanged += MessageHandler;
    }
    internal void RemoveMessageReader(IMessageReader messageReader)
    {
      messageReader.messageHandlerStatusChanged -= MessageHandler;
    }


    //private
    private IBinding[] m_ProcessDataBindings = null;
    private void MessageHandler(object sender, MessageEventArg messageArg)
    {
      if (!this.IAmResponsible(messageArg.MessageContent))
        return;
      messageArg.MessageContent.UpdateMyValues(x => m_ProcessDataBindings[x]);
    }
    private bool IAmResponsible(Message p)
    {
      throw new NotImplementedException();
    }
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
