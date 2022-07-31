//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  public interface IUAReferenceType : IUAType
  {
    LocalizedText[] InverseName { get; }
    bool Symmetric { get; set; }
  }
}