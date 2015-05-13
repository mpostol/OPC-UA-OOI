
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
    /// Validate the address space containing the imported <see cref="UANodeSet"/> models.
    /// </summary>
    /// <param name="targetNamespace">The target namespace.</param>
    /// <param name="getNodesFromModel">Encapsulates an action called to get nodes from the <see cref="UANodeSet" /> model.</param>
    /// <param name="factory">A factory used to export validated model.</param>
    void ImportUANodeSet(UANodeSet model);
    void ImportUANodeSet(IEnumerable<FileInfo> paths);
    IExportModelFactory InformationModelFactory { set; }
    void ValidateAndExportModel(string targetNamespace);

  }
}
