
using System;
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  public enum ReferenceKindEnum { Custom, HasComponent, HasModellingRule, HasTypeDefinition, HierarchicalReferences, HasSubtype, HasProperty };

  /// <summary>
  /// Interface IReferenceContext - represent context of the interface.
  /// </summary>
  public interface IUAReferenceContext
  {

    /// <summary>
    /// Gets the kind of the reference.
    /// </summary>
    /// <returns>ReferenceKindEnum.</returns>
    ReferenceKindEnum ReferenceKind { get; }
    /// <summary>
    /// Gets the name <see cref="XmlQualifiedName" /> of the reference type.
    /// </summary>
    /// <param name="modelContext">The model context containing node set related information.</param>
    /// <param name="traceEvent">An <see cref="Action" /> delegate is used to trace event as the <see cref="TraceMessage" />.</param>
    /// <returns>Return an instance of <see cref="XmlQualifiedName" /> representing the reference type name</returns>
    XmlQualifiedName GetReferenceTypeName(IUAModelContext modelContext, Action<TraceMessage> traceEvent);

  }
}
