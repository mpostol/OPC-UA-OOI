//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{
  [TestClass]
  public class PartDataManagementSetupUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      Mock<ServiceLocatorImplBase> _ServiceLocatorProviderMocq = new Mock<ServiceLocatorImplBase>();
      Mock<IEncodingFactory> _IEncodingFactoryMock = new Mock<IEncodingFactory>();
      ServiceLocator.SetLocatorProvider(() => _ServiceLocatorProviderMocq.Object);
      _ServiceLocatorProviderMocq.Setup(x => x.GetInstance<IEncodingFactory>()).Returns(_IEncodingFactoryMock.Object);
      bool _disposingFlag = false;
      int _dosposingCount = 0;
      using (PartDataManagementSetup _newInstance = new PartDataManagementSetup())
      {
        _newInstance.DisposeCheck(x => { _disposingFlag = x; _dosposingCount++; });
        Assert.IsNotNull(_newInstance.BindingFactory);
        Assert.IsNotNull(_newInstance.ConfigurationFactory);
        Assert.IsNotNull(_newInstance.EncodingFactory);
        Assert.AreSame(_IEncodingFactoryMock.Object, _newInstance.EncodingFactory);
        Assert.IsNull(_newInstance.MessageHandlerFactory);
        ServiceLocator.SetLocatorProvider(() => null);
      }
      Assert.AreEqual<int>(1, _dosposingCount);
      Assert.IsTrue(_disposingFlag);
    }
  }
}