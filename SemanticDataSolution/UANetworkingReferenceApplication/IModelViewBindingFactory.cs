
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{

  /// <summary>
  /// Interface IModelViewBindingFactory - if implemented by the ModelView class is used to created binding to UA Data received from the network.
  /// </summary>
  internal interface IModelViewBindingFactory
  {

    /// <summary>
    /// Gets the consumer binding.
    /// </summary>
    /// <param name="propertyName">Name of the property to be binded.</param>
    /// <returns>IConsumerBinding.</returns>
    IConsumerBinding GetConsumerBinding(string propertyName);

  }
}
