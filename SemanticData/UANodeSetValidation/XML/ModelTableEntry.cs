//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class ModelTableEntry.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.IModelTableEntry" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.IModelTableEntry" />
  public partial class ModelTableEntry : IModelTableEntry
  {
    /// <summary>
    /// Gets or sets the required model. A list of dependencies for the model. If the model requires a minimum version the PublicationDate shall be specified.
    /// Tools which attempt to resolve these dependencies may accept any PublicationDate after this date.
    /// </summary>
    /// <value>The required model.</value>
    IModelTableEntry[] IModelTableEntry.RequiredModel => RequiredModel;

    /// <summary>
    /// Gets or sets the role permissions. The list of default RolePermissions for all Nodes in the model.
    /// </summary>
    /// <value>The role permissions.</value>
    IRolePermission[] IModelTableEntry.RolePermissions => RolePermissions;

    /// <summary>
    /// Gets or sets the access restrictions. The default <c>AccessRestrictions</c> that apply to all <c>Nodes</c> in the model.
    /// </summary>
    /// <value>The access restrictions.</value>
    byte IModelTableEntry.AccessRestrictions => AccessRestrictions;

    /// <summary>
    /// Gets the <see cref="Uri"/> for the model. This URI should be one of the entries in the namespace table.
    /// </summary>
    /// <value>The model <see cref="Uri"/>.</value>
    Uri IModelTableEntry.ModelUri => new Uri(ModelUri);

    /// <summary>
    /// Gets or sets the publication date. When the model was published. This value is used for comparisons if the model is defined in multiple UANodeSet files.
    /// </summary>
    /// <value>The publication date.</value>
    DateTime? IModelTableEntry.PublicationDate => this.PublicationDateSpecified ? PublicationDate : new Nullable<DateTime>();

    /// <summary>
    /// Gets or sets the version. The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.
    /// </summary>
    /// <value>The version.</value>
    Version IModelTableEntry.Version
    {
      get
      {
        Version version = null;
        System.Version.TryParse(this.Version, out version);
        return version;
      }
    }
  }
}