namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class PropertyState<Type> : PropertyState
  {

    public PropertyState(NodeState parent) : base(parent) { }
    /// <summary>
    /// The value of the variable.
    /// </summary>
    public new Type Value
    {
      get { return (Type)base.Value; }
      set { base.Value = value; }
    }

  }

  public class PropertyState : BaseVariableState
  {

    public PropertyState(NodeState parent) : base(parent) { }

  }

}