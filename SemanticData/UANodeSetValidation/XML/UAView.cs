//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAView
  {
    /// <summary>
    /// Indicates whether the the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(UANode other)
    {
      UAView _other = other as UAView;
      if (_other == null)
        return false;
      return
        base.ParentEquals(_other) &&
        (this.ContainsNoLoops == _other.ContainsNoLoops) &&
        (this.EventNotifier == _other.EventNotifier);
    }
  }
}
