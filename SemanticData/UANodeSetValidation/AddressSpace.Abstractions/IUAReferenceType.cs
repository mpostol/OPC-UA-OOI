//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface IUAReferenceType - encapsulates a reference type definition.
  /// </summary>
  /// <remarks>
  /// References are defined as instances of ReferenceType nodes. ReferenceType nodes are visible in the Address Space and are defined using the ReferenceType node class.
  /// In contrast, a reference instance is an inherent part of a node and no node class is used to represent references.
  /// See also 5.3 ReferenceType NodeClass
  /// </remarks>
  public interface IUAReferenceType : IUAType
  {
    /// <summary>
    /// If a ReferenceType is symmetric, the InverseName Attribute shall be omitted. Therefore both directions are considered to be forward References.
    /// If the ReferenceType is non-symmetric and not abstract, the InverseName Attribute shall be set. The InverseName Attribute specifies the meaning of the ReferenceType as seen from the
    /// target node.
    /// </summary>
    LocalizedText[] InverseName { get; }

    /// <summary>
    /// The Symmetric Attribute is used to indicate whether or not the meaning of the ReferenceType is the same for both the source and target nodes.
    /// </summary>
    bool Symmetric { get; set; }
  }
}