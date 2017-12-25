
using System.Net.NetworkInformation;

namespace UAOOI.Networking.SemanticData.Diagnostics
{
  internal static class ReactiveNetworkingEventSourceExtensions
  {
    internal static void UdpStatistics(this ReactiveNetworkingEventSource eventSource, UdpStatistics udpStatistics)
    {
      eventSource.UdpStatistics(udpStatistics.DatagramsReceived, udpStatistics.DatagramsSent);
    }
  }
}
