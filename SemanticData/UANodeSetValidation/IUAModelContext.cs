//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  internal interface IUAModelContext
  {

    QualifiedName ImportQualifiedName(QualifiedName broseName);
    XmlQualifiedName ExportBrowseName(string nodeId, NodeId defaultValue);
    Parameter ExportArgument(DataSerialization.Argument item);
    IUANodeContext GetOrCreateNodeContext(string value, bool v);
    NodeId ImportNodeId(string nodeId, bool lookupAlias);
    XmlQualifiedName ExportQualifiedName(QualifiedName source);

  }

}