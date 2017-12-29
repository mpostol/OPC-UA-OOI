
using System;

namespace UAOOI.Networking.SemanticData.Diagnostics
{
  internal static class ReactiveNetworkingEventSourceExtensions
  {
    /// <summary>
    /// Logs the exception using <see cref="ReactiveNetworkingEventSource" />.
    /// </summary>
    /// <param name="eventSource">The event source source to be used for problem reporting.</param>
    /// <param name="className">Name of the class.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="e">The exception to be reported.</param>
    internal static void LogException(this ReactiveNetworkingEventSource eventSource, string className, string methodName, Exception e)
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

  }
}
