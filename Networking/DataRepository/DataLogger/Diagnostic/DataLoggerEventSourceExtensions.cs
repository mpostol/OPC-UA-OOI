//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.DataRepository.DataLogger.Diagnostic
{
  /// <summary>
  /// Class <see cref="DataLoggerEventSourceExtensions"/> - expanding the <see cref="DataLoggerEventSource"/>
  /// </summary>
  public static class DataLoggerEventSourceExtensions
  {
    /// <summary>
    /// Logs the exception using <see cref="DataLoggerEventSource"/>.
    /// </summary>
    /// <param name="eventSource">The event source to be used for problem reporting.</param>
    /// <param name="e">The exception to be reported.</param>
    public static void LogException(this DataLoggerEventSource eventSource, Exception e)
    {
      Exception _exception = e;
      string _innerText = "An exception has been caught:";
      while (e != null)
      {
        eventSource.Failure($"{_innerText} of type {_exception.GetType().Name} capturing the message: {e.Message}");
        e = e.InnerException;
        _innerText = "It contains inner exception:";
      }
    }
  }
}