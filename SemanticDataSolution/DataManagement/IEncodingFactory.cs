
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IEncodingFactory - provides functionality to lookup a dictionary containing value converters.
  /// It is expected that the encoding/decoding functionality is provided outside of this library
  /// The interface is used for late binding to inject dependency on the external library. 
  /// </summary>
  public interface IEncodingFactory
  {

    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="binding">An object responsible to transfer the value between the message and ultimate destination in the repository.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    void UpdateValueConverter(IBinding binding, string repositoryGroup, BuiltInType sourceEncoding);
    /// <summary>
    /// Gets the decoder that provides methods to be used to decode OPC UA Built-in types.
    /// </summary>
    /// <value>The object implementing <see cref="IUADecoder"/> interface.</value>
    IUADecoder UADecoder { get; }
    /// <summary>
    /// Gets the encoder that provides methods to be used to encode OPC UA Built-in types.
    /// </summary>
    /// <value>The object implementing <see cref="IUADecoder"/> interface.</value>
    IUAEncoder UAEncoder { get; }

  }
}
