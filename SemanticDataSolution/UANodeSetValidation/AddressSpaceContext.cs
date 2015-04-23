
using System;
using System.Diagnostics;
using System.IO;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class AddressSpaceContext - stub class - TBD
  /// </summary>
  public class AddressSpaceContext<ModelDesignType> : IAddressSpaceContext
  {

    /// <summary>
    /// Creates the instance of the address space.
    /// </summary>
    /// <param name="targetNamespace">The target namespace.</param>
    /// <param name="getNodesFromModel">An action called to get nodes from the information model.</param>
    /// <param name="traceEvent">An action to trace the compilation errors.</param>
    /// <returns>An instance of <see cref="ModelDesign.ModelDesign"/> containing the model.</returns>
    public ModelDesignType CreateInstance(string targetNamespace, Action<IAddressSpaceContext> getNodesFromModel, Action<TraceMessage> traceEvent)
    {
      traceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.CreateInstance"));
      m_NamespaceTable.Append(targetNamespace);
      return InternalCreateInstance(getNodesFromModel, traceEvent);
    }
    /// <summary>
    /// Creates the instance.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">filePath;The imported file does not exist</exception>
    public ModelDesignType CreateInstance(FileInfo filePath, Action<TraceMessage> traceEvent)
    {
      if (!filePath.Exists)
        throw new FileNotFoundException("The imported file does not exist", filePath.FullName);
      traceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.CreateInstance"));
      UANodeSet _nodeSet = UANodeSet.ReadXmlFile(filePath.FullName);
      if (_nodeSet.ServerUris != null)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (_nodeSet.Extensions != null)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions is omitted during the import"));
      m_NamespaceTable.Append(_nodeSet.NamespaceUris == null ? Opc.Ua.Namespaces.OpcUa : _nodeSet.NamespaceUris[0]);
      return InternalCreateInstance(context => context.ImportNodeSet(_nodeSet, traceEvent, true), traceEvent);
    }

    #region IAddressSpaceContext
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

    #endregion

    #region private
    private NamespaceTable m_NamespaceTable = new NamespaceTable();
    /// <summary>
    /// Create instance internally.
    /// </summary>
    /// <param name="getNodesFromModel">The action to get nodes from model.</param>
    /// <param name="traceEvent">The action to trace events.</param>
    /// <returns></returns>
    private ModelDesignType InternalCreateInstance(Action<IAddressSpaceContext> getNodesFromModel, Action<TraceMessage> traceEvent)
    {
      traceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext.InternalCreateInstance - starting address space compilation."));
      UANodeSet _standard = Extensions.LoadResource<UANodeSet>(Extensions.UADefinedTypesName);
      Debug.Assert(_standard != null);
      ImportNodeSet(_standard, traceEvent, false);
      getNodesFromModel(this);
      return CreateModelDesign(traceEvent);
    }
    private ModelDesignType CreateModelDesign(Action<TraceMessage> traceEvent)
    {
      throw new NotImplementedException();
    }
    #endregion

  }
}
