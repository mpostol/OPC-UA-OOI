
using System;
using System.Net;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.UDPMessageHandler
{
  /// <summary>
  /// Class ConsumerMessageHandlerFactory - implements <see cref="IMessageHandlerFactory"/> 
  /// </summary>
  public class MessageHandlerFactory : IMessageHandlerFactory
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageHandlerFactory" /> class.
    /// </summary>
    /// <param name="toDispose">To dispose captures functionality to create a collection of disposable objects.
    /// The objects are disposed when application exits.</param>
    /// <param name="viewModel">The ViewModel instance for this object.</param>
    /// <param name="trace">The delegate capturing logging functionality.</param>
    public MessageHandlerFactory(Action<IDisposable> toDispose, Action<string> trace)
    {
      m_Trace = trace;
      m_ToDispose = toDispose;
    }
    #endregion

    #region IMessageHandlerFactory
    /// <summary>
    /// Class UDPReaderConfiguration encapsulating configuration for <see cref="IMessageHandlerFactory.GetIMessageReader"/>.
    /// </summary>
    internal class UDPReaderConfiguration
    {
      internal static UDPReaderConfiguration Parse(string configuration)
      {
        string[] _parameters = configuration.Split(',');
        if (_parameters.Length != 4)
          throw new ArgumentException($"Wrong number of parameter {_parameters.Length} but expected 4");
        UDPReaderConfiguration _ret = new UDPReaderConfiguration();
        _ret.UDPPortNumber = int.Parse(_parameters[0]);
        _ret.JoinMulticastGroup = bool.Parse(_parameters[1]);
        if (_ret.JoinMulticastGroup)
          _ret.DefaultMulticastGroup = IPAddressValidationRule.ValidateIP(_parameters[2]);
        _ret.ReuseAddress = bool.Parse(_parameters[3]);
        return _ret;
      }
      internal int UDPPortNumber { get; set; }
      internal IPAddress DefaultMulticastGroup { get; set; } = null;
      internal bool ReuseAddress { get; set; }
      public override string ToString()
      {
        return $"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{ReuseAddress}";
      }

      private UDPReaderConfiguration() { }
      private bool JoinMulticastGroup;

    }
    /// <summary>
    /// Gets the message reader.
    /// </summary>
    /// <param name="name">The name of the reader.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="T:UAOOI.Networking.SemanticData.MessageHandling.IMessageReader" />.</param>
    /// <param name="uaDecoder">The decoder that provides methods to be used to decode OPC UA Built-in types.</param>
    /// <returns>An object implementing <see cref="T:UAOOI.Networking.SemanticData.MessageHandling.IMessageReader" /> that provides functionality supporting reading the messages from the wire.</returns>
    IMessageReader IMessageHandlerFactory.GetIMessageReader(string name, string configuration, IUADecoder uaDecoder)
    {
      UDPReaderConfiguration _configuration = UDPReaderConfiguration.Parse(configuration);
      BinaryUDPPackageReader _ret = new BinaryUDPPackageReader(uaDecoder, _configuration.UDPPortNumber, m_Trace);
      m_ToDispose(_ret);
      _ret.MulticastGroup = _configuration.DefaultMulticastGroup;
      _ret.ReuseAddress = _configuration.ReuseAddress;
      return _ret;
    }
    /// <summary>
    /// Class UDPWriterConfiguration encapsulating configuration for <see cref="IMessageHandlerFactory.GetIMessageWriter"/>.
    /// </summary>
    internal class UDPWriterConfiguration
    {
      internal static UDPWriterConfiguration Parse(string configuration)
      {
        string[] _parameters = configuration.Split(',');
        if (_parameters.Length != 2)
          throw new ArgumentException($"Wrong number of parameter {_parameters.Length} but expected 4");
        UDPWriterConfiguration _ret = new UDPWriterConfiguration();
        _ret.UDPPortNumber = int.Parse(_parameters[0]);
        _ret.RemoteHostName = _parameters[1];
        return _ret;
      }
      internal int UDPPortNumber { get; set; }
      internal string RemoteHostName { get; set; }
      public override string ToString()
      {
        return $"{UDPPortNumber},{RemoteHostName}";
      }

      private UDPWriterConfiguration() { }

    }
    /// <summary>
    /// Gets the new instance of <see cref="IMessageWriter"/>.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration.</param>
    /// <remarks>It is intentionally not implemented</remarks>
    /// <returns>An instance of <see cref="IMessageWriter"/>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    IMessageWriter IMessageHandlerFactory.GetIMessageWriter(string name, string configuration, IUAEncoder uaEncoder)
    {
      UDPWriterConfiguration _configuration = UDPWriterConfiguration.Parse(configuration);
      BinaryUDPPackageWriter _ret = new BinaryUDPPackageWriter(_configuration.RemoteHostName, _configuration.UDPPortNumber, m_Trace, uaEncoder);
      m_ToDispose(_ret);
      return _ret;
    }
    #endregion

    #region private
    private Action<IDisposable> m_ToDispose;
    private Action<string> m_Trace;
    #endregion

  }

}