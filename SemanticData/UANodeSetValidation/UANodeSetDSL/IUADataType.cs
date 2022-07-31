//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

//TODO Define independent Address Space API #645 - Remove dependency on InformationModelFactory
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  public interface IUADataType : IUAType
  {
    IDataTypeDefinition Definition { get; }
    DataTypePurpose Purpose { get; }
  }
}