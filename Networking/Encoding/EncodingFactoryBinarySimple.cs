
using System;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.Encoding
{

  /// <summary>
  /// Class EncodingFactoryBinarySimple - provides <see cref="IEncodingFactory"/> functionality limited to encoding simple data only.
  /// </summary>
  /// <seealso cref="UAOOI.Networking.SemanticData.IEncodingFactory" />
  [Export(typeof(IEncodingFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class EncodingFactoryBinarySimple : IEncodingFactory
  {

    #region IEncodingFactory
    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="binding">An object responsible to transfer the value between the message and ultimate destination in the repository.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// binding
    /// </exception>
    void IEncodingFactory.UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
    {
      if (sourceEncoding.BuiltInType != binding.Encoding.BuiltInType)
        throw new ArgumentOutOfRangeException(nameof(binding));
    }
    /// <summary>
    /// Gets the ua decoder.
    /// </summary>
    /// <value>The ua decoder.</value>
    IUAEncoder IEncodingFactory.UAEncoder { get; } = new UABinaryEncoderImplementation();
    /// <summary>
    /// Gets the decoder that provides methods to be used to decode OPC UA Built-in types.
    /// </summary>
    /// <value>The object implementing <see cref="T:UAOOI.Networking.SemanticData.Encoding.IUADecoder" /> interface.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    IUADecoder IEncodingFactory.UADecoder { get; } = new UABinaryDecoderImplementation();
    #endregion

  }

}
