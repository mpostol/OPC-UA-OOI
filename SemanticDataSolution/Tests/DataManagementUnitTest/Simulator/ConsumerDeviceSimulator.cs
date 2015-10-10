
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator : IBindingFactory
  {

    #region IBindingFactory
    public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
    {
      throw new NotImplementedException();
    }
    public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
    {
      throw new NotImplementedException();
    }
    #endregion

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
