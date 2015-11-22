
using System;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{
  /// <summary>
  /// Class UATypeInfo - stores information about an OPC UA Type.
  /// </summary>
  public class UATypeInfo
  {

    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="UATypeInfo"/> class representing an unknown type.
    /// </summary>
    [Obsolete("Shall be used only by the serializator")]
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
    public UATypeInfo(BuiltInType builtInType, int valueRank)
    {
      m_builtInType = builtInType;
      m_valueRank = valueRank;
    }
    #endregion

    #region properties
    /// <summary>
    /// The built-in type.
    /// </summary>
    /// <value>The type of the type represented by this instance.</value>
    public BuiltInType BuiltInType
    {
      get { return m_builtInType; }
      set { m_builtInType = value; }
    }
    /// <summary>
    /// The value rank.
    /// </summary>
    /// <value>The value rank of the type represented by this instance.</value>
    public int ValueRank
    {
      get { return m_valueRank; }
    }
    #endregion

    #region private
    private BuiltInType m_builtInType;
    private int m_valueRank;
    #endregion

  }
}
