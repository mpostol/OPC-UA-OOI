
using System;
using System.Linq;
using System.Net;

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
    /// <summary>
    /// Logs the exception using <see cref="ReferenceApplicationEventSource"/>.
    /// </summary>
    /// <param name="eventSource">The event source source to be used for problem reporting.</param>
    /// <param name="e">The exception to be reported.</param>
    internal static void LogException(this UDPMessageHandlerSemanticEventSource eventSource, string className, string methodName, Exception e)
    {
      Exception _exception = e;
      string _innerText = "An exception has benn caught:";
      while (e != null)
      {
        eventSource.Failure(className, methodName, $"{_innerText} of type {_exception.GetType().Name} capturing the message: {e.Message}");
        e = e.InnerException;
        _innerText = "It contains inner exception:";
      }
    }
    private static string MessageContentFormat(IPEndPoint endPoint, int length, byte[] message)
    {
      return ($"{endPoint.Address.ToString()}:{endPoint.Port} [{length}]: {String.Join(",", new ArraySegment<byte>(message, 0, Math.Min(message.Length, 80)).Select<byte, string>(x => x.ToString("X")).ToArray<string>())}");
    }

  }
}
