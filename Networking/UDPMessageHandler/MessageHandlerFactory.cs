//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.ComponentModel.Composition;
using UAOOI.Networking.Core;
using UAOOI.Networking.UDPMessageHandler.Configuration;
using UAOOI.Networking.UDPMessageHandler.Diagnostic;

namespace UAOOI.Networking.UDPMessageHandler
{

  /// <summary>
  /// Class <see cref="MessageHandlerFactory"/> - implements <see cref="IMessageHandlerFactory"/> 
  /// </summary>
  [Export(typeof(IMessageHandlerFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MessageHandlerFactory : IMessageHandlerFactory
  {

    #region IMessageHandlerFactory
    /// <summary>
    /// Gets an instance implementing <see cref="T:UAOOI.Networking.Core.IBinaryDataTransferGraphReceiver" /> interface.
    /// </summary>
    /// <param name="name">The name to be used for identification of the underlying TDG transport channel.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="T:UAOOI.Networking.Core.IBinaryDataTransferGraphReceiver" />.</param>
    /// <returns>An object implementing <see cref="!:IMessageReader" /> that provides functionality supporting reading the messages from the wire.</returns>
    IBinaryDataTransferGraphReceiver IMessageHandlerFactory.GetBinaryDTGReceiver(string name, string configuration)
    {
      UDPMessageHandlerSemanticEventSource.Log.GetIMessageHandler($"{nameof(IMessageHandlerFactory.GetBinaryDTGReceiver)}{{ name = {name}, configuration= {configuration} }}");
      UDPReaderConfiguration _configuration = UDPReaderConfiguration.Parse(configuration);
      BinaryUDPPackageReader _ret = new BinaryUDPPackageReader(_configuration);
      return _ret;
    }
    /// <summary>
    /// Gets an instance implementing <see cref="T:UAOOI.Networking.Core.IBinaryDataTransferGraphSender" /> interface.
    /// </summary>
    /// <param name="name">The name to be used for identification of the underlying TDG transport channel.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="T:UAOOI.Networking.Core.IBinaryDataTransferGraphSender" />.</param>
    /// <returns>An object implementing <see cref="!:IMessageWriter" /> that provides functionality supporting sending the messages over the wire.</returns>
    IBinaryDataTransferGraphSender IMessageHandlerFactory.GetBinaryDTGSender(string name, string configuration )
    {
      UDPMessageHandlerSemanticEventSource.Log.GetIMessageHandler($"{nameof(IMessageHandlerFactory.GetBinaryDTGSender)}{{ name = {name}, configuration= {configuration} }}");
      UDPWriterConfiguration _configuration = UDPWriterConfiguration.Parse(configuration);
      BinaryUDPPackageWriter _ret = new BinaryUDPPackageWriter(_configuration.RemoteHostName, _configuration.UDPPortNumber);
      return _ret;
    }
    #endregion

  }

}