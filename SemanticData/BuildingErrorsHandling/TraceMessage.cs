//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics;

namespace UAOOI.SemanticData.BuildingErrorsHandling
{
  /// <summary>
  /// Class TraceMessage.
  /// </summary>
  public class TraceMessage : IFormattable
  {
    /// <summary>
    /// Creates new instance of the <see cref="TraceMessage"/> to be used for diagnostic purpose
    /// </summary>
    /// <param name="message">The new <see cref="TraceMessage"/> containing the <paramref name="message"/>.</param>
    /// <returns>The new <see cref="TraceMessage"/> diagnostic message.</returns>
    public static TraceMessage DiagnosticTraceMessage(string message)
    {
      return new TraceMessage(BuildError.DiagnosticInformation, TraceEventType.Verbose, message);
    }
    /// <summary>
    /// creates new <see cref="TraceMessage" /> to be used for diagnostic purpose
    /// </summary>
    /// <param name="error">The <see cref="BuildError"/> to be added to the <see cref="TraceMessage"/>.</param>
    /// <param name="message">The new <see cref="TraceMessage" /> containing the <paramref name="message" />.</param>
    /// <returns>The new <see cref="TraceMessage" /> diagnostic message.</returns>
    public static TraceMessage BuildErrorTraceMessage(BuildError error, string message)
    {
      return new TraceMessage(error, TraceEventType.Information, message);
    }
    /// <summary>
    /// Gets the build error.
    /// </summary>
    /// <value>The build error.</value>
    public BuildError BuildError
    {
      get { return b_BuildError; }
      private set { b_BuildError = value; }
    }
    /// <summary>
    /// Gets the trace level.
    /// </summary>
    /// <value>The trace level.</value>
    public TraceEventType TraceLevel
    {
      get { return b_TraceLevel; }
      private set { b_TraceLevel = value; }
    }
    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <value>The message.</value>
    public string Message
    {
      get { return b_Location; }
      private set { b_Location = value; }
    }

    #region IFormattable
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation.</param>
    /// <param name="formatProvider">The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public string ToString(string format, IFormatProvider formatProvider)
    {
      //TODO implement IFormattable
      return this.ToString();
    }
    #endregion

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return String.Format("Trace: {0}, Error {1} {2}", TraceLevel, BuildError.ToString(), Message);
    }

    #region private
    private TraceMessage(BuildError buildError, TraceEventType traceLevel, string message)
    {
      BuildError = buildError;
      TraceLevel = traceLevel;
      Message = message;
    }
    private TraceEventType b_TraceLevel;
    private BuildError b_BuildError;
    private string b_Location;
    #endregion

  }
}
