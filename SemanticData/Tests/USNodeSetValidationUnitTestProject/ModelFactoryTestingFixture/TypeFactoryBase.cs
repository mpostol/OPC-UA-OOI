//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.ModelFactoryTestingFixture
{
  /// <summary>
  /// Class TypeFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.NodeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.ITypeFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.NodeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.ITypeFactory" />
  internal class TypeFactoryBase : NodeFactoryBase, ITypeFactory
  {
    /// <summary>
    /// Sets the base type of the node.
    /// </summary>
    /// <value>The base type represented by the <see cref="T:System.Xml.XmlQualifiedName" />.</value>
    public XmlQualifiedName BaseType
    {
      set { }
    }

    /// <summary>
    /// Sets a value indicating whether this instance is abstract.
    /// </summary>
    /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
    /// <remarks>Default Value is false</remarks>
    public bool IsAbstract
    {
      set { }
    }
  }
}