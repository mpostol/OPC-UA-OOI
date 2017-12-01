
using System.Windows.Input;

namespace UAOOI.Networking.ReferenceApplication.Producer
{
  internal interface IProducerViewModel
  {
    int BytesSent { get; set; }
    int PackagesSent { get; set; }
    ICommand ProducerRestart { get; set; }
    string ProducerErrorMessage { get; set; }

  }
}