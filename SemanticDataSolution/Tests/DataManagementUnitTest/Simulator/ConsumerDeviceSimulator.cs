using System;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator: IBindingFactory
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
    public IBinding GetDataBroker(string variableName)
    {
      throw new NotImplementedException();
    }
    private UDPSimulator m_UDPSimulator = null;
  }
}
