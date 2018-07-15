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
  public class PropertyState<type> : PropertyState
  {

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public PropertyState(NodeState parent) : base(parent) { }
    public PropertyState(NodeState parent, QualifiedName browseName, type value = default(type)) : base(parent, browseName)
    {
      Value = value;
    }
    /// <summary>
    /// The value of the variable.
    /// </summary>
    public new type Value
    {
      get { return (type)base.Value; }
      set { base.Value = value; }
    }
    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    /// <returns>Type.</returns>
    protected override Type GetValueType()
    {
      return typeof(type);
    }
  }
  public abstract class PropertyState : BaseVariableState
  {

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public PropertyState(NodeState parent) : base(parent) { }
    public PropertyState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }

}