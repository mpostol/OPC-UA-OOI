
using System;
using System.Runtime.Serialization;

namespace UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16
{
  /// <summary>
  /// Class UATypeInfo - stores information about an OPC UA Type.
  /// </summary>
  [DataContractAttribute(Name = "UATypeInfo", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public class UATypeInfo
  {

    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="UATypeInfo"/> class representing an unknown type.
    /// </summary>
    [Obsolete("Shall be used only by the serializer")]
    public UATypeInfo()
    {
      m_builtInType = BuiltInType.Null;
      m_valueRank = ValueRanks.Any;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="UATypeInfo"/> class representing <see cref="BuiltInType"/> and value rank.
    /// </summary>
    /// <param name="builtInType">Type of the an OPC UA entity.</param>
    public UATypeInfo(BuiltInType builtInType) : this(builtInType, ValueRanks.Scalar) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="UATypeInfo"/> class representing <see cref="BuiltInType"/> and value rank.
    /// </summary>
    /// <param name="builtInType">Type of the an OPC UA entity.</param>
    /// <param name="valueRank">The value rank.</param>
    public UATypeInfo(BuiltInType builtInType, int valueRank) : this(builtInType, valueRank, null) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="UATypeInfo"/> class.
    /// </summary>
    /// <param name="builtInType">Type of the built in.</param>
    /// <param name="valueRank">The value rank.</param>
    /// <param name="arrayDimensions">The array dimensions.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">$for {nameof(valueRank)} == {valueRank} {nameof(ArrayDimensions)} must be provided.</exception>
    public UATypeInfo(BuiltInType builtInType, int valueRank, int[] arrayDimensions)
    {
      if ((valueRank == 0 || valueRank > 1) && (arrayDimensions == null || arrayDimensions.Length == 0))
        throw new ArgumentOutOfRangeException(nameof(valueRank), $"for {nameof(valueRank)} == {valueRank} {nameof(ArrayDimensions)} must be provided.");
      m_builtInType = builtInType;
      m_valueRank = valueRank;
      m_ArrayDimensionsField = (arrayDimensions == null || arrayDimensions.Length == 0) ? null : (int[])arrayDimensions.Clone();
    }
    #endregion

    #region properties
    /// <summary>
    /// The built-in type.
    /// </summary>
    /// <value>The type of any value represented by this instance.</value>
    [DataMemberAttribute(EmitDefaultValue = false)]
    public BuiltInType BuiltInType
    {
      get { return m_builtInType; }
      set { m_builtInType = value; }
    }
    /// <summary>
    /// Gets or sets the array range. Indicates whether the dataType is an array and how many dimensions the array has.
    /// It may have the following values:
    /// n > 1: the dataType is an array with the specified number of dimensions.
    /// OneDimension(1): The dataType is an array with one dimension.
    /// OneOrMoreDimensions (0): The dataType is an array with one or more dimensions.
    /// Scalar (−1): The dataType is not an array.
    /// Any (−2): The dataType can be a scalar or an array with any number of dimensions.
    /// ScalarOrOneDimension(−3): The dataType can be a scalar or a one dimensional array.
    /// </summary>
    /// <remarks>
    /// <note>
    /// NOTE All DataTypes are considered to be scalar, even if they have array-like semantics like ByteString and String.
    /// </note>
    /// </remarks>
    /// <value>The array dimensions.</value>
    [DataMemberAttribute(IsRequired = true)]
    public int ValueRank
    {
      get { return m_valueRank; }
      set { m_valueRank = value; }
    }
    /// <summary>
    /// Gets or sets the array dimensions - Specifies the length of each dimension for an array dataType. 
    /// It is intended to describe the capability of the dataType, not the current size.
    /// The number of elements shall be equal to the value of the valueRank.Shall be null if valueRank ≤ 0.
    /// A value of 0 for an individual dimension indicates that the dimension has a variable length.
    /// </summary>
    /// <value>The array dimensions.</value>
    [DataMemberAttribute(EmitDefaultValue = false)]
    public int[] ArrayDimensions
    {
      get { return m_ArrayDimensionsField; }
      set { m_ArrayDimensionsField = value; }
    }
    #endregion

    #region private
    private BuiltInType m_builtInType;
    private int m_valueRank;
    private int[] m_ArrayDimensionsField;
    #endregion

  }
}
