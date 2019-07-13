//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UANodeSetChanges.
  /// </summary>
  public partial class UANodeSetChanges
  {

    internal void RecalculateNodeIds(IUAModelContext importNodeId)
    {
      if (this.Aliases != null)
        foreach (NodeIdAlias _alias in this.Aliases)
          _alias.RecalculateNodeIds(importNodeId.ImportNodeId);
      if (this.NodesToAdd != null )
        foreach (UANode _node in this.NodesToAdd)
          _node.RecalculateNodeIds(importNodeId);
      if (this.NodesToDelete != null)
        foreach (NodeToDelete _node in this.NodesToDelete)
          _node.RecalculateNodeIds(importNodeId.ImportNodeId);
      if (this.ReferencesToAdd != null)
        foreach (ReferenceChange _reference in this.ReferencesToAdd)
          _reference.RecalculateNodeIds(importNodeId.ImportNodeId);
      if (this.ReferencesToDelete != null)
        foreach (var _reference in this.ReferencesToDelete)
          _reference.RecalculateNodeIds(importNodeId.ImportNodeId);
    }

  }
}
