//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public interface IVariable
  {

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <value>The value.</value>
    object Value { get; }
    /// <summary>
    /// Called when ClearChangeMasks is called and the ChangeMask is not None.
    /// </summary>
    event NodeStateChangedHandler OnStateChanged;
    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    /// <value>The type of the data returned by the Value property.</value>
    Type ValueType { get; }

  }
}