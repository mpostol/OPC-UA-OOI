
using System;
using System.Net;
using System.Net.Sockets;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.MessageHandling;
using System.Linq;
using System.Diagnostics;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.ReferenceApplication.Producer
{
  internal class ProducerMessageHandlerFactory : IMessageHandlerFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerMessageHandlerFactory"/> class.
    /// </summary>
    /// <param name="toDispose">To dispose.</param>
    /// <param name="trace">The trace.</param>
    /// <param name="ViewModel">The <see cref="IProducerViewModel"/> instance implementing ViewModel layer in the MVVM programming pattern.</param>
    public ProducerMessageHandlerFactory(Action<IDisposable> toDispose, Action<string> trace, IProducerViewModel ViewModel)
    {
      m_ToDispose = toDispose;
      m_Trace = trace;
      m_ViewModel = ViewModel;
    }

    #region IMessageHandlerFactory
    /// <summary>
    /// Gets the new instance of <see cref="IMessageReader"/>.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration.</param>
    /// <remarks>It is intentionally not implemented</remarks>
    /// <returns>An instance of <see cref="IMessageReader"/>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    IMessageReader IMessageHandlerFactory.GetIMessageReader(string name, MessageChannelConfiguration configuration, IUADecoder uaDecoder)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Gets the i message writer.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>An instance of <see cref="IMessageWriter"/>.</returns>
    IMessageWriter IMessageHandlerFactory.GetIMessageWriter(string name, MessageChannelConfiguration configuration, IUAEncoder uaEncoder)
    {
      BinaryUDPPackageWriter _ret = new BinaryUDPPackageWriter(RemoteHostName, UDPPortNumber, m_Trace, m_ViewModel, uaEncoder);
      m_ToDispose(_ret);
      return _ret;
    }
    #endregion

    #region API
    /// <summary>
    /// Gets the remote station UDP port number.
    /// </summary>
    /// <value>The UDP port number.</value>
    internal int UDPPortNumber
    {
      get { return Properties.Settings.Default.RemoteUDPPortNumber; }
    }
    /// <summary>
    /// Gets the URL of the remote host.
    /// </summary>
    /// <value>The name of the remote host.</value>
    internal string RemoteHostName
    {
      get { return Properties.Settings.Default.RemoteHostName; }
    }
    #endregion

    #region private
    private IProducerViewModel m_ViewModel { get; set; }
    private Action<IDisposable> m_ToDispose;
    private Action<string> m_Trace;
    #endregion

  }
}
