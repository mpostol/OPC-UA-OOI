//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;
using UAOOI.Networking.Core;

namespace UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory
{
  internal abstract class MessageHandlerFactoryFixture : IMessageHandlerFactory
  {

    public MessageHandlerFactoryFixture()
    {
      BinaryDataTransferGraphReceiverFixtureList = new List<BinaryDataTransferGraphReceiverFixture>();
      BinaryDataTransferGraphSenderFixtureList = new List<BinaryDataTransferGraphSenderFixture>();
    }

    #region IMessageHandlerFactory
    public IBinaryDataTransferGraphReceiver GetBinaryDTGReceiver(string name, string configuration)
    {
      BinaryDataTransferGraphReceiverFixture _newFixture = NewBinaryDataTransferGraphReceiverFixture();
      BinaryDataTransferGraphReceiverFixtureList.Add(_newFixture);
      return _newFixture;
    }
    public IBinaryDataTransferGraphSender GetBinaryDTGSender(string name, string configuration)
    {
      BinaryDataTransferGraphSenderFixture _newFixture = NewBinaryDataTransferGraphSenderFixture();
      BinaryDataTransferGraphSenderFixtureList.Add(_newFixture);
      return _newFixture;
    }
    #endregion

    internal virtual void AssertConsistency()
    {
      foreach (BinaryDataTransferGraphReceiverFixture item in BinaryDataTransferGraphReceiverFixtureList)
        item.AssertConsistency();
      foreach (BinaryDataTransferGraphSenderFixture item in BinaryDataTransferGraphSenderFixtureList)
        item.AssertConsistency();
    }

    protected abstract BinaryDataTransferGraphReceiverFixture NewBinaryDataTransferGraphReceiverFixture();
    protected abstract BinaryDataTransferGraphSenderFixture NewBinaryDataTransferGraphSenderFixture();
    protected static List<BinaryDataTransferGraphReceiverFixture> BinaryDataTransferGraphReceiverFixtureList = new List<BinaryDataTransferGraphReceiverFixture>();
    protected static List<BinaryDataTransferGraphSenderFixture> BinaryDataTransferGraphSenderFixtureList = new List<BinaryDataTransferGraphSenderFixture>();

  }

}
