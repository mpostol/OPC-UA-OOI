using System;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class PacketFlagsDefinitions - contains definitions related to the package flags.
  /// </summary>
  public static class PacketFlagsDefinitions
  {
    /// <summary>
    /// Enum PacketFlags
    /// </summary>
    public enum PacketFlagsMessageType : byte
    {
      /// <summary>
      /// The regular messages
      /// </summary>
      RegularMessages = 0x00,
      /// <summary>
      /// The chunk packet
      /// </summary>
      ChunkPacket = 0x01
    }
    /// <summary>
    /// Enum PacketFlagsPackageContent - defines package content bits meaning
    /// </summary>
    [Flags]
    public enum PacketFlagsPackageContent : byte
    {
      /// <summary>
      /// The force key rotation bit. This bit is set if the key rotation is started earlier than planned.
      /// </summary>
      ForceKeyRotationBit = 0x10,
      /// <summary>
      /// The message is signed
      /// </summary>
      MessageSigned = 0x20,
      /// <summary>
      /// The message is encrypted
      /// </summary>
      MessageEncrypted = 0x4
    }

  }
}
