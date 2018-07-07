
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Adds a child to the node. 
    /// </summary>
    public void AddChild(BaseInstanceState child)
    {
      throw new NotImplementedException(nameof(AddChild));
      //if (!Object.ReferenceEquals(child.Parent, this))
      //{
      //  child.Parent = this;

      //  if (NodeId.IsNull(child.ReferenceTypeId))
      //  {
      //    child.ReferenceTypeId = ReferenceTypeIds.HasComponent;
      //  }
      //}

      //if (m_children == null)
      //{
      //  m_children = new List<BaseInstanceState>();
      //}

      //m_children.Add(child);
      //ChangeMasks |= NodeStateChangeMasks.Children;
    }
    /// <summary>
    /// Finds the child with the specified browse path.
    /// </summary>
    /// <param name="context">The context to use.</param>
    /// <param name="browsePath">The browse path.</param>
    /// <param name="index">The current position in the browse path.</param>
    /// <returns>The target if found. Null otherwise.</returns>
    public virtual BaseInstanceState FindChild(ISystemContext context, IList<QualifiedName> browsePath, int index)
    {
      if (index < 0 || index >= Int32.MaxValue)
        throw new ArgumentOutOfRangeException("index");

      BaseInstanceState instance = FindChild(context, browsePath[index], false, null);

      if (instance != null)
      {
        if (browsePath.Count == index + 1)
        {
          return instance;
        }

        return instance.FindChild(context, browsePath, index + 1);
      }

      return null;
    }
    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="browseName">The browse name of the children to add.</param>
    /// <param name="createOrReplace">if set to <c>true</c> and the child could exist then the child is created.</param>
    /// <param name="replacement">The replacement to use if createOrReplace is true.</param>
    /// <returns>The child.</returns>
    protected virtual BaseInstanceState FindChild(ISystemContext context, QualifiedName browseName, bool createOrReplace, BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      if (m_children != null)
      {
        for (int ii = 0; ii < m_children.Count; ii++)
        {
          BaseInstanceState child = m_children[ii];

          if (browseName == child.BrowseName)
          {
            if (createOrReplace && replacement != null)
            {
              m_children[ii] = child = replacement;
            }

            return child;
          }
        }
      }

      if (createOrReplace)
      {
        if (replacement != null)
        {
          AddChild(replacement);
        }
      }

      return null;
    }
    /// <summary>
    /// What has changed in the node since <see cref="ClearChangeMasks"/> was last called.
    /// </summary>
    /// <value>The change masks that indicates what has changed in a node.</value>
    public NodeStateChangeMasks ChangeMasks { get; protected set; }

    #region private
    private List<BaseInstanceState> m_children = new List<BaseInstanceState>();
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