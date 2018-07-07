
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class BaseObjectState : BaseInstanceState
  {

    public BaseObjectState(NodeState parent) : base(NodeClass.Object_1, parent) { }
    public virtual void GetChildren(ISystemContext context, IList<BaseInstanceState> children) { }

  }
}