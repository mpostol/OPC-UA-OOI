
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ConsumerDeviceSimulatorUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void ReadConfigurationTestMethod()
    {
      ConsumerDeviceSimulator _consumer = new ConsumerDeviceSimulator();
      UDPSimulator _transport = _consumer.ReadConfiguration();
      byte[] _buffer = new byte[] { 0x1, 0x5, 0x12 };
      bool _messageOK = false;
      _transport.Write(_buffer, x => _messageOK = _buffer.AsEnumerable<byte>().SequenceEqual<byte>(x));
      Assert.IsTrue(_messageOK);
    }
  }
}
