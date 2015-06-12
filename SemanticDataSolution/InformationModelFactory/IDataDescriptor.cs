
using System;
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface IDataDescriptor
  {
    /// <summary>
    /// Sets the type of the data. <see cref="XmlQualifiedName"/> of the DataType definition for the Value. It is not required that the pointed out element is defined in the same document. 
    /// If that is the case many documents must be combined to resolve and validate this reference.
    /// </summary>
    /// <value>The type of the data.</value>
    XmlQualifiedName DataType { set; }
    /// <summary>
    /// Sets the array dimensions. This PROPERTY specifies the length of each dimension for an array value. It is intended to describe the capability of the Variable, not the current size.
    /// The number of elements shall be equal to the value defined by the ValueRank. It shall be null if ValueRank ≤ 0. The value of 0 for an individual dimension indicates that the dimension has 
    /// a variable length. For example, if a Variable is defined by the following C array:
    /// Int32 myArray[346];
    /// then the DataType would point to an Int32, the ValueRank has the value 1 and the ArrayDimensions is an array with one entry having the value 346.
    /// Note that the maximum length of an array transferred on the wire is 2147483647 (max Int32) and a multidimensional array is encoded as a one dimensional array.
    /// </summary>
    /// <remarks>
    /// ArrayDimensions is ignored if ValueRank is not equal to the OneOrMoreDimensions.
    /// </remarks>
    /// <value>The array dimensions.</value>
    string ArrayDimensions { set; }
    /// <summary>
    /// Sets the value rank. This property indicates whether the value is an array and how many dimensions the array has.
    /// It may have the following values:
    ///  - n > 1: the Value is an array with the specified number of dimensions.
    ///  - <b>>OneDimension (1)</b>: The value is an array with one dimension. 
    ///  - OneOrMoreDimensions (0): The value is an array with one or more dimensions.
    ///  - Scalar (−1): The value is not an array.
    ///  - Any (−2): The value can be a scalar or an array with any number of dimensions.
    ///  - ScalarOrOneDimension (−3): The value can be a scalar or a one dimensional array.
    ///  NOTE: All build in DataTypes are considered to be scalar, even if they have array-like semantics like ByteString and String.
    /// </summary>
    /// 
    /// <value>The value rank.</value>
    int? ValueRank { set; }
  }
}
