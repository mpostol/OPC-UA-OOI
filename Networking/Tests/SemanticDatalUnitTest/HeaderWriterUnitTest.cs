
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.SemanticData.UnitTest
{
  [TestClass]
  public class HeaderWriterUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_HeaderWriterUnitTest")]
    public void CreatorTestMethod()
    {
      HeaderWriterTest _htw = new HeaderWriterTest(x => { });
      HeaderWriter _hw = new HeaderWriter(_htw, 16);
      Assert.AreEqual(16, _htw.Position);
    }
    [TestMethod]
    [TestCategory("DataManagement_HeaderWriterUnitTest")]
    public void EndPositionTestMethod()
    {
      ushort _length = 16;
      HeaderWriterTest _htw = new HeaderWriterTest(x => { });
      HeaderWriter _hw = new HeaderWriter(_htw, _length);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      Assert.AreEqual<long>(_length + 4, _htw.Position);
    }
    [TestMethod]
    [TestCategory("DataManagement_HeaderWriterUnitTest")]
    public void WriteTestMethod()
    {
      ushort _length = 16;
      HeaderWriterTest _htw = new HeaderWriterTest(x => { });
      HeaderWriter _hw = new HeaderWriter(_htw, _length);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _hw.WriteHeader((x, y) => { });
      Assert.AreEqual<long>(_length + 4, _htw.Position);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      _htw.Write((byte)0x1);
      Assert.AreEqual<long>(_length + 8, _htw.Position);
      _hw.WriteHeader((x, y) => { });
      Assert.AreEqual<long>(_length + 8, _htw.Position);
    }
  }
}
