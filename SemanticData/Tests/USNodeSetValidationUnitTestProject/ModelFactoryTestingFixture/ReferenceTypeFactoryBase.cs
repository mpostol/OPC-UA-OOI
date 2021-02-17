//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.ModelFactoryTestingFixture
{
  /// <summary>
  /// Class ReferenceTypeFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.TypeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IReferenceTypeFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.TypeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IReferenceTypeFactory" />
  internal class ReferenceTypeFactoryBase : TypeFactoryBase, IReferenceTypeFactory
  {
    /// <summary>
    /// Sets a value indicating whether this <see cref="T:UAOOI.SemanticData.InformationModelFactory.IReferenceTypeFactory" /> is symmetric. The Symmetric attribute is used to indicate whether or not the meaning of the reference type is the same for both the source and target nodes.
    /// If a reference type is symmetric, the InverseName attribute shall be omitted.Examples of symmetric reference types are “Connects To” and “Communicates With”. Both imply the same semantic coming from the source node or the target node.
    /// If the ReferenceType is non-symmetric and not abstract, the InverseName attribute shall be set. The optional InverseName attribute of LocalizedText ia a inverse name of the reference,
    /// i.e.the meaning of the type as seen from the target node. Examples of non-symmetric reference types include “Contains” and “Contained In”, and “Receives From” and “Sends To”.
    /// </summary>
    /// <value><c>true</c> if symmetric; otherwise, <c>false</c>.</value>
    /// <remarks>Default Value is <b>false</b></remarks>
    public bool Symmetric
    {
      set { }
    }

    /// <summary>
    /// Adds a new inverse name.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    public void AddInverseName(string localeField, string valueField) { }
  }
}