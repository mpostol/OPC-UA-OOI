//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.InformationModelFactory.UAConstants
{
  /// <summary>
  /// Enum AccessRestrictions - this is a subtype of the UInt16 with the OptionSetValues Property defined. It is used to define the access restrictions of a <see cref="INodeFactory"/>.
  /// Defined in Part3-8.56
  /// </summary>
  [Flags]
  public enum AccessRestrictions : ushort
  {
    /// <summary>
    /// The client can only access the Node when using a SecureChannel which digitally signs all messages.
    /// </summary>
    SigningRequired = 0x0,
    /// <summary>
    /// The client can only access the Node when using a SecureChannel which encrypts all messages.
    /// </summary>
    EncryptionRequired = 0x1,
    /// <summary>
    /// The client cannot access the Node when using SessionlessInvoke Service invocation.
    /// </summary>
    SessionRequired = 0x2
  }
}
