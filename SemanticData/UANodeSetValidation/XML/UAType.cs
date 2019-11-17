//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UAType.
  /// Implements the <see cref="UANode" />
  /// </summary>
  /// <seealso cref="UANode" />
  public abstract partial class UAType
  {

    /// <summary>
    /// Indicates whether the the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    /// <exception cref="NotImplementedException">Intentionally not implemented</exception>
    protected override bool ParentEquals(UANode other)
    {
      throw new NotImplementedException("Intentionally not implemented");
    }
    /// <summary>
    /// Clones the instance of the <see cref="UAType"/>.
    /// </summary>
    /// <param name="node">The node to be updated.</param>
    protected void CloneUAType(UAType node)
    {
      node.IsAbstract = this.IsAbstract;
      base.CloneUANode(node);
    }

  }
}
