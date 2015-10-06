
namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class DataManagementSetup -it is place holder to gather all external injection points used to initialize 
  /// the communication and bind to local resources.
  /// </summary>
  public class DataManagementSetup
  {

    #region Injection points
    public IBindingFactory BindingFactory { get; set; }
    public IEncodingFactory EncodingFactory { get; set; }
    public IMessageHandlerFactory MessageHandlerFactory { get; set; }
    public IConfigurationFactory ConfigurationFactory { get; set; }
    #endregion    
    #region Internal control entry points
    internal AssociationsCollection AssociationsCollection { get; private set; }
    internal MessageHandlersCollection MessageHandlersCollection { get; private set; }
    #endregion

    #region Master Controll functioanlity 
    public void Initialize()
    {
      throw new System.NotImplementedException();
    }
    public void Run()
    {
      throw new System.NotImplementedException();
    }
    #endregion
  }
}
