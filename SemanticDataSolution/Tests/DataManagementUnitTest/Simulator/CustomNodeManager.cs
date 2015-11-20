
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{

  internal class CustomNodeManager
  {

    internal IProducerBinding GetProducerBinding(string variableName, BuiltInType encoding)
    {
      if (variableName == "Value1")
      {
        Value1 = new ProducerBindingMonitoredValue<string>(variableName, encoding);
        return Value1;
      }
      else if (variableName == "Value2")
      {
        Value2 = new ProducerBindingMonitoredValue<double>(variableName, encoding);
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
        if (((IProducerBinding)Value1).Encoding != BuiltInType.String)
          throw new ArgumentOutOfRangeException("Wrong type");
        if ("Value1" != name)
          throw new ArgumentOutOfRangeException("Wrong name");
        Value1.MonitoredValue = (string)value;
      }
      else if (name == "Value2")
      {
        if (Value1.HandlerState != HandlerState.Operational)
          throw new ArgumentOutOfRangeException("Wrong HandlerState");
        if (((IProducerBinding)Value2).Encoding != BuiltInType.Double)
          throw new ArgumentOutOfRangeException("Wrong type");
        if ("Value2" != name)
          throw new ArgumentOutOfRangeException("Wrong name");
        Value2.MonitoredValue = (double)value;
      }
    }

    #endregion

  }

}
