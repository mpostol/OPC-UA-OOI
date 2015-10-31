using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{
  internal static class DataMemberConfigurationHelpers
  {
    /// <summary>
    /// Gets the <see cref="IConsumerBinding" /> instance for data member.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <returns>An instance of <see cref="IConsumerBinding" /> type.</returns>
    internal static IConsumerBinding GetConsumerBinding4DataMember(this DataMemberConfiguration member, string repositoryGroup, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IConsumerBinding _binding = bindingFactory.GetConsumerBinding(repositoryGroup, member.ProcessValueName);
      encodingFactory.UpdateValueConverter(_binding, repositoryGroup, member.SourceEncoding);
      return _binding;
    }
    /// <summary>
    /// Gets the consumer binding for data member.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <returns>An instance of <see cref="IProducerBinding" /> type.</returns>
    internal static IProducerBinding GetProducerBinding4DataMember(this DataMemberConfiguration member, string repositoryGroup, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IProducerBinding _binding = bindingFactory.GetProducerBinding(repositoryGroup, member.ProcessValueName);
      encodingFactory.UpdateValueConverter(_binding, repositoryGroup, member.SourceEncoding);
      return _binding;
    }

  }
}
