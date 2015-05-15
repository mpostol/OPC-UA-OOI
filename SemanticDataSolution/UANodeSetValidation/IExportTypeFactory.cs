
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportTypeFactory : IExportNodeFactory
  {

    System.Xml.XmlQualifiedName BaseType { set; }
    /// <summary>
    /// Sets a value indicating whether this instance is abstract.
    /// </summary>
    /// <remarks>Default Value is false</remarks>
    /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
    bool IsAbstract{ set; }

  }
}
