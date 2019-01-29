//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

namespace UAOOI.Networking.Core
{
  /// <summary>
  /// Interface IAssociationState - encapsulates the state machine implementation governing this instance behavior.
  /// The provided functionality behavior depends on the current value returned by the <see cref="IAssociationState.State"/> property.
  /// </summary>
  public interface IAssociationState
  {

    /// <summary>
    /// Gets the current state <see cref="HandlerState"/> of the <see cref="Association"/> instance.
    /// </summary>
    /// <value>The state of <see cref="HandlerState"/> type.</value>
    HandlerState State { get; }
    /// <summary>
    /// This method is used to enable a configured <see cref="Association"/> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational"/> state. 
    /// In the case of an error situation, the state changes into <see cref="HandlerState.Error"/>. The operation is rejected if the current <see cref="State"/>  is not <see cref="HandlerState.Disabled"/>.
    /// </summary>
    void Enable();
    /// <summary>
    /// This method is used to disable an already enabled <see cref="Association"/> object.
    /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled"/> or <see cref="HandlerState.NoConfiguration"/>.
    /// </summary>
    void Disable();

  }
}
