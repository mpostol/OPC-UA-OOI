//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{
  #region Object Identifiers

  /// <summary>
  /// A class that declares constants for all Objects in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class Objects
  {
    /// <summary>
    /// The identifier for the BoilerInputPipeType_FlowTransmitter1 Object.
    /// </summary>
    public const uint BoilerInputPipeType_FlowTransmitter1 = 121;

    /// <summary>
    /// The identifier for the BoilerInputPipeType_Valve Object.
    /// </summary>
    public const uint BoilerInputPipeType_Valve = 128;

    /// <summary>
    /// The identifier for the BoilerDrumType_LevelIndicator Object.
    /// </summary>
    public const uint BoilerDrumType_LevelIndicator = 136;

    /// <summary>
    /// The identifier for the BoilerOutputPipeType_FlowTransmitter2 Object.
    /// </summary>
    public const uint BoilerOutputPipeType_FlowTransmitter2 = 144;

    /// <summary>
    /// The identifier for the BoilerType_InputPipe Object.
    /// </summary>
    public const uint BoilerType_InputPipe = 152;

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_FlowTransmitter1 Object.
    /// </summary>
    public const uint BoilerType_InputPipe_FlowTransmitter1 = 153;

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_Valve Object.
    /// </summary>
    public const uint BoilerType_InputPipe_Valve = 160;

    /// <summary>
    /// The identifier for the BoilerType_Drum Object.
    /// </summary>
    public const uint BoilerType_Drum = 167;

    /// <summary>
    /// The identifier for the BoilerType_Drum_LevelIndicator Object.
    /// </summary>
    public const uint BoilerType_Drum_LevelIndicator = 168;

    /// <summary>
    /// The identifier for the BoilerType_OutputPipe Object.
    /// </summary>
    public const uint BoilerType_OutputPipe = 175;

    /// <summary>
    /// The identifier for the BoilerType_OutputPipe_FlowTransmitter2 Object.
    /// </summary>
    public const uint BoilerType_OutputPipe_FlowTransmitter2 = 176;

    /// <summary>
    /// The identifier for the BoilerType_FlowController Object.
    /// </summary>
    public const uint BoilerType_FlowController = 183;

    /// <summary>
    /// The identifier for the BoilerType_LevelController Object.
    /// </summary>
    public const uint BoilerType_LevelController = 187;

    /// <summary>
    /// The identifier for the BoilerType_CustomController Object.
    /// </summary>
    public const uint BoilerType_CustomController = 191;

    /// <summary>
    /// The identifier for the BoilerType_Simulation Object.
    /// </summary>
    public const uint BoilerType_Simulation = 197;
  }

  #endregion Object Identifiers

  #region ObjectType Identifiers

  /// <summary>
  /// A class that declares constants for all ObjectTypes in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class ObjectTypes
  {
    /// <summary>
    /// The identifier for the GenericControllerType ObjectType.
    /// </summary>
    public const uint GenericControllerType = 4;

    /// <summary>
    /// The identifier for the GenericSensorType ObjectType.
    /// </summary>
    public const uint GenericSensorType = 8;

    /// <summary>
    /// The identifier for the GenericActuatorType ObjectType.
    /// </summary>
    public const uint GenericActuatorType = 15;

    /// <summary>
    /// The identifier for the CustomControllerType ObjectType.
    /// </summary>
    public const uint CustomControllerType = 22;

    /// <summary>
    /// The identifier for the ValveType ObjectType.
    /// </summary>
    public const uint ValveType = 28;

    /// <summary>
    /// The identifier for the LevelControllerType ObjectType.
    /// </summary>
    public const uint LevelControllerType = 35;

    /// <summary>
    /// The identifier for the FlowControllerType ObjectType.
    /// </summary>
    public const uint FlowControllerType = 39;

    /// <summary>
    /// The identifier for the LevelIndicatorType ObjectType.
    /// </summary>
    public const uint LevelIndicatorType = 43;

    /// <summary>
    /// The identifier for the FlowTransmitterType ObjectType.
    /// </summary>
    public const uint FlowTransmitterType = 50;

    /// <summary>
    /// The identifier for the BoilerStateMachineType ObjectType.
    /// </summary>
    public const uint BoilerStateMachineType = 57;

    /// <summary>
    /// The identifier for the BoilerInputPipeType ObjectType.
    /// </summary>
    public const uint BoilerInputPipeType = 120;

    /// <summary>
    /// The identifier for the BoilerDrumType ObjectType.
    /// </summary>
    public const uint BoilerDrumType = 135;

    /// <summary>
    /// The identifier for the BoilerOutputPipeType ObjectType.
    /// </summary>
    public const uint BoilerOutputPipeType = 143;

    /// <summary>
    /// The identifier for the BoilerType ObjectType.
    /// </summary>
    public const uint BoilerType = 151;
  }

  #endregion ObjectType Identifiers

  #region ReferenceType Identifiers

  /// <summary>
  /// A class that declares constants for all ReferenceTypes in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class ReferenceTypes
  {
    /// <summary>
    /// The identifier for the FlowTo ReferenceType.
    /// </summary>
    public const uint FlowTo = 1;

    /// <summary>
    /// The identifier for the HotFlowTo ReferenceType.
    /// </summary>
    public const uint HotFlowTo = 2;

    /// <summary>
    /// The identifier for the SignalTo ReferenceType.
    /// </summary>
    public const uint SignalTo = 3;
  }

  #endregion ReferenceType Identifiers

  #region Variable Identifiers

  /// <summary>
  /// A class that declares constants for all Variables in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class Variables
  {
    /// <summary>
    /// The identifier for the GenericControllerType_Measurement Variable.
    /// </summary>
    public const uint GenericControllerType_Measurement = 5;

    /// <summary>
    /// The identifier for the GenericControllerType_SetPoint Variable.
    /// </summary>
    public const uint GenericControllerType_SetPoint = 6;

    /// <summary>
    /// The identifier for the GenericControllerType_ControlOut Variable.
    /// </summary>
    public const uint GenericControllerType_ControlOut = 7;

    /// <summary>
    /// The identifier for the GenericSensorType_Output Variable.
    /// </summary>
    public const uint GenericSensorType_Output = 9;

    /// <summary>
    /// The identifier for the GenericSensorType_Output_EURange Variable.
    /// </summary>
    public const uint GenericSensorType_Output_EURange = 13;

    /// <summary>
    /// The identifier for the GenericActuatorType_Input Variable.
    /// </summary>
    public const uint GenericActuatorType_Input = 16;

    /// <summary>
    /// The identifier for the GenericActuatorType_Input_EURange Variable.
    /// </summary>
    public const uint GenericActuatorType_Input_EURange = 20;

    /// <summary>
    /// The identifier for the CustomControllerType_Input1 Variable.
    /// </summary>
    public const uint CustomControllerType_Input1 = 23;

    /// <summary>
    /// The identifier for the CustomControllerType_Input2 Variable.
    /// </summary>
    public const uint CustomControllerType_Input2 = 24;

    /// <summary>
    /// The identifier for the CustomControllerType_Input3 Variable.
    /// </summary>
    public const uint CustomControllerType_Input3 = 25;

    /// <summary>
    /// The identifier for the CustomControllerType_ControlOut Variable.
    /// </summary>
    public const uint CustomControllerType_ControlOut = 26;

    /// <summary>
    /// The identifier for the CustomControllerType_DescriptionX Variable.
    /// </summary>
    public const uint CustomControllerType_DescriptionX = 27;

    /// <summary>
    /// The identifier for the BoilerStateMachineType_UpdateRate Variable.
    /// </summary>
    public const uint BoilerStateMachineType_UpdateRate = 119;

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_FlowTransmitter1_Output Variable.
    /// </summary>
    public const uint BoilerType_InputPipe_FlowTransmitter1_Output = 154;

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_Valve_Input Variable.
    /// </summary>
    public const uint BoilerType_InputPipe_Valve_Input = 161;

    /// <summary>
    /// The identifier for the BoilerType_Drum_LevelIndicator_Output Variable.
    /// </summary>
    public const uint BoilerType_Drum_LevelIndicator_Output = 169;

    /// <summary>
    /// The identifier for the BoilerType_OutputPipe_FlowTransmitter2_Output Variable.
    /// </summary>
    public const uint BoilerType_OutputPipe_FlowTransmitter2_Output = 177;

    /// <summary>
    /// The identifier for the BoilerType_FlowController_Measurement Variable.
    /// </summary>
    public const uint BoilerType_FlowController_Measurement = 184;

    /// <summary>
    /// The identifier for the BoilerType_FlowController_SetPoint Variable.
    /// </summary>
    public const uint BoilerType_FlowController_SetPoint = 185;

    /// <summary>
    /// The identifier for the BoilerType_FlowController_ControlOut Variable.
    /// </summary>
    public const uint BoilerType_FlowController_ControlOut = 186;

    /// <summary>
    /// The identifier for the BoilerType_LevelController_Measurement Variable.
    /// </summary>
    public const uint BoilerType_LevelController_Measurement = 188;

    /// <summary>
    /// The identifier for the BoilerType_LevelController_SetPoint Variable.
    /// </summary>
    public const uint BoilerType_LevelController_SetPoint = 189;

    /// <summary>
    /// The identifier for the BoilerType_LevelController_ControlOut Variable.
    /// </summary>
    public const uint BoilerType_LevelController_ControlOut = 190;

    /// <summary>
    /// The identifier for the BoilerType_CustomController_Input1 Variable.
    /// </summary>
    public const uint BoilerType_CustomController_Input1 = 192;

    /// <summary>
    /// The identifier for the BoilerType_CustomController_Input2 Variable.
    /// </summary>
    public const uint BoilerType_CustomController_Input2 = 193;

    /// <summary>
    /// The identifier for the BoilerType_CustomController_Input3 Variable.
    /// </summary>
    public const uint BoilerType_CustomController_Input3 = 194;

    /// <summary>
    /// The identifier for the BoilerType_CustomController_ControlOut Variable.
    /// </summary>
    public const uint BoilerType_CustomController_ControlOut = 195;

    /// <summary>
    /// The identifier for the BoilerType_CustomController_DescriptionX Variable.
    /// </summary>
    public const uint BoilerType_CustomController_DescriptionX = 196;

    /// <summary>
    /// The identifier for the BoilerType_Simulation_UpdateRate Variable.
    /// </summary>
    public const uint BoilerType_Simulation_UpdateRate = 229;
  }

  #endregion Variable Identifiers

  #region Object Node Identifiers

  /// <summary>
  /// A class that declares constants for all Objects in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class ObjectIds
  {
    /// <summary>
    /// The identifier for the BoilerInputPipeType_FlowTransmitter1 Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerInputPipeType_FlowTransmitter1 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerInputPipeType_FlowTransmitter1, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerInputPipeType_Valve Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerInputPipeType_Valve = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerInputPipeType_Valve, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerDrumType_LevelIndicator Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerDrumType_LevelIndicator = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerDrumType_LevelIndicator, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerOutputPipeType_FlowTransmitter2 Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerOutputPipeType_FlowTransmitter2 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerOutputPipeType_FlowTransmitter2, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_InputPipe Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_InputPipe = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_InputPipe, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_FlowTransmitter1 Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_InputPipe_FlowTransmitter1 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_InputPipe_FlowTransmitter1, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_Valve Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_InputPipe_Valve = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_InputPipe_Valve, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_Drum Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_Drum = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_Drum, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_Drum_LevelIndicator Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_Drum_LevelIndicator = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_Drum_LevelIndicator, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_OutputPipe Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_OutputPipe = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_OutputPipe, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_OutputPipe_FlowTransmitter2 Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_OutputPipe_FlowTransmitter2 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_OutputPipe_FlowTransmitter2, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_FlowController Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_FlowController = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_FlowController, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_LevelController Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_LevelController = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_LevelController, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_CustomController Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_CustomController = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_CustomController, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_Simulation Object.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_Simulation = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Objects.BoilerType_Simulation, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);
  }

  #endregion Object Node Identifiers

  #region ObjectType Node Identifiers

  /// <summary>
  /// A class that declares constants for all ObjectTypes in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class ObjectTypeIds
  {
    /// <summary>
    /// The identifier for the GenericControllerType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId GenericControllerType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.GenericControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericSensorType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId GenericSensorType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.GenericSensorType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericActuatorType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId GenericActuatorType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.GenericActuatorType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the CustomControllerType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId CustomControllerType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.CustomControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the ValveType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId ValveType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.ValveType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the LevelControllerType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId LevelControllerType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.LevelControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the FlowControllerType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId FlowControllerType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.FlowControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the LevelIndicatorType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId LevelIndicatorType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.LevelIndicatorType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the FlowTransmitterType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId FlowTransmitterType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.FlowTransmitterType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerStateMachineType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId BoilerStateMachineType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerStateMachineType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerInputPipeType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId BoilerInputPipeType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerInputPipeType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerDrumType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId BoilerDrumType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerDrumType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerOutputPipeType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId BoilerOutputPipeType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerOutputPipeType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType ObjectType.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);
  }

  #endregion ObjectType Node Identifiers

  #region ReferenceType Node Identifiers

  /// <summary>
  /// A class that declares constants for all ReferenceTypes in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class ReferenceTypeIds
  {
    /// <summary>
    /// The identifier for the FlowTo ReferenceType.
    /// </summary>
    public static readonly ExpandedNodeId FlowTo = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ReferenceTypes.FlowTo, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the HotFlowTo ReferenceType.
    /// </summary>
    public static readonly ExpandedNodeId HotFlowTo = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ReferenceTypes.HotFlowTo, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the SignalTo ReferenceType.
    /// </summary>
    public static readonly ExpandedNodeId SignalTo = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.ReferenceTypes.SignalTo, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);
  }

  #endregion ReferenceType Node Identifiers

  #region Variable Node Identifiers

  /// <summary>
  /// A class that declares constants for all Variables in the Model Design.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public static partial class VariableIds
  {
    /// <summary>
    /// The identifier for the GenericControllerType_Measurement Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericControllerType_Measurement = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericControllerType_Measurement, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericControllerType_SetPoint Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericControllerType_SetPoint = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericControllerType_SetPoint, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericControllerType_ControlOut Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericControllerType_ControlOut = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericControllerType_ControlOut, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericSensorType_Output Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericSensorType_Output = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericSensorType_Output, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericSensorType_Output_EURange Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericSensorType_Output_EURange = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericSensorType_Output_EURange, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericActuatorType_Input Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericActuatorType_Input = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericActuatorType_Input, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the GenericActuatorType_Input_EURange Variable.
    /// </summary>
    public static readonly ExpandedNodeId GenericActuatorType_Input_EURange = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.GenericActuatorType_Input_EURange, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the CustomControllerType_Input1 Variable.
    /// </summary>
    public static readonly ExpandedNodeId CustomControllerType_Input1 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.CustomControllerType_Input1, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the CustomControllerType_Input2 Variable.
    /// </summary>
    public static readonly ExpandedNodeId CustomControllerType_Input2 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.CustomControllerType_Input2, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the CustomControllerType_Input3 Variable.
    /// </summary>
    public static readonly ExpandedNodeId CustomControllerType_Input3 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.CustomControllerType_Input3, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the CustomControllerType_ControlOut Variable.
    /// </summary>
    public static readonly ExpandedNodeId CustomControllerType_ControlOut = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.CustomControllerType_ControlOut, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the CustomControllerType_DescriptionX Variable.
    /// </summary>
    public static readonly ExpandedNodeId CustomControllerType_DescriptionX = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.CustomControllerType_DescriptionX, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerStateMachineType_UpdateRate Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerStateMachineType_UpdateRate = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerStateMachineType_UpdateRate, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_FlowTransmitter1_Output Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_InputPipe_FlowTransmitter1_Output = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_InputPipe_FlowTransmitter1_Output, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_InputPipe_Valve_Input Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_InputPipe_Valve_Input = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_InputPipe_Valve_Input, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_Drum_LevelIndicator_Output Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_Drum_LevelIndicator_Output = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_Drum_LevelIndicator_Output, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_OutputPipe_FlowTransmitter2_Output Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_OutputPipe_FlowTransmitter2_Output = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_OutputPipe_FlowTransmitter2_Output, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_FlowController_Measurement Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_FlowController_Measurement = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_FlowController_Measurement, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_FlowController_SetPoint Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_FlowController_SetPoint = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_FlowController_SetPoint, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_FlowController_ControlOut Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_FlowController_ControlOut = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_FlowController_ControlOut, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_LevelController_Measurement Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_LevelController_Measurement = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_LevelController_Measurement, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_LevelController_SetPoint Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_LevelController_SetPoint = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_LevelController_SetPoint, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_LevelController_ControlOut Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_LevelController_ControlOut = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_LevelController_ControlOut, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_CustomController_Input1 Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_CustomController_Input1 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_CustomController_Input1, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_CustomController_Input2 Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_CustomController_Input2 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_CustomController_Input2, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_CustomController_Input3 Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_CustomController_Input3 = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_CustomController_Input3, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_CustomController_ControlOut Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_CustomController_ControlOut = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_CustomController_ControlOut, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_CustomController_DescriptionX Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_CustomController_DescriptionX = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_CustomController_DescriptionX, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);

    /// <summary>
    /// The identifier for the BoilerType_Simulation_UpdateRate Variable.
    /// </summary>
    public static readonly ExpandedNodeId BoilerType_Simulation_UpdateRate = new ExpandedNodeId(tempuri.org.UA.Examples.BoilerType.Variables.BoilerType_Simulation_UpdateRate, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType);
  }

  #endregion Variable Node Identifiers

  #region BrowseName Declarations

  /// <summary>
  /// Declares all of the BrowseNames used in the Model Design.
  /// </summary>
  public static partial class BrowseNames
  {
    /// <summary>
    /// The BrowseName for the BoilerDrumType component.
    /// </summary>
    public const string BoilerDrumType = "BoilerDrumType";

    /// <summary>
    /// The BrowseName for the BoilerInputPipeType component.
    /// </summary>
    public const string BoilerInputPipeType = "BoilerInputPipeType";

    /// <summary>
    /// The BrowseName for the BoilerOutputPipeType component.
    /// </summary>
    public const string BoilerOutputPipeType = "BoilerOutputPipeType";

    /// <summary>
    /// The BrowseName for the BoilerStateMachineType component.
    /// </summary>
    public const string BoilerStateMachineType = "BoilerStateMachineType";

    /// <summary>
    /// The BrowseName for the BoilerType component.
    /// </summary>
    public const string BoilerType = "BoilerType";

    /// <summary>
    /// The BrowseName for the ControlOut component.
    /// </summary>
    public const string ControlOut = "ControlOut";

    /// <summary>
    /// The BrowseName for the CustomController component.
    /// </summary>
    public const string CustomController = "CCX001";

    /// <summary>
    /// The BrowseName for the CustomControllerType component.
    /// </summary>
    public const string CustomControllerType = "CustomControllerType";

    /// <summary>
    /// The BrowseName for the DescriptionX component.
    /// </summary>
    public const string DescriptionX = "Description";

    /// <summary>
    /// The BrowseName for the Drum component.
    /// </summary>
    public const string Drum = "DrumX001";

    /// <summary>
    /// The BrowseName for the FlowController component.
    /// </summary>
    public const string FlowController = "FCX001";

    /// <summary>
    /// The BrowseName for the FlowControllerType component.
    /// </summary>
    public const string FlowControllerType = "FlowControllerType";

    /// <summary>
    /// The BrowseName for the FlowTo component.
    /// </summary>
    public const string FlowTo = "FlowTo";

    /// <summary>
    /// The BrowseName for the FlowTransmitter1 component.
    /// </summary>
    public const string FlowTransmitter1 = "FTX001";

    /// <summary>
    /// The BrowseName for the FlowTransmitter2 component.
    /// </summary>
    public const string FlowTransmitter2 = "FTX002";

    /// <summary>
    /// The BrowseName for the FlowTransmitterType component.
    /// </summary>
    public const string FlowTransmitterType = "FlowTransmitterType";

    /// <summary>
    /// The BrowseName for the GenericActuatorType component.
    /// </summary>
    public const string GenericActuatorType = "GenericActuatorType";

    /// <summary>
    /// The BrowseName for the GenericControllerType component.
    /// </summary>
    public const string GenericControllerType = "GenericControllerType";

    /// <summary>
    /// The BrowseName for the GenericSensorType component.
    /// </summary>
    public const string GenericSensorType = "GenericSensorType";

    /// <summary>
    /// The BrowseName for the HotFlowTo component.
    /// </summary>
    public const string HotFlowTo = "HotFlowTo";

    /// <summary>
    /// The BrowseName for the Input component.
    /// </summary>
    public const string Input = "Input";

    /// <summary>
    /// The BrowseName for the Input1 component.
    /// </summary>
    public const string Input1 = "Input1";

    /// <summary>
    /// The BrowseName for the Input2 component.
    /// </summary>
    public const string Input2 = "Input2";

    /// <summary>
    /// The BrowseName for the Input3 component.
    /// </summary>
    public const string Input3 = "Input3";

    /// <summary>
    /// The BrowseName for the InputPipe component.
    /// </summary>
    public const string InputPipe = "PipeX001";

    /// <summary>
    /// The BrowseName for the LevelController component.
    /// </summary>
    public const string LevelController = "LCX001";

    /// <summary>
    /// The BrowseName for the LevelControllerType component.
    /// </summary>
    public const string LevelControllerType = "LevelControllerType";

    /// <summary>
    /// The BrowseName for the LevelIndicator component.
    /// </summary>
    public const string LevelIndicator = "LIX001";

    /// <summary>
    /// The BrowseName for the LevelIndicatorType component.
    /// </summary>
    public const string LevelIndicatorType = "LevelIndicatorType";

    /// <summary>
    /// The BrowseName for the Measurement component.
    /// </summary>
    public const string Measurement = "Measurement";

    /// <summary>
    /// The BrowseName for the Output component.
    /// </summary>
    public const string Output = "Output";

    /// <summary>
    /// The BrowseName for the OutputPipe component.
    /// </summary>
    public const string OutputPipe = "PipeX002";

    /// <summary>
    /// The BrowseName for the SetPoint component.
    /// </summary>
    public const string SetPoint = "SetPoint";

    /// <summary>
    /// The BrowseName for the SignalTo component.
    /// </summary>
    public const string SignalTo = "SignalTo";

    /// <summary>
    /// The BrowseName for the Simulation component.
    /// </summary>
    public const string Simulation = "Simulation";

    /// <summary>
    /// The BrowseName for the UpdateRate component.
    /// </summary>
    public const string UpdateRate = "UpdateRate";

    /// <summary>
    /// The BrowseName for the Valve component.
    /// </summary>
    public const string Valve = "ValveX001";

    /// <summary>
    /// The BrowseName for the ValveType component.
    /// </summary>
    public const string ValveType = "ValveType";
  }

  #endregion BrowseName Declarations

  #region Namespace Declarations

  /// <summary>
  /// Defines constants for all namespaces referenced by the model design.
  /// </summary>
  public static partial class Namespaces
  {
    /// <summary>
    /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
    /// </summary>
    public const string OpcUa = "http://opcfoundation.org/UA/";

    /// <summary>
    /// The URI for the OpcUaXsd namespace (.NET code namespace is 'Opc.Ua').
    /// </summary>
    public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";

    /// <summary>
    /// The URI for the BoilerType namespace (.NET code namespace is 'tempuri.org.UA.Examples.BoilerType').
    /// </summary>
    public const string BoilerType = "http://tempuri.org/UA/Examples/BoilerType";

    /// <summary>
    /// The URI for the BoilerTypeXsd namespace (.NET code namespace is 'tempuri.org.UA.Examples.BoilerType').
    /// </summary>
    public const string BoilerTypeXsd = "http://tempuri.org/UA/Examples/BoilerType/Types.xsd";
  }

  #endregion Namespace Declarations

  #region GenericControllerState Class

