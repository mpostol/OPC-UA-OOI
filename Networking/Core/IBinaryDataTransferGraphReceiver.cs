//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;

namespace UAOOI.Networking.Core
{
  public interface IBinaryDataTransferGraphReceiver : IDisposable
  {

    event EventHandler<byte[]> OnNewFrameArrived;
    IAssociationState State { get; set; }
    void AttachToNetwork();

  }
}