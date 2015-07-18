
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IVariableTypeFactory
  /// </summary>
  public interface IVariableTypeFactory : ITypeFactory, IDataDescriptor
  {

    /// <summary>
    /// Sets the default value. The value of the Variable node that the server assigns while instantiating the node. Its data type is defined by the DataType field.
    /// </summary>
    /// <value>The default value.</value>
    XmlElement DefaultValue
    {
      set;
    }

  }
}
