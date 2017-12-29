
using System;

namespace UAOOI.Networking.ReferenceApplication.Diagnostic
{
  /// <summary>
  /// Class ReferenceApplicationEventSourceExtensions - expanding the <see cref="ReferenceApplicationEventSource"/> 
  /// </summary>
  internal static class ReferenceApplicationEventSourceExtensions
  {
    /// <summary>
    /// Logs the exception using <see cref="ReferenceApplicationEventSource"/>.
    /// </summary>
    /// <param name="eventSource">The event source source to be used for problem reporting.</param>
    /// <param name="e">The exception to be reported.</param>
    internal static void LogException (this ReferenceApplicationEventSource eventSource, Exception e)
    {
      Exception _exception = e;
      string _innerText = "An exception has benn caught:";
      while (e != null)
      {
        eventSource.Failure($"{_innerText} of type {_exception.GetType().Name} capturing the message: {e.Message}");
        e = e.InnerException;
        _innerText = "It contains inner exception:";
      }
    }
  }
}
