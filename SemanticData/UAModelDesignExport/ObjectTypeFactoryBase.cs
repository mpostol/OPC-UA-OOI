//___________________________________________________________________________________
//
//  Copyright (C) Year of Copyright, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class ObjectTypeFactoryBase : TypeFactoryBase, IObjectTypeFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public ObjectTypeFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      ObjectTypeDesign _ret = new ObjectTypeDesign()
      {
        SupportsEvents = false,
        SupportsEventsSpecified = false
      };
      base.Update(_ret, path, createInstanceType);
      return _ret;
    }

  }

}
