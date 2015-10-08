
namespace UAOOI.SemanticData.DataManagement
{
  public interface IConsumerBinding: IBinding
  {

    /// <summary>
    /// Assigns the <paramref name="value"/> to the associated variable hosted by the target repository.
    /// </summary>
    /// <param name="value">The value to be assigned to the precess variable.</param>
    void Assign2Repository(object value);

  }
}
