//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public abstract partial class UAInstance
  {
    /// <summary>
    /// Indicates whether the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(IUANode other)
    {
      UAInstance _other = other as UAInstance;
      if (Object.ReferenceEquals(_other, null))
        return false;
      return true;
    }

    /// <summary>
    /// Clones current object to a new one./>.
    /// </summary>
    /// <param name="ret">The ret.</param>
    protected void CloneUAInstance(UAInstance ret)
    {
      ret.ParentNodeIdNodeId = this.ParentNodeIdNodeId;
      ret.ParentNodeId = this.ParentNodeId;
      base.CloneUANode(this);
    }

    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      ParentNodeIdNodeId = modelContext.ImportNodeId(ParentNodeId, trace);
      base.RecalculateNodeIds(modelContext, trace);
    }
    /// <summary>
    /// The NodeId of the Node that is the parent of the Node within the information model. This field is used to indicate that a tight coupling exists between 
    /// the Node and its parent (e.g. when the parent is deleted the child is deleted as well). 
    /// </summary>
    /// <remarks>
    /// This information does not appear in the AddressSpace and is intended for use by design tools.
    /// </remarks>
    internal NodeId ParentNodeIdNodeId { get; private set; }
  }
}