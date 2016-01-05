using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{
  internal static class DataMemberConfigurationHelpers
  {
    /// <summary>
    /// Gets the <see cref="IConsumerBinding" /> instance for data member.
    /// </summary>
    /// <param name="member">The field description captured by object of type <see cref="FieldMetaData"/>.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <returns>An instance of <see cref="IConsumerBinding" /> type.</returns>
    internal static IConsumerBinding GetConsumerBinding4DataMember(this FieldMetaData member, string repositoryGroup, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IConsumerBinding _binding = bindingFactory.GetConsumerBinding(repositoryGroup, member.ProcessValueName, member.TypeInformation);
      encodingFactory.UpdateValueConverter(_binding, repositoryGroup, member.TypeInformation);
      return _binding;
    }
    /// <summary>
    /// Gets the consumer binding for data member.
    /// </summary>
    /// <param name="member">The field description captured bu object of type <see cref="FieldMetaData"/> .</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    /// <returns>An instance of <see cref="IProducerBinding" /> type.</returns>
    internal static IProducerBinding GetProducerBinding4DataMember(this FieldMetaData member, string repositoryGroup, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IProducerBinding _binding = bindingFactory.GetProducerBinding(repositoryGroup, member.ProcessValueName, member.TypeInformation);
      encodingFactory.UpdateValueConverter(_binding, repositoryGroup, member.TypeInformation);
      return _binding;
    }

  }
}
