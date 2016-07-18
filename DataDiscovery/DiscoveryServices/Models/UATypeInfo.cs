﻿
using System;
using System.Linq;
using Serialization = UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.DataDiscovery.DiscoveryServices.Models
{
  public partial class UATypeInfo
  {
    /// <summary>
    /// Performs an implicit conversion from <see cref="Serialization.UATypeInfo"/> to <see cref="UATypeInfo"/>.
    /// </summary>
    /// <param name="typeInfo">The type information.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator UATypeInfo(Serialization.UATypeInfo typeInfo)
    {
      UATypeInfo _newUATypeInfo = typeInfo == null ? null : new UATypeInfo()
      {
        ArrayDimensions = typeInfo.ArrayDimensions == null ? null : typeInfo.ArrayDimensions.Select<int, int>(x => x).ToArray<int>(),
        BuiltInType = (BuiltInType)Convert.ToUInt16(typeInfo.BuiltInType),
        TypeName = typeInfo.TypeName == null ? null : new System.Xml.XmlQualifiedName(typeInfo.TypeName.Name, typeInfo.TypeName.Namespace),
        ValueRank = typeInfo.ValueRank
      };
      return _newUATypeInfo;
    }
    /// <summary>
    /// Clones this instance and factor the object of <see cref="Serialization.UATypeInfo"/>.
    /// </summary>
    /// <returns>An instance of the <see cref="Serialization.UATypeInfo"/> derived from this instance.</returns>
    public Serialization.UATypeInfo Clone()
    {
      Serialization.UATypeInfo _newUATypeInfo = new Serialization.UATypeInfo( BuiltInType.ToBuiltInType(), 
                                                                              ValueRank, 
                                                                              ArrayDimensions == null ? null : ArrayDimensions.Select<int, int>(x => x).ToArray<int>())
      {
        TypeName = TypeName == null ? null : new System.Xml.XmlQualifiedName(TypeName.Name, TypeName.Namespace),
      };
      return _newUATypeInfo;
    }
  }
}
