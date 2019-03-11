/* ========================================================================
 * Copyright (c) 2005-2011 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;
using tempuri.org.UA.Examples.BoilerType;

namespace Commsvr.UA.Examples.BoilersSet
{
    #region Method Identifiers
    /// <summary>
    /// A class that declares constants for all Methods in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Methods
    {
        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Start Method.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_Start = 154;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Suspend Method.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_Suspend = 155;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Resume Method.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_Resume = 156;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Halt Method.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_Halt = 157;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Reset Method.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_Reset = 158;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Start Method.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_Start = 233;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Suspend Method.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_Suspend = 234;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Resume Method.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_Resume = 235;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Halt Method.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_Halt = 236;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Reset Method.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_Reset = 237;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Start Method.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_Start = 312;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Suspend Method.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_Suspend = 313;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Resume Method.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_Resume = 314;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Halt Method.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_Halt = 315;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Reset Method.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_Reset = 316;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Start Method.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_Start = 391;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Suspend Method.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_Suspend = 392;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Resume Method.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_Resume = 393;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Halt Method.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_Halt = 394;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Reset Method.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_Reset = 395;
    }
    #endregion

    #region Object Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Objects
    {
        /// <summary>
        /// The identifier for the BoilersArea Object.
        /// </summary>
        public const uint BoilersArea = 1;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha = 81;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe = 82;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1 = 83;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_Valve Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe_Valve = 90;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Drum = 97;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum_LevelIndicator Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Drum_LevelIndicator = 98;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_OutputPipe = 105;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2 = 106;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_FlowController = 113;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_LevelController = 117;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_CustomController = 121;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation Object.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation = 127;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo = 160;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe = 161;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1 = 162;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_Valve Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe_Valve = 169;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Drum = 176;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum_LevelIndicator Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Drum_LevelIndicator = 177;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_OutputPipe = 184;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2 = 185;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_FlowController = 192;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_LevelController = 196;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_CustomController = 200;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation Object.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation = 206;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie = 239;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe = 240;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1 = 241;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_Valve Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe_Valve = 248;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Drum = 255;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum_LevelIndicator Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Drum_LevelIndicator = 256;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_OutputPipe = 263;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2 = 264;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_FlowController = 271;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_LevelController = 275;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_CustomController = 279;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation Object.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation = 285;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta = 318;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe = 319;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1 = 320;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_Valve Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe_Valve = 327;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Drum = 334;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum_LevelIndicator Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Drum_LevelIndicator = 335;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_OutputPipe = 342;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2 = 343;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_FlowController = 350;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_LevelController = 354;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_CustomController = 358;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation Object.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation = 364;

        /// <summary>
        /// The identifier for the Drums Object.
        /// </summary>
        public const uint Drums = 398;

        /// <summary>
        /// The identifier for the Pipes Object.
        /// </summary>
        public const uint Pipes = 399;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output = 84;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EURange = 88;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_Valve_Input Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe_Valve_Input = 91;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EURange = 95;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output = 99;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EURange = 103;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output = 107;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EURange = 111;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_FlowController_Measurement = 114;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_FlowController_SetPoint = 115;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_FlowController_ControlOut = 116;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_LevelController_Measurement = 118;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_LevelController_SetPoint = 119;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_LevelController_ControlOut = 120;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_Input1 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_CustomController_Input1 = 122;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_Input2 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_CustomController_Input2 = 123;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_Input3 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_CustomController_Input3 = 124;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_CustomController_ControlOut = 125;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_DescriptionX Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_CustomController_DescriptionX = 126;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_CurrentState Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_CurrentState = 128;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_CurrentState_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_CurrentState_Id = 129;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_CurrentState_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_CurrentState_Number = 131;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_LastTransition = 133;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_LastTransition_Id = 134;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_LastTransition_Number = 136;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_LastTransition_TransitionTime = 137;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Deletable Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_Deletable = 139;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_RecycleCount Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_RecycleCount = 141;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateSessionId = 143;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateClientName = 144;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime = 145;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastTransitionTime = 146;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCall = 147;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodSessionId = 148;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodInputArguments = 149;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodOutputArguments = 150;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCallTime = 151;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodReturnStatus = 152;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_UpdateRate Variable.
        /// </summary>
        public const uint BoilersArea_BoilerAlpha_Simulation_UpdateRate = 159;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output = 163;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output_EURange = 167;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_Valve_Input Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe_Valve_Input = 170;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_InputPipe_Valve_Input_EURange = 174;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Drum_LevelIndicator_Output = 178;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Drum_LevelIndicator_Output_EURange = 182;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output = 186;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output_EURange = 190;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_FlowController_Measurement = 193;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_FlowController_SetPoint = 194;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_FlowController_ControlOut = 195;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_LevelController_Measurement = 197;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_LevelController_SetPoint = 198;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_LevelController_ControlOut = 199;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_Input1 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_CustomController_Input1 = 201;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_Input2 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_CustomController_Input2 = 202;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_Input3 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_CustomController_Input3 = 203;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_CustomController_ControlOut = 204;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_DescriptionX Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_CustomController_DescriptionX = 205;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_CurrentState Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_CurrentState = 207;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_CurrentState_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_CurrentState_Id = 208;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_CurrentState_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_CurrentState_Number = 210;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_LastTransition = 212;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_LastTransition_Id = 213;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_LastTransition_Number = 215;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_LastTransition_TransitionTime = 216;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Deletable Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_Deletable = 218;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_RecycleCount Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_RecycleCount = 220;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateSessionId = 222;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateClientName = 223;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_InvocationCreationTime = 224;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastTransitionTime = 225;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCall = 226;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodSessionId = 227;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodInputArguments = 228;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodOutputArguments = 229;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCallTime = 230;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodReturnStatus = 231;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_UpdateRate Variable.
        /// </summary>
        public const uint BoilersArea_BoilerBravo_Simulation_UpdateRate = 238;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output = 242;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output_EURange = 246;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_Valve_Input Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe_Valve_Input = 249;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_InputPipe_Valve_Input_EURange = 253;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output = 257;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output_EURange = 261;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output = 265;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output_EURange = 269;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_FlowController_Measurement = 272;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_FlowController_SetPoint = 273;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_FlowController_ControlOut = 274;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_LevelController_Measurement = 276;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_LevelController_SetPoint = 277;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_LevelController_ControlOut = 278;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_Input1 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_CustomController_Input1 = 280;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_Input2 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_CustomController_Input2 = 281;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_Input3 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_CustomController_Input3 = 282;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_CustomController_ControlOut = 283;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_DescriptionX Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_CustomController_DescriptionX = 284;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_CurrentState Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_CurrentState = 286;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_CurrentState_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_CurrentState_Id = 287;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_CurrentState_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_CurrentState_Number = 289;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_LastTransition = 291;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_LastTransition_Id = 292;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_LastTransition_Number = 294;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_LastTransition_TransitionTime = 295;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Deletable Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_Deletable = 297;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_RecycleCount Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_RecycleCount = 299;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateSessionId = 301;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateClientName = 302;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_InvocationCreationTime = 303;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastTransitionTime = 304;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCall = 305;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodSessionId = 306;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodInputArguments = 307;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodOutputArguments = 308;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCallTime = 309;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodReturnStatus = 310;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_UpdateRate Variable.
        /// </summary>
        public const uint BoilersArea_BoilerCharlie_Simulation_UpdateRate = 317;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output = 321;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output_EURange = 325;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_Valve_Input Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe_Valve_Input = 328;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_InputPipe_Valve_Input_EURange = 332;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Drum_LevelIndicator_Output = 336;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Drum_LevelIndicator_Output_EURange = 340;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output = 344;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output_EURange = 348;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_FlowController_Measurement = 351;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_FlowController_SetPoint = 352;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_FlowController_ControlOut = 353;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController_Measurement Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_LevelController_Measurement = 355;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController_SetPoint Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_LevelController_SetPoint = 356;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_LevelController_ControlOut = 357;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_Input1 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_CustomController_Input1 = 359;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_Input2 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_CustomController_Input2 = 360;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_Input3 Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_CustomController_Input3 = 361;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_ControlOut Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_CustomController_ControlOut = 362;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_DescriptionX Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_CustomController_DescriptionX = 363;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_CurrentState Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_CurrentState = 365;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_CurrentState_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_CurrentState_Id = 366;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_CurrentState_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_CurrentState_Number = 368;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_LastTransition = 370;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition_Id Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_LastTransition_Id = 371;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition_Number Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_LastTransition_Number = 373;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_LastTransition_TransitionTime = 374;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Deletable Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_Deletable = 376;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_RecycleCount Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_RecycleCount = 378;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateSessionId = 380;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateClientName = 381;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_InvocationCreationTime = 382;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastTransitionTime = 383;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCall = 384;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodSessionId = 385;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodInputArguments = 386;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodOutputArguments = 387;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCallTime = 388;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodReturnStatus = 389;

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_UpdateRate Variable.
        /// </summary>
        public const uint BoilersArea_BoilerDelta_Simulation_UpdateRate = 396;
    }
    #endregion

    #region Method Node Identifiers
    /// <summary>
    /// A class that declares constants for all Methods in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class MethodIds
    {
        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Start Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_Start = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerAlpha_Simulation_Start, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Suspend Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_Suspend = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerAlpha_Simulation_Suspend, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Resume Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_Resume = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerAlpha_Simulation_Resume, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Halt Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_Halt = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerAlpha_Simulation_Halt, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Reset Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_Reset = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerAlpha_Simulation_Reset, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Start Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_Start = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerBravo_Simulation_Start, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Suspend Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_Suspend = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerBravo_Simulation_Suspend, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Resume Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_Resume = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerBravo_Simulation_Resume, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Halt Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_Halt = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerBravo_Simulation_Halt, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Reset Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_Reset = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerBravo_Simulation_Reset, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Start Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_Start = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerCharlie_Simulation_Start, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Suspend Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_Suspend = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerCharlie_Simulation_Suspend, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Resume Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_Resume = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerCharlie_Simulation_Resume, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Halt Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_Halt = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerCharlie_Simulation_Halt, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Reset Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_Reset = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerCharlie_Simulation_Reset, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Start Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_Start = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerDelta_Simulation_Start, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Suspend Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_Suspend = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerDelta_Simulation_Suspend, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Resume Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_Resume = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerDelta_Simulation_Resume, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Halt Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_Halt = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerDelta_Simulation_Halt, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Reset Method.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_Reset = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Methods.BoilersArea_BoilerDelta_Simulation_Reset, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);
    }
    #endregion

    #region Object Node Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectIds
    {
        /// <summary>
        /// The identifier for the BoilersArea Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_InputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_Valve Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe_Valve = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_InputPipe_Valve, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Drum = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_Drum, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum_LevelIndicator Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Drum_LevelIndicator = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_Drum_LevelIndicator, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_OutputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_OutputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_FlowController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_FlowController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_LevelController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_LevelController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_CustomController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_CustomController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerAlpha_Simulation, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_InputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_Valve Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe_Valve = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_InputPipe_Valve, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Drum = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_Drum, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum_LevelIndicator Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Drum_LevelIndicator = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_Drum_LevelIndicator, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_OutputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_OutputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_FlowController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_FlowController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_LevelController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_LevelController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_CustomController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_CustomController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerBravo_Simulation, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_InputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_Valve Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe_Valve = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_InputPipe_Valve, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Drum = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_Drum, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum_LevelIndicator Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Drum_LevelIndicator = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_Drum_LevelIndicator, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_OutputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_OutputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_FlowController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_FlowController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_LevelController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_LevelController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_CustomController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_CustomController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerCharlie_Simulation, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_InputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_Valve Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe_Valve = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_InputPipe_Valve, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Drum = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_Drum, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum_LevelIndicator Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Drum_LevelIndicator = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_Drum_LevelIndicator, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_OutputPipe = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_OutputPipe, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2 Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_FlowController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_FlowController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_LevelController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_LevelController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_CustomController = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_CustomController, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation Object.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.BoilersArea_BoilerDelta_Simulation, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the Drums Object.
        /// </summary>
        public static readonly ExpandedNodeId Drums = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.Drums, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the Pipes Object.
        /// </summary>
        public static readonly ExpandedNodeId Pipes = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Objects.Pipes, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_InputPipe_FlowTransmitter1_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_Valve_Input Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe_Valve_Input = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_InputPipe_Valve_Input, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_InputPipe_Valve_Input_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Drum_LevelIndicator_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_OutputPipe_FlowTransmitter2_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_FlowController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_FlowController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_FlowController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_FlowController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_FlowController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_FlowController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_FlowController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_LevelController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_LevelController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_LevelController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_LevelController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_LevelController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_LevelController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_LevelController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_Input1 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_CustomController_Input1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_CustomController_Input1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_Input2 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_CustomController_Input2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_CustomController_Input2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_Input3 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_CustomController_Input3 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_CustomController_Input3, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_CustomController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_CustomController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_CustomController_DescriptionX Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_CustomController_DescriptionX = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_CustomController_DescriptionX, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_CurrentState Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_CurrentState = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_CurrentState, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_CurrentState_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_CurrentState_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_CurrentState_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_CurrentState_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_CurrentState_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_CurrentState_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_LastTransition = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_LastTransition, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_LastTransition_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_LastTransition_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_LastTransition_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_LastTransition_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_LastTransition_TransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_LastTransition_TransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_Deletable Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_Deletable = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_Deletable, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_RecycleCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_RecycleCount = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_RecycleCount, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateClientName = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_CreateClientName, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_InvocationCreationTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastTransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastTransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCall = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCall, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodInputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodInputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodOutputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodOutputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCallTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodCallTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodReturnStatus = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_ProgramDiagnostics_LastMethodReturnStatus, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerAlpha_Simulation_UpdateRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerAlpha_Simulation_UpdateRate = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerAlpha_Simulation_UpdateRate, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_InputPipe_FlowTransmitter1_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_Valve_Input Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe_Valve_Input = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_InputPipe_Valve_Input, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_InputPipe_Valve_Input_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_InputPipe_Valve_Input_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Drum_LevelIndicator_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Drum_LevelIndicator_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Drum_LevelIndicator_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Drum_LevelIndicator_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_OutputPipe_FlowTransmitter2_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_FlowController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_FlowController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_FlowController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_FlowController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_FlowController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_FlowController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_FlowController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_LevelController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_LevelController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_LevelController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_LevelController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_LevelController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_LevelController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_LevelController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_Input1 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_CustomController_Input1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_CustomController_Input1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_Input2 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_CustomController_Input2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_CustomController_Input2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_Input3 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_CustomController_Input3 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_CustomController_Input3, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_CustomController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_CustomController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_CustomController_DescriptionX Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_CustomController_DescriptionX = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_CustomController_DescriptionX, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_CurrentState Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_CurrentState = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_CurrentState, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_CurrentState_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_CurrentState_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_CurrentState_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_CurrentState_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_CurrentState_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_CurrentState_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_LastTransition = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_LastTransition, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_LastTransition_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_LastTransition_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_LastTransition_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_LastTransition_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_LastTransition_TransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_LastTransition_TransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_Deletable Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_Deletable = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_Deletable, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_RecycleCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_RecycleCount = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_RecycleCount, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateClientName = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_CreateClientName, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_InvocationCreationTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_InvocationCreationTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastTransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastTransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCall = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCall, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodInputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodInputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodOutputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodOutputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCallTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodCallTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodReturnStatus = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_ProgramDiagnostics_LastMethodReturnStatus, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerBravo_Simulation_UpdateRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerBravo_Simulation_UpdateRate = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerBravo_Simulation_UpdateRate, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_InputPipe_FlowTransmitter1_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_Valve_Input Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe_Valve_Input = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_InputPipe_Valve_Input, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_InputPipe_Valve_Input_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_InputPipe_Valve_Input_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Drum_LevelIndicator_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_OutputPipe_FlowTransmitter2_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_FlowController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_FlowController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_FlowController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_FlowController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_FlowController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_FlowController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_FlowController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_LevelController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_LevelController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_LevelController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_LevelController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_LevelController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_LevelController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_LevelController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_Input1 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_CustomController_Input1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_CustomController_Input1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_Input2 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_CustomController_Input2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_CustomController_Input2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_Input3 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_CustomController_Input3 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_CustomController_Input3, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_CustomController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_CustomController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_CustomController_DescriptionX Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_CustomController_DescriptionX = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_CustomController_DescriptionX, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_CurrentState Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_CurrentState = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_CurrentState, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_CurrentState_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_CurrentState_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_CurrentState_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_CurrentState_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_CurrentState_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_CurrentState_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_LastTransition = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_LastTransition, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_LastTransition_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_LastTransition_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_LastTransition_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_LastTransition_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_LastTransition_TransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_LastTransition_TransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_Deletable Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_Deletable = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_Deletable, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_RecycleCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_RecycleCount = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_RecycleCount, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateClientName = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_CreateClientName, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_InvocationCreationTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_InvocationCreationTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastTransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastTransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCall = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCall, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodInputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodInputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodOutputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodOutputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCallTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodCallTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodReturnStatus = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_ProgramDiagnostics_LastMethodReturnStatus, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerCharlie_Simulation_UpdateRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerCharlie_Simulation_UpdateRate = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerCharlie_Simulation_UpdateRate, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_InputPipe_FlowTransmitter1_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_Valve_Input Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe_Valve_Input = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_InputPipe_Valve_Input, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_InputPipe_Valve_Input_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_InputPipe_Valve_Input_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_InputPipe_Valve_Input_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum_LevelIndicator_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Drum_LevelIndicator_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Drum_LevelIndicator_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Drum_LevelIndicator_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Drum_LevelIndicator_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Drum_LevelIndicator_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output_EURange = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_OutputPipe_FlowTransmitter2_Output_EURange, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_FlowController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_FlowController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_FlowController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_FlowController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_FlowController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_FlowController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_FlowController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController_Measurement Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_LevelController_Measurement = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_LevelController_Measurement, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController_SetPoint Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_LevelController_SetPoint = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_LevelController_SetPoint, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_LevelController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_LevelController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_LevelController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_Input1 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_CustomController_Input1 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_CustomController_Input1, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_Input2 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_CustomController_Input2 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_CustomController_Input2, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_Input3 Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_CustomController_Input3 = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_CustomController_Input3, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_ControlOut Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_CustomController_ControlOut = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_CustomController_ControlOut, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_CustomController_DescriptionX Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_CustomController_DescriptionX = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_CustomController_DescriptionX, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_CurrentState Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_CurrentState = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_CurrentState, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_CurrentState_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_CurrentState_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_CurrentState_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_CurrentState_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_CurrentState_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_CurrentState_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_LastTransition = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_LastTransition, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition_Id Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_LastTransition_Id = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_LastTransition_Id, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition_Number Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_LastTransition_Number = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_LastTransition_Number, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_LastTransition_TransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_LastTransition_TransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_LastTransition_TransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_Deletable Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_Deletable = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_Deletable, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_RecycleCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_RecycleCount = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_RecycleCount, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateClientName Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateClientName = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_CreateClientName, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_InvocationCreationTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_InvocationCreationTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_InvocationCreationTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastTransitionTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastTransitionTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastTransitionTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCall Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCall = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCall, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodSessionId Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodSessionId = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodSessionId, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodInputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodInputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodInputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodOutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodOutputArguments = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodOutputArguments, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCallTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCallTime = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodCallTime, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodReturnStatus Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodReturnStatus = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_ProgramDiagnostics_LastMethodReturnStatus, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);

        /// <summary>
        /// The identifier for the BoilersArea_BoilerDelta_Simulation_UpdateRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId BoilersArea_BoilerDelta_Simulation_UpdateRate = new ExpandedNodeId(Commsvr.UA.Examples.BoilersSet.Variables.BoilersArea_BoilerDelta_Simulation_UpdateRate, Commsvr.UA.Examples.BoilersSet.Namespaces.BoilersSet);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the BoilerAlpha component.
        /// </summary>
        public const string BoilerAlpha = "Boiler #1";

        /// <summary>
        /// The BrowseName for the BoilerBravo component.
        /// </summary>
        public const string BoilerBravo = "Boiler #2";

        /// <summary>
        /// The BrowseName for the BoilerCharlie component.
        /// </summary>
        public const string BoilerCharlie = "Boiler #3";

        /// <summary>
        /// The BrowseName for the BoilerDelta component.
        /// </summary>
        public const string BoilerDelta = "Boiler #4";

        /// <summary>
        /// The BrowseName for the BoilersArea component.
        /// </summary>
        public const string BoilersArea = "BoilersArea";

        /// <summary>
        /// The BrowseName for the Drums component.
        /// </summary>
        public const string Drums = "Drums";

        /// <summary>
        /// The BrowseName for the Pipes component.
        /// </summary>
        public const string Pipes = "Pipes";
    }
    #endregion

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

        /// <summary>
        /// The URI for the BoilersSet namespace (.NET code namespace is 'Commsvr.UA.Examples.BoilersSet').
        /// </summary>
        public const string BoilersSet = "http://commsvr.com/UA/Examples/BoilersSet";

        /// <summary>
        /// The URI for the BoilersSetXsd namespace (.NET code namespace is 'Commsvr.UA.Examples.BoilersSet').
        /// </summary>
        public const string BoilersSetXsd = "http://commsvr.com/UA/Examples/BoilersSet/Types.xsd";
    }
    #endregion

}