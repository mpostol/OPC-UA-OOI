//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System.Diagnostics.Tracing;

namespace UAOOI.Networking.Core
{

  /// <summary>
  /// Interface IEventSourceProvider - if implemented returns an instance of <see cref="EventSource"/> to be registered by the logging infrastructure.
  /// </summary>
  public interface INetworkingEventSourceProvider
  {

    /// <summary>
    /// Gets the part event source.
    /// </summary>
    /// <returns>Returns an instance of <see cref="EventSource"/>.</returns>
    EventSource GetPartEventSource();

  }

}
