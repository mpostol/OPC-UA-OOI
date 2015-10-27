using UAOOI.SemanticData.DataManagement;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{
  internal class OPCUAServerProducerSimulator : DataManagementSetup
  {
    #region creator
    internal static void CreateDevice()
    {
      Current = new OPCUAServerProducerSimulator();
      Current.ConfigurationFactory = new ProducerConfigurationFactory();
      CustomNodeManager _simulator = new CustomNodeManager();
      Current.BindingFactory = _simulator;
      Current.EncodingFactory = _simulator;
      Current.MessageHandlerFactory = new ProducerMessageHandlerFactory();
    }
    #endregion
    /// <summary>
    /// Gets the current instance of the <see cref="OPCUAServerProducerSimulator"/>.
    /// </summary>
    /// <value>The current.</value>
    public static OPCUAServerProducerSimulator Current { get; private set; }

  }
}
