
using System;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Message - placeholder to implement the message send over the wire.
  /// </summary>
  public interface IPeriodicDataMessage
  {

    /// <summary>
    /// Updates my values - implementation .
    /// </summary>
    /// <param name="update">The update.</param>
    void UpdateMyValues(Func<int, IBinding> update);
    bool IAmDestination(ISemanticData dataId);

  }
}
