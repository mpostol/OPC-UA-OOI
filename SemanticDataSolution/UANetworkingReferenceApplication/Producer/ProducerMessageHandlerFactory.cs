
using System;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using UAOOI.SemanticData.DataManagement;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using System.Linq;
using System.Diagnostics;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{
  internal class ProducerMessageHandlerFactory : IMessageHandlerFactory
  {

    public ProducerMessageHandlerFactory(Action<IDisposable> toDispose)
    {
      this.m_ToDispose = toDispose;
    }
    #region IMessageHandlerFactory
    public IMessageReader GetIMessageReader(string name, XmlElement configuration)
    {
      throw new NotImplementedException();
    }
    public IMessageWriter GetIMessageWriter(string name, XmlElement configuration)
    {
      BinaryUDPPackageWriter _ret = new BinaryUDPPackageWriter(RemoteHostName, UDPPortNumber, ProducerId);
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
      get { return Properties.Settings.Default.UDPPort; }
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
    private class BinaryUDPPackageWriter : BinaryEncoder
    {

      #region creator
      public BinaryUDPPackageWriter(string remoteHostName, int remotePort, Guid producerId) : base(producerId)
      {
        State = new MyState(this);
        m_RemoteHostName = remoteHostName;
        m_remotePort = remotePort;
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
        // Get DNS host information.
        m_remoteHostInfo = Dns.GetHostEntry(m_RemoteHostName);
        // Get the DNS IP addresses associated with the host.
        // Get first IPAddress in list return by DNS.
        m_IPAddresses = m_remoteHostInfo.AddressList.Where<IPAddress>(x => x.AddressFamily == AddressFamily.InterNetwork).First<IPAddress>();
        Debug.Assert(m_IPAddresses != null);
        m_UdpClient = new UdpClient();
        m_NumberOfAttachToNetwork++;
      }
      protected override void SendFrame(byte[] buffer)
      {
        m_NumberOfSentBytes += buffer.Length;
        m_NumberOfSentMessages++;
        try
        {
          IPEndPoint _IPEndPoint = new IPEndPoint(m_IPAddresses, m_remotePort);
          m_UdpClient.Send(buffer, buffer.Length, _IPEndPoint);
        }
        catch (SocketException e)
        {
          Console.WriteLine("SocketException caught!!!");
          Console.WriteLine("Source : " + e.Source);
          Console.WriteLine("Message : " + e.Message);
          throw;
        }
        catch (ArgumentNullException e)
        {
          Console.WriteLine("ArgumentNullException caught!!!");
          Console.WriteLine("Source : " + e.Source);
          Console.WriteLine("Message : " + e.Message);
          throw;
        }
        catch (NullReferenceException e)
        {
          Console.WriteLine("NullReferenceException caught!!!");
          Console.WriteLine("Source : " + e.Source);
          Console.WriteLine("Message : " + e.Message);
          throw;
        }
        catch (Exception e)
        {
          Console.WriteLine("Exception caught!!!");
          Console.WriteLine("Source : " + e.Source);
          Console.WriteLine("Message : " + e.Message);
          throw;
        }
      }
      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      protected override void Dispose(bool disposing)
      {
        base.Dispose(disposing);
        if (!disposing)
          return;
        m_UdpClient.Close();
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
      //Methods
      private void OnEnable()
      {
        AttachToNetwork();
      }
      #endregion

      #region diagnostic instrumentation
      internal int m_NumberOfSentMessages = 0;
      internal int m_NumberOfSentBytes = 0;
      internal int m_NumberOfAttachToNetwork;
      #endregion

    }
    private Action<IDisposable> m_ToDispose;

  }
}
