//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAReferenceType
  {

    protected override UANode ParentClone()
    {
      UAReferenceType _ret = new UAReferenceType()
      {
        InverseName = this.InverseName,
        Symmetric = this.Symmetric
      };
      base.CloneUAType(_ret);
      return _ret;
    }

  }
}
