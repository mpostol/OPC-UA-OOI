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
  public partial class UAVariable : IUAVariable
  {
    public override NodeClassEnum NodeClass => NodeClassEnum.UAVariable;

    /// <summary>
    /// Indicates whether the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(IUANode other)
    {
      UAVariable _other = other as UAVariable;
      if (_other == null)
        return false;
      return
        base.ParentEquals(_other) &&
        //TODO compare Value, Translation
        this.DataType == _other.DataType &&
        this.ValueRank == _other.ValueRank &&
        this.ArrayDimensions == _other.ArrayDimensions &&
        this.AccessLevel == _other.AccessLevel &&
        this.UserAccessLevel == _other.UserAccessLevel &&
        this.MinimumSamplingInterval == _other.MinimumSamplingInterval &&
        this.Historizing == _other.Historizing;
    }

    public override void RemoveInheritedValues(IUANode baseNode)
    {
      base.RemoveInheritedValues(baseNode);
      UAVariable _other = baseNode as UAVariable;
      if (baseNode is null)
        throw new System.ArgumentNullException($"{nameof(baseNode)}", $"The parameter of the {nameof(RemoveInheritedValues)} must not be null");
      if (this.DataType == _other.DataType)
        this.DataType = null;
      if (this.ArrayDimensions == _other.ArrayDimensions)
        this.ArrayDimensions = string.Empty;
    }

    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      DataTypeNodeId = modelContext.ImportNodeId(DataType, trace);
      base.RecalculateNodeIds(modelContext, trace);
    }

    #region IUAVariable

    NodeId IUAVariable.DataType
    {
      get { return DataTypeNodeId; }
    }

    private NodeId DataTypeNodeId = null;

    uint? IUAVariable.AccessLevel
    {
      get { return this.AccessLevel; }
      set { this.AccessLevel = value.HasValue ? value.Value : 1; }
    }

    //bool? IUAVariable.Historizing
    //{
    //  get { return this.Historizing; }
    //  set { this.Historizing = value.HasValue ? value.Value : false; }
    //}

    byte? IUAVariable.UserAccessLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    bool IUAVariable.Translation { get => this.Translation != null; }

    #endregion IUAVariable

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UAVariable _ret = new UAVariable()
      {
        Value = this.Value,
        Translation = this.Translation,
        DataType = this.DataType,
        ValueRank = this.ValueRank,
        ArrayDimensions = this.ArrayDimensions,
        AccessLevel = this.AccessLevel,
        UserAccessLevel = this.UserAccessLevel,
        MinimumSamplingInterval = this.MinimumSamplingInterval,
        Historizing = this.Historizing,
      };
      return _ret;
    }

  }
}