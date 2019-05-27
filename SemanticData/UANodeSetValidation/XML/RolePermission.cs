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
  /// Class RolePermission.
  /// </summary>
  public partial class RolePermission
  {

    internal void RecalculateNodeIds(Func<string, string> importNodeId)
    {
      Value = importNodeId(Value);
    }

  }
}
