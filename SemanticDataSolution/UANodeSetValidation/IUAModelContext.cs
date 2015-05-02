
using System;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IUAModelContext - abstracts the <see cref="UAModelContext"/> functionality.
  /// </summary>
  public interface IUAModelContext
  {

    /// <summary>
    /// Exports the <see cref="Common.Types.Argument" />.
    /// </summary>
    /// <param name="argument">The argument to be exported.</param>
    /// <param name="traceEvent">An <see cref="Action"/> delegate is used to trace event as the <see cref="TraceMessage"/>.</param>
    /// <returns>Returns an object of <see cref="Opc.Ua.ModelCompiler.Parameter" /> type derived from <see cref="Common.Types.Argument" />.</returns>
    ParameterType ExportArgument<ParameterType>(DataSerialization.Argument argument, Action<TraceMessage> traceEvent, Func<Argument, XmlQualifiedName, ParameterType> createParameter);
    /// <summary>
    /// Exports the node identifier.
    /// </summary>
    /// <param name="nodeId">The node identifier as the string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="traceEvent">An <see cref="Action"/> delegate is used to trace event as the <see cref="TraceMessage"/>.</param>
    /// <returns>The identifier an object of <see cref="System.Xml.XmlQualifiedName" /> or null if <paramref name="nodeId" /> has default value.</returns>
    XmlQualifiedName ExportNodeId(string nodeId, uint defaultValue, Action<TraceMessage> traceEvent);

  }
}
