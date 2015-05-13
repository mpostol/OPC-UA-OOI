﻿
using System;
using System.Collections.Generic;
using System.IO;
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
    /// <param name="validation">If set to <c>true</c> the nodes are validated and progress is traced.</param>
    void ImportNodeSet(UANodeSet model, bool validation);
    /// <summary>
    /// Validate the address space containing the imported <see cref="UANodeSet"/> models.
    /// </summary>
    /// <param name="targetNamespace">The target namespace.</param>
    /// <param name="getNodesFromModel">Encapsulates an action called to get nodes from the <see cref="UANodeSet" /> model.</param>
    /// <param name="factory">A factory used to export validated model.</param>
    void ImportUANodeSet(string targetNamespace, Action<IAddressSpaceContext> getNodesFromModel, IExportModelFactory exportFactory);
    void ImportUANodeSet(IEnumerable<FileInfo> paths, IExportModelFactory factory);
    IExportModelFactory InformationModelFactory { set; }
  }
}
