
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  internal interface IConsumerModelView : IModelViewBindingFactory
  {
    int ConsumerBytesReceived { get; set; }
    int ConsumerFramesReceived { get; set; }
    ICommand ConsumerUpdateConfiguration { get; set; }
    int UDPPort { get; set; }
    string ConsumerErrorMessage { get; set; }
    ObservableCollection<string> ConsumerLog { get; set; }


  }
}