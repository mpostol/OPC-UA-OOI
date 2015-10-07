
using System;

namespace UAOOI.SemanticData.DataManagement
{
  ///// <summary>
  ///// Interface IEndPointConfiguration - Represents the current network attachment parameters.
  ///// Depending on the role of the <see cref="IAssociation"/> it describes: remote or local end point.
  ///// </summary>
  //public interface IEndPointConfiguration
  //{
  //}
  public interface IMessageWriter : IMessageHandler
  {
  }
  public interface IMessageReader : IMessageHandler
  {
    event EventHandler<MessageEventArg> messageHandlerStatusChanged;
  }
  public interface IMessageHandler
  {

    /// <summary>
    /// Gets the the state machine for the the <see cref="IMessageHandler"/> instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState"/> providing implementation of the machine state governing this instance behavior.</value>
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
