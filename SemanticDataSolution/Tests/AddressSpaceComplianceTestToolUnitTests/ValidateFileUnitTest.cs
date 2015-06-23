
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.AddressSpaceTestTool.UnitTests
{
  [TestClass]
  public class ValidateFileUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void GetFileToReadTestMethod()
    {
      Program.GetFileToRead(null);
    }
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ValidateFileTestMethod()
    {
      Program.ValidateFile(null);
    }
  }
}
