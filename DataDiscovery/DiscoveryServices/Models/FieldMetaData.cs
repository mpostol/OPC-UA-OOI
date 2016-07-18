
using Serialization = UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.DataDiscovery.DiscoveryServices.Models
{
  public partial class FieldMetaData
  {
    /// <summary>
    /// Performs an explicit conversion from <see cref="Serialization.FieldMetaData"/> to <see cref="FieldMetaData"/>.
    /// </summary>
    /// <param name="fieldMetaData">The field meta data.</param>
    /// <returns>An instance of this type - the result of the conversion.</returns>
    public static implicit operator FieldMetaData(Serialization.FieldMetaData fieldMetaData)
    {
      FieldMetaData _ret = new FieldMetaData()
      {
        ExtensionData = null,
        ProcessValueName = fieldMetaData.ProcessValueName,
        SymbolicName = fieldMetaData.SymbolicName,
        TypeInformation = fieldMetaData.TypeInformation
      };
      return _ret;
    }
    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>An instance of the <see cref="Serialization.FieldMetaData"/> derived from this instance.</returns>
    public Serialization.FieldMetaData Clone()
    {
      Serialization.FieldMetaData _ret = new Serialization.FieldMetaData()
      {
        ExtensionData = null,
        ProcessValueName = ProcessValueName,
        SymbolicName = SymbolicName,
        TypeInformation = TypeInformation == null ? null : TypeInformation.Clone()
      };
      return _ret;
    }
  }
}
