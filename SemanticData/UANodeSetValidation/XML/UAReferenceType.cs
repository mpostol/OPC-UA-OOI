//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAReferenceType : IUAReferenceType
  {
    DataSerialization.LocalizedText[] IUAReferenceType.InverseName
    {
      get
      {
        if (this.InverseName == null || this.InverseName.Length == 0)
          return null;
        List<DataSerialization.LocalizedText> ret = new List<DataSerialization.LocalizedText>();
        foreach (XML.LocalizedText item in this.InverseName)
          ret.Add(new DataSerialization.LocalizedText() { Locale = item.Locale, Text = item.Value });
        return ret.ToArray();
      }
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