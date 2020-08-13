//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.Networking.Core
{
  /// <summary>
  /// Interface IMessageHandlerFactory - creates objects supporting the Data Transfer Graph messages handling over the wire.
  /// </summary>
  public interface IMessageHandlerFactory
  {

    /// <summary>
    /// Gets an instance implementing <see cref="IBinaryDataTransferGraphReceiver" /> interface.
    /// </summary>
    /// <param name="name">The name to be used for identification of the underlying DTG transport channel.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="IBinaryDataTransferGraphReceiver" />.</param>
    /// <returns>An object implementing <see cref="IMessageReader" /> that provides functionality supporting reading the messages from the wire.</returns>
    IBinaryDataTransferGraphReceiver GetBinaryDTGReceiver(string name, string configuration);

    /// <summary>
    /// Gets an instance implementing <see cref="IBinaryDataTransferGraphSender" /> interface.
    /// </summary>
    /// <param name="name">The name to be used for identification of the underlying DTG transport channel.</param>
    /// <param name="configuration">The configuration of the object implementing the <see cref="IBinaryDataTransferGraphSender" />.</param>
    /// <returns>An object implementing <see cref="IMessageWriter" /> that provides functionality supporting sending the messages over the wire.</returns>
    IBinaryDataTransferGraphSender GetBinaryDTGSender(string name, string configuration);

  }
}
