
using System.ComponentModel.Composition;
using System.Diagnostics.Tracing;
using UAOOI.Networking.SemanticData.Diagnostics;

namespace UAOOI.Networking.ReferenceApplication.Diagnostic
{
  /// <summary>
  /// Class NetworkingEventSourceProvider - gets access to an instance of <see cref="EventSource"/> to be registered by the logging infrastructure.
  /// </summary>
  /// <seealso cref="UAOOI.Networking.SemanticData.Diagnostics.INetworkingEventSourceProvider" />
  [Export(typeof(INetworkingEventSourceProvider))]
  public class NetworkingEventSourceProvider : INetworkingEventSourceProvider
  {

    #region INetworkingEventSourceProvider
    /// <summary>
    /// Gets the part event source.
    /// </summary>
    /// <returns>Returns an instance of <see cref="T:System.Diagnostics.Tracing.EventSource" />.</returns>
    public EventSource GetPartEventSource()
    {
      return ReferenceApplicationEventSource.Log;
    }
    #endregion

  }
}
