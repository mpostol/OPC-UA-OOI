
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{

  public partial class DataItemState : BaseDataVariableState
  {
    public DataItemState(NodeState parent) : base(parent) { }
  }
  public class AnalogItemState : DataItemState
  {
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
  public class AnalogItemState<T> : AnalogItemState
  {

    #region Constructors
    public AnalogItemState(NodeState parent) : this(parent, "AnalogItemState", Model.ModelExtensions.CreateRange(1, 0)) { }
    /// <summary>
    /// Initializes the instance with its default attribute values.
    /// </summary>
    public AnalogItemState(NodeState parent, QualifiedName browseName, Range range, T value = default(T)) : base(parent)
    {
      this.BrowseName = browseName;
      this.EURange = new PropertyState<Range>(this);
      this.EURange.Value = range;
      this.EURange.BrowseName = nameof(EURange);
      this.Value = value;
    }
    #endregion

    #region Public Members
    /// <summary>
    /// The value of the variable.
    /// </summary>
    public new T Value
    {
      get { return (T)base.Value; }
      set { base.Value = value; }
    }
    #endregion

  }
  public class StateMachineState : BaseObjectState
  {
    public StateMachineState(NodeState parent) : base(parent) { }
  }
  public class FiniteStateMachineState : StateMachineState
  {
    public FiniteStateMachineState(NodeState parent) : base(parent) { }

  }
  public class ProgramStateMachineState : FiniteStateMachineState
  {

    public ProgramStateMachineState(NodeState parent) : base(parent) { }

  }

}
