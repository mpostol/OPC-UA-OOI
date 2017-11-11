
using System;
using System.Collections.Generic;

namespace UAOOI.Networking.ReferenceApplication.MEF
{
  /// <summary>
  /// Class that provides extension methods for the Exception class. These extension methods provide
  /// a mechanism for developers to get more easily to the root cause of an exception, especially in combination with 
  /// DI-containers such as Unity. 
  /// </summary>
  internal static class ExceptionExtensions
  {

    /// <summary>
    /// Register the type of an Exception that is thrown by the framework. The <see cref="GetRootException"/> method uses
    /// this list of Exception types to find out if something has gone wrong.  
    /// </summary>
    /// <param name="frameworkExceptionType">The type of exception to register.</param>
    public static void RegisterFrameworkExceptionType(Type frameworkExceptionType)
    {
      if (frameworkExceptionType == null)
        throw new ArgumentNullException(nameof(frameworkExceptionType));
      if (!m_FrameworkExceptionTypes.Contains(frameworkExceptionType))
        m_FrameworkExceptionTypes.Add(frameworkExceptionType);
    }
    /// <summary>
    /// Determines whether the exception type is already registered using the <see cref="RegisterFrameworkExceptionType"/> 
    /// method
    /// </summary>
    /// <param name="frameworkExceptionType">The type of framework exception to find.</param>
    /// <returns>
    /// 	<c>true</c> if the exception type is already registered; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFrameworkExceptionRegistered(Type frameworkExceptionType)
    {
      return m_FrameworkExceptionTypes.Contains(frameworkExceptionType);
    }
    /// <summary>
    /// Looks at all the inner exceptions of the <paramref name="exception"/> parameter to find the 
    /// most likely root cause of the exception. This works by skipping all registered exception types.
    /// </summary>
    /// <remarks>
    /// This method is not 100% accurate and should only be used to point a developer into the most likely direction.
    /// It should not be used to replace the Inner Exception stack of an exception, because this might hide required exception
    /// information. 
    /// </remarks>
    /// <param name="exception">The exception that will provide the list of inner exceptions to examine.</param>
    /// <returns>
    /// The exception that most likely caused the exception to occur. If it can't find the root exception, it will return the 
    /// <paramref name="exception"/> value itself.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We have to catch exception. This method is used in exception handling code, so it must not fail.")]
    public static Exception GetRootException(this Exception exception)
    {
      Exception _rootException = exception;
      try
      {
        while (true)
        {
          if (_rootException == null)
          {
            _rootException = exception;
            break;
          }
          if (!IsFrameworkException(_rootException))
            break;
          _rootException = _rootException.InnerException;
        }
      }
      catch (Exception)
      {
        _rootException = exception;
      }
      return _rootException;
    }

    #region private
    private static List<Type> m_FrameworkExceptionTypes = new List<Type>();
    private static bool IsFrameworkException(Exception exception)
    {
      bool _isFrameworkException = m_FrameworkExceptionTypes.Contains(exception.GetType());
      bool _childIsFrameworkException = false;
      if (exception.InnerException != null)
        _childIsFrameworkException = m_FrameworkExceptionTypes.Contains(exception.InnerException.GetType());
      return _isFrameworkException || _childIsFrameworkException;
    }
    #endregion

  }
}
