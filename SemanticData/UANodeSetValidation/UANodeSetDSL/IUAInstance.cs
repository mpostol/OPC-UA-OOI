//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.UANodeSetDSL
{
  /// <summary>
  /// Interface IInstanceFactory - It encapsulates definition of an instance.
  /// </summary>
  public interface IUAInstance : IUANode
  {
    /// <summary>
    /// Sets the modeling rule, which defines whether the component of a complex type are instantiated.
    /// This value is defined by processing the object pointed by the <seealso cref="ModelingRules"/> reference.
    /// </summary>
    /// <value>The modeling rule.</value>
    //ModelingRules? ModelingRule { set; get; }

    /// <summary>
    /// Sets the type definition.
    /// </summary>
    /// <value>The type definition.</value>
    //XmlQualifiedName TypeDefinition { set; get; }

    /// <summary>
    /// Sets the type of the reference if it is component of a complex definition.
    /// </summary>
    /// <value>The type of the reference used for parent child relationship.</value>
    //XmlQualifiedName ReferenceType { set; get; }
  }
}