using System;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class ServiceResultException.
  /// </summary>
  [Serializable]
  public class ServiceResultException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceResultException"/> class.
    /// </summary>
    public ServiceResultException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceResultException"/> class.
    /// </summary>
    /// <param name="traceMessage">The trace message.</param>
    /// <param name="message">The message.</param>
    public ServiceResultException(TraceMessage traceMessage, string message)
      : base(message)
    {
      TraceMessage = traceMessage;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceResultException"/> class.
    /// </summary>
    /// <param name="traceMessage">The trace message.</param>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public ServiceResultException(TraceMessage traceMessage, string message, Exception inner)
      : base(message, inner)
    {
      TraceMessage = traceMessage;
    }
    public TraceMessage TraceMessage { get; private set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceResultException" /> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected ServiceResultException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      throw new NotImplementedException();
    }

  }

}
