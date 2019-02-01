//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;
using UAOOI.Networking.Core;

namespace UAOOI.Networking.SemanticData.Common
{
  /// <summary>
  /// Class AssociationStateChangedEventArgs represents the class containing event data representing current configurable object state <see cref="HandlerState"/>.
  /// </summary>
  public class AssociationStateChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AssociationStateChangedEventArgs"/> class.
    /// </summary>
    /// <param name="state">The state of the configurable object state <see cref="HandlerState"/>.</param>
    public AssociationStateChangedEventArgs(HandlerState state)
    {
      State = state;
    }
    /// <summary>
    /// Gets current state of the configurable object.
    /// </summary>
    /// <value>The state <see cref="HandlerState"/>.</value>
    public HandlerState State { get; private set; }
  }
}
