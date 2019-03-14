//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IReferenceTypeFactory - encapsulates a reference type definition.
  /// </summary>
  /// <remarks>
  /// References are defined as instances of ReferenceType nodes. ReferenceType nodes are visible in the Address Space and are defined using the ReferenceType node class. 
  /// In contrast, a reference instance is an inherent part of a node and no node class is used to represent references.
  /// </remarks>
  public interface IReferenceTypeFactory : ITypeFactory
  {

    /// <summary>
    /// Adds a new inverse name.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    void AddInverseName(string localeField, string valueField);
    /// <summary>
    /// Sets a value indicating whether this <see cref="IReferenceTypeFactory"/> is symmetric. The Symmetric attribute is used to indicate whether or not the meaning of the reference type is the same for both the source and target nodes.
    /// If a reference type is symmetric, the InverseName attribute shall be omitted.Examples of symmetric reference types are “Connects To” and “Communicates With”. Both imply the same semantic coming from the source node or the target node.
    /// If the ReferenceType is non-symmetric and not abstract, the InverseName attribute shall be set. The optional InverseName attribute of LocalizedText ia a inverse name of the reference, 
    /// i.e.the meaning of the type as seen from the target node. Examples of non-symmetric reference types include “Contains” and “Contained In”, and “Receives From” and “Sends To”.
    /// </summary>
    /// <remarks>Default Value is <b>false</b></remarks>
    /// <value><c>true</c> if symmetric; otherwise, <c>false</c>.</value>
    bool Symmetric
    {
      set;
    }

  }
}
