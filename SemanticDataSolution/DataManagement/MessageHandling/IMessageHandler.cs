
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Interface IMessageHandler - provides basic functionality handling messages communication over the wire.
  /// </summary>
  public interface IMessageHandler
  {

    /// <summary>
    /// Gets the state machine for the the <see cref="IMessageHandler"/> instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState"/> providing implementation of the state machine governing this instance behavior.</value>
    IAssociationState State { get; }
    /// <summary>
    /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
    /// </summary>
    /// <remarks>
    /// Depending on the message transport layer type implementation of this function varies. 
    /// </remarks>
    void AttachToNetwork();

  }

}
