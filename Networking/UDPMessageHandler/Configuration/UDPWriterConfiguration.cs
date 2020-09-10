//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;

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
      try
      {
        string[] _parameters = configuration.Split(',');
        if (_parameters.Length != 2)
          throw new ArgumentException($"Wrong number of parameter {_parameters.Length} but expected 2");
        UDPWriterConfiguration _ret = new UDPWriterConfiguration
        {
          UDPPortNumber = int.Parse(_parameters[0]),
          RemoteHostName = _parameters[1]
        };
        return _ret;
      }
      catch (Exception _ex)
      {
        UDPMessageHandlerSemanticEventSource.Log.LogException(nameof(UDPWriterConfiguration), nameof(Parse), _ex);
        throw;
      }
    }

    internal int UDPPortNumber { get; set; }
    internal string RemoteHostName { get; set; }

    #endregion API

    #region Object

    public override string ToString()
    {
      return $"{UDPPortNumber},{RemoteHostName}";
    }

    #endregion Object

    #region private

    private UDPWriterConfiguration()
    {
    }

    #endregion private
  }
}