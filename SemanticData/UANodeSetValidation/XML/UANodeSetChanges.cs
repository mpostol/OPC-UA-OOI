//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UANodeSetChanges.
  /// </summary>
  public partial class UANodeSetChanges
  {
    internal void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      Func<string, NodeId> importNodeId = x => modelContext.ImportNodeId(x, trace);
      if (this.Aliases != null)
        foreach (NodeIdAlias _alias in this.Aliases)
          _alias.RecalculateNodeIds(importNodeId);
      if (this.NodesToAdd != null)
        foreach (UANode _node in this.NodesToAdd)
          _node.RecalculateNodeIds(modelContext, trace);
      if (this.NodesToDelete != null)
        foreach (NodeToDelete _node in this.NodesToDelete)
          _node.RecalculateNodeIds(importNodeId);
      if (this.ReferencesToAdd != null)
        foreach (ReferenceChange _reference in this.ReferencesToAdd)
          _reference.RecalculateNodeIds(importNodeId);
      if (this.ReferencesToDelete != null)
        foreach (ReferenceChange _reference in this.ReferencesToDelete)
          _reference.RecalculateNodeIds(importNodeId);
    }
  }
}