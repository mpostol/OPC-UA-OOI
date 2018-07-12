
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class PropertyState<Type> : PropertyState
  {

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public PropertyState(NodeState parent) : base(parent) { }
    public PropertyState(NodeState parent, QualifiedName browseName, Type value = default(Type)) : base(parent, browseName)
    {
      Value = value;
    }
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

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public PropertyState(NodeState parent) : base(parent) { }
    public PropertyState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }

}