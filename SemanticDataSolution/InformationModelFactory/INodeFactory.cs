
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{
  public interface INodeFactory : INodeContainer
  {
    string BrowseName
    {
      set;
    }
    void AddDescription(string localeField, string valueField);
    void AddDisplayName(string localeField, string valueField);
    IReferenceFactory NewReference();
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
