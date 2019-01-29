//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Networking.Core;

namespace UAOOI.Networking.SemanticData.UnitTest.Helpers
{
  internal class MyState : IAssociationState
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="MyState"/> class.
    /// </summary>
    public MyState()
    {
      State = HandlerState.Disabled;
    }
    /// <summary>
    /// Gets the current state <see cref="HandlerState" /> of the <see cref="Association" /> instance.
    /// </summary>
    /// <value>The state of <see cref="HandlerState" /> type.</value>
    public HandlerState State
    {
      get;
      private set;
    }
    /// <summary>
    /// This method is used to enable a configured <see cref="Association" /> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational" /> state.
    /// In the case of an error situation, the state changes into <see cref="HandlerState.Error" />. The operation is rejected if the current <see cref="State" />  is not <see cref="HandlerState.Disabled" />.
    /// </summary>
    /// <exception cref="System.ArgumentException">Wrong state</exception>
    public void Enable()
    {
      if (State != HandlerState.Disabled)
        throw new ArgumentException("Wrong state");
      State = HandlerState.Operational;
    }
    /// <summary>
    /// This method is used to disable an already enabled <see cref="Association" /> object.
    /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled" /> or <see cref="HandlerState.NoConfiguration" />.
    /// </summary>
    /// <exception cref="System.ArgumentException">Wrong state</exception>
    public void Disable()
    {
      if (State != HandlerState.Operational)
        throw new ArgumentException("Wrong state");
      State = HandlerState.Disabled;
    }

  }
}
