using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAOOI.Networking.SemanticData.Diagnostics
{
  public abstract class NetworkingEventSourceBase : INetworkingEventSourceProvider
  {

    public const EventTask Consumer = (EventTask)1;
    public const EventTask Producer = (EventTask)2;
    public const EventTask ConsumerProducer = (EventTask)3;

    public abstract EventSource GetPartEventSource();

  }
}
