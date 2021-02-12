//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class RolePermission - default RolePermissions for all Nodes in the model.
  /// </summary>
  /// <remarks>
  /// This type is defined in Part 6 F.5 but the definition is not compliant with the UANodeSet schema.
  /// This type is also defined in the Part 3 5.2.9 but the definition is not compliant.
  /// </remarks>
  public interface IRolePermission
  {
    /// <summary>
    /// Gets or sets the permissions.
    /// </summary>
    /// <remarks>
    /// This is a subtype of the UInt32 DataType with the OptionSetValues Property defined. It is used to define the permissions of a Node. The <c>PermissionType</c> is formally defined in Part3 8.55 Table 38.
    /// </remarks>
    /// <value>The permissions.</value>
    uint Permissions { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <remarks>
    /// Not defined in the spec.
    /// </remarks>
    /// <value>The value.</value>
    string Value { get; set; }
  }
}