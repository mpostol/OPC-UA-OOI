//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.UAModelDesignExport.XML
{
  [TestClass]
  public class UAResourcesUnitTest
  {
    [TestMethod]
    public void LoadUADefinedTypesTestMethod()
    {
      ModelDesign newInstance = UAResources.LoadUADefinedTypes();
      Assert.IsNotNull(newInstance);
    }
  }
}
