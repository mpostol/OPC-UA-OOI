//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.Core;
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
      OPCUAServerProducerSimulator _producer = OPCUAServerProducerSimulator.CreateDevice(_mhf, _dataSetGuid);
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
      _mhf.CheckConsistency();
      _producer.Update("Value1", "Value1");
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
      public IBinaryDataTransferGraphReceiver GetBinaryDTGReceiver(string name, string configuration)
      {
        throw new NotImplementedException();
      }
      public IBinaryDataTransferGraphSender GetBinaryDTGSender(string name, string configuration)
      {
        Assert.AreEqual("UDP", name);
        Assert.AreEqual<string>("4840,localhost", configuration);
        return new BinaryStreamObservableFixture();
      }
      #endregion

      #region private
      private IMessageWriter MessageWriter { get; set; }
      #endregion

      #region test environment
      private class BinaryStreamObservableFixture : IBinaryDataTransferGraphSender
      {
        public IAssociationState State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AttachToNetwork()
        {
          throw new NotImplementedException();
        }
        public void SendFrame(byte[] buffer)
        {
          throw new NotImplementedException();
        }
        public void Dispose()
        {
          throw new NotImplementedException();
        }
      }
      internal void CheckConsistency()
      {
        Assert.IsNotNull(MessageWriter);
      }
      #endregion

    }
    #endregion

  }

}
