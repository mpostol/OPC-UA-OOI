
using UAOOI.SemanticData.DataManagement.DataRepository;

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
    /// <param name="repositoryGroup">
    /// It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.
    /// </param>
    /// <param name="variableName">
    /// The name of a variable that is the ultimate destination of the values recovered from messages. 
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup"/>.
    /// </param>
    /// <returns>
    /// Returns an object implementing the <see cref="IConsumerBinding" /> interface that can be used to update selected variable on the factory side.
    /// </returns>
    IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName);
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IProducerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.
    /// </param>
    /// <param name="variableName">
    /// The name of a variable that is the source of the values forwarded by a message over the network. 
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup.
    /// </param>
    /// <returns>Returns an object implementing the <see cref="IProducerBinding" /> interface that can be used to create message and populate it with the data.</returns>
    IProducerBinding GetProducerBinding(string repositoryGroup, string variableName);
  
  }

}
