
namespace UAOOI.Networking.Encoding
{
 
  /// <summary>
  /// Class CompositionSettings - Exposes the composition settings.
  /// </summary>
  public class CompositionSettings
  {

    public const string EncodingFactoryContract = "ProducerCompositionSettings.EncodingFactory";
    /// <summary>
    /// The producer and consumer configuration repository group. Producer and consumer must use this name as the <see cref="SemanticData.IEncodingFactory.UpdateValueConverter"/> repositoryGroup parameter, 
    /// otherwise the <see cref="ArgumentOutOfRangeException"/> exception is thrown.
    /// </summary>
    public const string ConfigurationRepositoryGroup = "repositoryGroup";

  }

}

