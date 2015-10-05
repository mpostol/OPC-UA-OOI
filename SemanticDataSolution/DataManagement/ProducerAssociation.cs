
using System;

namespace UAOOI.SemanticData.DataManagement
{
  internal class ProducerAssociation : Association
  {
    public ProducerAssociation(ISemanticData data, string aliasName)
      : base(data, aliasName)
    {
      throw new NotImplementedException();
    }
    internal void AddMessageWriter(IMessageWriter messageWriter, Func<IMessageHandler> messageHandler)
    {
      throw new NotImplementedException();
    }
    void RemoveMessageWriter(IMessageHandler messageHandler)
    {
      throw new NotImplementedException();
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
