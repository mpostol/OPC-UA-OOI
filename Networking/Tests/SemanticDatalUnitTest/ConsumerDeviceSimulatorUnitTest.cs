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
  public class ConsumerDeviceSimulatorUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_ConsumerDeviceSimulator")]
    public void ConsumerDeviceSimulatorTestMethod()
    {
      uint DataSetGuid = uint.MaxValue;
      MessageHandlerFactoryTest _mhf = new MessageHandlerFactoryTest();
      using (ConsumerDeviceSimulator _consumer = ConsumerDeviceSimulator.CreateDevice(_mhf, DataSetGuid))
      {
        Assert.IsNull(_consumer.AssociationsCollection);
        Assert.IsNotNull(_consumer.BindingFactory);
        Assert.IsNotNull(_consumer.ConfigurationFactory);
        Assert.IsNotNull(_consumer.EncodingFactory);
        Assert.IsNotNull(_consumer.MessageHandlerFactory);
        Assert.IsNull(_consumer.MessageHandlersCollection);
        _consumer.InitializeAndRun();
        Assert.AreEqual<int>(1, _consumer.AssociationsCollection.Count);
        Assert.AreEqual<int>(1, _consumer.MessageHandlersCollection.Count);
        _consumer.CheckConsistency();
      }
      _mhf.AssertConsistency();
    }

    private class MessageHandlerFactoryTest : MessageHandlerFactoryFixture
    {

      #region testing environment
      protected override BinaryDataTransferGraphReceiverFixture NewBinaryDataTransferGraphReceiverFixture()
      {
        return new BinaryDataTransferGraphReceiverTest();
      }
      protected override BinaryDataTransferGraphSenderFixture NewBinaryDataTransferGraphSenderFixture()
      {
        throw new NotImplementedException();
      }
      #endregion

      private class BinaryDataTransferGraphReceiverTest : BinaryDataTransferGraphReceiverFixture { }
    }
  }
}


