//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public abstract class BaseDataVariableState : BaseVariableState
  {
    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public BaseDataVariableState(NodeState parent) : base(parent) { }

    public BaseDataVariableState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }
}