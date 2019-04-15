//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UAOOI.SemanticData.AddressSpacePrototyping
{

  [TestClass]
  [DeploymentItem(@"XMLModels\", @"XMLModels\")]
  public class ProgramUnitTest
  {
    [TestMethod]
    [TestCategory("Deployment")]
    public void DeploymentItemTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void RunTheApplicationTestMethod()
    {
      Program.Run(new string[] { @"XMLModels\DataTypeTest.NodeSet2.xml" });
    }
  }

}
