
namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Interface IBindingFactory - if implemented creates object implementing <see cref="IBinding"/> that can used by a data source to 
  /// update selected variable on the creator side.
  /// </summary>
  public interface IBindingFactory
  {
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IBinding" /> type.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group aimed to profiling the configuration behavior, e.g. select encoders. 
    /// The configuration of the repositories belong to the same group are handled according to the same profile.
    /// </param>
    /// <param name="variableName">Unique in the context in factory object name of a variable. The name is used to identify the destination variable hosted by the factory. The destination variable
    /// is updated periodically by a data produced - user of the <see cref="IBinding" /> object.</param>
    /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    IBinding GetBinding(string repositoryGroup, string variableName);
  
  }

}
