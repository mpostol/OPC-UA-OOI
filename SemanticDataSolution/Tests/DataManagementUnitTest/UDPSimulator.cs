using System;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  internal class UDPSimulator
  {
    internal UDPSimulator(ConsumerDeviceSimulator consumer)
    {
      m_Consumer = consumer;
    }
    internal byte[] Receive()
    {
      return m_Buffer;
    }
    internal void Write(byte[] buffer, Action<byte[]> predicate)
    {
      m_Buffer = (byte[])buffer.Clone();
      m_Consumer.ThreadSimulator(predicate);
    }
    private ConsumerDeviceSimulator m_Consumer = null;
    private byte[] m_Buffer = null;
  }
}
