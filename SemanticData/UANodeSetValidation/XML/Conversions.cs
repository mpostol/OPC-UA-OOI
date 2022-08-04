//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;
using UAOOI.SemanticData.AddressSpace.Abstractions;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  internal static class Conversions
  {
    internal static IUANode GetIUANode(this UANode node)
    {
      return node;
    }

    internal static DataSerialization.LocalizedText[] GetLocalizedTextArray(this LocalizedText[] value)
    {
      if (value == null || value.Length == 0)
        return null;
      List<DataSerialization.LocalizedText> ret = new List<DataSerialization.LocalizedText>();
      foreach (LocalizedText item in value)
        ret.Add(new DataSerialization.LocalizedText() { Locale = item.Locale, Text = item.Value });
      return ret.ToArray();
    }
  }
}