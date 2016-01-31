
using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;

namespace UAOOI.Configuration.DataBindings
{
  /// <summary>
  /// Class NodeDescriptorBase - provides description of the node to be configured.
  /// </summary>
  [Serializable]
  public class NodeDescriptorBase : Networking.Serialization.NodeDescriptor, INodeDescriptor, IComparable<INodeDescriptor>, IEqualityComparer<NodeDescriptorBase>
  {

    #region INodeDescriptor
    /// <summary>
    /// Gets the node class.
    /// </summary>
    /// <value>The node class.</value>
    //TODO must be refactored 
    public new CAS.UA.IServerConfiguration.InstanceNodeClassesEnum NodeClass
    {
      get
      {
        InstanceNodeClassesEnum _ret = InstanceNodeClassesEnum.NotDefined;
        switch (base.NodeClass)
        {
          case Configuration.Networking.Serialization.InstanceNodeClassesEnum.Object:
            _ret = InstanceNodeClassesEnum.Object;
            break;
          case Configuration.Networking.Serialization.InstanceNodeClassesEnum.Variable:
            _ret = InstanceNodeClassesEnum.Variable;
            break;
          case Configuration.Networking.Serialization.InstanceNodeClassesEnum.Method:
            _ret = InstanceNodeClassesEnum.Method;
            break;
          case Configuration.Networking.Serialization.InstanceNodeClassesEnum.View:
            _ret = InstanceNodeClassesEnum.View;
            break;
          case Configuration.Networking.Serialization.InstanceNodeClassesEnum.NotDefined:
            _ret = InstanceNodeClassesEnum.NotDefined;
            break;
        }
        return _ret;
      }
      set
      {
        Configuration.Networking.Serialization.InstanceNodeClassesEnum _ret = Configuration.Networking.Serialization.InstanceNodeClassesEnum.NotDefined;
        switch (value)
        {
          case InstanceNodeClassesEnum.Object:
            _ret = Configuration.Networking.Serialization.InstanceNodeClassesEnum.Object;
            break;
          case InstanceNodeClassesEnum.Variable:
            _ret = Configuration.Networking.Serialization.InstanceNodeClassesEnum.Variable;
            break;
          case InstanceNodeClassesEnum.Method:
            _ret = Configuration.Networking.Serialization.InstanceNodeClassesEnum.Method;
            break;
          case InstanceNodeClassesEnum.View:
            _ret = Configuration.Networking.Serialization.InstanceNodeClassesEnum.View;
            break;
          case InstanceNodeClassesEnum.NotDefined:
            _ret = Configuration.Networking.Serialization.InstanceNodeClassesEnum.NotDefined;
            break;
        }
        base.NodeClass = _ret;
      }
    }
    #endregion

    internal static NodeDescriptorBase Clone(INodeDescriptor descriptor)
    {
      NodeDescriptorBase _ret = new NodeDescriptorBase()
      {
        BindingDescription = descriptor.BindingDescription,
        DataType = descriptor.DataType,
        InstanceDeclaration = descriptor.InstanceDeclaration,
        NodeClass = descriptor.NodeClass,
        NodeIdentifier = descriptor.NodeIdentifier
      };
      return _ret;
    }

    #region operators
    /// <summary>
    /// Implements the == operator.
    /// </summary>
    /// <param name="x">The first object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <returns><c>true</c> if the specified objects are equal; otherwise, false.</returns>
    public static bool operator ==(NodeDescriptorBase x, NodeDescriptorBase y)
    {
      if (Object.Equals(x, null) && Object.Equals(y, null))
        return true;
      if (Object.Equals(x, null) || Object.Equals(y, null))
        return false;
      return x.CompareTo(y) == 0;
    }
    /// <summary>
    /// Implements the !=.
    /// </summary>
    /// <param name="x">The first object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <returns><c>true</c> if the specified objects are not equal; otherwise, false.</returns>
    public static bool operator !=(NodeDescriptorBase x, NodeDescriptorBase y)
    {
      if (x.Equals(null) && y.Equals(null))
        return false;
      if (Object.Equals(x, null) || Object.Equals(y, null))
        return true;
      return x.CompareTo(y) != 0;
    }
    #endregion

    #region IComparable
    /// <summary>
    /// Compares the current instance with another <see cref="INodeDescriptor"/> and returns an integer that indicates whether the current instance precedes, 
    /// follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">An instance of <see cref="INodeDescriptor"/> to compare with this instance.</param>
    /// <returns>
    /// A <see cref="int"/> signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
    /// Value, Meaning
    /// Less than zero:  This instance is less than <paramref name="other"/>.
    /// Zero: This instance is equal to <paramref name="other"/>.
    /// Greater than zero: This instance is greater than <paramref name="other"/>.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// 	<paramref name="other"/> is not the same type as this instance.
    /// </exception>
    public int CompareTo(INodeDescriptor other)
    {
      if (other == null)
        throw new ArgumentNullException(nameof(other), "Parameter cannot be null");
      if (this.NodeIdentifier == null || other.NodeIdentifier == null)
        throw new ArgumentNullException("NodeIdentifier cannot be null.");
      if (this.NodeIdentifier.IsEmpty || other.NodeIdentifier.IsEmpty)
        throw new ArgumentNullException("NodeIdentifier cannot be empty.");
      if (String.IsNullOrEmpty(this.NodeIdentifier.Namespace) || String.IsNullOrEmpty(other.NodeIdentifier.Namespace))
        throw new ArgumentNullException("NodeIdentifier Namespace cannot be null.");
      int ret = NodeIdentifier.Namespace.CompareTo(other.NodeIdentifier.Namespace);
      if (ret != 0)
        return ret;
      if (String.IsNullOrEmpty(this.NodeIdentifier.Name) || String.IsNullOrEmpty(other.NodeIdentifier.Namespace))
        throw new ArgumentNullException("NodeIdentifier Name cannot be null.");
      return NodeIdentifier.Name.CompareTo(other.NodeIdentifier.Name);
    }
    #endregion

    #region IEqualityComparer
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <returns><c>true</c> if the specified objects are equal; otherwise, false.</returns>
    public bool Equals(NodeDescriptorBase x, NodeDescriptorBase y)
    {
      return x.CompareTo(y) == 0;
    }
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    /// <exception cref="System.ArgumentNullException">if <paramref name="obj"/> is null</exception>
    public int GetHashCode(NodeDescriptorBase obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(obj));
      return obj.GetHashCode();
    }
    #endregion

    #region object
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode()
    {
      int _hash = NodeIdentifier.ToString().GetHashCode();
      return _hash;
    }
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return NodeIdentifier.ToString();
    }
    /// <summary>
    /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      if (Object.Equals(obj, null))
        return false;
      NodeDescriptorBase _other = obj as NodeDescriptorBase;
      if (Object.Equals(_other, null))
        throw new ArgumentException(nameof(obj));
      return CompareTo(_other) == 0;
    }
    #endregion

  }
}
