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
  public partial class UAVariableType
  {
    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="T:UAOOI.SemanticData.UANodeSetValidation.XML.UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UAVariableType _ret = new UAVariableType()
      {
        Value = this.Value,
        DataType = this.DataType,
        ValueRank = this.ValueRank,
        ArrayDimensions = this.ArrayDimensions
      };
      base.CloneUAType(_ret);
      return _ret;
    }

    /// <summary>
    /// Recalculates the node identifiers.
    /// </summary>
    /// <param name="modelContext">The model context.</param>
    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      base.RecalculateNodeIds(modelContext, trace);
      DataTypeNodeId = modelContext.ImportNodeId(DataType, trace);
    }

    internal NodeId DataTypeNodeId { get; private set; }
  }
}