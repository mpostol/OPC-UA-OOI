﻿//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAObject
  {
    /// <summary>
    /// Indicates whether the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(UANode other)
    {
      UAObject _other = other as UAObject;
      if (_other == null)
        return false;
      return
        base.ParentEquals(_other) &&
        (this.EventNotifier == _other.EventNotifier);
    }

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UAObject _ret = new UAObject()
      {
        EventNotifier = this.EventNotifier
      };
      base.CloneUAInstance(_ret);
      return _ret;
    }
  }
}