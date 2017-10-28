
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;

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
    #endregion

    #region BinaryDecoder
    public event EventHandler<UdpStatisticsEventArgs> UdpStatisticsEvent;
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
          throw new InvalidOperationException($"{nameof(ReuseAddress)} cannot be set the object is in Operational state");
        m_ReuseAddress = value;
      }
    }
    internal IPAddress MulticastGroup
    {
      get { return m_MulticastGroup; }
      set
      {
        if (State.State == HandlerState.Operational)
          throw new InvalidOperationException($"{nameof(MulticastGroup)} cannot be set the object is in Operational state");
        m_MulticastGroup = value;
      }
    }
    public UdpClient Client
    {
      get { return m_UdpClient; }
    }
    #endregion

    #region override
    public override string ToString()
    {
      string _multicastGroupMessage = m_MulticastGroup == null ? $"multicast off" : $"joined multicast: {m_MulticastGroup}";
      string _reuseAddressMessage = m_ReuseAddress ? "Address is reused" : "Address is not reused.";
      return $"BinaryUDPPackageReader UPD Port: {m_UDPPort} {_multicastGroupMessage} {_reuseAddressMessage}";
    }
    protected override void Trace(string message)
    {
      m_Trace(message);
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
    //private IConsumerViewModel m_ViewModel = null;
    private int m_NumberOfBytes = 0;
    private int m_NumberOfPackages = 0;
    private UdpClient m_UdpClient;
    private int m_UDPPort;
    private Action<string> m_Trace;
    private bool m_ReuseAddress = true;
    private IPAddress m_MulticastGroup = null;
    private IPGlobalProperties m_Properties = IPGlobalProperties.GetIPGlobalProperties();
    /// <summary>
    /// Implements <see cref="AsyncCallback"/> for UDP begin receive.
    /// </summary>
    /// <param name="asyncResult">The asynchronous result.</param>
    private void m_ReceiveAsyncCallback(IAsyncResult asyncResult)
    {
      m_Trace("Entering m_ReceiveAsyncCallback");
      //if (!asyncResult.IsCompleted)
      //  return;
      try
      {
        UdpClient _client = (UdpClient)asyncResult.AsyncState;
        IPEndPoint _UEndPoint = null;
        Byte[] _receiveBytes = _client.EndReceive(asyncResult, ref _UEndPoint);
        m_NumberOfPackages++;
        m_NumberOfBytes += _receiveBytes.Length;
        //m_ViewModel.ConsumerFramesReceived = m_NumberOfPackages;
        //m_ViewModel.ConsumerReceivedBytes = m_NumberOfBytes;
        int _length = _receiveBytes == null ? -1 : _receiveBytes.Length;
        m_Trace($"Message<{_UEndPoint.Address.ToString()}:{_UEndPoint.Port} [{_length}]>: {String.Join(", ", new ArraySegment<byte>(_receiveBytes, 0, Math.Min(_receiveBytes.Length, 80)).Select<byte, string>(x => x.ToString("X")).ToArray<string>())}");
        MemoryStream _stream = new MemoryStream(_receiveBytes, 0, _receiveBytes.Length);
        OnNewFrameArrived(new BinaryReader(_stream, System.Text.Encoding.UTF8));
        m_Trace("BeginReceive");
        UdpStatisticsEvent?.Invoke(this, new UdpStatisticsEventArgs(m_Properties.GetUdpIPv4Statistics()));
        m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), m_UdpClient);
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
      m_UdpClient = new UdpClient();
      m_UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, ReuseAddress);
      m_UdpClient.ExclusiveAddressUse = !ReuseAddress;
      IPEndPoint _ep = new IPEndPoint(IPAddress.Any, m_UDPPort);
      m_UdpClient.Client.Bind(_ep);
      if (m_MulticastGroup != null)
      {
        m_Trace($"Joining the multicast group: {m_MulticastGroup}");
        m_UdpClient.JoinMulticastGroup(m_MulticastGroup);
      }
      m_Trace(ToString());
      m_UdpClient.BeginReceive(new AsyncCallback(m_ReceiveAsyncCallback), m_UdpClient);
    }
    private void OnDisable()
    {
      Dispose();
    }
    #endregion

  }
}
