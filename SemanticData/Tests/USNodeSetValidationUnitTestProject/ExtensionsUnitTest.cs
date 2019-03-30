//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class ExtensionsUnitTest
  {
    [TestMethod]
    public void ArrayDimensionsToStringTestMethod()
    {
      uint[] _testingData = new uint[] { 1, 2, 3, 4, 5 };
      string _result = _testingData.ArrayDimensionsToString();
      Assert.AreEqual<string>("1, 2, 3, 4, 5", _result);
    }
  }
}
