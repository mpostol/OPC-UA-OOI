//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.Core;
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
      UInt32 DataSetGuid = UInt32.MaxValue;
      MyMessageHandlerFactory _mhf = new MyMessageHandlerFactory(DataSetGuid);
      ConsumerDeviceSimulator _consumer = ConsumerDeviceSimulator.CreateDevice(_mhf, DataSetGuid);
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
      _mhf.CheckConsistency();
      _mhf.SendData();
    }

    #region private
    private class MyMessageHandlerFactory : IMessageHandlerFactory
    {
      #region creator
      internal MyMessageHandlerFactory(UInt32 dataSetGuid)
      {
        this.MyMessageReader = new MessageReader(dataSetGuid);
      }
      #endregion

      #region IMessageHandlerFactory
      public IBinaryDataTransferGraphReceiver GetBinaryDTGReceiver(string name, string configuration)
      {
        Assert.AreEqual("UDP", name);
        Assert.IsNull(configuration);
        return new BinaryStreamObserverFixture();
      }
      public IBinaryDataTransferGraphSender GetBinaryDTGSender(string name, string configuration)
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
      private class BinaryStreamObserverFixture : IBinaryDataTransferGraphReceiver
      {
        public IAssociationState State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<byte[]> OnNewFrameArrived;

        public void AttachToNetwork()
        {
          throw new NotImplementedException();
        }

        public void Dispose()
        {
          throw new NotImplementedException();
        }
      }
      private MessageReader MyMessageReader { get; set; }
      #endregion
    }
    #endregion
  }

}
