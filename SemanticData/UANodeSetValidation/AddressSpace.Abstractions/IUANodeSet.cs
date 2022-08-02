//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Xml;
//TODO Define independent Address Space API #645 - this dependency must be removed
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  public interface IUANodeSet
  {
    Uri ParseUAModelContext(INamespaceTable addressSpaceContext, Action<TraceMessage> traceEvent);

    string[] NamespaceUris { get; set; }
    string[] ServerUris { get; set; }
    XmlElement[] Extensions { get; set; }
    IUANode[] Items { get; }
    DateTime LastModified { get; set; }
    bool LastModifiedSpecified { get; set; }
  }
}