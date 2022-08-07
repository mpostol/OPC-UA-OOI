//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  //[DataContract(Namespace = Opc.Ua.Namespaces.OpcUaXsd)]
  /// <summary>
  /// This is a subtype of the UInt32 DataType with the OptionSetValues Property defined. It is used to define the Attribute access restrictions of a Node. 
  /// The AttributeWriteMask is formally defined in Part 3 8.60 Table 43.
  /// 
  /// If a bit is set to 0, it means the Attribute is not writable. If a bit is set to 1, it means it is writable. If a Node does not support a specific Attribute, the corresponding bit has to be 
  /// set to 0.
  /// </summary>
  public enum AttributeWriteMask : UInt32
  {
    /// <remarks />
    [EnumMember(Value = "None_0")]
    None = 0,

    /// <remarks />
    [EnumMember(Value = "AccessLevel_1")]
    AccessLevel = 1,

    /// <remarks />
    [EnumMember(Value = "ArrayDimensions_2")]
    ArrayDimensions = 2,

    /// <remarks />
    [EnumMember(Value = "BrowseName_4")]
    BrowseName = 4,

    /// <remarks />
    [EnumMember(Value = "ContainsNoLoops_8")]
    ContainsNoLoops = 8,

    /// <remarks />
    [EnumMember(Value = "DataType_16")]
    DataType = 16,

    /// <remarks />
    [EnumMember(Value = "Description_32")]
    Description = 32,

    /// <remarks />
    [EnumMember(Value = "DisplayName_64")]
    DisplayName = 64,

    /// <remarks />
    [EnumMember(Value = "EventNotifier_128")]
    EventNotifier = 128,

    /// <remarks />
    [EnumMember(Value = "Executable_256")]
    Executable = 256,

    /// <remarks />
    [EnumMember(Value = "Historizing_512")]
    Historizing = 512,

    /// <remarks />
    [EnumMember(Value = "InverseName_1024")]
    InverseName = 1024,

    /// <remarks />
    [EnumMember(Value = "IsAbstract_2048")]
    IsAbstract = 2048,

    /// <remarks />
    [EnumMember(Value = "MinimumSamplingInterval_4096")]
    MinimumSamplingInterval = 4096,

    /// <remarks />
    [EnumMember(Value = "NodeClass_8192")]
    NodeClass = 8192,

    /// <remarks />
    [EnumMember(Value = "NodeId_16384")]
    NodeId = 16384,

    /// <remarks />
    [EnumMember(Value = "Symmetric_32768")]
    Symmetric = 32768,

    /// <remarks />
    [EnumMember(Value = "UserAccessLevel_65536")]
    UserAccessLevel = 65536,

    /// <remarks />
    [EnumMember(Value = "UserExecutable_131072")]
    UserExecutable = 131072,

    /// <remarks />
    [EnumMember(Value = "UserWriteMask_262144")]
    UserWriteMask = 262144,

    /// <remarks />
    [EnumMember(Value = "ValueRank_524288")]
    ValueRank = 524288,

    /// <remarks />
    [EnumMember(Value = "WriteMask_1048576")]
    WriteMask = 1048576,

    /// <remarks />
    [EnumMember(Value = "ValueForVariableType_2097152")]
    ValueForVariableType = 2097152,

    /// <remarks />
    [EnumMember(Value = "DataTypeDefinition_4194304")]
    DataTypeDefinition = 4194304,

    /// <remarks />
    [EnumMember(Value = "RolePermissions_8388608")]
    RolePermissions = 8388608,

    /// <remarks />
    [EnumMember(Value = "AccessRestrictions_16777216")]
    AccessRestrictions = 16777216,

    /// <remarks />
    [EnumMember(Value = "AccessLevelEx_33554432")]
    AccessLevelEx = 33554432,
  }
}