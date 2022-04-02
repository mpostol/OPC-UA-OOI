//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UANode.
  /// Implements the <see cref="IEquatable{UANode}"/>
  /// </summary>
  /// <seealso cref="IEquatable{UANode}" />
  public abstract partial class UANode : IEquatable<UANode>
  {
    #region IEquatable

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
      if (other.GetType() != this.GetType())
        return false;
      return
        ParentEquals(other) &&
        this.AccessRestrictions == other.AccessRestrictions &&
        this.BrowseNameQualifiedName.Equals(other.BrowseNameQualifiedName) &&
        this.Description.LocalizedTextArraysEqual(other.Description) &&
        this.DisplayName.LocalizedTextArraysEqual(other.DisplayName) &&
        this.Documentation.AreEqual(other.Documentation) &&
        this.ReleaseStatus == ReleaseStatus &&
        this.RolePermissions.RolePermissionsEquals(other.RolePermissions) &&
        this.SymbolicName.AreEqual(other.SymbolicName) &&
        this.UserWriteMask == other.UserWriteMask &&
        this.WriteMask == other.WriteMask &&
        this.References.ReferencesEquals(other.References);
    }

    #endregion IEquatable

    #region API

    internal QualifiedName BrowseNameQualifiedName { get; private set; }
    internal NodeId NodeIdNodeId { get; private set; }

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>UANode.</returns>
    public virtual UANode Clone()
    {
      return ParentClone();
    }

    internal virtual void RemoveInheritedValues(UANode baseNode)
    {
      //BrowseName
      if (this.DisplayName.LocalizedTextArraysEqual(baseNode.DisplayName))
        this.DisplayName = null;
      if (this.Description.LocalizedTextArraysEqual(baseNode.Description))
        this.Description = null;
      //Category
      //References
      if (this.RolePermissions.RolePermissionsEquals(baseNode.RolePermissions))
        this.RolePermissions = null;
      if (this.Documentation == baseNode.Documentation)
        this.Documentation = string.Empty;
      //NodeId is not inherited
      //WriteMask
      //UserWriteMask
      //AccessRestrictions
      //SymbolicName is not inherited
      //ReleaseStatus
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

    internal virtual void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      (BrowseNameQualifiedName, NodeIdNodeId) = modelContext.ImportBrowseName(BrowseName, this.NodeId, trace);
      if (!(this.References is null))
        foreach (Reference _reference in this.References)
          _reference.RecalculateNodeIds(x => modelContext.ImportNodeId(x, trace));
      ImportNodeId(this.RolePermissions, x => modelContext.ImportNodeId(x, trace));
    }

    #endregion API

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

    #endregion override Object

    #region private

    private void ImportNodeId(RolePermission[] rolePermissions, Func<string, NodeId> importNodeId)
    {
      if (this.RolePermissions is null)
        return;
      foreach (RolePermission _permission in rolePermissions)
        _permission.RecalculateNodeIds(importNodeId);
    }

    /// <summary>
    /// Clones the specified node.
    /// </summary>
    /// <param name="node2Clone">The node to clone.</param>
    protected void CloneUANode(UANode node2Clone)
    {
      node2Clone.AccessRestrictions = this.AccessRestrictions;
      node2Clone.BrowseName = this.BrowseName;
      node2Clone.Category = this.Category;
      node2Clone.Description = this.Description;
      node2Clone.DisplayName = this.DisplayName;
      node2Clone.Documentation = this.Documentation;
      node2Clone.Extensions = this.extensionsField;
      node2Clone.NodeId = this.NodeId;
      node2Clone.References = this.References;
      node2Clone.ReleaseStatus = this.ReleaseStatus;
      node2Clone.RolePermissions = this.RolePermissions;
      node2Clone.SymbolicName = this.SymbolicName;
      node2Clone.UserWriteMask = this.UserWriteMask;
      node2Clone.WriteMask = this.WriteMask;
    }

    /// <summary>
    /// Indicates whether the the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected abstract bool ParentEquals(UANode other);

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="UANode"/>.</returns>
    protected abstract UANode ParentClone();

    #endregion private
  }
}