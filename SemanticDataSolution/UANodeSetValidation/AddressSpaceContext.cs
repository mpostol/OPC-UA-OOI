
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  
  /// <summary>
  /// Class AddressSpaceContext - stub class - TBD
  /// </summary>
  public class AddressSpaceContext : IAddressSpaceContext
  {
    /// <summary>
    /// Analyze and imports the <see cref="UANodeSet" /> model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <param name="validation">If set to <c>true</c> the nodes are validated and progress is traced.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void ImportNodeSet(UANodeSet model, Action<TraceMessage> traceEvent, bool validation)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Exports the namespace table.
    /// </summary>
    /// <returns>System.String[].</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public string[] ExportNamespaceTable()
    {
      throw new NotImplementedException();
    }
  }
}
