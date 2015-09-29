using System;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// </summary>
  internal class ConsumerDeviceSimulator
  {
    internal UDPSimulator ReadConfiguration()
    {
      m_UDPSimulator = new UDPSimulator(this);
      return m_UDPSimulator;
    }
    internal void ThreadSimulator(Action<byte[]> predicate)
    {
      byte[] _buffer = m_UDPSimulator.Receive();
      predicate(_buffer);
    }
    private UDPSimulator m_UDPSimulator = null;
  }
}
