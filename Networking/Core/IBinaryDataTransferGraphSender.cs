//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.Core
{

  public interface IBinaryDataTransferGraphSender : IDisposable
  {

    void AttachToNetwork();
    void SendFrame(byte[] buffer);
    IAssociationState State { get; set; }

  }

}
