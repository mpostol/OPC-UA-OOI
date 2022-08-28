//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using OOIReleaseStatus = UAOOI.SemanticData.InformationModelFactory.ReleaseStatus;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UANode.
  /// Implements the <see cref="IEquatable{UANode}"/>
  /// </summary>
  /// <seealso cref="IEquatable{UANode}" />
  public abstract partial class UANode : IUANode
  {
    #region IEquatable

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
    public virtual bool Equals(IUANode other)
    {
      if (object.ReferenceEquals(other, null))
        return false;
      if (Object.ReferenceEquals(this, other))
        return true;
      if (other.GetType() != this.GetType())
        return false;
      IUANode thisNode = this;
      return
        ParentEquals(other) &&
        thisNode.AccessRestrictions == other.AccessRestrictions &&
        thisNode.BrowseName.Equals(other.BrowseName) &&
        thisNode.Description.LocalizedTextArraysEqual(other.Description) &&
        thisNode.DisplayName.LocalizedTextArraysEqual(other.DisplayName) &&
        thisNode.Documentation.AreEqual(other.Documentation) &&
        thisNode.ReleaseStatus == other.ReleaseStatus &&
        thisNode.RolePermissions.RolePermissionsEquals(other.RolePermissions) &&
        thisNode.SymbolicName.AreEqual(other.SymbolicName) &&
        thisNode.UserWriteMask == other.UserWriteMask &&
        thisNode.WriteMask == other.WriteMask &&
        thisNode.References.ReferencesEquals(other.References);
    }

    #endregion IEquatable

    //TODO Define independent Address Space API #645 LocalizedText conversion must be implemented.
    //public UANode()
    //{
    //  m_NodeIdNodeId = DataSerialization.NodeId.Parse(NodeId);
    //  m_BrowseName = BrowseName.ParseBrowseName(m_NodeIdNodeId, x => { });
    //}

    #region IUANode

    NodeId IUANode.NodeId { get => m_NodeIdNodeId; }

    /// <summary>
    /// Gets the node class of a Node.
    /// </summary>
    /// <value>The node class enum.</value>
    public abstract NodeClassEnum NodeClass { get; }

    QualifiedName IUANode.BrowseName { get => m_BrowseName; }

    DataSerialization.LocalizedText[] IUANode.DisplayName
    {
      get => DisplayName.GetLocalizedTextArray();
    }

    DataSerialization.LocalizedText[] IUANode.Description
    {
      get => Description.GetLocalizedTextArray();
    }

    AttributeWriteMask IUANode.WriteMask { get => this.WriteMask.GetAttributeWriteMask(); set => throw new NotImplementedException(); }
    AttributeWriteMask IUANode.UserWriteMask { get => this.UserWriteMask.GetAttributeWriteMask(); set => throw new NotImplementedException(); }
    IRolePermission[] IUANode.RolePermissions { get => this.RolePermissions; set => throw new NotImplementedException(); }
    IRolePermission[] IUANode.UserRolePermissions { get => null; set => throw new NotImplementedException(); }

    AccessRestrictions IUANode.AccessRestrictions
    {
      get => this.AccessRestrictions.GetAccessRestrictions(NodeClass, Trace);
      set => throw new NotImplementedException();
    }

    IReference[] IUANode.References { get => References; }

    public virtual void RemoveInheritedValues(IUANode baseNode)
    {
      //BrowseName
      if (((IUANode)this).DisplayName.LocalizedTextArraysEqual(baseNode.DisplayName))
        this.DisplayName = null;
      if (((IUANode)this).Description.LocalizedTextArraysEqual(baseNode.Description))
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

    OOIReleaseStatus IUANode.ReleaseStatus
    {
      get { return this.ReleaseStatus.GetReleaseStatus(); }
      set { throw new NotImplementedException(); }
    }

    #endregion IUANode

    #region API

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>UANode.</returns>
    public virtual UANode Clone()
    {
      return ParentClone();
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

    internal virtual void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      Trace = trace ?? throw new ArgumentNullException(nameof(trace));
      (m_BrowseName, m_NodeIdNodeId) = modelContext.ImportBrowseName(BrowseName, this.NodeId, trace);
      if (!(this.References is null))
        foreach (Reference _reference in this.References)
          _reference.RecalculateNodeIds(x => modelContext.ImportNodeId(x, trace));
      ImportNodeId(this.RolePermissions, x => modelContext.ImportNodeId(x, trace));
    }

    private Action<TraceMessage> Trace = null;

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

    private NodeId m_NodeIdNodeId = null;
    private QualifiedName m_BrowseName = null;

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
    /// Indicates whether the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected abstract bool ParentEquals(IUANode other);

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="UANode"/>.</returns>
    protected abstract UANode ParentClone();

    #endregion private

    #region debug

    [System.Diagnostics.Conditional("DEBUG")]
    internal virtual void Deserialize()
    {
      m_BrowseName = DataSerialization.QualifiedName.Parse(BrowseName);
      m_NodeIdNodeId = DataSerialization.NodeId.Parse(this.NodeId);
    }

    #endregion debug
  }
}