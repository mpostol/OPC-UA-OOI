
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class FolderState : BaseObjectState
  {

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public FolderState(NodeState parent) : base(parent) { }
    public FolderState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }

}