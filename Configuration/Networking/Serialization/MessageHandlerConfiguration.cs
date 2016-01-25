
using System;
using System.Linq;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class MessageTransportConfiguration - provide configuration for transport used to transfer messages over the wire.
  /// </summary>
  public abstract partial class MessageHandlerConfiguration : ICloneable
  {

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    /// <exception cref="System.NotImplementedException">It is intentionally not implemented.</exception>
    public virtual object Clone()
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Check if this instance of <see cref="MessageHandlerConfiguration"/> contains <paramref name="associationName"/> on the list of associated data sets..
    /// </summary>
    /// <param name="associationName">Name of the association.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public abstract bool Associated(string associationName);
  }
}
