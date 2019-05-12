//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


namespace UAOOI.SemanticData.UAModelDesignExport.Instrumentation
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
