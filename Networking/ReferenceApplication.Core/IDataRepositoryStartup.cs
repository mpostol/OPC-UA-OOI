//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.ReferenceApplication.Core
{
  /// <summary>
  /// Interface IDataRepositoryStartup - a contract to be used by IoC container to get DataRepository parts.
  /// Implements the <see cref="System.IDisposable" />
  /// </summary>
  /// <seealso cref="System.IDisposable" />
  public interface IDataRepositoryStartup : IDisposable
  {
    void Setup();
  }
}
