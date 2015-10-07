
using System;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class IDataUniqueIdentifier - instance of this interface provides globally unique identifier (GUID) of the data. 
  /// </summary>
  public interface IDataUniqueIdentifier
  {
    /// <summary>
    /// Gets the globally unique identifier (GUID) of the data.
    /// </summary>
    /// <value>The globally unique identifier of the data as a collection of values.</value>
    Guid Guid { get; }
  }

}
