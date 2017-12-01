
using System;
using System.Linq;
using System.Net;

namespace UAOOI.Networking.UDPMessageHandler.Diagnostic
{
  internal static class UDPMessageHandlerDiagnosticExtension
  {
    internal static void MessageContent(this UDPMessageHandlerSemanticEventSource eventSource, IPEndPoint endPoint, int length, byte[] message)
    {
      string _content =
        ($"{endPoint.Address.ToString()}:{endPoint.Port} [{length}]: {String.Join(",", new ArraySegment<byte>(message, 0, Math.Min(message.Length, 80)).Select<byte, string>(x => x.ToString("X")).ToArray<string>())}");
      eventSource.MessageContent(_content);
    }
    internal static void JoiningMulticastGroup(this UDPMessageHandlerSemanticEventSource eventSource, IPAddress multicastGroup)
    {
      eventSource.JoiningMulticastGroup(multicastGroup.ToString());
    }

  }
}
