using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.ReferenceApplication.Producer
{
  /// <summary>
  /// Class BinaryUDPPackageWriter - custom implementation of the <see cref="BinaryEncoder"/> using UDP protocol.
  /// </summary>
  internal class BinaryUDPPackageWriter : BinaryEncoder
  {

    #region creator
    public BinaryUDPPackageWriter
      (string remoteHostName, int remotePort, Guid producerId, Action<string> trace, IProducerViewModel ViewModel, IUAEncoder uaEncoder) : base(uaEncoder, producerId, MessageLengthFieldTypeEnum.TwoBytes)
    {
      m_Trace = trace;
      m_ViewModel = ViewModel;
      State = new MyState(this);
      m_RemoteHostName = remoteHostName;
      m_remotePort = remotePort;
      ViewModel.BytesSent = 0;
      ViewModel.PackagesSent = 0;
      trace("Created BinaryUDPPackageWriter");
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
        string _traceMessage = String.Format("Entering SendFrame buffer.Length = {0}", buffer.Length);
        m_Trace(_traceMessage);
        UdpClient _UdpClient = m_UdpClient;
        if (_UdpClient == null)
          return;
        try
        {
          m_NumberOfSentBytes += buffer.Length;
          m_ViewModel.BytesSent = m_NumberOfSentBytes;
          m_NumberOfSentMessages++;
          m_ViewModel.PackagesSent = m_NumberOfSentMessages;
          IPEndPoint _IPEndPoint = new IPEndPoint(m_IPAddresses, m_remotePort);
          _UdpClient.Send(buffer, buffer.Length, _IPEndPoint);
          _traceMessage = String.Format("After Send m_NumberOfSentBytes = {0}, m_NumberOfSentMessages = {1}", m_NumberOfSentBytes, m_NumberOfSentMessages);
        }
        catch (SocketException e)
        {
          _traceMessage = String.Format("SocketException caught!!! Source : {0} Message : {1}", e.Source, e.Message);
        }
        catch (ArgumentNullException e)
        {
          _traceMessage = String.Format("ArgumentNullException caught!!! Source : {0} Message : {1}", e.Source, e.Message);
        }
        catch (NullReferenceException e)
        {
          _traceMessage = String.Format("NullReferenceException caught!!! Source : {0} Message : {1}", e.Source, e.Message);
        }
        catch (Exception e)
        {
          _traceMessage = String.Format("Exception caught!!! Source : {0} Message : {1}", e.Source, e.Message);
        }
        finally
        {
          m_Trace(_traceMessage);
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
    private int m_remotePort = 4800;
    private string m_RemoteHostName;
    private Action<string> m_Trace;
    //Methods
    private void OnEnable()
    {
      m_Trace("Entering OnEnable");
      Debug.Assert(m_UdpClient == null);
      // Get DNS host information.
      IPAddress[] _remoteHostAddresses = Dns.GetHostAddresses(m_RemoteHostName);
      // Get the DNS IP addresses associated with the host.
      // Get first IPAddress in list return by DNS.
      m_IPAddresses = _remoteHostAddresses.Where<IPAddress>(x => x.AddressFamily == AddressFamily.InterNetwork).First<IPAddress>();
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
}
