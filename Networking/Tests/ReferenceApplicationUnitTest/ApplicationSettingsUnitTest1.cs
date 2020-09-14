//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition.Hosting;
using UAOOI.Networking.DataRepository.DataLogger;
using UAOOI.Networking.ReferenceApplication.Core;
using UAOOI.Networking.ReferenceApplication.MEF;

namespace UAOOI.Networking.ReferenceApplication.UnitTest
{
  [TestClass]
  public class ApplicationSettingsUnitTest1
  {
    [TestMethod]
    public void ApplicationSettingsMEFCompositionMethod()
    {
      using (AggregateCatalog newCatalog = DefaultServiceRegistrar.RegisterServices(null))
      {
        using (CompositionContainer _container = new CompositionContainer(newCatalog))
        {
          string _ProducerConfigurationFileName = _container.GetExportedValue<string>(CompositionSettings.ConfigurationFileNameContract);
          Assert.AreEqual<string>("ConfigurationDataProducer.xml", _ProducerConfigurationFileName, $"_ProducerConfigurationFileName = {_ProducerConfigurationFileName}");
          string _ConsumerConfigurationFileName = _container.GetExportedValue<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);
          Assert.AreEqual<string>("ConfigurationDataConsumer.xml", _ConsumerConfigurationFileName, $"_ConsumerConfigurationFileName = {_ConsumerConfigurationFileName}");
          ApplicationSettings _ApplicationSettings = _container.GetExportedValue<ApplicationSettings>();
          Assert.IsNotNull(_ApplicationSettings);
          ApplicationSettings _ApplicationSettings2 = _container.GetExportedValue<ApplicationSettings>();
          Assert.AreSame(_ApplicationSettings, _ApplicationSettings2);
        }
      }
    }
    [TestMethod]
    public void ApplicationSettingsISLCompositionMethod()
    {
      using (AggregateCatalog newCatalog = DefaultServiceRegistrar.RegisterServices(null))
      {
        using (CompositionContainer _container = new CompositionContainer(newCatalog))
        {
          IServiceLocator _serviceLocator = new ServiceLocatorAdapter(_container);
          ServiceLocator.SetLocatorProvider(() => _serviceLocator);
          string _ProducerConfigurationFileName = _serviceLocator.GetInstance<string>(CompositionSettings.ConfigurationFileNameContract);
          Assert.AreEqual<string>("ConfigurationDataProducer.xml", _ProducerConfigurationFileName, $"_ProducerConfigurationFileName = {_ProducerConfigurationFileName}");
          string _ConsumerConfigurationFileName = _serviceLocator.GetInstance<string>(ConsumerCompositionSettings.ConfigurationFileNameContract);
          Assert.AreEqual<string>("ConfigurationDataConsumer.xml", _ConsumerConfigurationFileName, $"_ConsumerConfigurationFileName = {_ConsumerConfigurationFileName}");
          ApplicationSettings _ApplicationSettings = _serviceLocator.GetInstance<ApplicationSettings>();
          Assert.IsNotNull(_ApplicationSettings);
          ApplicationSettings _ApplicationSettings2 = _serviceLocator.GetInstance<ApplicationSettings>();
          Assert.AreSame(_ApplicationSettings, _ApplicationSettings2);
        }
      }
    }

  }
}

