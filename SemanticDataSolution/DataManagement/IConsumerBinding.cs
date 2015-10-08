
namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IConsumerBinding - provide a basic implementation of the <see cref="IProducerBinding"/> interface.
  /// It is used by the consumer to save the data in the data repository.
  /// </summary>
  public interface IConsumerBinding: IBinding
  {

    /// <summary>
    /// Assigns the <paramref name="value"/> to the associated variable hosted by the target repository.
    /// </summary>
    /// <param name="value">The value to be assigned to the associated variable hosted by the target repository.</param>
    void Assign2Repository(object value);

  }
}
