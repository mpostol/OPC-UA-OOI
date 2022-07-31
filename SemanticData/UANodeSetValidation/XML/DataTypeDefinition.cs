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
  public partial class DataTypeDefinition : IDataTypeDefinition
  {
    IDataTypeField[] IDataTypeDefinition.Field
    {
      get => this.Field;
    }

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