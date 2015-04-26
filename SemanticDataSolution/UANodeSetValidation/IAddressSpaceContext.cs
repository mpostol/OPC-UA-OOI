
using System;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IAddressSpaceContext - 
  /// </summary>
  public interface IAddressSpaceContext
  {

    /// <summary>
    /// Analyze and imports the <see cref="UANodeSet"/> model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <param name="validation">If set to <c>true</c> the nodes are validated and progress is traced.</param>
    void ImportNodeSet(UANodeSet model, Action<TraceMessage> traceEvent, bool validation);
    /// <summary>
    /// Exports the namespace table.
    /// </summary>
    string[] ExportNamespaceTable();

  }
}
