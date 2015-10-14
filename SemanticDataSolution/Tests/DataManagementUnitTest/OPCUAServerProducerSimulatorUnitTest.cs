
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;

namespace UAOOI.SemanticData.DataManagement.UnitTest
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
      DataManagementSetup _producer = Simulator.OPCUAServerProducerSimulator.CreateDevice(_mhf, _dataSetGuid);
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
      _mhf.SendData();

    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void MessageHandlerFactoryCreatorReadTestMethod()
    {
      IMessageHandlerFactory _nmf = new MyMessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageWriter _nmr = _nmf.GetIMessageWriter("UDP", null);
      Assert.IsNotNull(_nmr);
    }
    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    [ExpectedException(typeof(NotImplementedException))]
    public void MessageHandlerFactoryCreatorWriteTestMethod()
    {
      IMessageHandlerFactory _nmf = new MyMessageHandlerFactory(Guid.NewGuid());
      Assert.IsNotNull(_nmf);
      IMessageReader _nmr = _nmf.GetIMessageReader("UDP", null);
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
      public MessageHandling.IMessageReader GetIMessageReader(string name, System.Xml.XmlElement configuration)
      {
        throw new NotImplementedException();
      }
      public MessageHandling.IMessageWriter GetIMessageWriter(string name, System.Xml.XmlElement configuration)
      {
        Assert.AreEqual("UDP", name);
        Assert.IsNull(configuration);
        return this.MessageWriter;
      }
      #endregion

      #region private
      private IMessageWriter MessageWriter { get; set; }

      #endregion

      #region test environment
      internal void CheckConsistency()
      {
        throw new NotImplementedException();
      }
      internal void SendData()
      {
        throw new NotImplementedException();
      } 
      #endregion
    }
    #endregion

  }

}
