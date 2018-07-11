
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  /// <summary>
  /// Class BaseVariableState - represents BaseVariableTypes in the UA AddressSpace
  /// </summary>
  /// <seealso cref="UAOOI.Networking.Simulator.Boiler.AddressSpace.BaseInstanceState" />
  public abstract class BaseVariableState : BaseInstanceState
  {

    public BaseVariableState(NodeState parent, QualifiedName browseName) : base(parent, NodeClass.Variable_2, browseName) { }

    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    protected BaseVariableState(NodeState parent) : base(parent) { }

    public object Value
    {
      get
      {
        return m_value;
      }

      set
      {
        if (!Object.ReferenceEquals(m_value, value))
          ChangeMasks |= NodeStateChangeMasks.Value;
        m_value = value;
      }
    }

    private object m_value;

  }
}
