//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Enum ReferenceKindEnum
  /// </summary>
  internal enum ReferenceKindEnum
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
    /// The HasSubtype
    /// </summary>
    HasSubtype,

    /// <summary>
    /// The HasProperty
    /// </summary>
    HasProperty
  };
}