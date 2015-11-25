
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class UABinaryReader - decodes OPC UA data types as binary values in a specific encoding from UA Binary encoded <see cref="Stream"/>.
  /// </summary>
  public class UABinaryReader : BinaryReader
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="UABinaryReader"/> class based on the specified <see cref="Stream"/> stream and using UTF-8 encoding.
    /// </summary>
    /// <param name="input">The input <see cref="Stream"/> containing UA Binary encoded values.</param>
    public UABinaryReader(Stream input) : base(input) { }
    #endregion

  }
}
