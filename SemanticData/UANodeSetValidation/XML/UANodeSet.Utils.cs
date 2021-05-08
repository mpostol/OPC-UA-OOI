//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.IO;
using System.Reflection;
using UAOOI.Common.Infrastructure.Serializers;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UANodeSet : IUANodeSetModelHeader
  {
    #region API

    internal Uri ParseUAModelContext(INamespaceTable addressSpaceContext, Action<TraceMessage> traceEvent)
    {
      UAModelContext model = UAModelContext.ParseUANodeSetModelHeader(this, addressSpaceContext, traceEvent);
      this.RecalculateNodeIds(model, traceEvent);
      return model.DefaultUri;
    }

    #endregion API

    #region static helpers

    internal static UANodeSet ReadUADefinedTypes()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      UANodeSet uaDefinedTypes = XmlFile.ReadXmlFile<UANodeSet>(assembly.GetManifestResourceStream(m_UADefinedTypesName));
      if (uaDefinedTypes.Models is null || uaDefinedTypes.Models.Length == 0)
        throw new ArgumentNullException(nameof(UANodeSet.Models));
      if (uaDefinedTypes.NamespaceUris is null)
        uaDefinedTypes.NamespaceUris = new string[] { uaDefinedTypes.Models[0].ModelUri };
      return uaDefinedTypes;
    }

    internal static UANodeSet ReadModelFile(FileInfo path)
    {
      return XmlFile.ReadXmlFile<UANodeSet>(path.OpenRead());
    }

    #endregion static helpers

    #region private

    private const string m_UADefinedTypesName = @"UAOOI.SemanticData.UANodeSetValidation.XML.Opc.Ua.NodeSet2.xml"; //OPC UA standard NodeSet model resource folder.

    private void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      if (this.Aliases != null)
        foreach (NodeIdAlias alias in this.Aliases)
          alias.RecalculateNodeIds(x => modelContext.ImportNodeId(x, trace));
      if (this.Items != null)
        foreach (UANode item in Items)
          item.RecalculateNodeIds(modelContext, trace);
    }

    #endregion private
  }
}