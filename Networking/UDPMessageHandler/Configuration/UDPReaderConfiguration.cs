
using System;
using System.Net;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;

namespace UAOOI.Networking.UDPMessageHandler.Configuration
{

  /// <summary>
  /// Class UDPReaderConfiguration encapsulates configuration for <see cref="IMessageHandlerFactory.GetIMessageReader"/>.
  /// </summary>
  internal class UDPReaderConfiguration
  {

    #region API
    internal static UDPReaderConfiguration Parse(string configuration)
    {
      try
      {
        string[] _parameters = configuration.Split(',');
        if (_parameters.Length != 4)
          throw new ArgumentException($"Wrong number of parameter {_parameters.Length} but expected 4");
        UDPReaderConfiguration _ret = new UDPReaderConfiguration();
        _ret.UDPPortNumber = int.Parse(_parameters[0]);
        _ret.JoinMulticastGroup = bool.Parse(_parameters[1]);
        if (_ret.JoinMulticastGroup)
          _ret.DefaultMulticastGroup = IPAddressValidationRule.ValidateIP(_parameters[2]);
        _ret.ReuseAddress = bool.Parse(_parameters[3]);
        return _ret;
      }
      catch (Exception _ex)
      {
        UDPMessageHandlerSemanticEventSource.Log.LogException( nameof(UDPReaderConfiguration), nameof(Parse), _ex);
        throw;
      }
    }
    internal int UDPPortNumber { get; set; }
    internal IPAddress DefaultMulticastGroup { get; set; } = null;
    internal bool ReuseAddress { get; set; }
    #endregion

    #region Object
    public override string ToString()
    {
      return $"{UDPPortNumber},{JoinMulticastGroup},{DefaultMulticastGroup},{ReuseAddress}";
    }
    #endregion

    #region private
    private UDPReaderConfiguration() { }
    private bool JoinMulticastGroup;
    #endregion

  }
}
