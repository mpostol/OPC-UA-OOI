//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
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
    XmlElement Value
    {
      set; get;
    }

    /// <summary>
    /// NodeId of the data type definition for instances of this type.
    /// </summary>
    NodeId DataType { get; }

    /// <summary>
    /// This Attribute indicates whether the Value Attribute of the VariableType is an array and how many dimensions the array has.
    /// It may have the following values:
    ///   n > 1: the Value is an array with the specified number of dimensions.
    ///   OneDimension(1): The value is an array with one dimension.
    ///   OneOrMoreDimensions(0): The value is an array with one or more dimensions.
    ///   Scalar(−1): The value is not an array.
    ///   Any(−2): The value can be a scalar or an array with any number of dimensions.
    ///   ScalarOrOneDimension(−3): The value can be a scalar or a one dimensional array.
    /// NOTE All DataTypes are considered to be scalar, even if they have array-like semantics like ByteString and String.
    /// </summary>
    int ValueRank { get; set; }

    /// <summary>
    /// This Attribute specifies the length of each dimension for an array value. The Attribute specifies the maximum supported length of each dimension. If the maximum is unknown the value is 0.
    /// The number of elements shall be equal to the value of the ValueRank Attribute.This Attribute shall be null if ValueRank ≤ 0.
    /// For example, if a VariableType is defined by the following C array:
    ///   Int32 myArray[346];
    /// then this VariableType’s DataType would point to an Int32, the VariableType’s ValueRank has the value 1 and the ArrayDimensions is an array with one entry having the value 346.
    /// </summary>
    string ArrayDimensions { get; set; }
  }
}