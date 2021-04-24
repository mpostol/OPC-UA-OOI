//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://github.com/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  //TODO Import all dependencies for the model #575
  internal class Discovery
  {
    internal static Discovery Instance => _Instance.Value;

    internal UANodeSet LoadUANodeSet(string modelUri)
    {
      return null;
    }

    private static Lazy<Discovery> _Instance = new Lazy<Discovery>(() => new Discovery());
  }
}