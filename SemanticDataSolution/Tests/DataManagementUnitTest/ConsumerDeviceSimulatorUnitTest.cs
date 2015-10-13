
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ConsumerDeviceSimulatorUnitTest
  {
    #region test part
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void ConsumerDeviceSimulatorTestMethod()
    {
      Guid DataSetGuid = Guid.NewGuid();
      MessageHandlerFactory _mhf = new MessageHandlerFactory(DataSetGuid);
      DataManagementSetup _consumer = ConsumerDeviceSimulator.CreateDevice(_mhf, DataSetGuid);
      Assert.IsNull(_consumer.AssociationsCollection);
      Assert.IsNotNull(_consumer.BindingFactory);
      Assert.IsNotNull(_consumer.ConfigurationFactory);
      Assert.IsNotNull(_consumer.EncodingFactory);
      Assert.IsNotNull(_consumer.MessageHandlerFactory);
      Assert.IsNull(_consumer.MessageHandlersCollection);
      _consumer.Initialize();
      _consumer.Run();
      Assert.AreEqual<int>(1, _consumer.AssociationsCollection.Count);
      Assert.AreEqual<int>(1, _consumer.MessageHandlersCollection.Count);
      ((ConsumerDeviceSimulator)_consumer).CheckConsistency();
      CheckConsistency(_consumer.MessageHandlersCollection);
      _mhf.CheckConsistency();
      _mhf.SendData();
      //UDPSimulator _transport = _consumer.ReadConfiguration();
      //byte[] _buffer = new byte[] { 0x1, 0x5, 0x12 };
      //bool _messageOK = false;
      //_transport.Write(_buffer, x => _messageOK = _buffer.AsEnumerable<byte>().SequenceEqual<byte>(x));
      //Assert.IsTrue(_messageOK);
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void MessageHandlerFactoryCreatorReadTestMethod()
    {
      IMessageHandlerFactory _nmf = new MessageHandlerFactory();
      Assert.IsNotNull(_nmf);
      IMessageReader _nmr = _nmf.GetIMessageReader("UDP", null);
      Assert.IsNotNull(_nmr);
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    [ExpectedException(typeof(NotImplementedException))]
    public void MessageHandlerFactoryCreatorWriteTestMethod()
    {
      IMessageHandlerFactory _nmf = new MessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageWriter _nmr = _nmf.GetIMessageWriter("UDP", null);
    }
    #endregion

    #region private
    private class MessageHandlerFactory : IMessageHandlerFactory
    {

      internal MessageHandlerFactory(Guid dataSetGuid)
      {
        this.MyMessageReader = new MessageReader(dataSetGuid);
      }
      internal MessageHandlerFactory() : this(Guid.NewGuid()) { }
      internal class MessageReader : IMessageReader
      {
        public MessageReader(Guid dataSetGuid)
        {
          State = new MyState();
          DataSetGuid = dataSetGuid;
        }
        #region IMessageReader
        public event EventHandler<MessageEventArg> ReadMessageCompleted;
        private bool m_HaveBeenActivated;
        public IAssociationState State
        {
          get;
          private set;
        }
        public void AttachToNetwork()
        {
          m_HaveBeenActivated = true;
        }
        #endregion
        private class MyState : IAssociationState
        {
          public MyState()
          {
            State = HandlerState.Disabled;
          }
          public HandlerState State
          {
            get;
            private set;
          }
          public void Enable()
          {
            if (State != HandlerState.Disabled)
              throw new ArgumentException("Wrong state");
            State = HandlerState.Operational;
          }
          public void Disable()
          {
            if (State != HandlerState.Operational)
              throw new ArgumentException("Wrong state");
            State = HandlerState.Disabled;
          }
        }
        internal void CheckConsistency()
        {
          Assert.IsNotNull(State);
          Assert.AreEqual<HandlerState>(HandlerState.Operational, State.State);
          Assert.IsNotNull(ReadMessageCompleted);
          Assert.IsTrue(m_HaveBeenActivated);
        }
        internal void SendData()
        {
          ReadMessageCompleted(this, new MessageEventArg(CreateMessage()));
        }
        private PeriodicDataMessage CreateMessage()
        {
          PeriodicDataMessage _ret = new PeriodicDataMessage(new object[] { "123", 1.23 }, DataSetGuid);
          return _ret;
        }
        public Guid DataSetGuid { get; set; }
      }
      internal MessageReader MyMessageReader { get; set; }

      #region IMessageHandlerFactory
      public IMessageReader GetIMessageReader(string name, System.Xml.XmlElement configuration)
      {
        return this.MyMessageReader;
      }
      public IMessageWriter GetIMessageWriter(string name, System.Xml.XmlElement configuration)
      {
        throw new NotImplementedException();
      }
      #endregion

      internal void CheckConsistency()
      {
        Assert.IsNotNull(MyMessageReader);
      }
      internal void SendData()
      {
        MyMessageReader.SendData();
      }

    }
    private void CheckConsistency(MessageHandlersCollection messageHandlersCollection)
    {
      foreach (MessageHandlerFactory.MessageReader _item in messageHandlersCollection.Values)
        _item.CheckConsistency();
    }
    #endregion

  }
}
