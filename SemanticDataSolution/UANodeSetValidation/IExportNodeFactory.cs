
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportNodeFactory : IExportNodeContainer
  {
    string BrowseName
    {
      set;
    }
    XML.LocalizedText[] Description
    {
      set;
    }
    XML.LocalizedText[] DisplayName
    {
      set;
    }
    IExportReferenceFactory NewReference();
    XmlQualifiedName SymbolicName
    {
      set;
    }
    /// <summary>
    /// Sets the write access.
    /// </summary>
    /// <remarks>Default Value "0"</remarks>
    /// <value>The write access.</value>
    uint WriteAccess
    {
      set;
    }

  }
}
