
using System;
using System.Threading;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  internal class OPCUADataModel
  {
    internal IModelViewBindingFactory ModelViewBindingFactory { get; set; }
    internal void Run()
    {
      IConsumerBinding Value1 = ModelViewBindingFactory.GetConsumerBinding("Value1");
      m_Timer = new Timer(x => Value1.Assign2Repository(DateTime.Now), null, 0, 500);
    }
    private Timer m_Timer;

  }
}
