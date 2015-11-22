
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using UAOOI.SemanticData.DataManagement;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  /// <summary>
  /// Class ConsumerMessageHandlerFactory - implements <see cref="IMessageHandlerFactory"/> 
  /// </summary>
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
    IMessageReader IMessageHandlerFactory.GetIMessageReader(string name, XmlElement configuration, IUADecoder uaDecoder)
    {
      BinaryUDPPackageReader _ret = new BinaryUDPPackageReader(uaDecoder, UDPPortNumber, m_Trace) { m_ViewModel = m_ParentViewModel };
      m_ToDispose(_ret);
      m_Trace(String.Format("Created BinaryUDPPackageReader UDPPortNumber = {0}", UDPPortNumber));
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
    IMessageWriter IMessageHandlerFactory.GetIMessageWriter(string name, XmlElement configuration)
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
    /// <summary>
    /// Class BinaryUDPPackageReader - custom implementation of the <see cref="BinaryDecoder"/> using UDP protocol.. 
    /// This class cannot be inherited.
    /// </summary>
    private sealed class BinaryUDPPackageReader : BinaryDecoder
    {

      /// <summary>
      /// Initializes a new instance of the <see cref="BinaryUDPPackageReader"/> class.
      /// </summary>
      /// <param name="port">The port.</param>
      /// <param name="trace">The trace.</param>
      public BinaryUDPPackageReader(IUADecoder uaDecoder, int port, Action<string> trace) : base(uaDecoder)
      {
        State = new MyState(this);
        m_Trace = trace;
        m_UDPPort = port;
      }

      #region BinaryDecoder
      /// <summary>
      /// Gets or sets the state.
      /// </summary>
      /// <value>The state.</value>
      public override IAssociationState State
      {
        get;
        protected set;
      }
      /// <summary>
      /// Attaches to network.
      /// </summary>
      public override void AttachToNetwork()
      {
        Debug.Assert(HandlerState.Operational != State.State);
      }
      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      protected override void Dispose(bool disposing)
      {
        m_Trace("Entering Dispose");
        base.Dispose(disposing);
        if (!disposing)
          return;
        if (m_UdpClient == null)
          return;
        m_UdpClient.Close();
        m_UdpClient = null;
      }
      #endregion

      internal IConsumerViewModel m_ViewModel;
      #region private
      //types
      private class MyState : IAssociationState
      {

        #region IAssociationState
        /// <summary>
        /// Initializes a new instance of the <see cref="MyState"/> class.
        /// </summary>
        public MyState(BinaryUDPPackageReader parent)
        {
          State = HandlerState.Disabled;
          m_Parent = parent;
        }
        /// <summary>
        /// Gets the current state <see cref="HandlerState" /> of the <see cref="Association" /> instance.
        /// </summary>
        /// <value>The state of <see cref="HandlerState" /> type.</value>
        public HandlerState State
        {
          get;
          private set;
        }
        /// <summary>
        /// This method is used to enable a configured <see cref="Association" /> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational" /> state.
        /// In the case of an error situation, the state changes into <see cref="HandlerState.Error" />. The operation is rejected if the current <see cref="State" />  is not <see cref="HandlerState.Disabled" />.
        /// </summary>
        /// <exception cref="System.ArgumentException">Wrong state</exception>
        public void Enable()
        {
          if (State != HandlerState.Disabled)
            throw new ArgumentException("Wrong state");
          State = HandlerState.Operational;
          m_Parent.OnEnable();
        }
        /// <summary>
        /// This method is used to disable an already enabled <see cref="Association" /> object.
        /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled" /> or <see cref="HandlerState.NoConfiguration" />.
        /// </summary>
        /// <exception cref="System.ArgumentException">Wrong state</exception>
        public void Disable()
        {
          if (State != HandlerState.Operational)
            throw new ArgumentException("Wrong state");
          State = HandlerState.Disabled;
          m_Parent.Dispose();
        }
        #endregion

        private BinaryUDPPackageReader m_Parent;

      }
      //vars
      private int m_NumberOfBytes = 0;
      private int m_NumberOfPackages = 0;
      private UdpClient m_UdpClient;
      private int m_UDPPort;
      private Action<string> m_Trace;
      /// <summary>
      /// Implements <see cref="AsyncCallback"/> for UDP begin receive.
      /// </summary>
      /// <param name="asyncResult">The asynchronous result.</param>
      private void m_ReceiveAsyncCallback(IAsyncResult asyncResult)
      {
        m_Trace("Entering m_ReceiveAsyncCallback");
        try
        {
          IPEndPoint _UEndPoint = null;
          Byte[] _receiveBytes = m_UdpClient.EndReceive(asyncResult, ref _UEndPoint);
          m_NumberOfPackages++;
          m_NumberOfBytes += _receiveBytes.Length;
          m_ViewModel.ConsumerFramesReceived = m_NumberOfPackages;
          m_ViewModel.ConsumerBytesReceived = m_NumberOfBytes;
          m_Trace(String.Format("Received length ={0}", _receiveBytes == null ? -1 : _receiveBytes.Length));
          MemoryStream _stream = new MemoryStream(_receiveBytes, 0, _receiveBytes.Length);
          base.OnNewFrameArrived(new UABinaryReader(_stream));
          m_Trace("BeginReceive");
          m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), null);
        }
        catch (ObjectDisposedException _ex)
        {
          m_Trace(String.Format("ObjectDisposedException = {0}", _ex.Message));
        }
        catch (Exception _ex)
        {
          m_Trace(String.Format("Exception {0}, message = {1}", _ex, GetType().Name, _ex.Message));
        }
        m_Trace("Exiting m_ReceiveAsyncCallback");
      }
      //Methods
      private void OnEnable()
      {
        m_UdpClient = new UdpClient(m_UDPPort);
        m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), null);
      }
      private void OnDisable()
      {
        Dispose();
      }
      #endregion

    }
    private Action<IDisposable> m_ToDispose;
    private IConsumerViewModel m_ParentViewModel;
    private Action<string> m_Trace;
    #endregion

  }

}