//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.UANodeSetDSL
{
  /// <summary>
  /// Interface IUAType - instances implementing this interface supports type definition factoring.
  /// </summary>
  public interface IUAType : IUANode
  {
    /// <summary>
    /// Sets the base type of the node.
    /// </summary>
    /// <value>The base type represented by the <see cref="XmlQualifiedName"/>.</value>
    //XmlQualifiedName BaseType { set; }
    /// <summary>
    /// Sets a value indicating whether this instance is abstract.
    /// </summary>
    /// <remarks>Default Value is false</remarks>
    /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
    bool IsAbstract { get; set; }
  }
}