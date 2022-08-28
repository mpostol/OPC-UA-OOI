//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAVariableType : IUAVariableType
  {
    /// <summary>
    /// The NodeClass identifies the NodeClass of a Node.
    /// </summary>
    public override NodeClassEnum NodeClass => NodeClassEnum.UAVariableType;

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
    /// <param name="trace">The trace.</param>
    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      base.RecalculateNodeIds(modelContext, trace);
      m_DataTypeNodeId = modelContext.ImportNodeId(this.DataType, trace);
    }

    #region IUAVariableType

    NodeId IUAVariableType.DataType => m_DataTypeNodeId;

    #endregion IUAVariableType

    private NodeId m_DataTypeNodeId;
  }
}