
using System;
using System.IO;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{

  /// <summary>
  /// Interface IBinaryHeaderEncoder - instance of this interface is used to manage the message and packet headers content on the producer side.
  /// </summary>
  public interface IBinaryHeaderEncoder: IBinaryEncoder
  {
    
    /// <summary>
    /// Sets the position within the current stream.
    /// </summary>
    /// <param name="offset">
    /// A byte offset relative to origin.
    /// </param>
    /// <param name="origin">
    /// A field of <see cref="SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="Int64"/>.</returns>
    long Seek(int offset, SeekOrigin origin);

  }

}
