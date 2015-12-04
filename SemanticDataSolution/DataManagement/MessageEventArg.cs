
using System;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class MessageEventArg - class representing an event that contains new <see cref="Message"/> to be processed by the consumer or a producer outcome to be 
  /// sent over the network by the underlying message transport protocol..
  /// </summary>
  public class MessageEventArg : EventArgs
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageEventArg" /> class.
    /// </summary>
    /// <param name="newMessage">The new message to be processed by the consumer or a producer outcome to be
    /// sent over the network by the underlying message transport protocol.</param>
    /// <param name="dataSetId">The data set identifier.</param>
    public MessageEventArg(IMessageReader newMessage, UInt32 dataSetId)
    {
      MessageContent = newMessage;
      DataSetId = dataSetId;
    }
    /// <summary>
    /// Gets the content of the just received message to be processed by the consumer or a producer outcome to be 
    /// sent over the network by the underlying message transport protocol.
    /// </summary>
    /// <value>The content of the message.</value>
    internal IMessageReader MessageContent { get; private set; }
    /// <summary>
    /// Gets the data set identifier.
    /// </summary>
    /// <value>The data set identifier.</value>
    internal UInt32 DataSetId { get; private set; }

  }
}
