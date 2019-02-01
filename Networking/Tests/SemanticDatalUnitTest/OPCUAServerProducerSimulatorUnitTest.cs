//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory;
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
      MessageHandlerFactoryTest _mhf = new MessageHandlerFactoryTest();
      using (OPCUAServerProducerSimulator _producer = OPCUAServerProducerSimulator.CreateDevice(_mhf, _dataSetGuid))
      {
        Assert.IsNull(_producer.AssociationsCollection);
        Assert.IsNotNull(_producer.BindingFactory);
        Assert.IsNotNull(_producer.ConfigurationFactory);
        Assert.IsNotNull(_producer.EncodingFactory);
        Assert.IsNotNull(_producer.MessageHandlerFactory);
        Assert.IsNull(_producer.MessageHandlersCollection);
        _producer.TestStart();
        Assert.AreEqual<int>(1, _producer.AssociationsCollection.Count);
        Assert.AreEqual<int>(1, _producer.MessageHandlersCollection.Count);
        _producer.CheckConsistency();
        _mhf.AssertConsistency();
        _producer.Update("Value1", "Value1");
      }
    }
    #endregion

    #region private
    private class MessageHandlerFactoryTest : MessageHandlerFactoryFixture
    {

      #region MessageHandlerFactoryFixture
      protected override BinaryDataTransferGraphReceiverFixture NewBinaryDataTransferGraphReceiverFixture()
      {
        throw new NotImplementedException();
      }
      protected override BinaryDataTransferGraphSenderFixture NewBinaryDataTransferGraphSenderFixture()
      {
        return new BinaryDataTransferGraphSenderTest();
      }
      internal override void AssertConsistency()
      {
        Assert.AreEqual<int>(0, BinaryDataTransferGraphReceiverFixtureList.Count);
        Assert.AreEqual<int>(1, BinaryDataTransferGraphSenderFixtureList.Count);
      }
      #endregion

      private class BinaryDataTransferGraphSenderTest : BinaryDataTransferGraphSenderFixture { }

    }
    #endregion

  }

}