#if (!OPCUA_EXCLUDE_GenericControllerState)

  /// <summary>
  /// Stores an instance of the GenericControllerType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class GenericControllerState : BaseObjectState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public GenericControllerState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.GenericControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAHQAAAEdlbmVyaWNDb250cm9sbGVyVHlwZUluc3RhbmNlAQEEAAEBBAD/////AwAAABVgiQoCAAAA" +
           "AQALAAAATWVhc3VyZW1lbnQBAQUAAC4ARAUAAAAAC/////8BAf////8AAAAAFWCJCgIAAAABAAgAAABT" +
           "ZXRQb2ludAEBBgAALgBEBgAAAAAL/////wMD/////wAAAAAVYIkKAgAAAAEACgAAAENvbnRyb2xPdXQB" +
           "AQcAAC4ARAcAAAAAC/////8BAf////8AAAAA";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the Measurement Property.
    /// </summary>
    public PropertyState<double> Measurement
    {
      get => m_measurement;

      set
      {
        if (!Object.ReferenceEquals(m_measurement, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_measurement = value;
      }
    }

    /// <summary>
    /// A description for the SetPoint Property.
    /// </summary>
    public PropertyState<double> SetPoint
    {
      get => m_setPoint;

      set
      {
        if (!Object.ReferenceEquals(m_setPoint, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_setPoint = value;
      }
    }

    /// <summary>
    /// A description for the ControlOut Property.
    /// </summary>
    public PropertyState<double> ControlOut
    {
      get => m_controlOut;

      set
      {
        if (!Object.ReferenceEquals(m_controlOut, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_controlOut = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_measurement != null)
      {
        children.Add(m_measurement);
      }

      if (m_setPoint != null)
      {
        children.Add(m_setPoint);
      }

      if (m_controlOut != null)
      {
        children.Add(m_controlOut);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Measurement:
          {
            if (createOrReplace)
            {
              if (Measurement == null)
              {
                if (replacement == null)
                {
                  Measurement = new PropertyState<double>(this);
                }
                else
                {
                  Measurement = (PropertyState<double>)replacement;
                }
              }
            }

            instance = Measurement;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.SetPoint:
          {
            if (createOrReplace)
            {
              if (SetPoint == null)
              {
                if (replacement == null)
                {
                  SetPoint = new PropertyState<double>(this);
                }
                else
                {
                  SetPoint = (PropertyState<double>)replacement;
                }
              }
            }

            instance = SetPoint;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.ControlOut:
          {
            if (createOrReplace)
            {
              if (ControlOut == null)
              {
                if (replacement == null)
                {
                  ControlOut = new PropertyState<double>(this);
                }
                else
                {
                  ControlOut = (PropertyState<double>)replacement;
                }
              }
            }

            instance = ControlOut;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private PropertyState<double> m_measurement;
    private PropertyState<double> m_setPoint;
    private PropertyState<double> m_controlOut;

    #endregion Private Fields
  }

#endif

  #endregion GenericControllerState Class

  #region GenericSensorState Class

#if (!OPCUA_EXCLUDE_GenericSensorState)

  /// <summary>
  /// Stores an instance of the GenericSensorType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class GenericSensorState : BaseObjectState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public GenericSensorState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.GenericSensorType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGQAAAEdlbmVyaWNTZW5zb3JUeXBlSW5zdGFuY2UBAQgAAQEIAP////8BAAAAFWCJCgIAAAABAAYA" +
           "AABPdXRwdXQBAQkAAC8BAEAJCQAAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UB" +
           "AQ0AAC4ARA0AAAABAHQD/////wEB/////wAAAAA=";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the Output Variable.
    /// </summary>
    public AnalogItemState<double> Output
    {
      get => m_output;

      set
      {
        if (!Object.ReferenceEquals(m_output, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_output = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_output != null)
      {
        children.Add(m_output);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Output:
          {
            if (createOrReplace)
            {
              if (Output == null)
              {
                if (replacement == null)
                {
                  Output = new AnalogItemState<double>(this);
                }
                else
                {
                  Output = (AnalogItemState<double>)replacement;
                }
              }
            }

            instance = Output;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private AnalogItemState<double> m_output;

    #endregion Private Fields
  }

#endif

  #endregion GenericSensorState Class

  #region GenericActuatorState Class

#if (!OPCUA_EXCLUDE_GenericActuatorState)

  /// <summary>
  /// Stores an instance of the GenericActuatorType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class GenericActuatorState : BaseObjectState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public GenericActuatorState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.GenericActuatorType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGwAAAEdlbmVyaWNBY3R1YXRvclR5cGVJbnN0YW5jZQEBDwABAQ8A/////wEAAAAVYIkKAgAAAAEA" +
           "BQAAAElucHV0AQEQAAAvAQBACRAAAAAAC/////8CAv////8BAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdl" +
           "AQEUAAAuAEQUAAAAAQB0A/////8BAf////8AAAAA";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the Input Variable.
    /// </summary>
    public AnalogItemState<double> Input
    {
      get => m_input;

      set
      {
        if (!Object.ReferenceEquals(m_input, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_input = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_input != null)
      {
        children.Add(m_input);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Input:
          {
            if (createOrReplace)
            {
              if (Input == null)
              {
                if (replacement == null)
                {
                  Input = new AnalogItemState<double>(this);
                }
                else
                {
                  Input = (AnalogItemState<double>)replacement;
                }
              }
            }

            instance = Input;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private AnalogItemState<double> m_input;

    #endregion Private Fields
  }

#endif

  #endregion GenericActuatorState Class

  #region CustomControllerState Class

#if (!OPCUA_EXCLUDE_CustomControllerState)

  /// <summary>
  /// Stores an instance of the CustomControllerType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class CustomControllerState : BaseObjectState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public CustomControllerState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.CustomControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAHAAAAEN1c3RvbUNvbnRyb2xsZXJUeXBlSW5zdGFuY2UBARYAAQEWAP////8FAAAAFWCJCgIAAAAB" +
           "AAYAAABJbnB1dDEBARcAAC4ARBcAAAAAC/////8CAv////8AAAAAFWCJCgIAAAABAAYAAABJbnB1dDIB" +
           "ARgAAC4ARBgAAAAAC/////8CAv////8AAAAAFWCJCgIAAAABAAYAAABJbnB1dDMBARkAAC4ARBkAAAAA" +
           "C/////8CAv////8AAAAAFWCJCgIAAAABAAoAAABDb250cm9sT3V0AQEaAAAuAEQaAAAAAAv/////AQH/" +
           "////AAAAABVgyQoCAAAADAAAAERlc2NyaXB0aW9uWAEACwAAAERlc2NyaXB0aW9uAQEbAAAuAEQbAAAA" +
           "ABX/////AQH/////AAAAAA==";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the Input1 Property.
    /// </summary>
    public PropertyState<double> Input1
    {
      get => m_input1;

      set
      {
        if (!Object.ReferenceEquals(m_input1, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_input1 = value;
      }
    }

    /// <summary>
    /// A description for the Input2 Property.
    /// </summary>
    public PropertyState<double> Input2
    {
      get => m_input2;

      set
      {
        if (!Object.ReferenceEquals(m_input2, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_input2 = value;
      }
    }

    /// <summary>
    /// A description for the Input3 Property.
    /// </summary>
    public PropertyState<double> Input3
    {
      get => m_input3;

      set
      {
        if (!Object.ReferenceEquals(m_input3, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_input3 = value;
      }
    }

    /// <summary>
    /// A description for the ControlOut Property.
    /// </summary>
    public PropertyState<double> ControlOut
    {
      get => m_controlOut;

      set
      {
        if (!Object.ReferenceEquals(m_controlOut, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_controlOut = value;
      }
    }

    /// <summary>
    /// A description for the Description Property.
    /// </summary>
    public PropertyState<LocalizedText> DescriptionX
    {
      get => m_descriptionX;

      set
      {
        if (!Object.ReferenceEquals(m_descriptionX, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_descriptionX = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_input1 != null)
      {
        children.Add(m_input1);
      }

      if (m_input2 != null)
      {
        children.Add(m_input2);
      }

      if (m_input3 != null)
      {
        children.Add(m_input3);
      }

      if (m_controlOut != null)
      {
        children.Add(m_controlOut);
      }

      if (m_descriptionX != null)
      {
        children.Add(m_descriptionX);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Input1:
          {
            if (createOrReplace)
            {
              if (Input1 == null)
              {
                if (replacement == null)
                {
                  Input1 = new PropertyState<double>(this);
                }
                else
                {
                  Input1 = (PropertyState<double>)replacement;
                }
              }
            }

            instance = Input1;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Input2:
          {
            if (createOrReplace)
            {
              if (Input2 == null)
              {
                if (replacement == null)
                {
                  Input2 = new PropertyState<double>(this);
                }
                else
                {
                  Input2 = (PropertyState<double>)replacement;
                }
              }
            }

            instance = Input2;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Input3:
          {
            if (createOrReplace)
            {
              if (Input3 == null)
              {
                if (replacement == null)
                {
                  Input3 = new PropertyState<double>(this);
                }
                else
                {
                  Input3 = (PropertyState<double>)replacement;
                }
              }
            }

            instance = Input3;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.ControlOut:
          {
            if (createOrReplace)
            {
              if (ControlOut == null)
              {
                if (replacement == null)
                {
                  ControlOut = new PropertyState<double>(this);
                }
                else
                {
                  ControlOut = (PropertyState<double>)replacement;
                }
              }
            }

            instance = ControlOut;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.DescriptionX:
          {
            if (createOrReplace)
            {
              if (DescriptionX == null)
              {
                if (replacement == null)
                {
                  DescriptionX = new PropertyState<LocalizedText>(this);
                }
                else
                {
                  DescriptionX = (PropertyState<LocalizedText>)replacement;
                }
              }
            }

            instance = DescriptionX;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private PropertyState<double> m_input1;
    private PropertyState<double> m_input2;
    private PropertyState<double> m_input3;
    private PropertyState<double> m_controlOut;
    private PropertyState<LocalizedText> m_descriptionX;

    #endregion Private Fields
  }

#endif

  #endregion CustomControllerState Class

  #region ValveState Class

#if (!OPCUA_EXCLUDE_ValveState)

  /// <summary>
  /// Stores an instance of the ValveType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class ValveState : GenericActuatorState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public ValveState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.ValveType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAEQAAAFZhbHZlVHlwZUluc3RhbmNlAQEcAAEBHAD/////AQAAABVgiQoCAAAAAQAFAAAASW5wdXQB" +
           "AR0AAC8BAEAJHQAAAAAL/////wIC/////wEAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UBASEAAC4ARCEA" +
           "AAABAHQD/////wEB/////wAAAAA=";

    #endregion Initialization String

#endif

    #endregion Constructors
  }

#endif

  #endregion ValveState Class

  #region LevelControllerState Class

#if (!OPCUA_EXCLUDE_LevelControllerState)

  /// <summary>
  /// Stores an instance of the LevelControllerType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class LevelControllerState : GenericControllerState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public LevelControllerState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.LevelControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGwAAAExldmVsQ29udHJvbGxlclR5cGVJbnN0YW5jZQEBIwABASMA/////wMAAAAVYIkKAgAAAAEA" +
           "CwAAAE1lYXN1cmVtZW50AQEkAAAuAEQkAAAAAAv/////AQH/////AAAAABVgiQoCAAAAAQAIAAAAU2V0" +
           "UG9pbnQBASUAAC4ARCUAAAAAC/////8DA/////8AAAAAFWCJCgIAAAABAAoAAABDb250cm9sT3V0AQEm" +
           "AAAuAEQmAAAAAAv/////AQH/////AAAAAA==";

    #endregion Initialization String

#endif

    #endregion Constructors
  }

#endif

  #endregion LevelControllerState Class

  #region FlowControllerState Class

#if (!OPCUA_EXCLUDE_FlowControllerState)

  /// <summary>
  /// Stores an instance of the FlowControllerType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class FlowControllerState : GenericControllerState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public FlowControllerState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.FlowControllerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGgAAAEZsb3dDb250cm9sbGVyVHlwZUluc3RhbmNlAQEnAAEBJwD/////AwAAABVgiQoCAAAAAQAL" +
           "AAAATWVhc3VyZW1lbnQBASgAAC4ARCgAAAAAC/////8BAf////8AAAAAFWCJCgIAAAABAAgAAABTZXRQ" +
           "b2ludAEBKQAALgBEKQAAAAAL/////wMD/////wAAAAAVYIkKAgAAAAEACgAAAENvbnRyb2xPdXQBASoA" +
           "AC4ARCoAAAAAC/////8BAf////8AAAAA";

    #endregion Initialization String

#endif

    #endregion Constructors
  }

#endif

  #endregion FlowControllerState Class

  #region LevelIndicatorState Class

#if (!OPCUA_EXCLUDE_LevelIndicatorState)

  /// <summary>
  /// Stores an instance of the LevelIndicatorType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class LevelIndicatorState : GenericSensorState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public LevelIndicatorState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.LevelIndicatorType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGgAAAExldmVsSW5kaWNhdG9yVHlwZUluc3RhbmNlAQErAAEBKwD/////AQAAABVgiQoCAAAAAQAG" +
           "AAAAT3V0cHV0AQEsAAAvAQBACSwAAAAAC/////8BAf////8BAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdl" +
           "AQEwAAAuAEQwAAAAAQB0A/////8BAf////8AAAAA";

    #endregion Initialization String

#endif

    #endregion Constructors
  }

#endif

  #endregion LevelIndicatorState Class

  #region FlowTransmitterState Class

#if (!OPCUA_EXCLUDE_FlowTransmitterState)

  /// <summary>
  /// Stores an instance of the FlowTransmitterType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class FlowTransmitterState : GenericSensorState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public FlowTransmitterState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.FlowTransmitterType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGwAAAEZsb3dUcmFuc21pdHRlclR5cGVJbnN0YW5jZQEBMgABATIA/////wEAAAAVYIkKAgAAAAEA" +
           "BgAAAE91dHB1dAEBMwAALwEAQAkzAAAAAAv/////AQH/////AQAAABVgiQoCAAAAAAAHAAAARVVSYW5n" +
           "ZQEBNwAALgBENwAAAAEAdAP/////AQH/////AAAAAA==";

    #endregion Initialization String

#endif

    #endregion Constructors
  }

#endif

  #endregion FlowTransmitterState Class

  #region BoilerStateMachineState Class

#if (!OPCUA_EXCLUDE_BoilerStateMachineState)

  /// <summary>
  /// Stores an instance of the BoilerStateMachineType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class BoilerStateMachineState : ProgramStateMachineState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public BoilerStateMachineState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerStateMachineType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAHgAAAEJvaWxlclN0YXRlTWFjaGluZVR5cGVJbnN0YW5jZQEBOQABATkA/////woAAAAVYIkKAgAA" +
           "AAAADAAAAEN1cnJlbnRTdGF0ZQEBOgAALwEAyAo6AAAAABX/////AQH/////AgAAABVgiQoCAAAAAAAC" +
           "AAAASWQBATsAAC4ARDsAAAAAEf////8BAf////8AAAAAFWCJCgIAAAAAAAYAAABOdW1iZXIBAT0AAC4A" +
           "RD0AAAAAB/////8BAf////8AAAAAFWCJCgIAAAAAAA4AAABMYXN0VHJhbnNpdGlvbgEBPwAALwEAzwo/" +
           "AAAAABX/////AQH/////AwAAABVgiQoCAAAAAAACAAAASWQBAUAAAC4AREAAAAAAEf////8BAf////8A" +
           "AAAAFWCJCgIAAAAAAAYAAABOdW1iZXIBAUIAAC4AREIAAAAAB/////8BAf////8AAAAAFWCJCgIAAAAA" +
           "AA4AAABUcmFuc2l0aW9uVGltZQEBQwAALgBEQwAAAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAAJ" +
           "AAAARGVsZXRhYmxlAQFGAAAuAERGAAAAAAH/////AQH/////AAAAABVgiQoCAAAAAAAMAAAAUmVjeWNs" +
           "ZUNvdW50AQFIAAAuAERIAAAAAAb/////AQH/////AAAAACRhggoEAAAAAAAFAAAAU3RhcnQBAXIAAwAA" +
           "AABLAAAAQ2F1c2VzIHRoZSBQcm9ncmFtIHRvIHRyYW5zaXRpb24gZnJvbSB0aGUgUmVhZHkgc3RhdGUg" +
           "dG8gdGhlIFJ1bm5pbmcgc3RhdGUuAC8BAHoJcgAAAAEBAQAAAAA1AQEBYgAAAAAAJGGCCgQAAAAAAAcA" +
           "AABTdXNwZW5kAQFzAAMAAAAATwAAAENhdXNlcyB0aGUgUHJvZ3JhbSB0byB0cmFuc2l0aW9uIGZyb20g" +
           "dGhlIFJ1bm5pbmcgc3RhdGUgdG8gdGhlIFN1c3BlbmRlZCBzdGF0ZS4ALwEAewlzAAAAAQEBAAAAADUB" +
           "AQFoAAAAAAAkYYIKBAAAAAAABgAAAFJlc3VtZQEBdAADAAAAAE8AAABDYXVzZXMgdGhlIFByb2dyYW0g" +
           "dG8gdHJhbnNpdGlvbiBmcm9tIHRoZSBTdXNwZW5kZWQgc3RhdGUgdG8gdGhlIFJ1bm5pbmcgc3RhdGUu" +
           "AC8BAHwJdAAAAAEBAQAAAAA1AQEBagAAAAAAJGGCCgQAAAAAAAQAAABIYWx0AQF1AAMAAAAAYAAAAENh" +
           "dXNlcyB0aGUgUHJvZ3JhbSB0byB0cmFuc2l0aW9uIGZyb20gdGhlIFJlYWR5LCBSdW5uaW5nIG9yIFN1" +
           "c3BlbmRlZCBzdGF0ZSB0byB0aGUgSGFsdGVkIHN0YXRlLgAvAQB9CXUAAAABAQMAAAAANQEBAWQAADUB" +
           "AQFsAAA1AQEBcAAAAAAAJGGCCgQAAAAAAAUAAABSZXNldAEBdgADAAAAAEoAAABDYXVzZXMgdGhlIFBy" +
           "b2dyYW0gdG8gdHJhbnNpdGlvbiBmcm9tIHRoZSBIYWx0ZWQgc3RhdGUgdG8gdGhlIFJlYWR5IHN0YXRl" +
           "LgAvAQB+CXYAAAABAQEAAAAANQEBAWAAAAAAADVgiQoCAAAAAQAKAAAAVXBkYXRlUmF0ZQEBdwADAAAA" +
           "ACYAAABUaGUgcmF0ZSBhdCB3aGljaCB0aGUgc2ltdWxhdGlvbiBydW5zLgAuAER3AAAAAAf/////AwP/" +
           "////AAAAAA==";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// The rate at which the simulation runs.
    /// </summary>
    public PropertyState<uint> UpdateRate
    {
      get => m_updateRate;

      set
      {
        if (!Object.ReferenceEquals(m_updateRate, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_updateRate = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_updateRate != null)
      {
        children.Add(m_updateRate);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.UpdateRate:
          {
            if (createOrReplace)
            {
              if (UpdateRate == null)
              {
                if (replacement == null)
                {
                  UpdateRate = new PropertyState<uint>(this);
                }
                else
                {
                  UpdateRate = (PropertyState<uint>)replacement;
                }
              }
            }

            instance = UpdateRate;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private PropertyState<uint> m_updateRate;

    #endregion Private Fields
  }

#endif

  #endregion BoilerStateMachineState Class

  #region BoilerInputPipeState Class

#if (!OPCUA_EXCLUDE_BoilerInputPipeState)

  /// <summary>
  /// Stores an instance of the BoilerInputPipeType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class BoilerInputPipeState : FolderState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public BoilerInputPipeState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerInputPipeType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAGwAAAEJvaWxlcklucHV0UGlwZVR5cGVJbnN0YW5jZQEBeAABAXgAAQAAAAAwAAEBeQACAAAAxGDA" +
           "CgEAAAAQAAAARmxvd1RyYW5zbWl0dGVyMQEABgAAAEZUWDAwMQEBeQADAAAAABAAAABGbG93VHJhbnNt" +
           "aXR0ZXIxAC8BATIAeQAAAAEBAAAAADABAQF4AAEAAAAVYIkKAgAAAAEABgAAAE91dHB1dAEBegAALwEA" +
           "QAl6AAAAAAv/////AQH/////AQAAABVgiQoCAAAAAAAHAAAARVVSYW5nZQEBfgAALgBEfgAAAAEAdAP/" +
           "////AQH/////AAAAAMRgwAoBAAAABQAAAFZhbHZlAQAJAAAAVmFsdmVYMDAxAQGAAAMAAAAABQAAAFZh" +
           "bHZlAC8BARwAgAAAAAH/////AQAAABVgiQoCAAAAAQAFAAAASW5wdXQBAYEAAC8BAEAJgQAAAAAL////" +
           "/wIC/////wEAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UBAYUAAC4ARIUAAAABAHQD/////wEB/////wAA" +
           "AAA=";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the FTX001 Object.
    /// </summary>
    public FlowTransmitterState FlowTransmitter1
    {
      get => m_flowTransmitter1;

      set
      {
        if (!Object.ReferenceEquals(m_flowTransmitter1, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_flowTransmitter1 = value;
      }
    }

    /// <summary>
    /// A description for the ValveX001 Object.
    /// </summary>
    public ValveState Valve
    {
      get => m_valve;

      set
      {
        if (!Object.ReferenceEquals(m_valve, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_valve = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_flowTransmitter1 != null)
      {
        children.Add(m_flowTransmitter1);
      }

      if (m_valve != null)
      {
        children.Add(m_valve);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.FlowTransmitter1:
          {
            if (createOrReplace)
            {
              if (FlowTransmitter1 == null)
              {
                if (replacement == null)
                {
                  FlowTransmitter1 = new FlowTransmitterState(this);
                }
                else
                {
                  FlowTransmitter1 = (FlowTransmitterState)replacement;
                }
              }
            }

            instance = FlowTransmitter1;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Valve:
          {
            if (createOrReplace)
            {
              if (Valve == null)
              {
                if (replacement == null)
                {
                  Valve = new ValveState(this);
                }
                else
                {
                  Valve = (ValveState)replacement;
                }
              }
            }

            instance = Valve;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private FlowTransmitterState m_flowTransmitter1;
    private ValveState m_valve;

    #endregion Private Fields
  }

#endif

  #endregion BoilerInputPipeState Class

  #region BoilerDrumState Class

#if (!OPCUA_EXCLUDE_BoilerDrumState)

  /// <summary>
  /// Stores an instance of the BoilerDrumType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class BoilerDrumState : FolderState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public BoilerDrumState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerDrumType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAFgAAAEJvaWxlckRydW1UeXBlSW5zdGFuY2UBAYcAAQGHAAEAAAAAMAABAYgAAQAAAIRgwAoBAAAA" +
           "DgAAAExldmVsSW5kaWNhdG9yAQAGAAAATElYMDAxAQGIAAAvAQErAIgAAAABAQAAAAAwAQEBhwABAAAA" +
           "FWCJCgIAAAABAAYAAABPdXRwdXQBAYkAAC8BAEAJiQAAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAA" +
           "BwAAAEVVUmFuZ2UBAY0AAC4ARI0AAAABAHQD/////wEB/////wAAAAA=";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the LIX001 Object.
    /// </summary>
    public LevelIndicatorState LevelIndicator
    {
      get => m_levelIndicator;

      set
      {
        if (!Object.ReferenceEquals(m_levelIndicator, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_levelIndicator = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_levelIndicator != null)
      {
        children.Add(m_levelIndicator);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.LevelIndicator:
          {
            if (createOrReplace)
            {
              if (LevelIndicator == null)
              {
                if (replacement == null)
                {
                  LevelIndicator = new LevelIndicatorState(this);
                }
                else
                {
                  LevelIndicator = (LevelIndicatorState)replacement;
                }
              }
            }

            instance = LevelIndicator;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private LevelIndicatorState m_levelIndicator;

    #endregion Private Fields
  }

#endif

  #endregion BoilerDrumState Class

  #region BoilerOutputPipeState Class

#if (!OPCUA_EXCLUDE_BoilerOutputPipeState)

  /// <summary>
  /// Stores an instance of the BoilerOutputPipeType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class BoilerOutputPipeState : FolderState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public BoilerOutputPipeState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerOutputPipeType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////8EYIAAAQAA" +
           "AAEAHAAAAEJvaWxlck91dHB1dFBpcGVUeXBlSW5zdGFuY2UBAY8AAQGPAAEAAAAAMAABAZAAAQAAAIRg" +
           "wAoBAAAAEAAAAEZsb3dUcmFuc21pdHRlcjIBAAYAAABGVFgwMDIBAZAAAC8BATIAkAAAAAEBAAAAADAB" +
           "AQGPAAEAAAAVYIkKAgAAAAEABgAAAE91dHB1dAEBkQAALwEAQAmRAAAAAAv/////AQH/////AQAAABVg" +
           "iQoCAAAAAAAHAAAARVVSYW5nZQEBlQAALgBElQAAAAEAdAP/////AQH/////AAAAAA==";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the FTX002 Object.
    /// </summary>
    public FlowTransmitterState FlowTransmitter2
    {
      get => m_flowTransmitter2;

      set
      {
        if (!Object.ReferenceEquals(m_flowTransmitter2, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_flowTransmitter2 = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_flowTransmitter2 != null)
      {
        children.Add(m_flowTransmitter2);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.FlowTransmitter2:
          {
            if (createOrReplace)
            {
              if (FlowTransmitter2 == null)
              {
                if (replacement == null)
                {
                  FlowTransmitter2 = new FlowTransmitterState(this);
                }
                else
                {
                  FlowTransmitter2 = (FlowTransmitterState)replacement;
                }
              }
            }

            instance = FlowTransmitter2;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private FlowTransmitterState m_flowTransmitter2;

    #endregion Private Fields
  }

#endif

  #endregion BoilerOutputPipeState Class

  #region BoilerState Class

#if (!OPCUA_EXCLUDE_BoilerState)

  /// <summary>
  /// Stores an instance of the BoilerType ObjectType.
  /// </summary>
  /// <exclude />
  [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
  public partial class BoilerState : BaseObjectState
  {
    #region Constructors

    /// <summary>
    /// Initializes the type with its default attribute values.
    /// </summary>
    public BoilerState(NodeState parent) : base(parent)
    {
    }

    /// <summary>
    /// Returns the id of the default type definition node for the instance.
    /// </summary>
    protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
    {
      return NodeId.Create(tempuri.org.UA.Examples.BoilerType.ObjectTypes.BoilerType, tempuri.org.UA.Examples.BoilerType.Namespaces.BoilerType, namespaceUris);
    }

#if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

    #region Initialization String

        private const string InitializationString =
           "AQAAACkAAABodHRwOi8vdGVtcHVyaS5vcmcvVUEvRXhhbXBsZXMvQm9pbGVyVHlwZf////+EYIAAAQAA" +
           "AAEAEgAAAEJvaWxlclR5cGVJbnN0YW5jZQEBlwABAZcAAQQAAAAAMAABAZgAADAAAQGnAAAwAAEBrwAA" +
           "JAABAcUABwAAAMRgwAoBAAAACQAAAElucHV0UGlwZQEACAAAAFBpcGVYMDAxAQGYAAMAAAAACQAAAElu" +
           "cHV0UGlwZQAvAQF4AJgAAAABAwAAAAAwAQEBlwAAMAABAZkAAQEBAAABAacAAgAAAMRgwAoBAAAAEAAA" +
           "AEZsb3dUcmFuc21pdHRlcjEBAAYAAABGVFgwMDEBAZkAAwAAAAAQAAAARmxvd1RyYW5zbWl0dGVyMQAv" +
           "AQEyAJkAAAABAQAAAAAwAQEBmAABAAAAFWCJCgIAAAABAAYAAABPdXRwdXQBAZoAAC8BAEAJmgAAAAAL" +
           "/////wEBAgAAAAEBAwAAAQG4AAEBAwAAAQHBAAEAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UBAZ4AAC4A" +
           "RJ4AAAABAHQD/////wEB/////wAAAADEYMAKAQAAAAUAAABWYWx2ZQEACQAAAFZhbHZlWDAwMQEBoAAD" +
           "AAAAAAUAAABWYWx2ZQAvAQEcAKAAAAAB/////wEAAAAVYIkKAgAAAAEABQAAAElucHV0AQGhAAAvAQBA" +
           "CaEAAAAAC/////8CAgEAAAABAQMAAQEBugABAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdlAQGlAAAuAESl" +
           "AAAAAQB0A/////8BAf////8AAAAAxGDACgEAAAAEAAAARHJ1bQEACAAAAERydW1YMDAxAQGnAAMAAAAA" +
           "BAAAAERydW0ALwEBhwCnAAAAAQQAAAAAMAEBAZcAAQEBAAEBAZgAADAAAQGoAAEBAgAAAQGvAAEAAACE" +
           "YMAKAQAAAA4AAABMZXZlbEluZGljYXRvcgEABgAAAExJWDAwMQEBqAAALwEBKwCoAAAAAQEAAAAAMAEB" +
           "AacAAQAAABVgiQoCAAAAAQAGAAAAT3V0cHV0AQGpAAAvAQBACakAAAAAGv////8BAQEAAAABAQMAAAEB" +
           "vAABAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdlAQGtAAAuAEStAAAAAQB0A/////8BAf////8AAAAAxGDA" +
           "CgEAAAAKAAAAT3V0cHV0UGlwZQEACAAAAFBpcGVYMDAyAQGvAAMAAAAACgAAAE91dHB1dFBpcGUALwEB" +
           "jwCvAAAAAQMAAAAAMAEBAZcAAQECAAEBAacAADAAAQGwAAEAAACEYMAKAQAAABAAAABGbG93VHJhbnNt" +
           "aXR0ZXIyAQAGAAAARlRYMDAyAQGwAAAvAQEyALAAAAABAQAAAAAwAQEBrwABAAAAFWCJCgIAAAABAAYA" +
           "AABPdXRwdXQBAbEAAC8BAEAJsQAAAAAL/////wEBAQAAAAEBAwAAAQHCAAEAAAAVYIkKAgAAAAAABwAA" +
           "AEVVUmFuZ2UBAbUAAC4ARLUAAAABAHQD/////wEB/////wAAAABEYMAKAQAAAA4AAABGbG93Q29udHJv" +
           "bGxlcgEABgAAAEZDWDAwMQEBtwADAAAAAA4AAABGbG93Q29udHJvbGxlcgAvAQEnALcAAAD/////AwAA" +
           "ABVgiQoCAAAAAQALAAAATWVhc3VyZW1lbnQBAbgAAC4ARLgAAAAAC/////8BAQEAAAABAQMAAQEBmgAA" +
           "AAAAFWCJCgIAAAABAAgAAABTZXRQb2ludAEBuQAALgBEuQAAAAAL/////wMDAQAAAAEBAwABAQHDAAAA" +
           "AAAVYIkKAgAAAAEACgAAAENvbnRyb2xPdXQBAboAAC4ARLoAAAAAC/////8BAQEAAAABAQMAAAEBoQAA" +
           "AAAARGDACgEAAAAPAAAATGV2ZWxDb250cm9sbGVyAQAGAAAATENYMDAxAQG7AAMAAAAADwAAAExldmVs" +
           "Q29udHJvbGxlcgAvAQEjALsAAAD/////AwAAABVgiQoCAAAAAQALAAAATWVhc3VyZW1lbnQBAbwAAC4A" +
           "RLwAAAAAC/////8BAQEAAAABAQMAAQEBqQAAAAAAFWCJCgIAAAABAAgAAABTZXRQb2ludAEBvQAALgBE" +
           "vQAAAAAL/////wMD/////wAAAAAVYIkKAgAAAAEACgAAAENvbnRyb2xPdXQBAb4AAC4ARL4AAAAAC///" +
           "//8BAQEAAAABAQMAAAEBwAAAAAAARGDACgEAAAAQAAAAQ3VzdG9tQ29udHJvbGxlcgEABgAAAENDWDAw" +
           "MQEBvwADAAAAABAAAABDdXN0b21Db250cm9sbGVyAC8BARYAvwAAAP////8FAAAAFWCJCgIAAAABAAYA" +
           "AABJbnB1dDEBAcAAAC4ARMAAAAAAC/////8CAgEAAAABAQMAAQEBvgAAAAAAFWCJCgIAAAABAAYAAABJ" +
           "bnB1dDIBAcEAAC4ARMEAAAAAC/////8CAgEAAAABAQMAAQEBmgAAAAAAFWCJCgIAAAABAAYAAABJbnB1" +
           "dDMBAcIAAC4ARMIAAAAAC/////8CAgEAAAABAQMAAQEBsQAAAAAAFWCJCgIAAAABAAoAAABDb250cm9s" +
           "T3V0AQHDAAAuAETDAAAAAAv/////AQEBAAAAAQEDAAABAbkAAAAAABVgyQoCAAAADAAAAERlc2NyaXB0" +
           "aW9uWAEACwAAAERlc2NyaXB0aW9uAQHEAAAuAETEAAAAABX/////AQH/////AAAAAIRggAoBAAAAAQAK" +
           "AAAAU2ltdWxhdGlvbgEBxQAALwEBOQDFAAAAAQEAAAAAJAEBAZcACgAAABVgiQoCAAAAAAAMAAAAQ3Vy" +
           "cmVudFN0YXRlAQHGAAAvAQDICsYAAAAAFf////8BAf////8CAAAAFWCJCgIAAAAAAAIAAABJZAEBxwAA" +
           "LgBExwAAAAAR/////wEB/////wAAAAAVYIkKAgAAAAAABgAAAE51bWJlcgEByQAALgBEyQAAAAAH////" +
           "/wEB/////wAAAAAVYIkKAgAAAAAADgAAAExhc3RUcmFuc2l0aW9uAQHLAAAvAQDPCssAAAAAFf////8B" +
           "Af////8DAAAAFWCJCgIAAAAAAAIAAABJZAEBzAAALgBEzAAAAAAR/////wEB/////wAAAAAVYIkKAgAA" +
           "AAAABgAAAE51bWJlcgEBzgAALgBEzgAAAAAH/////wEB/////wAAAAAVYIkKAgAAAAAADgAAAFRyYW5z" +
           "aXRpb25UaW1lAQHPAAAuAETPAAAAAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAkAAABEZWxldGFi" +
           "bGUBAdEAAC4ARNEAAAAAAf////8BAf////8AAAAAFWCJCgIAAAAAAAwAAABSZWN5Y2xlQ291bnQBAdMA" +
           "AC4ARNMAAAAABv////8BAf////8AAAAAJGGCCgQAAAAAAAUAAABTdGFydAEB4AADAAAAAEsAAABDYXVz" +
           "ZXMgdGhlIFByb2dyYW0gdG8gdHJhbnNpdGlvbiBmcm9tIHRoZSBSZWFkeSBzdGF0ZSB0byB0aGUgUnVu" +
           "bmluZyBzdGF0ZS4ALwEAegngAAAAAQH/////AAAAACRhggoEAAAAAAAHAAAAU3VzcGVuZAEB4QADAAAA" +
           "AE8AAABDYXVzZXMgdGhlIFByb2dyYW0gdG8gdHJhbnNpdGlvbiBmcm9tIHRoZSBSdW5uaW5nIHN0YXRl" +
           "IHRvIHRoZSBTdXNwZW5kZWQgc3RhdGUuAC8BAHsJ4QAAAAEB/////wAAAAAkYYIKBAAAAAAABgAAAFJl" +
           "c3VtZQEB4gADAAAAAE8AAABDYXVzZXMgdGhlIFByb2dyYW0gdG8gdHJhbnNpdGlvbiBmcm9tIHRoZSBT" +
           "dXNwZW5kZWQgc3RhdGUgdG8gdGhlIFJ1bm5pbmcgc3RhdGUuAC8BAHwJ4gAAAAEB/////wAAAAAkYYIK" +
           "BAAAAAAABAAAAEhhbHQBAeMAAwAAAABgAAAAQ2F1c2VzIHRoZSBQcm9ncmFtIHRvIHRyYW5zaXRpb24g" +
           "ZnJvbSB0aGUgUmVhZHksIFJ1bm5pbmcgb3IgU3VzcGVuZGVkIHN0YXRlIHRvIHRoZSBIYWx0ZWQgc3Rh" +
           "dGUuAC8BAH0J4wAAAAEB/////wAAAAAkYYIKBAAAAAAABQAAAFJlc2V0AQHkAAMAAAAASgAAAENhdXNl" +
           "cyB0aGUgUHJvZ3JhbSB0byB0cmFuc2l0aW9uIGZyb20gdGhlIEhhbHRlZCBzdGF0ZSB0byB0aGUgUmVh" +
           "ZHkgc3RhdGUuAC8BAH4J5AAAAAEB/////wAAAAA1YIkKAgAAAAEACgAAAFVwZGF0ZVJhdGUBAeUAAwAA" +
           "AAAmAAAAVGhlIHJhdGUgYXQgd2hpY2ggdGhlIHNpbXVsYXRpb24gcnVucy4ALgBE5QAAAAAH/////wMD" +
           "/////wAAAAA=";

    #endregion Initialization String

#endif

    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// A description for the PipeX001 Object.
    /// </summary>
    public BoilerInputPipeState InputPipe
    {
      get => m_inputPipe;

      set
      {
        if (!Object.ReferenceEquals(m_inputPipe, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_inputPipe = value;
      }
    }

    /// <summary>
    /// A description for the DrumX001 Object.
    /// </summary>
    public BoilerDrumState Drum
    {
      get => m_drum;

      set
      {
        if (!Object.ReferenceEquals(m_drum, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_drum = value;
      }
    }

    /// <summary>
    /// A description for the PipeX002 Object.
    /// </summary>
    public BoilerOutputPipeState OutputPipe
    {
      get => m_outputPipe;

      set
      {
        if (!Object.ReferenceEquals(m_outputPipe, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_outputPipe = value;
      }
    }

    /// <summary>
    /// A description for the FCX001 Object.
    /// </summary>
    public FlowControllerState FlowController
    {
      get => m_flowController;

      set
      {
        if (!Object.ReferenceEquals(m_flowController, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_flowController = value;
      }
    }

    /// <summary>
    /// A description for the LCX001 Object.
    /// </summary>
    public LevelControllerState LevelController
    {
      get => m_levelController;

      set
      {
        if (!Object.ReferenceEquals(m_levelController, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_levelController = value;
      }
    }

    /// <summary>
    /// A description for the CCX001 Object.
    /// </summary>
    public CustomControllerState CustomController
    {
      get => m_customController;

      set
      {
        if (!Object.ReferenceEquals(m_customController, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_customController = value;
      }
    }

    /// <summary>
    /// A description for the Simulation Object.
    /// </summary>
    public BoilerStateMachineState Simulation
    {
      get => m_simulation;

      set
      {
        if (!Object.ReferenceEquals(m_simulation, value))
        {
          ChangeMasks |= NodeStateChangeMasks.Children;
        }

        m_simulation = value;
      }
    }

    #endregion Public Properties

    #region Overridden Methods

    /// <summary>
    /// Populates a list with the children that belong to the node.
    /// </summary>
    /// <param name="context">The context for the system being accessed.</param>
    /// <param name="children">The list of children to populate.</param>
    public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
    {
      if (m_inputPipe != null)
      {
        children.Add(m_inputPipe);
      }

      if (m_drum != null)
      {
        children.Add(m_drum);
      }

      if (m_outputPipe != null)
      {
        children.Add(m_outputPipe);
      }

      if (m_flowController != null)
      {
        children.Add(m_flowController);
      }

      if (m_levelController != null)
      {
        children.Add(m_levelController);
      }

      if (m_customController != null)
      {
        children.Add(m_customController);
      }

      if (m_simulation != null)
      {
        children.Add(m_simulation);
      }

      base.GetChildren(context, children);
    }

    /// <summary>
    /// Finds the child with the specified browse name.
    /// </summary>
    protected override BaseInstanceState FindChild(
        ISystemContext context,
        QualifiedName browseName,
        bool createOrReplace,
        BaseInstanceState replacement)
    {
      if (QualifiedName.IsNull(browseName))
      {
        return null;
      }

      BaseInstanceState instance = null;

      switch (browseName.Name)
      {
        case tempuri.org.UA.Examples.BoilerType.BrowseNames.InputPipe:
          {
            if (createOrReplace)
            {
              if (InputPipe == null)
              {
                if (replacement == null)
                {
                  InputPipe = new BoilerInputPipeState(this);
                }
                else
                {
                  InputPipe = (BoilerInputPipeState)replacement;
                }
              }
            }

            instance = InputPipe;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Drum:
          {
            if (createOrReplace)
            {
              if (Drum == null)
              {
                if (replacement == null)
                {
                  Drum = new BoilerDrumState(this);
                }
                else
                {
                  Drum = (BoilerDrumState)replacement;
                }
              }
            }

            instance = Drum;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.OutputPipe:
          {
            if (createOrReplace)
            {
              if (OutputPipe == null)
              {
                if (replacement == null)
                {
                  OutputPipe = new BoilerOutputPipeState(this);
                }
                else
                {
                  OutputPipe = (BoilerOutputPipeState)replacement;
                }
              }
            }

            instance = OutputPipe;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.FlowController:
          {
            if (createOrReplace)
            {
              if (FlowController == null)
              {
                if (replacement == null)
                {
                  FlowController = new FlowControllerState(this);
                }
                else
                {
                  FlowController = (FlowControllerState)replacement;
                }
              }
            }

            instance = FlowController;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.LevelController:
          {
            if (createOrReplace)
            {
              if (LevelController == null)
              {
                if (replacement == null)
                {
                  LevelController = new LevelControllerState(this);
                }
                else
                {
                  LevelController = (LevelControllerState)replacement;
                }
              }
            }

            instance = LevelController;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.CustomController:
          {
            if (createOrReplace)
            {
              if (CustomController == null)
              {
                if (replacement == null)
                {
                  CustomController = new CustomControllerState(this);
                }
                else
                {
                  CustomController = (CustomControllerState)replacement;
                }
              }
            }

            instance = CustomController;
            break;
          }

        case tempuri.org.UA.Examples.BoilerType.BrowseNames.Simulation:
          {
            if (createOrReplace)
            {
              if (Simulation == null)
              {
                if (replacement == null)
                {
                  Simulation = new BoilerStateMachineState(this);
                }
                else
                {
                  Simulation = (BoilerStateMachineState)replacement;
                }
              }
            }

            instance = Simulation;
            break;
          }
      }

      if (instance != null)
      {
        return instance;
      }

      return base.FindChild(context, browseName, createOrReplace, replacement);
    }

    #endregion Overridden Methods

    #region Private Fields

    private BoilerInputPipeState m_inputPipe;
    private BoilerDrumState m_drum;
    private BoilerOutputPipeState m_outputPipe;
    private FlowControllerState m_flowController;
    private LevelControllerState m_levelController;
    private CustomControllerState m_customController;
    private BoilerStateMachineState m_simulation;

    #endregion Private Fields
  }

#endif

  #endregion BoilerState Class
}