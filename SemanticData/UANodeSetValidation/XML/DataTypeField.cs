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
  /// Class DataTypeField
  /// </summary>
  public partial class DataTypeField : IDataTypeField
  {
    internal void RecalculateNodeIds(Func<string, NodeId> importNodeId)
    {
      DataTypeNodeIdBackingField = importNodeId(DataType);
    }

    NodeId IDataTypeField.DataTypeNodeId
    {
      get { return DataTypeNodeIdBackingField; }
    }

    private NodeId DataTypeNodeIdBackingField;

    #region IDataTypeField

    DataSerialization.LocalizedText[] IDataTypeField.DisplayName
    { get => this.DisplayName.GetLocalizedTextArray(); set => throw new NotImplementedException(); }

    DataSerialization.LocalizedText[] IDataTypeField.Description
    { get => this.Description.GetLocalizedTextArray(); set => throw new NotImplementedException(); }

    #endregion IDataTypeField
  }
}