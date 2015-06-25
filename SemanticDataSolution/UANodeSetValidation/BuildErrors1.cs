
namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class BuildError - provides building descriptions of building errors. 
  /// </summary>
  public partial class BuildError
  {
    /// <summary>
    /// Error: P0-0001010000; Focus: XML; The XML attribute or element is not supported and neglected.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001010000.</value>
    public static BuildError NotSupportedFeature { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P0-0001010000", 
                    Descriptor = "The XML attribute or element is not supported and neglected." }; } }
    /// <summary>
    /// Error: P0-0605020210; Focus: NodeClass; The syntax of the ExpandedNodeId is not valid.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0605020210.</value>
    public static BuildError ExpandedNodeIdInvalidSyntax { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P0-0605020210", 
                    Descriptor = "The syntax of the ExpandedNodeId is not valid." }; } }
    /// <summary>
    /// Error: P0-0605020209; Focus: NodeClass; The syntax of the NodeId is not valid.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0605020209.</value>
    public static BuildError NodeIdInvalidSyntax { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P0-0605020209", 
                    Descriptor = "The syntax of the NodeId is not valid." }; } }
    /// <summary>
    /// Error: P0-0605020213; Focus: NodeClass; The syntax of the QualifiedName is not valid.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0605020213.</value>
    public static BuildError QualifiedNameInvalidSyntax { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P0-0605020213", 
                    Descriptor = "The syntax of the QualifiedName is not valid." }; } }
    /// <summary>
    /// Error: P3-0502070000; Focus: NodeClass; The value must be less than 0x200000 for the UAVariable and less than 0x400000 for other node types.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0502070000.</value>
    public static BuildError WrongWriteMaskValue { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0502070000", 
                    Descriptor = "The value must be less than 0x200000 for the UAVariable and less than 0x400000 for other node types." }; } }
    /// <summary>
    /// Error: P3-0502050000; Focus: NodeClass; The string part of the DisplayName is restricted to 512 characters.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0502050000.</value>
    public static BuildError WrongDisplayNameLength { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0502050000", 
                    Descriptor = "The string part of the DisplayName is restricted to 512 characters." }; } }
    /// <summary>
    /// Error: P3-06543210001; Focus: Reference; The reference target doesn't exist.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-06543210001.</value>
    public static BuildError DanglingReferenceType { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-06543210001", 
                    Descriptor = "The reference target doesn't exist." }; } }
    /// <summary>
    /// Error: P3-06543210000; Focus: Reference; The node pointed by the ReferenceType doesn't exist.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-06543210000.</value>
    public static BuildError DanglingReferenceTarget { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-06543210000", 
                    Descriptor = "The node pointed by the ReferenceType doesn't exist." }; } }
    /// <summary>
    /// Error: P3-0710000000; Focus: Reference; Each Node shall be the TargetNode of at most one Reference of type HasSubtype.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0710000000.</value>
    public static BuildError HasSubtypeMulitarget { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0710000000", 
                    Descriptor = "Each Node shall be the TargetNode of at most one Reference of type HasSubtype." }; } }
    /// <summary>
    /// Error: P3-0503030201; Focus: Reference; Wrong Reference type targeting the Property component.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0503030201.</value>
    public static BuildError WrongReference2Property { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0503030201", 
                    Descriptor = "Wrong Reference type targeting the Property component." }; } }
    /// <summary>
    /// Error: P3-0707000000; Focus: Reference; Wrong Reference type targeting the DataVariable component.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0707000000.</value>
    public static BuildError WrongReference2Variable { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0707000000", 
                    Descriptor = "Wrong Reference type targeting the DataVariable component." }; } }
    /// <summary>
    /// Error: P3-0707000001; Focus: Reference; Wrong Reference type for Method component.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0707000001.</value>
    public static BuildError WrongReference2Method { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0707000001", 
                    Descriptor = "Wrong Reference type for Method component." }; } }
    /// <summary>
    /// Error: P6-0503011400; Focus: NodeClass; BrowseName cannot be null string.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P6-0503011400.</value>
    public static BuildError EmptyBrowseName { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P6-0503011400", 
                    Descriptor = "BrowseName cannot be null string." }; } }
    /// <summary>
    /// Error: P6-0F03000000; Focus: NodeClass; SymbolicName contains characters that are not allowed. Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P6-0F03000000.</value>
    public static BuildError WrongSymbolicName { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P6-0F03000000", 
                    Descriptor = "SymbolicName contains characters that are not allowed. Only letters, digits or the underscore (‘_’) are permitted." }; } }
    /// <summary>
    /// Error: P3-0505010000; Focus: NodeClass; Object NodeClass: EventNotifier is out of range.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0505010000.</value>
    public static BuildError WrongEventNotifier { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0505010000", 
                    Descriptor = "Object NodeClass: EventNotifier is out of range." }; } }
    /// <summary>
    /// Error: P3-0505010001; Focus: NodeClass; Object NodeClass: Value of the EventNotifier attribute is not supported.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0505010001.</value>
    public static BuildError EventNotifierValueNotSupported { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0505010001", 
                    Descriptor = "Object NodeClass: Value of the EventNotifier attribute is not supported." }; } }
    /// <summary>
    /// Error: P3-0506020000; Focus: NodeClass; ValueRank value is out of range.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0506020000.</value>
    public static BuildError WrongValueRank { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0506020000", 
                    Descriptor = "ValueRank value is out of range." }; } }
    /// <summary>
    /// Error: P3-0506040000; Focus: NodeClass; AccessLevel value is out of range.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0506040000.</value>
    public static BuildError WrongAccessLevel { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0506040000", 
                    Descriptor = "AccessLevel value is out of range." }; } }
    /// <summary>
    /// Error: P3-0503020000; Focus: NodeClass; Unexpected value of the InverseName.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0503020000.</value>
    public static BuildError WrongInverseName { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0503020000", 
                    Descriptor = "Unexpected value of the InverseName." }; } }
    /// <summary>
    /// Error: P3-0502020000; Focus: NodeClass; Nodes shall be unambiguously identified using NodeId
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0502020000.</value>
    public static BuildError NodeIdDuplicated { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0502020000", 
                    Descriptor = "Nodes shall be unambiguously identified using NodeId" }; } }
    /// <summary>
    /// Error: P3-0502020001; Focus: NodeClass; NodeId is not defined.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0502020001.</value>
    public static BuildError NodeIdNotDefined { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0502020001", 
                    Descriptor = "NodeId is not defined." }; } }
    /// <summary>
    /// Error: P0-0001020000; Focus: XML; Node cannot be null
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001020000.</value>
    public static BuildError NodeCannotBeNull { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P0-0001020000", 
                    Descriptor = "Node cannot be null" }; } }
    /// <summary>
    /// Error: P0-0002010000; Focus: NonCategorized; General processing error see trace for details.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0002010000.</value>
    public static BuildError NonCategorized { get { return new BuildError() 
                  { Focus = Focus.NonCategorized, 
                    Identifier = "P0-0002010000", 
                    Descriptor = "General processing error see trace for details." }; } }
    /// <summary>
    /// Error: P0-0003010000; Focus: Diagnostic; It is diagnostic information
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0003010000.</value>
    public static BuildError DiagnosticInformation { get { return new BuildError() 
                  { Focus = Focus.Diagnostic, 
                    Identifier = "P0-0003010000", 
                    Descriptor = "It is diagnostic information" }; } }


  }
}