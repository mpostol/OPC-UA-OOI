
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace UAOOI.Networking.UDPMessageHandler.Diagnostic
{
  internal static class UDPMessageHandlerDiagnosticExtension
  {

    internal static void ReceivedMessageContent(this UDPMessageHandlerSemanticEventSource eventSource, IPEndPoint endPoint, int length, byte[] message)
    {
      eventSource.ReceivedMessageContent(MessageContentFormat(endPoint, length, message));
    }
    internal static void SentMessageContent(this UDPMessageHandlerSemanticEventSource eventSource, IPEndPoint endPoint, int length, byte[] message)
    {
      eventSource.SentMessageContent(MessageContentFormat(endPoint, length, message));
    }
    internal static void JoiningMulticastGroup(this UDPMessageHandlerSemanticEventSource eventSource, IPAddress multicastGroup)
    {
      eventSource.JoiningMulticastGroup(multicastGroup.ToString());
    }
    internal static void ReaderUdpStatistics(this UDPMessageHandlerSemanticEventSource eventSource, UdpStatistics payload0)
    {
      eventSource.ReaderUdpStatistics(payload0.DatagramsReceived, payload0.DatagramsSent);
    }
    private static string MessageContentFormat(IPEndPoint endPoint, int length, byte[] message)
    {
      return ($"{endPoint.Address.ToString()}:{endPoint.Port} [{length}]: {String.Join(",", new ArraySegment<byte>(message, 0, Math.Min(message.Length, 80)).Select<byte, string>(x => x.ToString("X")).ToArray<string>())}");
    }

  }
}
