//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAReferenceType : IUAReferenceType
  {
    public override NodeClassEnum NodeClass => NodeClassEnum.UAReferenceType;

    DataSerialization.LocalizedText[] IUAReferenceType.InverseName
    {
      get { return this.InverseName.GetLocalizedTextArray(); }
    }

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="T:UAOOI.SemanticData.UANodeSetValidation.XML.UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UAReferenceType _ret = new UAReferenceType()
      {
        InverseName = this.InverseName,
        Symmetric = this.Symmetric
      };
      base.CloneUAType(_ret);
      return _ret;
    }

    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      base.RecalculateNodeIds(modelContext, trace);
      modelContext.RegisterUAReferenceType(BrowseName);
    }
  }
}