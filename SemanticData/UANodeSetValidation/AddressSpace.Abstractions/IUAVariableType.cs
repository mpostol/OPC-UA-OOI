//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface IUAVariableType - In the OPC UA Address Space the Variable NodeClass is used to provide a value, which may be simple or complex.
  /// </summary>
  public interface IUAVariableType : IUAType
  {
    /// <summary>
    /// Sets the default value. The value of the Variable node that the server assigns while instantiating the node. Its data type is defined by the <see cref="IDataDescriptor.DataType"/>.
    /// </summary>
    /// <value>The default value.</value>
    XmlElement /*Default */ Value
    {
      set; get;
    }

    string ArrayDimensions { get; set; }
    int ValueRank { get; set; }
    NodeId DataTypeNodeId { get; }
  }
}