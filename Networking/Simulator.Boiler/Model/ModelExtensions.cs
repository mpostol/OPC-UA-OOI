
using System;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.Model
{
  /// <summary>
  /// <see cref="Range"/> helper functions.
  /// </summary>
  internal static class ModelExtensions
  {
    /// <summary>
    /// Initializes the object with the high and low limits.
    /// </summary>
    internal static Range CreateRange(double high, double low)
    {
      Tuple<double, double> _value = CreateValue(high, low);
      Range _ret = new Range()
      {
        Low = _value.Item1,
        High = _value.Item2,
      };
      return _ret;
    }
    internal static void Initialize(this LevelIndicatorState instance, QualifiedName qualifiedName)
    {
      instance.BrowseName = qualifiedName;
      instance.Output = new AnalogItemState<double>(instance, BrowseNames.Output, CreateRange(0, 1), 0.5);
    }
    internal static void Initialize(this BoilerInputPipeState instance)
    {
      instance.BrowseName = BrowseNames.InputPipe;
      instance.FlowTransmitter1 = new FlowTransmitterState(instance);
      instance.FlowTransmitter1.Initialize(BrowseNames.FlowTransmitter1);
      instance.Valve = new ValveState(instance);
      instance.Valve.Initialize(BrowseNames.Valve);
    }
    internal static void Initialize(this FlowTransmitterState instance, QualifiedName qualifiedName)
    {
      instance.BrowseName = qualifiedName;
      instance.Output = new AnalogItemState<double>(instance, BrowseNames.Output, CreateRange(1, 0), 0.5);
    }
    internal static void Initialize(this ValveState instance, QualifiedName qualifiedName)
    {
      instance.BrowseName = qualifiedName;
      instance.Input = new AnalogItemState<double>(instance, BrowseNames.Input, CreateRange(1, 0), 0.5);
    }
    /// <summary>
    /// Returns the difference between high and low of the <see cref="Range"/>.
    /// </summary>
    public static double Magnitude(this Range value)
    {
      return Math.Abs(value.High - value.Low);
    }
    private static Tuple<double, double> CreateValue(double high, double low)
    {
      return high > low ? Tuple.Create<double, double>(low, high) : Tuple.Create(high, low);
    }
  }
}
