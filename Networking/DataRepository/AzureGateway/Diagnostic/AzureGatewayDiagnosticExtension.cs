//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Linq;
using System.Net;

namespace UAOOI.Networking.DataRepository.AzureGateway.Diagnostic
{
  internal static class AzureGatewayDiagnosticExtension
  {
    internal static void ReceivedMessageContent(this AzureGatewaySemanticEventSource eventSource, IPEndPoint endPoint, int length, byte[] message)
    {
      eventSource.ReceivedMessageContent(MessageContentFormat(endPoint, length, message));
    }

    internal static void SentMessageContent(this AzureGatewaySemanticEventSource eventSource, IPEndPoint endPoint, int length, byte[] message)
    {
      eventSource.SentMessageContent(MessageContentFormat(endPoint, length, message));
    }

    internal static void JoiningMulticastGroup(this AzureGatewaySemanticEventSource eventSource, IPAddress multicastGroup)
    {
      eventSource.JoiningMulticastGroup(multicastGroup.ToString());
    }

    /// <summary>
    /// Logs the exception using <see cref="AzureGatewaySemanticEventSource" />.
    /// </summary>
    /// <param name="eventSource">The event source to be used for problem reporting.</param>
    /// <param name="className">Name of the class.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="e">The exception to be reported.</param>
    internal static void LogException(this AzureGatewaySemanticEventSource eventSource, string className, string methodName, Exception e)
    {
      Exception _exception = e;
      string _innerText = "An exception has been caught:";
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