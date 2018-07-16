//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  /// <summary>
  /// Class BaseVariableState - represents BaseVariableTypes in the UA AddressSpace
  /// </summary>
  /// <seealso cref="UAOOI.Networking.Simulator.Boiler.AddressSpace.BaseInstanceState" />
  public abstract class BaseVariableState : BaseInstanceState, IVariable
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseVariableState"/> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="browseName">Name of the browse.</param>
    public BaseVariableState(NodeState parent, QualifiedName browseName) : base(parent, NodeClass.Variable_2, browseName) { }
    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <value>The value.</value>
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

    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    /// <value>The type of the data returned by the Value property.</value>
    public UATypeInfo ValueType => GetValueType();


    [Obsolete("This constructor is provided only to make auto-generated code error free")]
    protected BaseVariableState(NodeState parent) : base(parent) { }
    protected override void CallRegister(List<BaseInstanceState> hasComponentPathAndMe, Action<BaseInstanceState, string[]> register)
    {
      base.CallRegister(hasComponentPathAndMe, register);
      string[] _browsePath = hasComponentPathAndMe.Select<BaseInstanceState, string>(x => x.BrowseName.Name).ToArray<string>();
      register(this, _browsePath);
    }
    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    /// <returns>Type.</returns>
    protected abstract UATypeInfo GetValueType();
    private object m_value;

  }
}
