
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  [TestClass]
  public class UABinaryWriterUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_UABinaryWriterUnitTest")]
    public void WriterCreatorTestMethod()
    {
      using (MemoryStream _stream = new MemoryStream())
      using (UABinaryWriter _writer = new UABinaryWriter(_stream))
        Assert.IsNotNull(_writer);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryWriterUnitTest")]
    public void ReaderCreatorTestMethod()
    {
      int _encodedLength = 16;
      byte[] _randomContent = new byte[_encodedLength];
      using (MemoryStream _stream = new MemoryStream(_randomContent))
      using (UABinaryReader _reader = new UABinaryReader(_stream))
      {
        Assert.IsNotNull(_reader);
        byte[] _buffer = new byte[_reader.BaseStream.Length];
        Assert.AreEqual<int>(_encodedLength, _buffer.Length);
        int leng = _reader.Read(_buffer, 0, _buffer.Length);
        Assert.AreEqual<long>(_encodedLength, _reader.BaseStream.Position);
        _reader.Close();
        CollectionAssert.AreEqual(_buffer, _randomContent);
      }
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryWriterUnitTest")]
    public void GuidTestMethod()
    {
      byte[] _Encodedguid = null;
      Guid _Guid;
      using (MemoryStream _stream = new MemoryStream())
      using (UABinaryWriter _writer = new UABinaryWriter(_stream))
      {
        Assert.IsNotNull(_writer);
        _Guid = Guid.NewGuid();
        _writer.Write(_Guid);
        _writer.Close();
        _Encodedguid = _stream.ToArray();
        Assert.IsNotNull(_Encodedguid);
        Assert.AreEqual<int>(16, _Encodedguid.Length);
      }
      using (MemoryStream _stream = new MemoryStream(_Encodedguid))
      using (UABinaryReader _reader = new UABinaryReader(_stream))
      {
        Assert.IsNotNull(_reader);
        Guid _decodedGuid = _reader.ReadGuid();
        Assert.AreEqual<Guid>(_Guid, _decodedGuid);
      }
    }

  }

}
