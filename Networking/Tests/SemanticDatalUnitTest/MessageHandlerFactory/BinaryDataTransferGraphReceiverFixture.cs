//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Networking.Core;

namespace UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory
{
  internal abstract class BinaryDataTransferGraphReceiverFixture : BinaryDataTransferGraphBaseFixture, IBinaryDataTransferGraphReceiver
  {


    #region IBinaryDataTransferGraphReceiver
    public event EventHandler<byte[]> OnNewFrameArrived;
    #endregion

    internal void SendUDPMessage(byte[] buffer, uint semanticData)
    {
      OnNewFrameArrived.Invoke(this, buffer);
      m_NumberOfSentMessages++;
      m_NumberOfSentBytes += buffer.Length;
      m_SemanticData = semanticData;
    }

    #region tetst instrumentation
    internal uint m_SemanticData;
    internal int m_NumberOfSentBytes = 0;
    internal int m_NumberOfSentMessages = 0;
    #endregion

  }
}
