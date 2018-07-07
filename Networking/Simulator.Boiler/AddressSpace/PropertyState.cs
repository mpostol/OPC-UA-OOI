namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class PropertyState<Type> : PropertyState
  {

    public PropertyState(NodeState parent) : base(parent) { }

  }

  public class PropertyState : BaseVariableState
  {
    public PropertyState(NodeState parent) : base(parent) { }

  }

}