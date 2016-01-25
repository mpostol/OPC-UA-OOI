
using System;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class IDataUniqueIdentifier - instance of this interface provides globally unique identifier (GUID) of the data. 
  /// </summary>
  public interface IDataUniqueIdentifier
  {

    /// <summary>
    /// Gets the globally unique identifier (GUID) of the data set.
    /// </summary>
    /// <value>The globally unique identifier of the data set as a collection of values.</value>
    Guid Guid { get; }

  }

}
