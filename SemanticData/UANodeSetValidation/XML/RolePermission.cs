//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class RolePermission.
  /// </summary>
  public partial class RolePermission : IRolePermission
  {
    internal void RecalculateNodeIds(Func<string, NodeId> importNodeId)
    {
      ValueNodeId = importNodeId(Value);
    }

    internal NodeId ValueNodeId { get; private set; }
  }
}