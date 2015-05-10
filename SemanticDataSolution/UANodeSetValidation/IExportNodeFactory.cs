
namespace UAOOI.SemanticData.UANodeSetValidation
{
  public interface IExportNodeFactory
  {
    string BrowseName
    {
      set;
    }
    IExportInstanceFactory[] Children
    {
      set;
      get;
    }
    XML.LocalizedText[] Description
    {
      set;
      get;
    }
    XML.LocalizedText[] DisplayName
    {
      set;
    }

    IUAReferenceContext[] References
    {
      set;
    }
    System.Xml.XmlQualifiedName SymbolicName
    {
      get;
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
