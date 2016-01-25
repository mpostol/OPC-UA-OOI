namespace UAOOI.SemanticData.DataManagement.Encoding
{

  /// <summary>
  /// Interface DataValue - A class that stores the value of variable with an optional status code and timestamps.
  /// </summary>
  public interface IDataValue
  {

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    IVariant Value { get; }
    /// <summary>
    /// Gets the status code.
    /// </summary>
    /// <value>The status code associated with the value..</value>
    IStatusCode StatusCode { get; }
    /// <summary>
    /// Gets the source timestamp.
    /// </summary>
    /// <value>The source timestamp associated with the value..</value>
    System.DateTime? SourceTimestamp { get; }
    /// <summary>
    /// Gets the source picoseconds - additional resolution for the source timestamp.
    /// </summary>
    /// <value>The source picoseconds.</value>
    ushort SourcePicoseconds { get; }
    /// <summary>
    /// Gets the server timestamp.
    /// </summary>
    /// <value>The server timestamp.</value>
    System.DateTime? ServerTimestamp { get; }
    /// <summary>
    /// Gets the server picoseconds - additional resolution for the server timestamp.
    /// </summary>
    /// <value>The server picoseconds.</value>
    ushort ServerPicoseconds { get; }

  }
}
