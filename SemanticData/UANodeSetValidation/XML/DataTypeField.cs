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
  /// <summary>
  /// Class DataTypeField
  /// </summary>
  public partial class DataTypeField
  {
    internal void RecalculateNodeIds(Func<string, NodeId> importNodeId)
    {
      DataTypeNodeId = importNodeId(DataType);
    }

    internal NodeId DataTypeNodeId { get; private set; }
  }
}