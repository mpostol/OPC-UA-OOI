//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  public interface IDataTypeDefinition
  {
    bool IsOptionSet { get; set; }
    bool IsUnion { get; set; }
    IDataTypeField[] Field { get; }
    string Name { get; set; }
    string SymbolicName { get; set; }
    string BaseType { get; set; }
  }

  public partial interface IDataTypeField
  {
    LocalizedText[] DisplayName { get; set; }
    LocalizedText[] Description { get; set; }
    string Name { get; set; }
    string SymbolicName { get; set; }
    string DataType { get; set; }
    int ValueRank { get; set; }
    string ArrayDimensions { get; set; }
    uint MaxStringLength { get; set; }
    int Value { get; set; }
    bool IsOptional { get; set; }
  }

  //
  public partial interface IDataTypeField
  {
    NodeId DataTypeNodeId { get; }
  }
}