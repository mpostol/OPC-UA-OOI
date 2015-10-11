
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;
using UAOOI.SemanticData.DataManagement.MessageHandling;

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
      DataManagementSetup _consumer = ConsumerDeviceSimulator.CreateDevice(new MessageHandlerFactory());
      Assert.IsNull(_consumer.AssociationsCollection);
      Assert.IsNotNull(_consumer.BindingFactory);
      Assert.IsNotNull(_consumer.ConfigurationFactory);
      Assert.IsNotNull(_consumer.EncodingFactory);
      Assert.IsNotNull(_consumer.MessageHandlerFactory);
      Assert.IsNull(_consumer.MessageHandlersCollection);
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
      IMessageHandlerFactory _nmf = new MessageHandlerFactory();
      Assert.IsNotNull(_nmf);
      IMessageWriter _nmr = _nmf.GetIMessageWriter("UDP", null);
    }
    #endregion

    #region private
    private class MessageHandlerFactory : IMessageHandlerFactory
    {

      internal MessageHandlerFactory()
      {
        this.MyMessageReader = new MessageReader();
      }
      internal class MessageReader : IMessageReader
      {

        #region IMessageReader
        public event EventHandler<MessageEventArg> ReadMessageCompleted;
        public IAssociationState State
        {
          get { throw new NotImplementedException(); }
        }
        public void AttachToNetwork()
        {
          throw new NotImplementedException();
        }
        #endregion

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

    }
    #endregion

  }
}
