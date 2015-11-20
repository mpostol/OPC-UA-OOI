
using System;
using UAOOI.SemanticData.DataManagement;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  internal class MainWindowModel: IBindingFactory, IEncodingFactory
  {

    internal IConsumerViewModel ViewModelBindingFactory { get; set; }

    #region IBindingFactory
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belong to the same group are handled according to the same profile.</param>
    /// <param name="variableName">The name of a variable that is the ultimate destination of the values recovered from messages. Must be unique in the context of the repositories group.
    /// is updated periodically by a data produced - user of the <see cref="IBinding" /> object.</param>
    /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    /// <exception cref="System.ArgumentNullException">repositoryGroup</exception>
    public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentNullException("repositoryGroup");
      return ViewModelBindingFactory.GetConsumerBinding(variableName, encoding);
    }
    /// <summary>
    /// Gets the producer binding.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="variableName">Name of the variable.</param>
    /// <remarks>It is intentionally not implemented.</remarks>
    /// <returns>IProducerBinding.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region IEncodingFactory
    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// repositoryGroup
    /// or
    /// sourceEncoding
    /// </exception>
    public void UpdateValueConverter(IBinding converter, string repositoryGroup, BuiltInType encoding)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentOutOfRangeException("repositoryGroup");
      if (encoding != converter.Encoding)
        throw new ArgumentOutOfRangeException("sourceEncoding");
    }
    #endregion

    private const string m_RepositoryGroup = "repositoryGroup";

  }
}
