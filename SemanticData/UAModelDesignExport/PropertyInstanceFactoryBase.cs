//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
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

  /// <summary>
  /// Class PropertyInstanceFactoryBase.
  /// </summary>
  internal class PropertyInstanceFactoryBase : VariableInstanceFactoryBase, IPropertyInstanceFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyInstanceFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public PropertyInstanceFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      PropertyDesign _ret = new PropertyDesign();
      base.Update(_ret, path, createInstanceType);
      return _ret;
    }

  }

}
