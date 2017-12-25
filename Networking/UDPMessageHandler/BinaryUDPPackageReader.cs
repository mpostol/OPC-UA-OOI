
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.UDPMessageHandler.Configuration;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;

namespace UAOOI.Networking.UDPMessageHandler
{
  /// <summary>
  /// Class BinaryUDPPackageReader - custom implementation of the <see cref="BinaryDecoder"/> using UDP protocol.. 
  /// This class cannot be inherited.
  /// </summary>
  internal sealed class BinaryUDPPackageReader : BinaryDecoder
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryUDPPackageReader" /> class.
    /// </summary>
    /// <param name="uaDecoder">The ua decoder.</param>
    /// <param name="configuration">The configuration of the reader.</param>
    internal BinaryUDPPackageReader(IUADecoder uaDecoder, UDPReaderConfiguration configuration) : base(uaDecoder)
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), $"{nameof(BinaryUDPPackageReader)}({configuration.ToString()})");
      State = new MyState(this);
      m_UDPPort = configuration.UDPPortNumber;
      MulticastGroup = configuration.DefaultMulticastGroup;
      ReuseAddress = configuration.ReuseAddress;
    }
    #endregion

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
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(AttachToNetwork));
      Debug.Assert(HandlerState.Operational != State.State);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(Dispose));
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (Client == null)
        return;
      Client.Close();
      Client = null;
      m_MulticastGroup = null;
    }
    #endregion

    #region API
    internal bool ReuseAddress
    {
      get { return m_ReuseAddress; }
      set
      {
        if (State.State == HandlerState.Operational)
        {
          string _msg = $"{nameof(ReuseAddress)} cannot be set in the Operational state";
          UDPMessageHandlerSemanticEventSource.Log.Failure(_msg);
          throw new InvalidOperationException(_msg);
        }
        m_ReuseAddress = value;
      }
    }
    internal IPAddress MulticastGroup
    {
      get { return m_MulticastGroup; }
      set
      {
        if (State.State == HandlerState.Operational)
        {
          string _msg = $"{nameof(MulticastGroup)} cannot be set in the Operational state";
          UDPMessageHandlerSemanticEventSource.Log.Failure(_msg);
          throw new InvalidOperationException(_msg);
        }
        m_MulticastGroup = value;
      }
    }
    #endregion

    #region override
    public override string ToString()
    {
      string _multicastGroupMessage = m_MulticastGroup == null ? $"multicast off" : $"joined multicast: {m_MulticastGroup}";
      string _reuseAddressMessage = m_ReuseAddress ? "Address is reused" : "Address is not reused.";
      return $"BinaryUDPPackageReader UPD Port: {m_UDPPort} {_multicastGroupMessage} {_reuseAddressMessage}";
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
    private UdpClient Client { get; set; }
    private int m_UDPPort;
    private bool m_ReuseAddress = true;
    private IPAddress m_MulticastGroup = null;
    private IPGlobalProperties m_Properties = IPGlobalProperties.GetIPGlobalProperties();
    //methods
    /// <summary>
    /// Implements <see cref="AsyncCallback"/> for UDP begin receive.
    /// </summary>
    /// <param name="asyncResult">The asynchronous result.</param>
    private void ReceiveAsyncCallback(IAsyncResult asyncResult)
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(ReceiveAsyncCallback));
      //if (!asyncResult.IsCompleted)
      //  return;
      try
      {
        UdpClient _client = (UdpClient)asyncResult.AsyncState;
        IPEndPoint _UEndPoint = null;
        Byte[] _receiveBytes = _client.EndReceive(asyncResult, ref _UEndPoint);
        int _length = _receiveBytes == null ? -1 : _receiveBytes.Length;
        UDPMessageHandlerSemanticEventSource.Log.ReceivedMessageContent(_UEndPoint, _length, _receiveBytes);
        MemoryStream _stream = new MemoryStream(_receiveBytes, 0, _receiveBytes.Length);
        OnNewFrameArrived(new BinaryReader(_stream, System.Text.Encoding.UTF8));
        Client.BeginReceive(new AsyncCallback(ReceiveAsyncCallback), Client);
      }
      catch (ObjectDisposedException _ex)
      {
        UDPMessageHandlerSemanticEventSource.Log.Failure($"ObjectDisposedException = {_ex.Message}");
      }
      catch (Exception _ex)
      {
        UDPMessageHandlerSemanticEventSource.Log.Failure($"Exception {_ex.Message}, type = {_ex.GetType().Name}");
      }
    }
    //Methods
    private void OnEnable()
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(OnEnable));
      Client = new UdpClient();
      Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, ReuseAddress);
      Client.ExclusiveAddressUse = !ReuseAddress;
      IPEndPoint _ep = new IPEndPoint(IPAddress.Any, m_UDPPort);
      Client.Client.Bind(_ep);
      if (m_MulticastGroup != null)
      {
        UDPMessageHandlerSemanticEventSource.Log.JoiningMulticastGroup(m_MulticastGroup);
        Client.JoinMulticastGroup(m_MulticastGroup);
      }
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(Client.BeginReceive));
      Client.BeginReceive(new AsyncCallback(ReceiveAsyncCallback), Client);
    }
    private void OnDisable()
    {
      Dispose();
    }
    #endregion

  }
}
