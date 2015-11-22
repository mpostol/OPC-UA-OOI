
namespace UAOOI.SemanticData.DataManagement.Encoding
{
  
  /// <summary>
  /// Class StatusCode - if implemented represents a numeric code that describes the result of a service or operation.
  /// </summary>
  public interface IStatusCode
  {

    /// <summary>
    /// Gets the code of status.
    /// </summary>
    /// <value>The code - a numeric code that describes the result of a service or operation.</value>
    uint Code { get;  }

  }

}
