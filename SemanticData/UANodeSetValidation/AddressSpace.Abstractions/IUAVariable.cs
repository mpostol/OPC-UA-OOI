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
  internal interface IUAVariable : IUANode
  {
    /// <summary>
    /// The most recent value of the Variable that the Server has. Its data type is defined by the DataType Attribute. It is the only Attribute that does not have a data type associated with it.
    /// This allows all Variables to have a value defined by the same Value Attribute.
    /// </summary>
    XmlElement Value
    {
      set; get;
    }

    /// <summary>
    /// NodeId of the DataType definition for the Value Attribute. Standard DataTypes are defined in Clause 7.23.
    /// </summary>
    //string DataType { get; set; }
    NodeId DataType { get; }

    /// <summary>
    /// This Attribute indicates whether the Value Attribute of the Variable is an array and how many dimensions the array has.
    /// It may have the following values:
    ///    n > 1: the Value is an array with the specified number of dimensions.
    ///    OneDimension (1): The value is an array with one dimension.
    ///    OneOrMoreDimensions (0): The value is an array with one or more dimensions.
    ///    Scalar (−1): The value is not an array.
    ///    Any (−2): The value can be a scalar or an array with any number of dimensions.
    ///    ScalarOrOneDimension (−3): The value can be a scalar or a one dimensional array.
    ///NOTE: All DataTypes are considered to be scalar, even if they have array-like semantics like ByteString and String.
    /// </summary>
    int ValueRank { get; set; }

    /// <summary>
    /// This Attribute specifies the maximum supported length of each dimension. If the maximum is unknown the value shall be 0.
    /// The number of elements shall be equal to the number of dimensions of the Value. This Attribute shall be null if the Value is not an array.
    /// For example, if a Variable is defined by the following C array:
    ///     Int32 myArray[346];
    /// then this Variable’s DataType would be set to Int32, and the Variable’s ValueRank has the value 1. The ArrayDimensions is an array with a length of one where the element has the value 346.
    /// Regardless of the number of dimensions, the maximum number of elements of an array transferred on the wire is 2147483647 (max Int32).
    /// </summary>
    string ArrayDimensions { get; set; }

    /// <summary>
    /// The AccessLevel attribute indicates the accessibility of the Value of a Variable node not taking user access rights into account and
    /// applies only to a UAVariable element. The AccessLevel attribute is used to indicate how the Value of a Variable node can be accessed (read/write) and
    /// if it contains current and/or historic data. The AccessLevel does not take any user access rights into account, i.e. although the Variable is writable this
    /// may be restricted to a certain user / user group.
    /// </summary>
    /// <remarks>
    /// Exposed using the type AccessLevelType. The AccessLevelType is defined in P 3 8.57. as the Standard DataType.
    /// </remarks>
    /// <value>The access level.</value>
    //TODO AccessLevelType must be defined for IUAVariable and IUAVariableType #673
    uint? AccessLevel
    {
      set; get;
    }

    /// <summary>
    /// The UserAccessLevel attribute is used to indicate how the Value attribute of a Variable NodeClass can be accessed (read/write)
    /// and if it contains current or historic data taking user access rights into account. It applies only to a UAVariable element. If the OPC UA Server does not
    /// have the ability to get any user access rights related information from the underlying system it should use the same bit mask as used in the AccessLevel attribute.
    /// The UserAccessLevel attribute can restrict the accessibility indicated by the AccessLevel, but not exceed it.
    /// </summary>
    /// <remarks>
    /// Exposed using the type AccessLevelType. The AccessLevelType is defined in P 3 8.57. as the Standard DataType.
    /// </remarks>
    /// <value>The user access level.</value>
    //TODO AccessLevelType must be defined for IUAVariable and IUAVariableType #673
    byte? UserAccessLevel
    {
      set; get;
    }

    /// <summary>
    /// Sets the minimum sampling interval. The MinimumSamplingInterval attribute indicates how “current” the Value of the Variable NodeClass will be kept.
    /// It specifies (in milliseconds) how fast the server can reasonably sample the value for changes. The accuracy of this value (the ability of the server to attain
    /// “best case” performance) can be greatly affected by the system load and other factors. A MinimumSamplingInterval of 0 indicates that the server is to monitor the
    /// item continuously. A MinimumSamplingInterval of -1 means indeterminate value.
    /// </summary>
    /// <value>The minimum sampling interval.</value>
    double MinimumSamplingInterval
    {
      set; get;
    }

    /// <summary>
    /// Sets a value indicating whether this <see cref="IUAVariable"/> is historizing. The Historizing attribute indicates whether the server is actively
    /// collecting data for the history of the Variable node. This differs from the AccessLevel which identifies if the Variable has any historical data. A value of <c>true</c>
    /// indicates that the server is actively collecting data. A value of <c>false</c> indicates that  the server is not actively collecting data. Default value is <c>false</c>.
    /// </summary>
    /// <value><c>true</c> if historizing; otherwise, <c>false</c>.</value>
    bool Historizing
    {
      set; get;
    }
    /// <summary>
    /// Not Supported Feature
    /// Not exposed in the AS
    /// </summary>
    bool Translation { get; }
  }
}