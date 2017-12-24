
using System;

namespace UAOOI.Networking.UDPMessageHandler.Configuration
{
  /// <summary>
  /// Class UDPWriterConfiguration encapsulates configuration for <see cref="IMessageHandlerFactory.GetIMessageWriter"/>.
  /// </summary>
  internal class UDPWriterConfiguration
  {

    #region API
    internal static UDPWriterConfiguration Parse(string configuration)
    {
      string[] _parameters = configuration.Split(',');
      if (_parameters.Length != 2)
        throw new ArgumentException($"Wrong number of parameter {_parameters.Length} but expected 2");
      UDPWriterConfiguration _ret = new UDPWriterConfiguration();
      _ret.UDPPortNumber = int.Parse(_parameters[0]);
      _ret.RemoteHostName = _parameters[1];
      return _ret;
    }
    internal int UDPPortNumber { get; set; }
    internal string RemoteHostName { get; set; }
    #endregion

    #region Object
    public override string ToString()
    {
      return $"{UDPPortNumber},{RemoteHostName}";
    }
    #endregion

    #region private
    private UDPWriterConfiguration() { }
    #endregion

  }
}
