//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class ModelDesignUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      ModelDesign _newInstance = new ModelDesign();
      Assert.IsNull(_newInstance.AnyAttr);
    }
  }
}
