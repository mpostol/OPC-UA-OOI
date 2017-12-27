
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.SemanticData.Diagnostics
{
  /// <summary>
  /// Class NetworkingEventSourceProvider - gets access to an instance of <see cref="EventSource"/> to be registered by the logging infrastructure.
  /// </summary>
  /// <seealso cref="UAOOI.Networking.SemanticData.Diagnostics.INetworkingEventSourceProvider" />
  [Export(typeof(INetworkingEventSourceProvider))]
  public class NetworkingEventSourceProvider : INetworkingEventSourceProvider
  {
    /// <summary>
    /// Gets the part event source.
    /// </summary>
    /// <returns>Returns an instance of <see cref="EventSource" />.</returns>
    public EventSource GetPartEventSource()
    {
      return ReactiveNetworkingEventSource.Log;
    }
  }
}
