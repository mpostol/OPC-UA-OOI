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
      DataTypeNodeId = importNodeId(DataType);
    }

    public NodeId DataTypeNodeId { get; private set; }

    #region IDataTypeField

    DataSerialization.LocalizedText[] IDataTypeField.DisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    DataSerialization.LocalizedText[] IDataTypeField.Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    #endregion IDataTypeField
  }
}