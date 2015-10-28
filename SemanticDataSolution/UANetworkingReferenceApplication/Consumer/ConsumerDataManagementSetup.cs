
using System;
using UAOOI.SemanticData.DataManagement;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  internal class ConsumerDataManagementSetup : DataManagementSetup
  {

    #region creator of the ConsumerDeviceSimulator
    /// <summary>
    /// Creates the device (e.g. HMI) simulator.
    /// </summary>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="toDispose">
    /// To dispose captures functionality to create a collection of disposable objects. 
    /// The objects are disposed when application exits.
    /// </param>
    /// <returns>ConsumerDataManagementSetup.</returns>
    internal static void CreateDevice(IModelViewBindingFactory bindingFactory, Action<IDisposable> toDispose)
    {
      Current = new ConsumerDataManagementSetup();
      Current.ConfigurationFactory = new ConsumerConfigurationFactory();
      MainWindowModel _model = new MainWindowModel() { ModelViewBindingFactory = bindingFactory };
      Current.BindingFactory = _model;
      Current.EncodingFactory = _model;
      Current.MessageHandlerFactory = new ConsumerMessageHandlerFactory(toDispose);
      Current.Initialize();
      Current.Run();
    }
    #endregion

    /// <summary>
    /// Singleton implementation - gets the current instance of this class.
    /// </summary>
    /// <value>The current.</value>
    public static ConsumerDataManagementSetup Current { get; private set; }


  }
}
