//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UANodeSet
  {

    #region API
    internal void RecalculateNodeIds(IUAModelContext modelContext)
    {
      if (this.Aliases != null)
        foreach (NodeIdAlias _alias in this.Aliases)
          _alias.RecalculateNodeIds(modelContext.ImportNodeId);
      if (this.Items != null)
        foreach (UANode item in Items)
          item.RecalculateNodeIds(modelContext);
    }
    #endregion

    #region static helpers
    internal static UANodeSet ReadUADefinedTypes()
    {
      return LoadResource(m_UADefinedTypesName);
    }
    /// <summary>
    /// Loads a schema from an embedded resource.
    /// </summary>
    private static UANodeSet LoadResource(string path)
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      XmlSerializer _serializer = new XmlSerializer(typeof(UANodeSet));
      using (StreamReader _stream = new StreamReader(assembly.GetManifestResourceStream(path)))
        return (UANodeSet)_serializer.Deserialize(_stream);
    }
    internal static UANodeSet ReadModelFile(FileInfo path)
    {
      XmlSerializer _serializer = new XmlSerializer(typeof(UANodeSet));
      using (FileStream _stream = new FileStream(path.FullName, FileMode.Open, FileAccess.Read))
        return (UANodeSet)_serializer.Deserialize(_stream);
    }
    //OPC UA standard NodeSet model resource folder.
    private const string m_UADefinedTypesName = @"UAOOI.SemanticData.UANodeSetValidation.XML.Opc.Ua.NodeSet2.xml";
    #endregion

    //TODO #40 remove
//#if DEBUG
//    /// <summary>
//    /// Gets the name of the xml file path containing the standard OPC UA NodeSet
//    /// </summary>
//    /// <value>The name of the resource.</value>
//    internal static string UTUADefinedTypesName { get { return m_UADefinedTypesName; } }
//#endif

  }
}
