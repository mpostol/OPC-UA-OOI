
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  public partial class DataMemberConfiguration
  {
    /// <summary>
    /// Gets the <see cref="IConsumerBinding" /> instance for data member.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <returns>An instance of <see cref="IConsumerBinding" /> type.</returns>
    internal IConsumerBinding GetConsumerBinding4DataMember(string repositoryGroup, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IConsumerBinding _binding = bindingFactory.GetConsumerBinding(repositoryGroup, this.ProcessValueName);
      encodingFactory.UpdateValueConverter(_binding, repositoryGroup, this.SourceEncoding);
      return _binding;
    }
    /// <summary>
    /// Gets the consumer binding for data member.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <returns>An instance of <see cref="IProducerBinding" /> type.</returns>
    internal IProducerBinding GetProducerBinding4DataMember(string repositoryGroup, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IProducerBinding _binding = bindingFactory.GetProducerBinding(repositoryGroup, this.ProcessValueName);
      encodingFactory.UpdateValueConverter(_binding, repositoryGroup, this.SourceEncoding);
      return _binding;
    }

  }
}
