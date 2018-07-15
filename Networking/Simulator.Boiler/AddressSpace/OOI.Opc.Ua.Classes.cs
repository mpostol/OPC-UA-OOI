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

  public abstract partial class DataItemState : BaseDataVariableState
  {
    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public DataItemState(NodeState parent) : base(parent) { }

    public DataItemState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }
  public abstract class AnalogItemState : DataItemState
  {
    public AnalogItemState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public AnalogItemState(NodeState parent) : base(parent) { }

    /// <summary>
    /// Gets or sets the engineering unit range.
    /// </summary>
    /// <value>The eu range.</value>
    public PropertyState<Range> EURange
    {
      get
      {
        return m_eURange;
      }
      set
      {
        if (!Object.ReferenceEquals(m_eURange, value))
          ChangeMasks |= NodeStateChangeMasks.Children;
        m_eURange = value;
      }
    }
    private PropertyState<Range> m_eURange;
  }
  public class AnalogItemState<type> : AnalogItemState
  {

    #region Constructors
    public AnalogItemState(NodeState parent) : this(parent, "AnalogItemState", Model.ModelExtensions.CreateRange(1, 0)) { }
    /// <summary>
    /// Initializes the instance with its default attribute values.
    /// </summary>
    public AnalogItemState(NodeState parent, QualifiedName browseName, Range range, type value = default(type)) : base(parent, browseName)
    {
      this.EURange = new PropertyState<Range>(this, nameof(EURange));
      this.EURange.Value = range;
      this.Value = value;
    }
    #endregion

    #region Public Members
    /// <summary>
    /// The value of the variable.
    /// </summary>
    public new type Value
    {
      get { return (type)base.Value; }
      set { base.Value = value; }
    }
    #endregion
    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    /// <returns>Type.</returns>
    protected override Type GetValueType()
    {
      return typeof(type);
    }
  }
  public class StateMachineState : BaseObjectState
  {
    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public StateMachineState(NodeState parent) : base(parent) { }

    public StateMachineState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }
  public class FiniteStateMachineState : StateMachineState
  {
    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public FiniteStateMachineState(NodeState parent) : base(parent) { }

    public FiniteStateMachineState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }
  public class ProgramStateMachineState : FiniteStateMachineState
  {

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    public ProgramStateMachineState(NodeState parent) : base(parent) { }
    public ProgramStateMachineState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }

  }

}
