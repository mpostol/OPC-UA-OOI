
using System;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class ConsumerAssociation - implements the association for the consumer side.
  /// </summary>
  internal class ConsumerAssociation : Association
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerAssociation" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="dataSet">The members.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <exception cref="System.NullReferenceException">itemConfiguration argument must not be null
    /// or
    /// members argument must not be null</exception>
    internal ConsumerAssociation(ISemanticData data, DataSetConfiguration dataSet, IBindingFactory bindingFactory, IEncodingFactory encodingFactory) :
      base(data, dataSet.AssociationName)
    {
      m_ProcessDataBindings = dataSet.DataSet.Select<DataMemberConfiguration, IConsumerBinding>(x => x.GetConsumerBinding4DataMember(dataSet.RepositoryGroup, bindingFactory, encodingFactory)).ToArray<IConsumerBinding>();
    }
    #endregion

    #region API
    protected internal override void AddMessageHandler(IMessageHandler messageHandler)
    {
      AddMessageReader(messageHandler as IMessageReader);
    }
    internal void AddMessageReader(IMessageReader messageReader)
    {
      if (messageReader == null)
        throw new ArgumentNullException("messageReader");
      messageReader.ReadMessageCompleted += MessageHandler;
    }
    internal void RemoveMessageReader(IMessageReader messageReader)
    {
      if (messageReader == null)
        throw new ArgumentNullException("messageReader");
      messageReader.ReadMessageCompleted -= MessageHandler;
    }
    #endregion

    #region Association
    protected override void InitializeCommunication()
    {
      //Do nothing;
    }
    protected override void OnEnabling()
    {
      foreach (IBinding _va in m_ProcessDataBindings)
        _va.OnEnabling();
    }
    protected override void OnDisabling()
    {
      foreach (IBinding _va in m_ProcessDataBindings)
        _va.OnDisabling();
    }
    #endregion

    #region private
    private IConsumerBinding[] m_ProcessDataBindings = null;
    private void MessageHandler(object sender, MessageEventArg messageArg)
    {
      if (this.State.State != HandlerState.Operational)
        return;
      if (!messageArg.MessageContent.IAmDestination(this.DataDescriptor))
        return;
      messageArg.MessageContent.UpdateMyValues(x => m_ProcessDataBindings[x], m_ProcessDataBindings.Length);
    }
    #endregion

  }

}
