//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.Networking.Core;

namespace UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory
{

  internal abstract class BinaryDataTransferGraphSenderFixture : BinaryDataTransferGraphBaseFixture,  IBinaryDataTransferGraphSender
  {

    #region IBinaryStreamObservable
    public void SendFrame(byte[] buffer)
    {
      m_NumberOfSentBytes += buffer.Length;
      m_NumberOfSentMessages++;
      Buffer = buffer;
    }
    #endregion

    #region instrumentation
    internal byte[] Buffer { get; private set; }
    internal int m_NumberOfSentMessages = 0;
    internal int m_NumberOfSentBytes = 0;
    #endregion

  }

}
