
using System;
using System.Diagnostics;
using System.IO;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class AddressSpaceContext - stub class - TBD
  /// </summary>
  public class AddressSpaceContext<ModelDesignType> : IAddressSpaceContext
  {
    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSpaceContext{ModelDesignType}"/> class.
    /// </summary>
    /// <param name="traceEvent">An action to trace the compilation errors.</param>
    public AddressSpaceContext(Action<TraceMessage> traceEvent)
    {
      if (traceEvent != null)
        m_TraceEvent = traceEvent;
      m_NamespaceTable = new NamespaceTable(traceEvent);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContext - starting address space compilation."));
      UANodeSet _standard = UANodeSet.ReadUADefinedTypes();
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext - uploading the UA defined types."));
      Debug.Assert(_standard != null);
      ImportNodeSet(_standard, false);
    }
    #endregion    

    /// <summary>
    /// Creates the instance of the address space.
    /// </summary>
    /// <param name="targetNamespace">The target namespace.</param>
    /// <param name="getNodesFromModel">An action called to get nodes from the information model.</param>
    /// <returns>An instance of <see cref="ModelDesign.ModelDesign"/> containing the model.</returns>
    public ModelDesignType CreateInstance(string targetNamespace, Action<IAddressSpaceContext> getNodesFromModel)
    {
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.CreateInstance"));
      m_NamespaceTable.Append(targetNamespace, m_TraceEvent);
      return InternalCreateInstance(getNodesFromModel);
    }
    /// <summary>
    /// Creates the instance.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">filePath;The imported file does not exist</exception>
    public ModelDesignType CreateInstance(FileInfo filePath)
    {
      if (!filePath.Exists)
        throw new FileNotFoundException("The imported file does not exist", filePath.FullName);
      m_TraceEvent(TraceMessage.DiagnosticTraceMessage("Entering AddressSpaceContextService.CreateInstance"));
      UANodeSet _nodeSet = UANodeSet.ReadXmlFile(filePath.FullName);
      if (_nodeSet.ServerUris != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "ServerUris is omitted during the import"));
      if (_nodeSet.Extensions != null)
        m_TraceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Extensions is omitted during the import"));
      m_NamespaceTable.Append(_nodeSet.NamespaceUris == null ? Namespaces.OpcUa : _nodeSet.NamespaceUris[0], m_TraceEvent);
      return InternalCreateInstance(context => context.ImportNodeSet(_nodeSet, true));
    }

    #region IAddressSpaceContext
    /// <summary>
    /// Analyze and imports the <see cref="UANodeSet" /> model.
    /// </summary>
    /// <param name="model">The model to be imported.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <param name="validation">If set to <c>true</c> the nodes are validated and progress is traced.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void ImportNodeSet(UANodeSet model, bool validation)
    {
      //string _namespace = model.NamespaceUris == null ? m_NamespaceTable.GetString(0) : model.NamespaceUris[0];
      //traceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Entering AddressSpaceContext.ImportNodeSet - starting import {0}.", _namespace)));
      //UAModelContext _modelContext = new UAModelContext(model.Aliases, model.NamespaceUris, this);
      //traceEvent(TraceMessage.DiagnosticTraceMessage("AddressSpaceContext.ImportNodeSet - context for imported model is created and starting import nodes."));
      //foreach (UANode _nd in model.Items)
      //  this.ImportUANode(_nd, _modelContext, validation ? traceEvent : x => { });
      //traceEvent(TraceMessage.DiagnosticTraceMessage(String.Format("Finishing AddressSpaceContext.ImportNodeSet - imported {0} nodes.", model.Items.Length)));
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
    private NamespaceTable m_NamespaceTable = null;
    /// <summary>
    /// Create instance internally.
    /// </summary>
    /// <param name="getNodesFromModel">The action to get nodes from model.</param>
    /// <param name="traceEvent">The action to trace events.</param>
    /// <returns></returns>
    private ModelDesignType InternalCreateInstance(Action<IAddressSpaceContext> getNodesFromModel)
    {
      getNodesFromModel(this);
      return CreateModelDesign(m_TraceEvent);
    }
    private ModelDesignType CreateModelDesign(Action<TraceMessage> traceEvent)
    {
      throw new NotImplementedException();
    }
    private Action<TraceMessage> m_TraceEvent = x => { };
    #endregion

  }
}
