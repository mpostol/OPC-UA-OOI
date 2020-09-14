//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Networking.DataRepository.DataLogger.Instrumentation;

namespace UAOOI.Networking.DataRepository.DataLogger
{
  [TestClass]
  public class ConsumerViewModelUnit
  {
    [TestMethod]
    public void ConstructorTest()
    {
      TestConsumerViewModel _viewModel = new TestConsumerViewModel();
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void TraceTest()
    {
      TestConsumerViewModel _viewModel = new TestConsumerViewModel();
      _viewModel.Trace("");
    }
  }
}