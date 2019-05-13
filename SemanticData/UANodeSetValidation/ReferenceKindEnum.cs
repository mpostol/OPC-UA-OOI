//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
