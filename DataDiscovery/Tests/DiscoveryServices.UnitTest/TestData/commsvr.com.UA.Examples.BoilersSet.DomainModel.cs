
using System;
using System.Xml;
using UAOOI.DataDiscovery.DiscoveryServices.Models;

namespace CUAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData
{
  public static class ReferenceDomainModel
  {
    internal static DomainModel GerReferenceDomainModel()
    {
      return new DomainModel()
      {
        AliasName = "BoilersArea",
        Description = "Domain model for the BoilersArea",
        DomainModelGuid = new Guid("81976533-C604-4AEA-A2F9-F27687FF1A17"),
        DomainModelUri = new Uri(@"http://commsvr.com/UA/Examples/BoilersSet"),
        SemanticsDataCollection = NewSemanticsDataCollection(),
        TypeDictionaries = NewTypeDictionaries(),
        UniversalAddressSpaceLocator = @"http://localhost/opc/Commsvr.UA.Examples.BoilersSet.NodeSet2.xml",
        UniversalAuthorizationServerLocator = @"http://localhost/opc/Commsvr.UA.Examples.BoilersSet.OTPTable.xml",
        UniversalDiscoveryServiceLocator = @"http://localhost/opc/Commsvr.UA.Examples.BoilersSet.DiscoveryServiceLocator.xml",
      };
    }
    private static TypeDictionary[] NewTypeDictionaries()
    {
      return new TypeDictionary[]
      {
        new TypeDictionary() { TargetNamespace = @"http://opcfoundation.org/UA/",
                               StructuredType = new StructuredType[]
                                  {
                                    CreateStructuredType("Range", RangeFields),
                                    CreateStructuredType("EUInformation", EUInformationFields)
                                  }
                              }
      };
    }
    private static StructuredTypeField[] EUInformationFields()
    {
      return new StructuredTypeField[]
      {
        new StructuredTypeField() { Name = "NamespaceUri", TypeName = new XmlQualifiedName("NamespaceUri", "http://opcfoundation.org/UA/"),  SwitchOperandSpecified = false, SwitchValueSpecified = false  },
        new StructuredTypeField() { Name = "UnitId", TypeName = new XmlQualifiedName("Int32", "http://opcfoundation.org/UA/"),  SwitchOperandSpecified = false, SwitchValueSpecified = false  },
        new StructuredTypeField() { Name = "DisplayName", TypeName = new XmlQualifiedName("LocalizedText", "http://opcfoundation.org/UA/"),  SwitchOperandSpecified = false, SwitchValueSpecified = false  },
        new StructuredTypeField() { Name = "Description", TypeName = new XmlQualifiedName("LocalizedText", "http://opcfoundation.org/UA/"),  SwitchOperandSpecified = false, SwitchValueSpecified = false  }
      };
    }
    private static StructuredType CreateStructuredType(string name, Func<StructuredTypeField[]> createFields)
    {
      return new StructuredType() { StructureKind = StructureKindEnum.Structure, Name = name, Field = createFields() };
    }
    private static StructuredTypeField[] RangeFields()
    {
      return new StructuredTypeField[]
      {
        new StructuredTypeField() { Name = "Low", TypeName = new XmlQualifiedName("Double", "http://opcfoundation.org/UA/"),  SwitchOperandSpecified = false, SwitchValueSpecified = false  },
        new StructuredTypeField() { Name = "High", TypeName = new XmlQualifiedName("Double", "http://opcfoundation.org/UA/"),  SwitchOperandSpecified = false, SwitchValueSpecified = false  }
      };
    }
    private static SemanticsDataIndex[] NewSemanticsDataCollection()
    {
      return new SemanticsDataIndex[] { NewBoilerAlpha() };
    }
    private static SemanticsDataIndex NewBoilerAlpha()
    {
      return new SemanticsDataIndex()
      {
        DataSet = NewBoilerAlphaDataSet(),
        Index = 0,
        SymbolicName = "BoilersArea_BoilerAlpha",
      };
    }
    private static FieldMetaData[] NewBoilerAlphaDataSet()
    {
      return new FieldMetaData[]
        {
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_Definition", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.String, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_ValuePrecision", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_InstrumentRange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")} },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EURange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EngineeringUnits", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "EUInformation", @"http://opcfoundation.org/UA/")  } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_Valve_Input", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_Valve_Input_Definition", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.String, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_Valve_Input_ValuePrecision", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_Valve_Input_InstrumentRange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")} },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EURange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EngineeringUnits", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "EUInformation", @"http://opcfoundation.org/UA/")  } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_Definition", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.String, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_ValuePrecision", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_InstrumentRange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")} },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EURange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EngineeringUnits", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "EUInformation", @"http://opcfoundation.org/UA/")  } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_Definition", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.String, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_ValuePrecision", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_InstrumentRange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")} },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EURange", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "Range", @"http://opcfoundation.org/UA/")   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EngineeringUnits", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.ExtensionObject, ValueRank= -1 , TypeName = new System.Xml.XmlQualifiedName( "EUInformation", @"http://opcfoundation.org/UA/")  } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_FlowController_Measurement", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_FlowController_SetPoint", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_FlowController_ControlOut", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_LevelController_Measurement", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_LevelController_SetPoint", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_LevelController_ControlOut", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_CustomController_Input1", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_CustomController_Input2", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_CustomController_Input3", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_CustomController_ControlOut", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_CustomController_DescriptionX", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.LocalizedText, ValueRank= -1   } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_CustomController_Input1", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },

          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_CurrentState", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.LocalizedText, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_CurrentState_Id", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.NodeId, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_CurrentState_Name", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.LocalizedText, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_CurrentState_Number", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.UInt32, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_CurrentState_EffectiveDisplayName", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_LastTransition", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.LocalizedText, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_LastTransition_Id", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.NodeId, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_LastTransition_Name", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.LocalizedText, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_LastTransition_Number", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.UInt32, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_LastTransition_TransitionTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.DateTime, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_LastTransition_EffectiveTransitionTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.DateTime, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_Deletable", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Boolean, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_AutoDelete", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Boolean, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_RecycleCount", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.UInt32, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateSessionId", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateClientName", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastTransitionTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCall", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodSessionId", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodInputArguments", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodOutputArguments", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCallTime", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodReturnStatus", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_FinalResultData", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Double, ValueRank= -1   } },
          new FieldMetaData { SymbolicName = "BoilersArea_BoilerAlpha_Simulation_UpdateRate", TypeInformation = new UATypeInfo(  ) { BuiltInType = BuiltInType.Int32, ValueRank= -1   } },
        };
    }
  }
}
