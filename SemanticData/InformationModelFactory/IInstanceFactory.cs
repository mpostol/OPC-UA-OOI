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
  /// Interface IInstanceFactory - It encapsulates definition of an instance.
  /// </summary>
  public interface IInstanceFactory : INodeFactory
  {

    /// <summary>
    /// Sets the modeling rule, which defines whether the component of a complex type are instantiated. 
    /// This value is defined by processing the object pointed by the HasModelingRule reference.
    /// </summary>
    /// <value>The modeling rule.</value>
    ModelingRules? ModelingRule
    {
      set;
    }
    /// <summary>
    /// Sets the type definition.
    /// </summary>
    /// <value>The type definition.</value>
    XmlQualifiedName TypeDefinition
    {
      set;
    }
    /// <summary>
    /// Sets the type of the reference if it is component of a complex definition.
    /// </summary>
    /// <value>The type of the reference used for parent child relationship.</value>
    XmlQualifiedName ReferenceType { set; }

  }
}
