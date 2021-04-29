//____________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/TP
//____________________________________________________________________________

namespace UAOOI.Common.Infrastructure.Serializers
{
  /// <summary>
  /// Represents XML file style sheet name provider
  /// </summary>
  public interface IStylesheetNameProvider
  {
    /// <summary>
    /// The style sheet name
    /// </summary>
    string StylesheetName { get; }
  }
}