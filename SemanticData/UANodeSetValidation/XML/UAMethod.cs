//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Class UAMethod
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.XML.UAInstance" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.XML.UAInstance" />
  public partial class UAMethod
  {
    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      base.RecalculateNodeIds(modelContext, trace);
      MethodDeclarationNodeId = modelContext.ImportNodeId(this.MethodDeclarationId, trace);
    }

    internal NodeId MethodDeclarationNodeId { get; private set; }

    /// <summary>
    /// Indicates whether the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(UANode other)
    {
      UAMethod _other = other as UAMethod;
      if (Object.ReferenceEquals(_other, null))
        return false;
      return
        base.ParentEquals(_other) &&
        // TODO compare ArgumentDescription
        this.Executable == _other.Executable &&
        this.UserExecutable == _other.UserExecutable;
      // not exposed and must be excluded from the comparison this.MethodDeclarationId == _other.MethodDeclarationId;
    }

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="T:UAOOI.SemanticData.UANodeSetValidation.XML.UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UAMethod _ret = new UAMethod
      {
        Executable = this.Executable,
        UserExecutable = this.UserExecutable
      };
      base.CloneUAInstance(_ret);
      return _ret;
    }
  }
}