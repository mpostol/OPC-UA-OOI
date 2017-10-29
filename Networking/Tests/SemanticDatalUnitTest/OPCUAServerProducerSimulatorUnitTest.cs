
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SemanticData.UnitTest.Simulator;

namespace UAOOI.Networking.SemanticData.UnitTest
{

  [TestClass]
  public class OPCUAServerProducerSimulatorUnitTest
  {

    #region test part
    [TestMethod]
    [TestCategory("DataManagement_OPCUAServerProducerSimulator")]
    public void CreatorTestMethod()
    {
      Guid _dataSetGuid = Guid.NewGuid();
      MyMessageHandlerFactory _mhf = new MyMessageHandlerFactory(_dataSetGuid);
      DataManagementSetup _producer = OPCUAServerProducerSimulator.CreateDevice(_mhf, _dataSetGuid);
      Assert.IsNull(_producer.AssociationsCollection);
      Assert.IsNotNull(_producer.BindingFactory);
      Assert.IsNotNull(_producer.ConfigurationFactory);
      Assert.IsNotNull(_producer.EncodingFactory);
      Assert.IsNotNull(_producer.MessageHandlerFactory);
      Assert.IsNull(_producer.MessageHandlersCollection);
      _producer.Initialize();
      _producer.Run();
      Assert.AreEqual<int>(1, _producer.AssociationsCollection.Count);
      Assert.AreEqual<int>(1, _producer.MessageHandlersCollection.Count);
      ((OPCUAServerProducerSimulator)_producer).CheckConsistency();
      _mhf.CheckConsistency();
      ((OPCUAServerProducerSimulator)_producer).Update("Value1", "Value1");
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void MessageHandlerFactoryCreatorReadTestMethod()
    {
      IMessageHandlerFactory _nmf = new MyMessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageWriter _nmr = _nmf.GetIMessageWriter("UDP", "4840,localhost", null);
      Assert.IsNotNull(_nmr);
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    [ExpectedException(typeof(NotImplementedException))]
    public void MessageHandlerFactoryCreatorWriteTestMethod()
    {
      IMessageHandlerFactory _nmf = new MyMessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageReader _nmr = _nmf.GetIMessageReader("UDP", null, null);
    }
    #endregion

    #region private
    private class MyMessageHandlerFactory : IMessageHandlerFactory
    {

      #region creator
      public MyMessageHandlerFactory(Guid dataSetGuid)
      {
        this.MessageWriter = new MyMessageWriter(dataSetGuid);
      }
      #endregion

      #region IMessageHandlerFactory
      public MessageHandling.IMessageReader GetIMessageReader(string name, string configuration, IUADecoder uaDecoder)
      {
        throw new NotImplementedException();
      }
      public MessageHandling.IMessageWriter GetIMessageWriter(string name, string configuration, IUAEncoder uaEncoder)
      {
        Assert.AreEqual("UDP", name);
        Assert.AreEqual<string>("4840,localhost", configuration);
        return MessageWriter;
      }
      #endregion

      #region private
      private IMessageWriter MessageWriter { get; set; }
      #endregion

      #region test environment
      internal void CheckConsistency()
      {
        Assert.IsNotNull(MessageWriter);
      }
      #endregion

    }
    #endregion

  }

}
