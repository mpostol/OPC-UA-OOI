//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IUAModelContext - represents an OPC UA Information Model
  /// </summary>
  internal interface IUAModelContext
  {

    /// <summary>
    /// Imports the qualified name. It recalculates the <see cref="QualifiedName.NamespaceIndex"/> against local namespace index table. 
    /// </summary>
    /// <param name="qualifiedName">The <see cref="QualifiedName"/> serialized as string to be imported.</param>
    /// <returns> An instance of the <see cref="QualifiedName"/> with recalculated <see cref="QualifiedName.NamespaceIndex"/>.</returns>
    string ImportQualifiedName(string qualifiedName);
    /// <summary>
    /// Imports the node identifier if <paramref name="nodeId"/> is not empty.
    /// </summary>
    /// <param name="nodeId">The node identifier with the syntax defined in Part 6-5.3.1.10.</param>
    /// <returns>An instance of the <see cref="NodeId"/> or null is the <paramref name="nodeId"/> is null or empty.</returns>
    string ImportNodeId(string nodeId);

  }

}