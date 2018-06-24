
namespace UAOOI.Networking.DataLogger
{
  /// <summary>
  /// Class ConsumerCompositionSettings - provides contracts to be used for composition
  /// </summary>
  public class ConsumerCompositionSettings
  {

    /// <summary>
    /// The configuration file name contract name
    /// </summary>
    public const string ConfigurationFileNameContract = "DataLogger.ConfigurationFileNameContract";
    /// <summary>
    /// The view model contract name
    /// </summary>
    public const string ViewModelContract = "ConsumerCompositionSettings.ConsumerViewModel";
    [System.Obsolete("Consider usinh new instead of the composition")]
    internal const string ConfigurationFactoryContract = "ConsumerCompositionSettings.ConfigurationFactory";
    [System.Obsolete("Consider usinh new instead of the composition")]
    internal const string BindingFactoryContract = "ConsumerCompositionSettings.BindingFactory";

  }
}
