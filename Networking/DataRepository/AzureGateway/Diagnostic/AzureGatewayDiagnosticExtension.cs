//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Runtime.CompilerServices;

namespace UAOOI.Networking.DataRepository.AzureGateway.Diagnostic
{
  /// <summary>
  /// Class <see cref="AzureGatewayDiagnosticExtension"/> - expanding the <see cref="AzureGatewaySemanticEventSource"/>
  /// </summary>
  internal static class AzureGatewayDiagnosticExtension
  {
    /// <summary>
    /// Logs the exception using <see cref="AzureGatewaySemanticEventSource" />.
    /// </summary>
    /// <param name="eventSource">The event source to be used for problem reporting.</param>
    /// <param name="className">Name of the class.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="e">The exception to be reported.</param>
    internal static void LogException(this AzureGatewaySemanticEventSource eventSource, string className, Exception e, [CallerMemberName] string methodName = nameof(LogException))
    {
      Exception _exception = e;
      string _innerText = "An exception has been caught:";
      while (e != null)
      {
        eventSource.ProgramFailure(className, methodName, $"{_innerText} of type {_exception.GetType().Name} capturing the message: {e.Message}");
        e = e.InnerException;
        _innerText = "It contains inner exception:";
      }
    }
  }
}