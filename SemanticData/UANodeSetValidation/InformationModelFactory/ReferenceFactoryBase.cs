//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  /// <summary>
  /// Class ReferenceFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IReferenceFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IReferenceFactory" />
  internal class ReferenceFactoryBase : IReferenceFactory
  {

    /// <summary>
    /// Sets the type of the reference.
    /// </summary>
    /// <value>The type of the reference.</value>
    public XmlQualifiedName ReferenceType
    {
      set { }
    }
    /// <summary>
    /// Sets the target identifier.
    /// </summary>
    /// <value>The target identifier.</value>
    public XmlQualifiedName TargetId
    {
      set { }
    }
    /// <summary>
    /// Sets a value indicating whether this instance is inverse.
    /// </summary>
    /// <value><c>true</c> if this instance is inverse; otherwise, <c>false</c>.</value>
    public bool IsInverse
    {
      set { }
    }

  }
}
