
using System;

namespace UAOOI.Networking.DataLogger.UnitTest.Instrumentation
{
  internal class TestConsumerViewModel : ConsumerViewModel
  {
    internal protected override void Trace(string message)
    {
      throw new NotImplementedException();
    }
  }
}
