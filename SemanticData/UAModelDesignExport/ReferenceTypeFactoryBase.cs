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
  /// Class ReferenceTypeFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.TypeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IReferenceTypeFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.TypeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IReferenceTypeFactory" />
  internal class ReferenceTypeFactoryBase : TypeFactoryBase, IReferenceTypeFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public ReferenceTypeFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //IReferenceTypeFactory
    /// <summary>
    /// Sets a value indicating whether this <see cref="T:UAOOI.SemanticData.InformationModelFactory.IReferenceTypeFactory" /> is symmetric. The Symmetric attribute is used to indicate whether or not the meaning of the reference type is the same for both the source and target nodes.
    /// If a reference type is symmetric, the InverseName attribute shall be omitted.Examples of symmetric reference types are “Connects To” and “Communicates With”. Both imply the same semantic coming from the source node or the target node.
    /// If the ReferenceType is non-symmetric and not abstract, the InverseName attribute shall be set. The optional InverseName attribute of LocalizedText ia a inverse name of the reference,
    /// i.e.the meaning of the type as seen from the target node. Examples of non-symmetric reference types include “Contains” and “Contained In”, and “Receives From” and “Sends To”.
    /// </summary>
    /// <value><c>true</c> if symmetric; otherwise, <c>false</c>.</value>
    /// <remarks>Default Value is <b>false</b></remarks>
    public bool Symmetric
    {
      set;
      private get;
    }
    /// <summary>
    /// Adds a new inverse name.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    public void AddInverseName(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_InverseName, TraceEvent);
    }

    //internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      ReferenceTypeDesign _ret = new ReferenceTypeDesign()
      {
        InverseName = m_InverseName,
        Symmetric = this.Symmetric,
        SymmetricSpecified = this.Symmetric
      };
      Update(_ret, path, createInstanceType);
      return _ret;
    }

    //private
    private LocalizedText m_InverseName;
  }

}
