
namespace UAOOI.Configuration.Core
{

  /// <summary>
  /// Provides access to the instance node configuration editor
  /// </summary>
  public interface IInstanceConfiguration
  {

    /// <summary>
    /// Edits this instance.
    /// </summary>
    void Edit();
    /// <summary>
    /// Create new empty data bindings configuration for this instance node to store proprietary information of the UA server.
    /// </summary>
    void ClearConfiguration();

  }
}
