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
using TraceMessage = UAOOI.SemanticData.BuildingErrorsHandling.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  /// <summary>
  /// Class ObjectInstanceFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.InstanceFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IObjectInstanceFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.InstanceFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IObjectInstanceFactory" />
  internal class ObjectInstanceFactoryBase : InstanceFactoryBase, IObjectInstanceFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public ObjectInstanceFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    #region IObjectInstanceFactory
    /// <summary>
    /// Sets a value indicating whether the node supports events.
    /// </summary>
    /// <value><c>null</c> if supports events contains no value, <c>true</c> if [supports events]; otherwise, <c>false</c>.</value>
    public bool? SupportsEvents
    {
      set;
      private get;
    }
    #endregion

    #region internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      ObjectDesign _ret = new ObjectDesign()
      {
        SupportsEvents = this.SupportsEvents.GetValueOrDefault(),
        SupportsEventsSpecified = this.SupportsEvents.HasValue
      };
      base.UpdateInstance(_ret, path, TraceEvent, createInstanceType);
      return _ret;
    }
    #endregion

  }
}
