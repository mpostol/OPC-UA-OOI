
//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.BuildingErrorsHandling
{
  /// <summary>
  /// Class BuildError - provides building descriptions of building errors. 
  /// </summary>
  public partial class BuildError
  {
    /// <summary>
    /// Error: P3-0305000000; Focus: Reference; It is not allowed that References are used to create a looping hierarchy.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0305000000.</value>
    public static BuildError NotValidLoopingHierarchy { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0305000000", 
                    Descriptor = "It is not allowed that References are used to create a looping hierarchy." }; } }
    /// <summary>
    /// Error: P0-0001010000; Focus: Diagnostic; The XML attribute or element is not supported and neglected.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001010000.</value>
    public static BuildError NotSupportedFeature { get { return new BuildError() 
                  { Focus = Focus.Diagnostic, 
                    Identifier = "P0-0001010000", 
                    Descriptor = "The XML attribute or element is not supported and neglected." }; } }
    /// <summary>
    /// Error: P0-0001020000; Focus: XML; Node cannot be null
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001020000.</value>
    public static BuildError NodeCannotBeNull { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P0-0001020000", 
                    Descriptor = "Node cannot be null" }; } }
    /// <summary>
    /// Error: P0-0001030000; Focus: XML; Models element cannot be null or empty
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001030000.</value>
    public static BuildError ModelsCannotBeNull { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P0-0001030000", 
                    Descriptor = "Models element cannot be null or empty" }; } }
    /// <summary>
    /// Error: P0-0001040000; Focus: XML; NamespaceUris element cannot be null or empty
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001040000.</value>
    public static BuildError NamespaceUrisCannotBeNull { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P0-0001040000", 
                    Descriptor = "NamespaceUris element cannot be null or empty" }; } }
    /// <summary>
    /// Error: P0-0001050000; Focus: XML; Selected model contains errors.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0001050000.</value>
    public static BuildError ModelContainsErrors { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P0-0001050000", 
                    Descriptor = "Selected model contains errors." }; } }
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
    /// <summary>
    /// Error: P0-0605020209; Focus: NodeClass; The syntax of the NodeId is not valid.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0605020209.</value>
    public static BuildError NodeIdInvalidSyntax { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P0-0605020209", 
                    Descriptor = "The syntax of the NodeId is not valid." }; } }
    /// <summary>
    /// Error: P0-0605020210; Focus: NodeClass; The syntax of the ExpandedNodeId is not valid.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P0-0605020210.</value>
    public static BuildError ExpandedNodeIdInvalidSyntax { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P0-0605020210", 
                    Descriptor = "The syntax of the ExpandedNodeId is not valid." }; } }
    /// <summary>
    /// Error: P6-0503021400; Focus: DataEncoding; The syntax of the QualifiedName is not valid.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P6-0503021400.</value>
    public static BuildError QualifiedNameInvalidSyntax { get { return new BuildError() 
                  { Focus = Focus.DataEncoding, 
                    Identifier = "P6-0503021400", 
                    Descriptor = "The syntax of the QualifiedName is not valid." }; } }
    /// <summary>
    /// Error: P3-0403040000; Focus: NodeClass; The reference target doesn't exist. OPC UA does not require that the TargetNode exists, thus References may point to a Node that does not exist.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0403040000.</value>
    public static BuildError DanglingReferenceTarget { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0403040000", 
                    Descriptor = "The reference target doesn't exist. OPC UA does not require that the TargetNode exists, thus References may point to a Node that does not exist." }; } }
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
    /// Error: P3-0502050000; Focus: NodeClass; The string part of the DisplayName is restricted to 512 characters.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0502050000.</value>
    public static BuildError WrongDisplayNameLength { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0502050000", 
                    Descriptor = "The string part of the DisplayName is restricted to 512 characters." }; } }
    /// <summary>
    /// Error: P3-0502070000; Focus: NodeClass; The value must be less than 0x200000 for the UAVariable and less than 0x400000 for other node types.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0502070000.</value>
    public static BuildError WrongWriteMaskValue { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0502070000", 
                    Descriptor = "The value must be less than 0x200000 for the UAVariable and less than 0x400000 for other node types." }; } }
    /// <summary>
    /// Error: P3-0856000000; Focus: NodeClass; The value must be less than 0x7.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0856000000.</value>
    public static BuildError WrongAccessRestriction { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0856000000", 
                    Descriptor = "The value must be less than 0x7." }; } }
    /// <summary>
    /// Error: P3-0503020000; Focus: NodeClass; Unexpected value of the InverseName.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0503020000.</value>
    public static BuildError WrongInverseName { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0503020000", 
                    Descriptor = "Unexpected value of the InverseName." }; } }
    /// <summary>
    /// Error: P3-0503020001; Focus: NodeClass; The BrowseName of a ReferenceType shall be unique.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0503020001.</value>
    public static BuildError DuplicatedReferenceType { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0503020001", 
                    Descriptor = "The BrowseName of a ReferenceType shall be unique." }; } }
    /// <summary>
    /// Error: P3-0503020002; Focus: NodeClass; The BrowseName of a ReferenceType is defined outside of the model.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0503020002.</value>
    public static BuildError BrowseNameReferenceTypeScope { get { return new BuildError() 
                  { Focus = Focus.NodeClass, 
                    Identifier = "P3-0503020002", 
                    Descriptor = "The BrowseName of a ReferenceType is defined outside of the model." }; } }
    /// <summary>
    /// Error: P3-0503030201; Focus: Reference; Wrong Reference type targeting the Property component.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0503030201.</value>
    public static BuildError WrongReference2Property { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0503030201", 
                    Descriptor = "Wrong Reference type targeting the Property component." }; } }
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
    /// Error: P3-0707000000; Focus: Reference; Wrong Reference type targeting the DataVariable component.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0707000000.</value>
    public static BuildError WrongReference2Variable { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0707000000", 
                    Descriptor = "Wrong Reference type targeting the DataVariable component." }; } }
    /// <summary>
    /// Error: P3-0707000001; Focus: Reference; Wrong Reference type targeting the Method component.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0707000001.</value>
    public static BuildError WrongReference2Method { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0707000001", 
                    Descriptor = "Wrong Reference type targeting the Method component." }; } }
    /// <summary>
    /// Error: P3-0707000002; Focus: Reference; Dangling reference - undefined target of the HasComponent reference.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0707000002.</value>
    public static BuildError UndefinedHasComponentTarget { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0707000002", 
                    Descriptor = "Dangling reference - undefined target of the HasComponent reference." }; } }
    /// <summary>
    /// Error: P3-0708000000; Focus: Reference; Dangling reference - undefined target of the HasProperty reference.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0708000000.</value>
    public static BuildError UndefinedHasPropertyTarget { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0708000000", 
                    Descriptor = "Dangling reference - undefined target of the HasProperty reference." }; } }
    /// <summary>
    /// Error: P3-0710000000; Focus: Reference; Each Node shall be the TargetNode of at most one Reference of type HasSubtype.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0710000000.</value>
    public static BuildError HasSubtypeMulitarget { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0710000000", 
                    Descriptor = "Each Node shall be the TargetNode of at most one Reference of type HasSubtype." }; } }
    /// <summary>
    /// Error: P3-0710000001; Focus: Reference; Dangling reference - undefined target of the HasSubtype reference.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0710000001.</value>
    public static BuildError UndefinedHasSubtypeTarget { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0710000001", 
                    Descriptor = "Dangling reference - undefined target of the HasSubtype reference." }; } }
    /// <summary>
    /// Error: P3-0713000000; Focus: Reference; Undefined HasTypeDefinition - each Variable and each Object shall be the SourceNode of exactly one HasTypeDefinition Reference.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0713000000.</value>
    public static BuildError UndefinedHasTypeDefinition { get { return new BuildError() 
                  { Focus = Focus.Reference, 
                    Identifier = "P3-0713000000", 
                    Descriptor = "Undefined HasTypeDefinition - each Variable and each Object shall be the SourceNode of exactly one HasTypeDefinition Reference." }; } }
    /// <summary>
    /// Error: P3-0802020000; Focus: Naming; Undefined namespace index - The numeric values used to identify namespaces correspond to the index into the NamespaceArray.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0802020000.</value>
    public static BuildError UndefinedNamespaceIndex { get { return new BuildError() 
                  { Focus = Focus.Naming, 
                    Identifier = "P3-0802020000", 
                    Descriptor = "Undefined namespace index - The numeric values used to identify namespaces correspond to the index into the NamespaceArray." }; } }
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
    /// Error: P6-0F02000000; Focus: XML; The required model is missing.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P6-0F02000000.</value>
    public static BuildError LackOfRequiredModel { get { return new BuildError() 
                  { Focus = Focus.XML, 
                    Identifier = "P6-0F02000000", 
                    Descriptor = "The required model is missing." }; } }
    /// <summary>
    /// Error: P3-XXXXX00000; Focus: DataType; Abstract DataType of the field of a concrete structure is not permitted.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-XXXXX00000.</value>
    public static BuildError FieldAbstractDataType { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-XXXXX00000", 
                    Descriptor = "Abstract DataType of the field of a concrete structure is not permitted." }; } }
    /// <summary>
    /// Error: P3-XXXXX00000; Focus: DataType; All fields must be present in derived Structure DataType.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-XXXXX00000.</value>
    public static BuildError MissingFieldInDerivedStructure { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-XXXXX00000", 
                    Descriptor = "All fields must be present in derived Structure DataType." }; } }
    /// <summary>
    /// Error: P3-XXXXX00000; Focus: DataType; Wrong DataType of field derived from Structure.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-XXXXX00000.</value>
    public static BuildError WrongDerivedFieldData { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-XXXXX00000", 
                    Descriptor = "Wrong DataType of field derived from Structure." }; } }
    /// <summary>
    /// Error: P3-0508010000; Focus: DataType; It is not permitted for two DataTypes to point to the same DataTypeEncoding.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0508010000.</value>
    public static BuildError WrongDataType2DataTypeEncodingReference { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-0508010000", 
                    Descriptor = "It is not permitted for two DataTypes to point to the same DataTypeEncoding." }; } }
    /// <summary>
    /// Error: P3-0508010000; Focus: DataType; The DataTypeEncoding Object shall point to exactly one Variable of type DataTypeDescriptionType
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0508010000.</value>
    public static BuildError WrongDataTypeEncoding2DataTypeDescriptionReference { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-0508010000", 
                    Descriptor = "The DataTypeEncoding Object shall point to exactly one Variable of type DataTypeDescriptionType" }; } }
    /// <summary>
    /// Error: P3-0508010000; Focus: DataType; The DataTypeDescription Variable shall belong to a DataTypeDictionary Variable.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0508010000.</value>
    public static BuildError WrongDataTypeDescription2DataTypeDictionaryReference { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-0508010000", 
                    Descriptor = "The DataTypeDescription Variable shall belong to a DataTypeDictionary Variable." }; } }
    /// <summary>
    /// Error: P3-0508030000; Focus: DataType; Only concrete Structured DataTypes may use HasEncoding References. Abstract, Built-in, Enumeration, and Simple DataTypes are not allowed to be the SourceNode of a HasEncoding Reference.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0508030000.</value>
    public static BuildError AbstractDataType2HasEncodingReference { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-0508030000", 
                    Descriptor = "Only concrete Structured DataTypes may use HasEncoding References. Abstract, Built-in, Enumeration, and Simple DataTypes are not allowed to be the SourceNode of a HasEncoding Reference." }; } }
    /// <summary>
    /// Error: P3-0508030000; Focus: DataType; Each concrete Structured DataType shall point to at least one DataTypeEncoding Object with the BrowseName “Default Binary” or “Default XML” having the NamespaceIndex 0.
    /// </summary>
    /// <value>An instance of <see cref="BuildError"/> describing the error P3-0508030000.</value>
    public static BuildError ConcreteDataType2HasEncodingReference { get { return new BuildError() 
                  { Focus = Focus.DataType, 
                    Identifier = "P3-0508030000", 
                    Descriptor = "Each concrete Structured DataType shall point to at least one DataTypeEncoding Object with the BrowseName “Default Binary” or “Default XML” having the NamespaceIndex 0." }; } }


  }
}