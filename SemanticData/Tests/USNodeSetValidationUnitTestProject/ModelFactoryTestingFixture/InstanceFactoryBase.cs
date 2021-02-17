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
  /// Class InstanceFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.NodeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IInstanceFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.NodeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IInstanceFactory" />
  internal class InstanceFactoryBase : NodeFactoryBase, IInstanceFactory
  {
    /// <summary>
    /// Sets the modeling rule, which defines whether the component of a complex type are instantiated.
    /// This value is defined by processing the object pointed by the HasModelingRule reference.
    /// </summary>
    /// <value>The modeling rule.</value>
    public ModelingRules? ModelingRule
    {
      set { }
    }

    /// <summary>
    /// Sets the type definition.
    /// </summary>
    /// <value>The type definition.</value>
    public System.Xml.XmlQualifiedName TypeDefinition
    {
      set { }
    }

    /// <summary>
    /// Sets the type of the reference if it is component of a complex definition.
    /// </summary>
    /// <value>The type of the reference used for parent child relationship.</value>
    public System.Xml.XmlQualifiedName ReferenceType
    {
      set { }
    }
  }
}