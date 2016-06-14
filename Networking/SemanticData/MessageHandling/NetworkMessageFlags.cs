using System;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class PacketFlagsDefinitions - contains definitions related to the package flags.
  /// </summary>
  public static class PacketFlagsDefinitions
  {
    /// <summary>
    /// Enum PacketFlags
    /// </summary>
    public enum NetworkMessageType : byte 
    {
      /// <summary>
      /// The regular messages
      /// </summary>
      RegularMessages = 0x00,
      /// <summary>
      /// The chunk packet
      /// </summary>
      ChunkPacket = 0x01,
      /// <summary>
      /// The discovery request payload 
      /// </summary>
      DiscoveryRequest = 0x02,
      /// <summary>
      /// The discovery response payload
      /// </summary>
      DiscoveryResponse = 0x03,
      /// <summary>
      /// The 1XX (0x04 - 0x07) bits combination is reserved for further expansion.
      /// </summary>
      Reserved = 0x04,
    }
    /// <summary>
    /// Enum PacketFlagsPackageContent - defines package content bits meaning
    /// </summary>
    [Flags]
    public enum NetworkMessageFlagsPackageContent : byte
    {
      /// <summary>
      /// The bit 3 is reserved 
      /// </summary>
      Reserved0x10 = 0x8,
      /// <summary>
      /// The bit 4 is reserved 
      /// </summary>
      Reserved0x20 = 0x10,
      /// <summary>
      /// The force key rotation bit. This bit is set if the key rotation is started earlier than planned. It is set until the new key is used. 
      /// The publisher must give subscribers a reasonable time to request new keys. The minimum time is five times the KeepAliveTime configured 
      /// for the corresponding PubSub group.
      /// </summary>
      ForceKeyRotationBit = 0x20,
      /// <summary>
      /// The message is signed.
      /// </summary>
      MessageSigned = 0x40,
      /// <summary>
      /// The message is encrypted.
      /// </summary>
      MessageEncrypted = 0x80
    }

  }
}
