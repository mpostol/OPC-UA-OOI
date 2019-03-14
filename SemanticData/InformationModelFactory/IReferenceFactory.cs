//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IReferenceFactory - represents nodes reference instance.
  /// </summary>
  public interface IReferenceFactory
  {

    /// <summary>
    /// Sets the type of the reference.
    /// </summary>
    /// <value>The type of the reference.</value>
    XmlQualifiedName ReferenceType
    {
      set;
    }
    /// <summary>
    /// Sets the target identifier.
    /// </summary>
    /// <value>The target identifier.</value>
    XmlQualifiedName TargetId
    {
      set;
    }

    /// <summary>
    /// Sets a value indicating whether this instance is inverse.
    /// </summary>
    /// <value><c>true</c> if this instance is inverse; otherwise, <c>false</c>.</value>
    [System.ComponentModel.DefaultValue(false)]
    bool IsInverse
    {
      set;
    }

  }
}
