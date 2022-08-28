//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface IModelTableEntry 
  /// </summary>
  public interface IModelTableEntry
  {
    /// <summary>
    /// Gets or sets the access restrictions. The default <c>AccessRestrictions</c> that apply to all <c>Nodes</c> in the model.
    /// </summary>
    /// <value>The access restrictions.</value>
    byte AccessRestrictions { get; }

    /// <summary>
    /// Gets the <see cref="Uri"/> for the model. This URI should be one of the entries in the namespace table.
    /// </summary>
    /// <value>The model <see cref="Uri"/>.</value>
    Uri ModelUri { get; }

    /// <summary>
    /// Gets or sets the publication date. When the model was published. This value is used for comparisons if the model is defined in multiple UANodeSet files.
    /// </summary>
    /// <value>The publication date.</value>
    DateTime? PublicationDate { get; }

    /// <summary>
    /// Gets or sets the required model. A list of dependencies for the model. If the model requires a minimum version the PublicationDate shall be specified.
    /// Tools which attempt to resolve these dependencies may accept any PublicationDate after this date.
    /// </summary>
    /// <value>The required model.</value>
    IModelTableEntry[] RequiredModel { get; }

    /// <summary>
    /// Gets or sets the role permissions. The list of default RolePermissions for all Nodes in the model.
    /// </summary>
    /// <value>The role permissions.</value>
    IRolePermission[] RolePermissions { get; }

    /// <summary>
    /// Gets or sets the version. The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.
    /// </summary>
    /// <value>The version.</value>
    Version Version { get; }
  }
}