//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


using System;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.Utilities
{
  /// <summary>
  /// Class ModelTableEntry that is defined in the <see cref="UANodeSet"/> along with any dependencies these models have.
  /// </summary>
  public partial class ModelTableEntry
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelTableEntry"/> class.
    /// </summary>
    public ModelTableEntry()
    {
      this.AccessRestrictions = (byte)0;
    }
    /// <summary>
    /// Gets or sets the role permissions. The list of default RolePermissions for all Nodes in the model.
    /// </summary>
    /// <value>The role permissions.</value>
    public RolePermission[] RolePermissions { get; set; }
    /// <summary>
    /// Gets or sets the required model. A list of dependencies for the model. If the model requires a minimum version the PublicationDate shall be specified. 
    /// Tools which attempt to resolve these dependencies may accept any PublicationDate after this date.
    /// </summary>
    /// <value>The required model.</value>
    public ModelTableEntry[] RequiredModel { get; set; }
    /// <summary>
    /// Gets or sets the model URI. The URI for the model. This URI should be one of the entries in the <see cref="NamespaceTable"/> table.
    /// </summary>
    /// <value>The model URI.</value>
    public string ModelUri { get; set; }
    /// <summary>
    /// Gets or sets the version. The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.
    /// </summary>
    /// <value>The version.</value>
    public string Version { get; set; }
    /// <summary>
    /// Gets or sets the publication date. When the model was published. This value is used for comparisons if the model is defined in multiple UANodeSet files.
    /// </summary>
    /// <value>The publication date.</value>
    public DateTime? PublicationDate { get; set; }
    /// <summary>
    /// Gets or sets the access restrictions. The default AccessRestrictions that apply to all Nodes in the model.
    /// </summary>
    /// <value>The access restrictions.</value>
    public byte AccessRestrictions { get; set; }
  }


}
