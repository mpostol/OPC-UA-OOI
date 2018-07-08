
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class NodeState
  {

    /// <summary>
    /// Creates an empty object.
    /// </summary>
    /// <param name="nodeClass">The node class.</param>
    protected NodeState(NodeClass nodeClass)
    {
      m_nodeClass = nodeClass;
    }
    /// <summary>
    /// The browse name of the node.
    /// </summary>
    /// <value>The name qualified with a namespace.</value>
    public QualifiedName BrowseName
    {
      get
      {
        return m_browseName;
      }
      set
      {
        if (!Object.ReferenceEquals(m_browseName, value))
        {
          ChangeMasks |= NodeStateChangeMasks.NonValue;
        }

        m_browseName = value;
      }
    }
    public NodeStateChangeMasks ChangeMasks { get; protected set; }

    #region private
    private NodeClass m_nodeClass;
    private QualifiedName m_browseName;
    #endregion

  }
  /// <summary>
  /// Indicates what has changed in a node.
  /// </summary>
  public enum NodeStateChangeMasks
  {
    /// <summary>
    /// None has changed
    /// </summary>
    None = 0x00,

    /// <summary>
    /// One or more children have been added, removed or replaced.
    /// </summary>
    Children = 0x01,

    /// <summary>
    /// One or more references have been added or removed.
    /// </summary>
    References = 0x02,

    /// <summary>
    /// The value attribute has changed.
    /// </summary>
    Value = 0x04,

    /// <summary>
    /// One or more non-value attribute has changed.
    /// </summary>
    NonValue = 0x08,

    /// <summary>
    /// The node has been deleted.
    /// </summary>
    Deleted = 0x10
  }

}