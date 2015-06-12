using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface IParameter: IDataDescriptor
  {

    /// <summary>
    /// Sets the name of the parameter.
    /// </summary>
    /// <value>The name.</value>
    string Name { set; }
    /// <summary>
    /// Sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    int? Identifier { set; }

  }
}
