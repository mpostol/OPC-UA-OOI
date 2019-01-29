//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UAOOI.Networking.Core;
using UAOOI.Networking.UDPMessageHandler.Configuration;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;

namespace UAOOI.Networking.UDPMessageHandler
{
  /// <summary>
  /// Class BinaryUDPPackageReader - custom implementation of the <see cref="BinaryDecoder"/> using UDP protocol.. 
  /// This class cannot be inherited.
  /// </summary>
  internal sealed class BinaryUDPPackageReader : IBinaryDataTransferGraphReceiver
  {

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryUDPPackageReader" /> class.
    /// </summary>
    /// <param name="configuration">The configuration of the reader.</param>
    internal BinaryUDPPackageReader(UDPReaderConfiguration configuration)
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), $"{nameof(BinaryUDPPackageReader)}({configuration.ToString()})");
      State = new MyState(this);
      m_UDPPort = configuration.UDPPortNumber;
      MulticastGroup = configuration.DefaultMulticastGroup;
      ReuseAddress = configuration.ReuseAddress;
    }
    #endregion

    #region IBinaryDataTransferObjectReceiver
    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>The state.</value>
    public IAssociationState State { get; set; }
    public void AttachToNetwork()
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(AttachToNetwork));
      Debug.Assert(HandlerState.Operational != State.State);
    }
    public event EventHandler<byte[]> OnNewFrameArrived;
    #endregion

    #region API
    internal bool ReuseAddress
    {
      get => m_ReuseAddress;
      set
      {
        if (State.State == HandlerState.Operational)
        {
          string _msg = $"{nameof(ReuseAddress)} cannot be set in the Operational state";
          UDPMessageHandlerSemanticEventSource.Log.Failure(nameof(BinaryUDPPackageReader), nameof(ReuseAddress), _msg);
          throw new InvalidOperationException(_msg);
        }
        m_ReuseAddress = value;
      }
    }
    internal IPAddress MulticastGroup
    {
      get => m_MulticastGroup;
      set
      {
        if (State.State == HandlerState.Operational)
        {
          string _msg = $"{nameof(MulticastGroup)} cannot be set in the Operational state";
          UDPMessageHandlerSemanticEventSource.Log.Failure(nameof(BinaryUDPPackageReader), nameof(ReuseAddress), _msg);
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

    #region IDispose
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(Dispose));
      if (Client == null)
        return;
      UDPMessageHandlerSemanticEventSource.Log.EnteringMethod(nameof(BinaryUDPPackageReader), nameof(Client.Close));
      Client.Close();
      Client = null;
      m_MulticastGroup = null;
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
    private readonly int m_UDPPort;
    private bool m_ReuseAddress = true;
    private IPAddress m_MulticastGroup = null;
    private readonly IPGlobalProperties m_Properties = IPGlobalProperties.GetIPGlobalProperties();
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
        byte[] _receiveBytes = _client.EndReceive(asyncResult, ref _UEndPoint);
        int _length = _receiveBytes == null ? -1 : _receiveBytes.Length;
        UDPMessageHandlerSemanticEventSource.Log.ReceivedMessageContent(_UEndPoint, _length, _receiveBytes);
        OnNewFrameArrived?.Invoke(this, _receiveBytes);
        Client.BeginReceive(new AsyncCallback(ReceiveAsyncCallback), Client);
      }
      catch (Exception _ex)
      {
        UDPMessageHandlerSemanticEventSource.Log.LogException(nameof(BinaryUDPPackageReader), nameof(ReceiveAsyncCallback), _ex);
      }
    }
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
    #endregion

  }
}
