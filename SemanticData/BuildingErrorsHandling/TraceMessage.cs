//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Diagnostics;

namespace UAOOI.SemanticData.BuildingErrorsHandling
{
  /// <summary>
  /// Class TraceMessage - supports formatting of the warnings for the tracing purpose
  /// </summary>
  public class TraceMessage : IFormattable
  {
    #region public API

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
      return new TraceMessage(error, TraceEventType.Warning, message);
    }

    /// <summary>
    /// Gets the build error.
    /// </summary>
    /// <value>The build error.</value>
    public BuildError BuildError
    {
      get => b_BuildError;
      private set => b_BuildError = value;
    }

    /// <summary>
    /// Gets the trace level.
    /// </summary>
    /// <value>The trace level.</value>
    public TraceEventType TraceLevel
    {
      get => b_TraceLevel;
      private set => b_TraceLevel = value;
    }

    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <value>The message.</value>
    public string Message
    {
      get => b_Location;
      private set => b_Location = value;
    }

    #endregion public API

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
      return ToString();
    }

    #endregion IFormattable

    #region object

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return string.Format("Trace: {0}, Error {1} {2}", TraceLevel, BuildError.ToString(), Message);
    }

    #endregion object

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

    #endregion private
  }
}