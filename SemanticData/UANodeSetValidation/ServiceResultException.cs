//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using UAOOI.SemanticData.BuildingErrorsHandling;

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
    /// <summary>
    /// Gets the trace message.
    /// </summary>
    /// <value>The trace message.</value>
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
    /// <summary>
    /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      //TODO implement GetObjectData
    }
  }

}

