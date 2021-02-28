//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UANodeSet : IUANodeSetModelHeader
  {
    #region API

    internal IUAModelContext ParseUAModelContext(IAddressSpaceURIRecalculate addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      UAModelContext model = UAModelContext.ParseUANodeSetModelHeader(this, addressSpaceContext, traceEvent);
      this.RecalculateNodeIds(model, traceEvent);
      return model;
    }

    #endregion API

    #region static helpers

    internal static UANodeSet ReadUADefinedTypes()
    {
      UANodeSet uaDefinedTypes = LoadResource(m_UADefinedTypesName);
      if (uaDefinedTypes.Models is null || uaDefinedTypes.Models.Length == 0)
        throw new ArgumentNullException(nameof(UANodeSet.Models));
      if (uaDefinedTypes.NamespaceUris is null)
        uaDefinedTypes.NamespaceUris = new string[] { uaDefinedTypes.Models[0].ModelUri };
      return uaDefinedTypes;
    }

    internal static UANodeSet ReadModelFile(FileInfo path)
    {
      XmlSerializer _serializer = new XmlSerializer(typeof(UANodeSet));
      using (FileStream _stream = new FileStream(path.FullName, FileMode.Open, FileAccess.Read))
        return (UANodeSet)_serializer.Deserialize(_stream);
    }

    //OPC UA standard NodeSet model resource folder.
    private const string m_UADefinedTypesName = @"UAOOI.SemanticData.UANodeSetValidation.XML.Opc.Ua.NodeSet2.xml";

    #endregion static helpers

    #region private

    private void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      if (this.Aliases != null)
        foreach (NodeIdAlias alias in this.Aliases)
          alias.RecalculateNodeIds(x => modelContext.ImportNodeId(x, trace));
      if (this.Items != null)
        foreach (UANode item in Items)
          item.RecalculateNodeIds(modelContext, trace);
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

    #endregion private
  }
}