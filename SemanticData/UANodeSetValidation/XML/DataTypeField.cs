//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class DataTypeField
  /// </summary>
  public partial class DataTypeField
  {

    internal void RecalculateNodeIds(Func<string, string> importNodeId)
    {
      DataType = importNodeId(DataType);
    }

  }
}
