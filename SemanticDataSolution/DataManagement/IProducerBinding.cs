
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement
{
  public interface IProducerBinding : INotifyPropertyChanged
  {
    bool NewValue { get; }
    /// <summary>
    /// Gets the new value and resets the flag <see cref="IProducerBinding"/>.
    /// </summary>
    /// <returns>System.Object.</returns>
    object GetNewValue();
  }
}
