using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class OPCUAServerProducerSimulatorUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_OPCUAServerProducerSimulator")]
    [ExpectedException(typeof(NotImplementedException))]
    public void CreatorTestMethod()
    {
      DataManagementSetup _consumer = Simulator.OPCUAServerProducerSimulator.CreateDevice(new MyMessageHandlerFactory());
    }
    private class MyMessageHandlerFactory : MessageHandling.IMessageHandlerFactory
    {
      public MessageHandling.IMessageReader GetIMessageReader(string name, System.Xml.XmlElement configuration)
      {
        throw new NotImplementedException();
      }
      public MessageHandling.IMessageWriter GetIMessageWriter(string name, System.Xml.XmlElement configuration)
      {
        throw new NotImplementedException();
      }
    }
  }
}
