using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{

  internal class CustomNodeManager
  {

    internal IProducerBinding GetProducerBinding(string variableName)
    {
      if (variableName == "Value1")
      {
        Value1 = new ProducerBindingMonitoredValue<string>(variableName);
        return Value1;
      }
      else if (variableName == "Value2")
      {
        Value2 = new ProducerBindingMonitoredValue<double>(variableName);
        return Value2;
      }
      else
        throw new ArgumentOutOfRangeException("variableName");
    }
    private ProducerBindingMonitoredValue<string> Value1 { get; set; }
    private ProducerBindingMonitoredValue<double> Value2 { get; set; }

    #region test environment
    internal void Update(object value, string name)
    {
      if (name == "Value1")
      {
        if (Value1.HandlerState != HandlerState.Operational)
          throw new ArgumentOutOfRangeException("Wrong HandlerState");
        if (((IProducerBinding)Value1).TargetType != value.GetType())
          throw new ArgumentOutOfRangeException("Wrong type");
        if ("Value1" != name)
          throw new ArgumentOutOfRangeException("Wrong name");
        Value1.MonitoredValue = (string)value;
      }
      else if (name == "Value2")
      {
        if (Value1.HandlerState != HandlerState.Operational)
          throw new ArgumentOutOfRangeException("Wrong HandlerState");
        if (((IProducerBinding)Value2).TargetType != value.GetType())
          throw new ArgumentOutOfRangeException("Wrong type");
        if ("Value2" != name)
          throw new ArgumentOutOfRangeException("Wrong name");
        Value2.MonitoredValue = (double)value;
      }
    }
    #endregion

  }
}
