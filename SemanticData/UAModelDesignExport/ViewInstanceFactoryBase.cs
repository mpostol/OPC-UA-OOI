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
  /// Class ViewInstanceFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.InstanceFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IViewInstanceFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.InstanceFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IViewInstanceFactory" />
  internal class ViewInstanceFactoryBase : InstanceFactoryBase, IViewInstanceFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public ViewInstanceFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //IViewInstanceFactory
    /// <summary>
    /// Sets a value indicating whether the events are supported.
    /// </summary>
    /// <value><c>null</c> if it contains no value, <c>true</c> if events are supported; otherwise, <c>false</c>.</value>
    public bool? SupportsEvents
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets a value indicating whether the part of the Address Space represented by View contains no loops.
    /// The mandatory ContainsNoLoops attribute is set to false if the server is not able to identify if the view contains loops or not.
    /// </summary>
    /// <value><c>true</c> if the part of the Address Space represented by View contains no loops; otherwise, <c>false</c>.</value>
    public bool ContainsNoLoops
    {
      set;
      private get;
    }

    //internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      ViewDesign _ret = new ViewDesign()
      {
        ContainsNoLoops = this.ContainsNoLoops, //TODO test against the loops.
        SupportsEvents = this.SupportsEvents.GetValueOrDefault(),
      };
      UpdateInstance(_ret, path, TraceEvent, createInstanceType);
      return _ret;
    }

  }
}
