//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory
{

  //TODO Mantis - report error 
  /// <summary>
  /// Enum ReleaseStatus
  /// </summary>
  /// <remarks>
  /// Not defined in the specification Part 2, 5, 6 and  Errata Release 1.04.2 September 25, 2018
  /// </remarks>
    public enum ReleaseStatus
  {

    /// <summary>
    /// The model has been released.
    /// </summary>
    Released,
    /// <summary>
    /// It is draft model
    /// </summary>
    Draft,
    /// <summary>
    /// Consider the model as deprecated
    /// </summary>
    Deprecated,

  }

}
