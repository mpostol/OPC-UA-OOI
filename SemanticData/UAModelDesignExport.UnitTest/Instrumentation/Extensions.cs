//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport.Instrumentation
{
  internal static class Extensions
  {
    internal static string Key(this Reference value)
    {
      return value.ReferenceType.ToString() + " " + value.TargetId.ToString();
    }
    internal static bool AreEqual(this string first, string second)
    {
      if (String.IsNullOrEmpty(first))
        return String.IsNullOrEmpty(second);
      return String.Compare(first, second) == 0;
    }
  }
}
