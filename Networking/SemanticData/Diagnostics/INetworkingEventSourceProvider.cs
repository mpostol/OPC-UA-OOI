
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;

namespace UAOOI.Networking.SemanticData.Diagnostics
{
  /// <summary>
  /// Interface IEventSourceProvider - if implemented returns an instance of <see cref="EventSource"/> to be registered by the logging infrastructure.
  /// </summary>
  public interface INetworkingEventSourceProvider
  {

    /// <summary>
    /// Gets the part event source.
    /// </summary>
    /// <returns>An instance of <see cref="EventSource"/>.</returns>
    EventSource GetPartEventSource();

  }
  [Export(typeof(INetworkingEventSourceProvider))]
  public class NetworkingEventSourceProvider : INetworkingEventSourceProvider
  {
    public EventSource GetPartEventSource()
    {
      return ReactiveNetworkingEventSource.Log;
    }
  }

}
