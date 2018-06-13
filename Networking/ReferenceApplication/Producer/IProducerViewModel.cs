
using System;

namespace UAOOI.Networking.ReferenceApplication.Producer
{

  /// <summary>
  /// Interface IProducerViewModel - defines a contract to implement ViewModel used by the producer to expose diagnostic information on the UI
  /// </summary>
  public interface IProducerViewModel
  {
    /// <summary>
    /// Occurs when producer is restarted by the application user and new setting must be applied.
    /// </summary>
    event EventHandler<EventArgs> ProducerRestart;
    /// <summary>
    /// Sets the number bytes sent.
    /// </summary>
    /// <value>The bytes sent.</value>
    int BytesSent { set; }
    /// <summary>
    /// Gets or sets the number of packages sent.
    /// </summary>
    /// <value>The packages sent.</value>
    int PackagesSent { set; }
    /// <summary>
    /// Sets the producer error message.
    /// </summary>
    /// <value>The producer error message.</value>
    string ProducerErrorMessage { set; }

  }
}