//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.ReferenceApplication.Core
{
  public interface IDataRepositoryStartup : IDisposable
  {
    void Setup();
  }
}
