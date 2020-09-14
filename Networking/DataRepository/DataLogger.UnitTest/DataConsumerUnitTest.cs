//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.DataRepository.DataLogger.Instrumentation;

namespace UAOOI.Networking.DataRepository.DataLogger
{
  [TestClass]
  public class DataConsumerUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      TestConsumerViewModel _viewModel = new TestConsumerViewModel();
      PartIBindingFactory _DataConsumer = new PartIBindingFactory(_viewModel);
    }
  }
}