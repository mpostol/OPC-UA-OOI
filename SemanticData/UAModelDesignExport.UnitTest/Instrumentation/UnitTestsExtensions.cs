//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport.Instrumentation
{

  internal static class UnitTestsExtensions
  {

    internal static void Compare(this LocalizedText expected, LocalizedText actual)
    {
      if (expected == null && actual == null)
        return;
      Assert.IsNotNull(expected);
      Assert.IsNotNull(actual);
      Assert.AreEqual<string>(expected.Key, actual.Key);
      Assert.AreEqual<string>(expected.Value, actual.Value);
    }

  }
}
