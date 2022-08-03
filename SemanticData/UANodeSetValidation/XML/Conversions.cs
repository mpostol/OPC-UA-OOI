//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.AddressSpace.Abstractions;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  internal static class Conversions
  {
    internal static IUANode GetIUANode(this UANode node)
    {
      return node;
    }
  }
}