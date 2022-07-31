//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.UANodeSetDSL
{
  public interface IReference
  {
    bool IsForward { get; set; }
    NodeId ReferenceTypeNodeid { get; }
    NodeId ValueNodeId { get; }
  }
}