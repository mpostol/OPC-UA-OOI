
using System;
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Enum ReferenceKindEnum
  /// </summary>
  public enum ReferenceKindEnum 
  {
    /// <summary>
    /// The custom reference
    /// </summary>
    Custom,
    /// <summary>
    /// The HasComponent 
    /// </summary>
    HasComponent,
    /// <summary>
    /// The HasModellingRule
    /// </summary>
    HasModellingRule,
    /// <summary>
    /// The HasTypeDefinition
    /// </summary>
    HasTypeDefinition,
    /// <summary>
    /// The HierarchicalReferences
    /// </summary>
    HierarchicalReferences,
    /// <summary>
    /// The HasSubtype
    /// </summary>
    HasSubtype,
    /// <summary>
    /// The HasProperty
    /// </summary>
    HasProperty 
  };

}
