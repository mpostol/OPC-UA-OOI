//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UANode.
  /// Implements the <see cref="IEquatable{UANode}"/>
  /// </summary>
  /// <seealso cref="IEquatable{UANode}" />
  public abstract partial class UANode : IEquatable<UANode>
  {
    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
    public virtual bool Equals(UANode other)
    {
      if (object.ReferenceEquals(other, null))
        return false;
      if (Object.ReferenceEquals(this, other))
        return true;
      return
        ParentEquals(other) &&
        this.AccessRestrictions == other.AccessRestrictions &&
        this.BrowseName == other.BrowseName &&
        this.Description.LocalizedTextArraysEqual(other.Description) &&
        this.DisplayName.LocalizedTextArraysEqual(other.DisplayName) &&
        this.Documentation == other.Documentation &&
        //this.NodeId == other.NodeId && it is not Information Model
        this.ReleaseStatus == ReleaseStatus &&
        this.RolePermissions.RolePermissionsEquals(other.RolePermissions) &&
        this.SymbolicName == other.SymbolicName &&
        this.UserWriteMask == other.UserWriteMask &&
        this.WriteMask == other.WriteMask &&
        this.References.ReferencesEquals(other.References);
    }
    internal virtual void RemoveInheritedValues(UANode baseNode)
    {
      if (this.BrowseName == baseNode.BrowseName)
        this.BrowseName = String.Empty;
      if (this.Description.LocalizedTextArraysEqual(baseNode.Description))
        this.Description = null;
      if (this.RolePermissions.RolePermissionsEquals(baseNode.RolePermissions))
        this.RolePermissions = null;
      if (this.Documentation == baseNode.Documentation)
        this.Documentation = String.Empty;
      if (this.Description.LocalizedTextArraysEqual(baseNode.Description))
        this.Description = null;
    }
    /// <summary>
    /// Implements the == operator. Determines whether two instances of <see cref="UANode"/> represent the same information.
    /// </summary>
    /// <param name="value1">The first object to compare, or null.</param>
    /// <param name="value2">The second object to compare, or null.</param>
    /// <returns>The result of the operator. The <see cref="UANode.Equals(UANode)"/> procedure is used to compare.</returns>
    public static bool operator ==(UANode value1, UANode value2)
    {
      if (Object.ReferenceEquals(value1, null))
        return Object.ReferenceEquals(value2, null);
      return value1.Equals(value2);
    }
    /// <summary>
    /// Implements the != operator. Determines whether two instances of <see cref="UANode"/> don't represent the same information.
    /// </summary>
    /// <param name="value1">The first object to compare, or null.</param>
    /// <param name="value2">The second object to compare, or null.</param>
    /// <returns>The result of the operator. The <see cref="UANode.Equals(UANode)"/> procedure is used to compare.</returns>
    public static bool operator !=(UANode value1, UANode value2)
    {
      if (Object.ReferenceEquals(value1, null))
        return !Object.ReferenceEquals(value2, null);
      return !value1.Equals(value2);
    }
    /// <summary>
    /// Gets the node class enum based on the type of this instance.
    /// </summary>
    /// <value>The node class enum.</value>
    internal NodeClassEnum NodeClassEnum
    {
      get
      {
        if (this.GetType() == typeof(UAReferenceType))
          return NodeClassEnum.UAReferenceType;
        if (this.GetType() == typeof(UADataType))
          return NodeClassEnum.UADataType;
        if (this.GetType() == typeof(UAVariableType))
          return NodeClassEnum.UAVariableType;
        if (this.GetType() == typeof(UAObjectType))
          return NodeClassEnum.UAObjectType;
        if (this.GetType() == typeof(UAView))
          return NodeClassEnum.UAView;
        if (this.GetType() == typeof(UAMethod))
          return NodeClassEnum.UAMethod;
        if (this.GetType() == typeof(UAVariable))
          return NodeClassEnum.UAVariable;
        if (this.GetType() == typeof(UAObject))
          return NodeClassEnum.UAObject;
        return NodeClassEnum.Unknown;
      }
    }
    #region override Object
    /// <summary>
    /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    /// <exception cref="NotImplementedException">Object.Equals must not be used and is intentionally not implemented</exception>
    public override bool Equals(object obj)
    {
      throw new NotImplementedException("Object.Equals must not be used and is intentionally not implemented");
    }
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    /// <exception cref="NotImplementedException">Object.GetHashCode must not be used and is intentionally not implemented</exception>
    public override int GetHashCode()
    {
      throw new NotImplementedException("Object.GetHashCode must not be used and is intentionally not implemented");
    }
    #endregion

    /// <summary>
    /// Indicates whether the the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected abstract bool ParentEquals(UANode other);

  }
}
