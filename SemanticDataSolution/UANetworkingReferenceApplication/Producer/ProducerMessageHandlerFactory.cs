
using System;
using System.Net;
using System.Net.Sockets;
using UAOOI.SemanticData.DataManagement;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using System.Linq;
using System.Diagnostics;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
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
      BinaryUDPPackageWriter _ret = new BinaryUDPPackageWriter(RemoteHostName, UDPPortNumber, ProducerId, m_Trace, m_ViewModel, uaEncoder);
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
    /// <summary>
    /// Gets the producer identifier.
    /// </summary>
    /// <value>The producer identifier.</value>
    internal Guid ProducerId
    {
      get
      {
        return new Guid(Properties.Settings.Default.ProducerId);
      }
    }
    #endregion

    #region private
    /// <summary>
    /// Class BinaryUDPPackageWriter - custom implementation of the <see cref="BinaryEncoder"/> using UDP protocol.
    /// </summary>
    private IProducerViewModel m_ViewModel { get; set; }
    private class BinaryUDPPackageWriter : BinaryEncoder
    {

      #region creator
      public BinaryUDPPackageWriter
        (string remoteHostName, int remotePort, Guid producerId, Action<string> trace, IProducerViewModel ViewModel, IUAEncoder uaEncoder) :
          base(uaEncoder, producerId, FieldEncodingEnum.VariantFieldEncoding, MessageLengthFieldTypeEnum.TwoBytes)
      {
        m_Trace = trace;
        m_ViewModel = ViewModel;
        State = new MyState(this);
        m_RemoteHostName = remoteHostName;
        m_remotePort = remotePort;
        trace("Created BinaryUDPPackageWriter");
        ViewModel.BytesSent = 0;
        ViewModel.PackagesSent = 0;
      }
      #endregion

      #region BinaryMessageEncoder
      public override IAssociationState State
      {
        get;
        protected set;
      }
      public override void AttachToNetwork()
      {
        m_Trace("Entering AttachToNetwork");
        m_NumberOfAttachToNetwork++;
      }
      protected override void SendFrame(byte[] buffer)
      {
        lock (this)
        {
          string _msg = String.Format("Entering SendFrame buffer.Length = {0}", buffer.Length);
          m_Trace(_msg);
          try
          {
            m_NumberOfSentBytes += buffer.Length;
            m_ViewModel.BytesSent = m_NumberOfSentBytes;
            m_NumberOfSentMessages++;
            m_ViewModel.PackagesSent = m_NumberOfSentMessages;
            IPEndPoint _IPEndPoint = new IPEndPoint(m_IPAddresses, m_remotePort);
            m_UdpClient.Send(buffer, buffer.Length, _IPEndPoint);
            _msg = String.Format("After Send m_NumberOfSentBytes = {0}, m_NumberOfSentMessages = {1}", m_NumberOfSentBytes, m_NumberOfSentMessages);
          }
          catch (SocketException e)
          {
            _msg = String.Format("SocketException caught!!! Source : {0} Message : {1}", e.Source, e.Message);
          }
          catch (ArgumentNullException e)
          {
            _msg = String.Format("ArgumentNullException caught!!! Source : {0} Message : {1}", e.Source, e.Message);
          }
          catch (NullReferenceException e)
          {
            _msg = String.Format("NullReferenceException caught!!! Source : {0} Message : {1}", e.Source, e.Message);
          }
          catch (Exception e)
          {
            _msg = String.Format("Exception caught!!! Source : {0} Message : {1}", e.Source, e.Message);
          }
          finally
          {
            m_Trace(_msg);
          }
        }
      }
      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      protected override void Dispose(bool disposing)
      {
        string _msg = String.Format("Entering Dispose disposing = {0}", disposing);
        m_Trace(_msg);
        lock (this)
        {
          base.Dispose(disposing);
          if (!disposing)
            return;
          if (m_UdpClient == null)
            return;
          _msg = "Closing UdpClient";
          m_Trace(_msg);
          m_UdpClient.Close();
          m_UdpClient = null;
        }
      }
      #endregion

      #region private
      //types
      private class MyState : IAssociationState
      {

        #region IAssociationState
        /// <summary>
        /// Initializes a new instance of the <see cref="MyState"/> class.
        /// </summary>
        public MyState(BinaryUDPPackageWriter parent)
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

        private BinaryUDPPackageWriter m_Parent;

      }
      //vars
      private UdpClient m_UdpClient;
      private IPAddress m_IPAddresses;
      private IPHostEntry m_remoteHostInfo;
      private int m_remotePort = 4800;
      private string m_RemoteHostName;
      private Action<string> m_Trace;
      //Methods
      private void OnEnable()
      {
        m_Trace("Entering OnEnable");
        Debug.Assert(m_UdpClient == null);
        // Get DNS host information.
        m_remoteHostInfo = Dns.GetHostEntry(m_RemoteHostName);
        // Get the DNS IP addresses associated with the host.
        // Get first IPAddress in list return by DNS.
        m_IPAddresses = m_remoteHostInfo.AddressList.Where<IPAddress>(x => x.AddressFamily == AddressFamily.InterNetwork).First<IPAddress>();
        Debug.Assert(m_IPAddresses != null);
        m_UdpClient = new UdpClient();
        string _msg = String.Format("Created UdpClient for m_RemoteHostName: {0} Ip : {1}", m_RemoteHostName, m_IPAddresses.ToString());
        m_Trace("Created To the Network");
      }
      #endregion

      #region diagnostic instrumentation
      internal int m_NumberOfSentMessages = 0;
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork;
      private IProducerViewModel m_ViewModel;
      #endregion

    }
    private Action<IDisposable> m_ToDispose;
    private Action<string> m_Trace;
    #endregion

  }
}
