using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  internal interface IModelViewBindingFactory
  {
    IConsumerBinding GetConsumerBinding(string variableName);
  }
}
