
using System;
using System.Net;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.ReferenceApplication.Consumer
{
  /// <summary>
  /// Class ConsumerMessageHandlerFactory - implements <see cref="IMessageHandlerFactory"/> 
  /// </summary>
  //TODO IMessageHandlerFactory - move implementation to separate library #218
  internal class ConsumerMessageHandlerFactory : IMessageHandlerFactory
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerMessageHandlerFactory" /> class.
    /// </summary>
    /// <param name="toDispose">To dispose captures functionality to create a collection of disposable objects.
    /// The objects are disposed when application exits.</param>
    /// <param name="viewModel">The ViewModel instance for this object.</param>
    /// <param name="trace">The delegate capturing logging functionality.</param>
    public ConsumerMessageHandlerFactory(Action<IDisposable> toDispose, IConsumerViewModel viewModel, Action<string> trace)
    {
      m_ParentViewModel = viewModel;
      m_Trace = trace;
      m_ToDispose = toDispose;
    }
    #endregion

    #region IMessageHandlerFactory
    /// <summary>
    /// Gets the new instance of <see cref="IMessageReader"/>.
    /// </summary>
    /// <param name="name">The name of the reader.</param>
    /// <param name="configuration">The configuration of the reader.</param>
    /// <returns>An instance of <see cref="IMessageReader"/>.</returns>
    IMessageReader IMessageHandlerFactory.GetIMessageReader(string name, MessageChannelConfiguration configuration, IUADecoder uaDecoder)
    {
      BinaryUDPPackageReader _ret = new BinaryUDPPackageReader(uaDecoder, UDPPortNumber, m_Trace, m_ParentViewModel);
      m_ToDispose(_ret);
      if (Properties.Settings.Default.JoinMulticastGroup)
        _ret.MulticastGroup = IPAddress.Parse(Properties.Settings.Default.DefaultMulticastGroup);
      _ret.ReuseAddress = Properties.Settings.Default.ReuseAddress;
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

    #region API
    /// <summary>
    /// Gets the listen to UDP port number.
    /// </summary>
    /// <value>The UDP port number.</value>
    internal int UDPPortNumber
    {
      get { return Properties.Settings.Default.UDPPort; }
    }
    #endregion

    #region private
    private Action<IDisposable> m_ToDispose;
    private IConsumerViewModel m_ParentViewModel;
    private Action<string> m_Trace;
    #endregion

  }

}