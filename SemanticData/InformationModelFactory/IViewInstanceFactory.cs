
namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IViewInstanceFactory - encapsulates definition of a View NodeClass
  /// </summary>
  public interface IViewInstanceFactory : IInstanceFactory
  {

    /// <summary>
    /// Sets a value indicating whether [supports events].
    /// </summary>
    /// <value><c>null</c> if [supports events] contains no value, <c>true</c> if [supports events]; otherwise, <c>false</c>.</value>
    bool? SupportsEvents
    {
      set;
    }
    /// <summary>
    /// Sets a value indicating whether the part of the Address Space represented by View contains no loops.
    /// </summary>
    /// <value><c>true</c> if the part of the Address Space represented by View contains no loops; otherwise, <c>false</c>.</value>
    bool ContainsNoLoops
    {
      set;
    }

  }
}
