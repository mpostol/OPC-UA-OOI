//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class ReferenceChange
  {
    internal void RecalculateNodeIds(Func<string, NodeId> importNodeId)
    {
      SourceNodeId = importNodeId(Source);
      ValueNodeId = importNodeId(Value);
      ReferenceTypeNodeId = importNodeId(Value);
    }

    internal NodeId SourceNodeId { get; private set; }
    internal NodeId ValueNodeId { get; private set; }
    internal NodeId ReferenceTypeNodeId { get; private set; }
  }
}