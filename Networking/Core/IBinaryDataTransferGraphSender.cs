//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.Core
{

  /// <summary>
  /// Interface IBinaryDataTransferGraphSender
  /// Implements the <see cref="System.IDisposable" />
  /// </summary>
  /// <seealso cref="System.IDisposable" />
  public interface IBinaryDataTransferGraphSender : IDisposable
  {

    /// <summary>
    /// Sends a Data Transfer Graph to a remote host (DTG).
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    void SendFrame(byte[] buffer);
    /// <summary>
    /// Gets or sets the state of the transport channel. Interface <see cref="IAssociationState"/> encapsulates the state machine implementation governing this instance behavior.
    /// The provided functionality behavior depends on the current value returned by the <see cref="IAssociationState.State"/> property.
    /// </summary>
    /// <value>An object implementing <see cref="IAssociationState"/> representing the state machine of communication channel.</value>
    IAssociationState State { get; set; }
    /// <summary>
    /// Attach the communication channel to the network.
    /// </summary>
    void AttachToNetwork();

  }

}
