//__________________________________________________________________________________________
//
//  Copyright 2022 Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and to get started
//  comment using the discussion panel at
//  https://github.com/mpostol/TP/discussions/182
//  with an introduction of yourself and tell us about what you do with this community.
//__________________________________________________________________________________________

using System;
using System.IO;
using System.Reflection;
using UAOOI.Common.Infrastructure.Serializers;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UANodeSet : IUANodeSet, IUANodeSetModelHeader
  {
    #region IUANodeSet

    public Uri ParseUAModelContext(INamespaceTable namespaceTable, Action<TraceMessage> traceEvent)
    {
      UAModelContext model = UAModelContext.ParseUANodeSetModelHeader(this, namespaceTable, traceEvent);
      this.RecalculateNodeIds(model, traceEvent);
      return model.DefaultUri;
    }

    IUANode[] IUANodeSet.Items { get => Items; }

    #endregion IUANodeSet

    #region static helpers

    public static UANodeSet ReadUADefinedTypes()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      UANodeSet uaDefinedTypes = null;
      using (Stream stream = assembly.GetManifestResourceStream(m_UADefinedTypesName))
        uaDefinedTypes = XmlFile.ReadXmlFile<UANodeSet>(stream);
      if (uaDefinedTypes.Models is null || uaDefinedTypes.Models.Length == 0)
        throw new ArgumentNullException(nameof(UANodeSet.Models));
      if (uaDefinedTypes.NamespaceUris is null)
        uaDefinedTypes.NamespaceUris = new string[] { uaDefinedTypes.Models[0].ModelUri };
      return uaDefinedTypes;
    }

    public static UANodeSet ReadModelFile(FileInfo path)
    {
      if (path == null)
        throw new ArgumentNullException($"{nameof(path)}");
      using (Stream stream = path.OpenRead())
        return XmlFile.ReadXmlFile<UANodeSet>(stream);
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