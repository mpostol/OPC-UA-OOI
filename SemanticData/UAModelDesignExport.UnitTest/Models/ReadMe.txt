
Reference 
	IsOneWay is ignored by the compiler. It is assigned always false.
	Symmetric - default value is false and after serialization it always exist.
	To test customs references duplicated references must be removed manualy to make sure the preserved are associated with the orginal nodes.
UAVariableType
	Value is not descrobed in the specyfication - it is assumed it represents default value.
VariableDesign
	UserAccessLevel - is not supported.
MethodDesign
    InputArguments, OutputArguments: Identifier is not supported by the compiler and IdentifierSpecified should be always false.
	NonExecutable is compiled to the attributes Executable, UserExecutable of an alone (not atached) node that is added to the model = both are set to false if NonExecutable is false. It is not supported by the recovery mechanizm. 
Reserwed Browse names: InputArguments, OutputArguments For the reserwer BrowseName identifiers the Opc.UA namespace is assigned while QualifiedName is created.
ModelDesign notes

InstanceDesign
	not supported attributes: Declaration, PreserveDefaultAttributes, MinCardinality, MaxCardinality.
DataTypeDesign:
	not supported attributes: NotInAddressSpace, NoArraysAllowed. 
		NotInAddressSpace: compiler omots this node.
		NoArraysAllowed: do not generate ListOf... from the schema. To recover schema must be anlized.
		EncodingDesign - is not supported - it is not used in any model I have.
