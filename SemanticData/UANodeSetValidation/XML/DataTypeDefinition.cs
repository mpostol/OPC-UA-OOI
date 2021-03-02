//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class DataTypeDefinition
  {
    internal void RecalculateNodeIds(Func<string, NodeId> importNodeId)
    {
      //BaseType - is obsolete and no longer used. Left in for backwards compatibility.
      // this.Name - name should be QualifiedName but it is not.
      if (Field is null)
        return;
      foreach (DataTypeField _field in Field)
        _field.RecalculateNodeIds(importNodeId);
    }
  }
}