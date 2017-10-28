using System;
using System.Net.NetworkInformation;

namespace UAOOI.Networking.UDPMessageHandler
{
  public class UdpStatisticsEventArgs : EventArgs
  {
    public UdpStatisticsEventArgs(UdpStatistics statistics)
    {
      this.UdpStatistics = statistics;
    }
    public UdpStatistics UdpStatistics { get; private set; }
  }
}