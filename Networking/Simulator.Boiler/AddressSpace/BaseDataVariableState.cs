
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class BaseDataVariableState : BaseVariableState
  {
    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public BaseDataVariableState(NodeState parent) : base(parent) { }

    public BaseDataVariableState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }
}