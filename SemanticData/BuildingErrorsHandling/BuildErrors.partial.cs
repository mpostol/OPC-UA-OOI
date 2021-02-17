//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.BuildingErrorsHandling
{
  /// <summary>
  /// Enum Focus
  /// </summary>
  public enum Focus
  {
    /// <summary>
    /// The reference
    /// </summary>
    Reference,

    /// <summary>
    /// The diagnostic
    /// </summary>
    Diagnostic,

    /// <summary>
    /// The NodeClass
    /// </summary>
    NodeClass,

    /// <summary>
    /// The XML error
    /// </summary>
    XML,

    /// <summary>
    /// The non categorized error, e.g. exception during execution.
    /// </summary>
    NonCategorized,

    /// <summary>
    /// The data encoding errors - the syntax is validated against OPC UA XML encoding.
    /// </summary>
    DataEncoding,

    /// <summary>
    /// The data type definition error - eny error that relates to custom DataType definion.
    /// </summary>
    DataType,

    /// <summary>
    /// The naming
    /// </summary>
    Naming
  }

  /// <summary>
  /// Class BuildError - provides building descriptions of building errors.
  /// </summary>
  public partial class BuildError
  {
    /// <summary>
    /// Gets the focus.
    /// </summary>
    /// <value>The focus.</value>
    public Focus Focus { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the error.
    /// </summary>
    /// <value>The identifier.</value>
    public string Identifier { get; set; }

    /// <summary>
    /// Gets or sets the descriptor of the error.
    /// </summary>
    /// <value>The descriptor.</value>
    public string Descriptor { get; set; }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return string.Format("Focus: {0}, Identifier: {1} Description: {2}", Focus, Identifier, Descriptor);
    }
  }
}