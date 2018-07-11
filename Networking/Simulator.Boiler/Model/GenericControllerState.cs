
using System;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{

  /// <summary>
  /// Class GenericControllerState - an object representing a generic controller.
  /// </summary>
  /// <seealso cref="UAOOI.Networking.Simulator.Boiler.AddressSpace.BaseObjectState" />
  /// <remarks>This part adds behavior to generated class <see cref="GenericControllerState" /></remarks>
  public partial class GenericControllerState
  {

    public GenericControllerState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.ControlOut = new PropertyState<double>(this, BrowseNames.ControlOut);
      this.Measurement = new PropertyState<double>(this, BrowseNames.Measurement);
      this.SetPoint = new PropertyState<double>(this, BrowseNames.SetPoint);
    }
    #region Public Interface
    /// <summary>
    /// Updates the measurement and calculates the new control output.
    /// </summary>
    public double UpdateMeasurement(AnalogItemState<double> source)
    {
      Range range = source.EURange.Value;
      m_measurement.Value = source.Value;
      // clamp the set-point.
      if (range != null)
      {
        if (m_setPoint.Value > range.High)
          m_setPoint.Value = range.High;
        if (m_setPoint.Value < range.Low)
          m_setPoint.Value = range.Low;
      }
      // calculate error.
      m_controlOut.Value = m_setPoint.Value - m_measurement.Value;
      if (range != null)
      {
        m_controlOut.Value /= range.Magnitude();
        if (Math.Abs(m_controlOut.Value) > 1.0)
          m_controlOut.Value = (m_controlOut.Value < 0) ? -1.0 : +1.0;
      }
      // return the new output.
      return m_controlOut.Value;
    }
    #endregion
  }
}
