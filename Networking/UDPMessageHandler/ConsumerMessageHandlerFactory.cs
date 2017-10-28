
using System;
using System.Net;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.UDPMessageHandler
{
  /// <summary>
  /// Class ConsumerMessageHandlerFactory - implements <see cref="IMessageHandlerFactory"/> 
  /// </summary>
  public class ConsumerMessageHandlerFactory : IMessageHandlerFactory
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerMessageHandlerFactory" /> class.
    /// </summary>
    /// <param name="toDispose">To dispose captures functionality to create a collection of disposable objects.
    /// The objects are disposed when application exits.</param>
    /// <param name="viewModel">The ViewModel instance for this object.</param>
    /// <param name="trace">The delegate capturing logging functionality.</param>
    public ConsumerMessageHandlerFactory(Action<IDisposable> toDispose, Action<string> trace)
    {
      m_Trace = trace;
      m_ToDispose = toDispose;
    }
    #endregion
    class UDPReaderConfiguration
    {
      public UDPReaderConfiguration(MessageChannelConfiguration configuration)
      {
        throw new NotImplementedException();
      }
      public int UDPPortNumber { get; set; }
      public bool JoinMulticastGroup { get; set; }
      public string DefaultMulticastGroup { get; set; }
      public bool ReuseAddress { get; set; }

    }
    #region IMessageHandlerFactory
    /// <summary>
    /// Gets the new instance of <see cref="IMessageReader"/>.
    /// </summary>
    /// <param name="name">The name of the reader.</param>
    /// <param name="configuration">The configuration of the reader.</param>
    /// <returns>An instance of <see cref="IMessageReader"/>.</returns>
    IMessageReader IMessageHandlerFactory.GetIMessageReader(string name, MessageChannelConfiguration configuration, IUADecoder uaDecoder)
    {
      UDPReaderConfiguration _configuration = new UDPReaderConfiguration(configuration);
      BinaryUDPPackageReader _ret = new BinaryUDPPackageReader(uaDecoder, _configuration.UDPPortNumber, m_Trace);
      m_ToDispose(_ret);
      if (_configuration.JoinMulticastGroup)
        _ret.MulticastGroup = IPAddress.Parse(_configuration.DefaultMulticastGroup);
      _ret.ReuseAddress = _configuration.ReuseAddress;
      return _ret;
    }
    /// <summary>
    /// Gets the new instance of <see cref="IMessageWriter"/>.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration.</param>
    /// <remarks>It is intentionally not implemented</remarks>
    /// <returns>An instance of <see cref="IMessageWriter"/>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    IMessageWriter IMessageHandlerFactory.GetIMessageWriter(string name, MessageChannelConfiguration configuration, IUAEncoder uaEncoder)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region private
    private Action<IDisposable> m_ToDispose;
    private Action<string> m_Trace;
    #endregion

  }

}