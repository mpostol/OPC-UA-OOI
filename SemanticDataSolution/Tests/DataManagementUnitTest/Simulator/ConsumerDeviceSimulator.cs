
using System;
using UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{
  /// <summary>
  /// Class ConsumerDeviceSimulator - simulates a device that consumes data provided using the integration services.
  /// It could be for example HMI or PLC.
  /// </summary>
  internal class ConsumerDeviceSimulator : DataManagementSetup
  {

    internal static DataManagementSetup CreateDevice(MessageHandling.IMessageHandlerFactory communicationFactory)
    {
      DataManagementSetup _ret = new ConsumerDeviceSimulator();
      _ret.ConfigurationFactory = new ConfigurationFactory();
      _ret.BindingFactory = new MVVMSimulator();
      _ret.EncodingFactory = new EncodingFactory();
      _ret.MessageHandlerFactory = communicationFactory;
      return _ret;
    }
    /// <summary>
    /// Class ConfigurationFactory.
    /// </summary>
    private class ConfigurationFactory : IConfigurationFactory
    {
      /// <summary>
      /// Gets the configuration.
      /// </summary>
      /// <returns>Am object of <see cref="ConfigurationData" /> type capturing the communication configuration.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public Configuration.ConfigurationData GetConfiguration()
      {
        throw new NotImplementedException();
      }
      public event EventHandler<EventArgs> OnAssociationConfigurationChange;
      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

    }
    private class MVVMSimulator: IBindingFactory
    {
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
    }
    private class EncodingFactory: Encoding.IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        throw new NotImplementedException();
      }
    }
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
