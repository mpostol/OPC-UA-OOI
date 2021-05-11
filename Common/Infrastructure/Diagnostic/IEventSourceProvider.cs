//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Diagnostics.Tracing;

namespace UAOOI.Common.Infrastructure.Diagnostic
{
  /// <summary>
  /// Interface IEventSourceProvider - if implemented returns an instance of <see cref="EventSource"/> to be registered by the logging infrastructure.
  /// </summary>
  public interface IEventSourceProvider
  {
    /// <summary>
    /// Gets the part event source.
    /// </summary>
    /// <returns>Returns an instance of <see cref="EventSource"/>.</returns>
    EventSource GetPartEventSource();
  }
}