//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.DataRepository.DataLogger.Instrumentation
{
  internal class TestConsumerViewModel : ConsumerViewModel
  {
    protected internal override void Trace(string message)
    {
      throw new NotImplementedException();
    }
  }
}