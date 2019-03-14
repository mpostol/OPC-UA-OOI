//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Enum ModelingRules - represents modeling rules described in Part 3 6.4.4 ModelingRules.
  /// </summary>
  public enum ModelingRules
  {

    /// <summary>
    /// The mandatory - 6.4.4.5.2 Mandatory
    /// </summary>
    Mandatory,
    /// <summary>
    /// The optional - 6.4.4.5.3 Optional
    /// </summary>
    Optional,
    /// <summary>
    /// The mandatory placeholder - 6.4.4.5.6 MandatoryPlaceholder
    /// </summary>
    MandatoryPlaceholder,
    /// <summary>
    /// The optional placeholder: 6.4.4.5.5	OptionalPlaceholder
    /// </summary>
    OptionalPlaceholder,
    /// <summary>
    /// The exposes its array - 6.4.4.5.4	ExposesItsArray
    /// </summary>
    ExposesItsArray,

  }
}
