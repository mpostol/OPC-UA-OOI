
using System;

namespace UAOOI.SemanticData.DataManagement
{
  public class MessageEventArg : EventArgs
  {
    public MessageEventArg(Message newMessage)
    {
      MessageContent = newMessage;
    }
    internal Message MessageContent { get; private set; }
  }
}
