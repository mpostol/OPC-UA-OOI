//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;
using UAOOI.Networking.Core;

namespace UAOOI.Networking.SemanticData.MessageHandling
{

  /// <summary>
  /// Interface IMessageHandler - provides basic functionality handling messages communication over the wire.
  /// </summary>
  public interface IMessageHandler : IDisposable
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
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items 
    /// within the data set.
    /// </summary>
    /// <value>The content mask represented as unsigned number <see cref="UInt64"/>. The order of the bits starting from the least significant 
    /// bit matches the order of the data items within the data set.
    /// </value>
    UInt64 ContentMask { get; }

  }

}
