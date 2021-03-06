﻿//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Runtime.CompilerServices;

namespace UAOOI.Networking.DataRepository.DataLogger.Diagnostic
{
  /// <summary>
  /// Class <see cref="DataLoggerEventSourceExtensions"/> - expanding the <see cref="DataLoggerEventSource"/>
  /// </summary>
  internal static class DataLoggerEventSourceExtensions
  {
    /// <summary>
    /// Logs the exception using <see cref="DataLoggerEventSource" />.
    /// </summary>
    /// <param name="eventSource">The event source to be used for problem reporting.</param>
    /// <param name="className">Name of the class where the exception has been caught.</param>
    /// <param name="e">The exception to be reported.</param>
    /// <param name="methodName">Name of the method.</param>
    internal static void LogException(this DataLoggerEventSource eventSource, string className, Exception e, [CallerMemberName] string methodName = nameof(LogException))
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