
using System.Windows.Input;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  internal interface IProducerModelView
  {
    int BytesSent { get; set; }
    int PackagesSent { get; set; }
    ICommand ProducerRestart { get; set; }
    string ProducerErrorMessage { get; set; }

  }
}