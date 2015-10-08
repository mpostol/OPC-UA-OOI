
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement
{
  
  /// <summary>
  /// Interface IProducerBinding - provide a basic implementation of the <see cref="IProducerBinding"/> interface.
  /// It is used by the producer to get data from data repository.
  /// </summary>
  public interface IProducerBinding : IBinding, INotifyPropertyChanged
  {
    
    /// <summary>
    /// Gets a value indicating whether the new value is available in the repository.
    /// </summary>
    /// <value><c>true</c> if the new value is available in repository; otherwise, <c>false</c>.</value>
    bool NewValue { get; }
    /// <summary>
    /// Gets the new value and resets the flag <see cref="IProducerBinding"/>.
    /// </summary>
    /// <returns>System.Object.</returns>
    object GetFromRepository();

  }
}
