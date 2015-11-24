
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.SemanticData.DataManagement.Encoding;
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
      MyMessageHandlerFactory _mhf = new MyMessageHandlerFactory(DataSetGuid);
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
      _mhf.CheckConsistency();
      _mhf.SendData();
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void MessageHandlerFactoryCreatorReadTestMethod()
    {
      IMessageHandlerFactory _nmf = new MyMessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageReader _nmr = _nmf.GetIMessageReader("UDP", null, new Helpers.UABinaryDecoderImplementation());
      Assert.IsNotNull(_nmr);
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    [ExpectedException(typeof(NotImplementedException))]
    public void MessageHandlerFactoryCreatorWriteTestMethod()
    {
      IMessageHandlerFactory _nmf = new MyMessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageWriter _nmr = _nmf.GetIMessageWriter("UDP", null, null);
    }
    #endregion

    #region private
    private class MyMessageHandlerFactory : IMessageHandlerFactory
    {

      #region creator
      internal MyMessageHandlerFactory(Guid dataSetGuid)
      {
        this.MyMessageReader = new MessageReader(dataSetGuid);
      }
      #endregion

      #region IMessageHandlerFactory
      public IMessageReader GetIMessageReader(string name, System.Xml.XmlElement configuration, IUADecoder uaDecoder)
      {
        Assert.AreEqual("UDP", name);
        Assert.IsNull(configuration);
        Assert.IsNotNull(uaDecoder);
        return MyMessageReader;
      }
      public IMessageWriter GetIMessageWriter(string name, System.Xml.XmlElement configuration, IUAEncoder uaEncoder)
      {
        throw new NotImplementedException();
      }
      #endregion

      #region testing environment
      internal void CheckConsistency()
      {
        Assert.IsNotNull(MyMessageReader);
      }
      internal void SendData()
      {
        MyMessageReader.SendData();
      }

      #endregion

      #region private
      private MessageReader MyMessageReader { get; set; }
      #endregion

    }
    #endregion

  }

}
