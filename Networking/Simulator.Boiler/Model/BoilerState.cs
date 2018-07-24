using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace tempuri.org.UA.Examples.BoilerType
{
  public partial class BoilerState
  {

    public BoilerState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      CustomController = new CustomControllerState(this, BrowseNames.CustomController);
      Drum = new BoilerDrumState(this, BrowseNames.Drum, ModelExtensions.CreateRange(1000, 0));
      FlowController = new FlowControllerState(this, BrowseNames.FlowController);
      InputPipe = new BoilerInputPipeState(this, BrowseNames.InputPipe);
      LevelController = new LevelControllerState(this, BrowseNames.LevelController);
      LevelController.SetPoint.Value = 500.0;
      OutputPipe = new BoilerOutputPipeState(this, BrowseNames.OutputPipe);
      Simulation = new BoilerStateMachineState(this, BrowseNames.Simulation);
    }
    internal void StartSimulation()
    {
      if (m_simulationTimer != null)
      {
        m_simulationTimer.Dispose();
        m_simulationTimer = null;
      }
      uint updateRate = this.Simulation.UpdateRate.Value;
      if (updateRate < 100)
      {
        updateRate = 100;
        Simulation.UpdateRate.Value = updateRate;
      }
      m_simulationTimer = new Timer(DoSimulation, null, (int)updateRate, (int)updateRate);
    }
    internal ITraceSource Logger { get; set; } = new DoNothingTraceSource();

    //#region Initialization
    ///// <summary>
    ///// Initializes the object as a collection of counters which change value on read.
    ///// </summary>
    //protected override void OnAfterCreate(ISystemContext context, NodeState node)
    //{
    //  base.OnAfterCreate(context, node);

    //  this.Simulation.OnAfterTransition = OnControlSimulation;
    //  m_random = new Random();
    //}
    //#endregion

    #region IDisposeable Methods
    /// <summary>
    /// Cleans up when the object is disposed.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (m_simulationTimer != null)
        {
          m_simulationTimer.Dispose();
          m_simulationTimer = null;
        }
      }
    }
    #endregion

    #region Private
    //class
    private class DoNothingTraceSource : ITraceSource
    {
      public void TraceData(TraceEventType eventType, int id, object data) { }
    }
    //Fields
    private ISystemContext m_simulationContext = null;
    private Timer m_simulationTimer = null;
    private Random m_random = new Random();
    private long m_simulationCounter = 0;
    //Methods
    /// <summary>
    /// Changes the state of the simulation.
    /// </summary>
    private void OnControlSimulation(ISystemContext context, StateMachineState machine, uint transitionId, uint causeId, IList<object> inputArguments, IList<object> outputArguments)
    {
      switch (causeId)
      {
        case Methods.ProgramStateMachineType_Start:
          {
            if (m_simulationTimer != null)
            {
              m_simulationTimer.Dispose();
              m_simulationTimer = null;
            }
            uint updateRate = this.Simulation.UpdateRate.Value;
            if (updateRate < 100)
            {
              updateRate = 100;
              Simulation.UpdateRate.Value = updateRate;
            }
            m_simulationContext = context;
            m_simulationTimer = new Timer(DoSimulation, null, (int)updateRate, (int)updateRate);
            break;
          }
        case Methods.ProgramStateMachineType_Halt:
        case Methods.ProgramStateMachineType_Suspend:
          {
            if (m_simulationTimer != null)
            {
              m_simulationTimer.Dispose();
              m_simulationTimer = null;
            }
            m_simulationContext = context;
            break;
          }
        case Methods.ProgramStateMachineType_Reset:
          {
            if (m_simulationTimer != null)
            {
              m_simulationTimer.Dispose();
              m_simulationTimer = null;
            }
            m_simulationContext = context;
            break;
          }
      }
    }
    /// <summary>
    /// Rounds a value to the significant digits specified and adds a random perturbation.
    /// </summary>
    private double RoundAndPerturb(double value, byte significantDigits)
    {
      double offsetToApply = 0;
      if (value != 0)
      {
        // need to move all significant digits above the decimal point.
        double _offset = significantDigits - Math.Log10(Math.Abs(value));
        offsetToApply = Math.Floor(_offset);
        if (offsetToApply == _offset)
          offsetToApply -= 1;
      }
      // round value to significant digits.
      double _perturbedValue = Math.Round(value * Math.Pow(10.0, offsetToApply));
      // apply the perturbation.
      _perturbedValue += (m_random.NextDouble() - 0.5) * 5;
      // restore original exponent.
      _perturbedValue = Math.Round(_perturbedValue) * Math.Pow(10.0, -offsetToApply);
      // return value.
      return _perturbedValue;
    }
    /// <summary>
    /// Moves the value towards the target.
    /// </summary>
    private double Adjust(double value, double target, double step, Range range)
    {
      // convert percentage step to an absolute step if range is specified.
      if (range != null)
        step = step * range.Magnitude();
      double difference = target - value;
      if (difference < 0)
      {
        value -= step;
        if (value < target)
          return target;
      }
      else
      {
        value += step;
        if (value > target)
          return target;
      }
      return value;
    }
    /// <summary>
    /// Returns the value as a percentage of the range.
    /// </summary>
    private double GetPercentage(AnalogItemState<double> value)
    {
      double percentage = value.Value;
      Range range = value.EURange.Value;
      if (range != null)
      {
        percentage /= Math.Abs(range.High - range.Low);
        if (Math.Abs(percentage) > 1.0)
          percentage = 1.0;
      }
      return percentage;
    }
    /// <summary>
    /// Returns the value as a percentage of the range.
    /// </summary>
    private double GetValue(double value, Range range)
    {
      if (range != null)
        return value * range.Magnitude();
      return value;
    }
    /// <summary>
    /// Updates the values for the simulation. 
    /// </summary>
    private void DoSimulation(object state)
    {
      try
      {
        Logger.TraceData(TraceEventType.Verbose, 210, $"Entering {nameof(DoSimulation)} #{m_simulationCounter}");
        m_simulationCounter++;

        // adjust level.
        m_drum.LevelIndicator.Output.Value = Adjust(m_drum.LevelIndicator.Output.Value, m_levelController.SetPoint.Value, 0.1, m_drum.LevelIndicator.Output.EURange.Value);

        // calculate inputs for custom controller. 
        m_customController.Input1.Value = m_levelController.UpdateMeasurement(m_drum.LevelIndicator.Output);
        m_customController.Input2.Value = GetPercentage(m_inputPipe.FlowTransmitter1.Output);
        m_customController.Input3.Value = GetPercentage(m_outputPipe.FlowTransmitter2.Output);

        // calculate output for custom controller. 
        m_customController.ControlOut.Value = (m_customController.Input1.Value + m_customController.Input3.Value - m_customController.Input2.Value) / 2;

        // update flow controller set point.
        m_flowController.SetPoint.Value = GetValue((m_customController.ControlOut.Value + 1) / 2, m_inputPipe.FlowTransmitter1.Output.EURange.Value);

        double error = m_flowController.UpdateMeasurement(m_inputPipe.FlowTransmitter1.Output);

        // adjust the input valve.
        m_inputPipe.Valve.Input.Value = Adjust(m_inputPipe.Valve.Input.Value, (error > 0) ? 100 : 0, 10, null);

        // adjust the input flow.
        m_inputPipe.FlowTransmitter1.Output.Value = Adjust(m_inputPipe.FlowTransmitter1.Output.Value, m_flowController.SetPoint.Value, 0.6, m_inputPipe.FlowTransmitter1.Output.EURange.Value);

        // add pertubations.
        m_drum.LevelIndicator.Output.Value = RoundAndPerturb(m_drum.LevelIndicator.Output.Value, 3);
        m_inputPipe.FlowTransmitter1.Output.Value = RoundAndPerturb(m_inputPipe.FlowTransmitter1.Output.Value, 3);
        m_outputPipe.FlowTransmitter2.Output.Value = RoundAndPerturb(m_outputPipe.FlowTransmitter2.Output.Value, 3);
        this.ClearChangeMasks(m_simulationContext, true);
      }
      catch (Exception e)
      {
        Logger.TraceData(TraceEventType.Error, 225, $"Unexpected error during boiler simulation: {e}.");
      }
    }
    #endregion

  }
  public partial class FlowControllerState
  {
    public FlowControllerState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }
  public partial class BoilerInputPipeState
  {
    public BoilerInputPipeState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.FlowTransmitter1 = new FlowTransmitterState(this, BrowseNames.FlowTransmitter1);
      this.Valve = new ValveState(this, BrowseNames.Valve);
    }
  }
  public partial class LevelControllerState
  {
    public LevelControllerState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }
  public partial class BoilerOutputPipeState
  {
    public BoilerOutputPipeState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.FlowTransmitter2 = new FlowTransmitterState(this, BrowseNames.FlowTransmitter2);
    }
  }
  public partial class BoilerStateMachineState
  {
    public BoilerStateMachineState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.UpdateRate = new PropertyState<uint>(this, BrowseNames.UpdateRate, 200);
    }
  }
  public partial class FlowTransmitterState
  {
    public FlowTransmitterState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.Output = new AnalogItemState<double>(this, BrowseNames.Output, ModelExtensions.CreateRange(1, 0), 0.5);
    }
  }
  public partial class ValveState
  {
    public ValveState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }
  public partial class GenericActuatorState
  {
    public GenericActuatorState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.Input = new AnalogItemState<double>(this, BrowseNames.Input, ModelExtensions.CreateRange(1, 0), 0.5);
    }

  }
  partial class BoilerDrumState
  {

    public BoilerDrumState(NodeState parent, QualifiedName browseName, Range range) : base(parent, browseName)
    {
      this.LevelIndicator = new LevelIndicatorState(this, BrowseNames.LevelIndicator, range);
    }
  }
  public partial class LevelIndicatorState
  {

    public LevelIndicatorState(NodeState parent, QualifiedName browseName, Range range) : base(parent, browseName)
    {
      this.Output = new AnalogItemState<double>(this, BrowseNames.Output, range);
    }

  }
  public partial class GenericSensorState : BaseObjectState
  {
    public GenericSensorState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }

}

