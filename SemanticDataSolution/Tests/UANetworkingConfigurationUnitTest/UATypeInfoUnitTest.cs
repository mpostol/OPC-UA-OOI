using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  [TestClass]
  public class UATypeInfoUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CreatorADNullTestMethod1()
    {
      new UATypeInfo(BuiltInType.Byte, 0, null);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CreatorADLengthTestMethod1()
    {
      new UATypeInfo(BuiltInType.Byte, 0, new int[] { });
    }
    [TestMethod]
    public void CreatorTestMethod1()
    {
      Assert.IsNotNull(new UATypeInfo(BuiltInType.Byte, 0, new int[] { 1, 2, 3 }));
    }
  }
}
