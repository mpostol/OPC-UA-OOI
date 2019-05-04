//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{

  public abstract partial class UAInstance
  {

    /// <summary>
    /// Indicates whether the the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(UANode other)
    {
      UAInstance _other = other as UAInstance;
      if (_other == null)
        return false;
      return true; //this.ParentNodeId == _other.ParentNodeId; it is not attribute exposed by the node.
    }
  }

}
