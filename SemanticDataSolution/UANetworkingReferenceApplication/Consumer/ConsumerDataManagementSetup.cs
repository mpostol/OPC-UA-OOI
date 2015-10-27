
using System;
using UAOOI.SemanticData.DataManagement;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  internal class ConsumerDataManagementSetup : DataManagementSetup
  {

    #region creator of the ConsumerDeviceSimulator
    internal static ConsumerDataManagementSetup CreateDevice(IModelViewBindingFactory bindingFactory, Action<IDisposable> toDispose)
    {
      ConsumerDataManagementSetup _ret = new ConsumerDataManagementSetup();
      _ret.ConfigurationFactory = new ConsumerConfigurationFactory();
      MainWindowModel _model = new MainWindowModel() { ModelViewBindingFactory = bindingFactory };
      _ret.BindingFactory = _model;
      _ret.EncodingFactory = _model;
      _ret.MessageHandlerFactory = new ConsumerMessageHandlerFactory(toDispose, bindingFactory.UDPPort);
      _ret.Initialize();
      _ret.Run();
      return _ret;
    }
    #endregion

  }
}
