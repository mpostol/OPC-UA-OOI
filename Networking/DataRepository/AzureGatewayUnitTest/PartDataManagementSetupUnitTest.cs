//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{
  [TestClass]
  public class PartDataManagementSetupUnitTest
  {
    [TestMethod]
    [DeploymentItem(@"TestingData\")]
    public void ConstructorTest()
    {
      Assert.IsTrue(File.Exists(@"ConfigurationDataConsumer.BoilersSet.xml"));
      Mock<ServiceLocatorImplBase> serviceLocatorProviderMock = new Mock<ServiceLocatorImplBase>();
      Mock<IEncodingFactory> encodingFactoryMock = new Mock<IEncodingFactory>();
      Mock<IMessageHandlerFactory> messageHandlerFactoryMock = new Mock<IMessageHandlerFactory>();
      Mock<ITraceSource> traceSource = new Mock<ITraceSource>();
      serviceLocatorProviderMock.Setup(x => x.GetInstance<IEncodingFactory>()).Returns(encodingFactoryMock.Object);
      serviceLocatorProviderMock.Setup(y => y.GetInstance<IMessageHandlerFactory>()).Returns(messageHandlerFactoryMock.Object);
      serviceLocatorProviderMock.Setup(y => y.GetInstance<ITraceSource>()).Returns(traceSource.Object);
      ServiceLocator.SetLocatorProvider(() => serviceLocatorProviderMock.Object);
      bool _disposingFlag = false;
      int _dosposingCount = 0;
      PartDataManagementSetup.ConfigurationFilePath = @"ConfigurationDataConsumer.BoilersSet.xml";
      using (PartDataManagementSetup _newInstance = new PartDataManagementSetup())
      {
        _newInstance.DisposeCheck(x => { _disposingFlag = x; _dosposingCount++; });
        Assert.IsNotNull(_newInstance.BindingFactory);
        Assert.IsNotNull(_newInstance.ConfigurationFactory);
        Assert.IsInstanceOfType(_newInstance.ConfigurationFactory, typeof(PartConfigurationFactory));
        Assert.IsNotNull(_newInstance.EncodingFactory);
        Assert.AreSame(encodingFactoryMock.Object, _newInstance.EncodingFactory);
        Assert.IsNotNull(_newInstance.MessageHandlerFactory);
        Assert.AreSame(messageHandlerFactoryMock.Object, _newInstance.MessageHandlerFactory);
        ServiceLocator.SetLocatorProvider(() => null);
      }
      Assert.AreEqual<int>(1, _dosposingCount);
      Assert.IsTrue(_disposingFlag);
    }
  }
}