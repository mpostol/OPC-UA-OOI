
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using System.Linq;

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
    protected override void CallRegister(List<BaseInstanceState> hasComponentPathAndMe, Action<BaseInstanceState, string[]> register)
    {
      base.CallRegister(hasComponentPathAndMe, register);
      string[] _browsePath = hasComponentPathAndMe.Select<BaseInstanceState, string>(x => x.BrowseName.Name).ToArray<string>();
      register(this, _browsePath);
    }

    private object m_value;

  }
}
