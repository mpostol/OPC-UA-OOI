//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UAOOI.SemanticData.AddressSpacePrototyping
{

  [TestClass]
  [DeploymentItem(@"XMLModels\", @"XMLModels\")]
  public class ValidateFileUnitTest
  {
    [TestMethod]
    [TestCategory("Deployment")]
    public void DeploymentItemTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestCategory("Deployment")]
    public void GetFileToReadArgumentOutOfRangeExceptionTestMethod1()
    {
      FileInfo _testDataFileInfo = Program.GetFileToRead(new string[] { });
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestCategory("Deployment")]
    public void GetFileToReadArgumentOutOfRangeExceptionTestMethod2()
    {
      FileInfo _testDataFileInfo = Program.GetFileToRead(null);
    }
    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    [TestCategory("Deployment")]
    public void GetFileToReadAFileNotFoundExceptionTestMethod()
    {
      FileInfo _testDataFileInfo = Program.GetFileToRead(new string[] { @"XMLModels\BleBle.xml" });
    }
    [TestMethod]
    [TestCategory("Code")]
    public void GetFileToReadTestMethod()
    {
      FileInfo _testDataFileInfo = Program.GetFileToRead(new string[] { @"XMLModels\DataTypeTest.NodeSet2.xml" });
      Assert.IsTrue(_testDataFileInfo.Exists);
    }
  }

}
