
namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Interface IBindingFactory - if implemented creates object implementing <see cref="IBinding"/> that can used by a data source to 
  /// update selected variable on the creator side.
  /// </summary>
  public interface IBindingFactory
  {
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belong to the same group are handled according to the same profile.
    /// </param>
    /// <param name="variableName">The name of a variable that is the ultimate destination of the values recovered from messages. Must be unique in the context of the repositories group.
    /// is updated periodically by a data produced - user of the <see cref="IBinding" /> object.
    /// </param>
    /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName);
    /// <summary>
    /// Gets the producer binding.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="variableName">Name of the variable.</param>
    /// <returns>IProducerBinding.</returns>
    IProducerBinding GetProducerBinding(string repositoryGroup, string variableName);
  
  }

}
