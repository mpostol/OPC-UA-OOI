
using System;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest.Simulator
{

  internal class CustomNodeManager
  {

    internal IProducerBinding GetProducerBinding(string variableName, BuiltInType encoding)
    {
      UATypeInfo _uaTypeInfo = new UATypeInfo(encoding);
      if (variableName == "Value1")
      {
        Value1 = new ProducerBindingMonitoredValue<string>(variableName, _uaTypeInfo);
        return Value1;
      }
      else if (variableName == "Value2")
      {
        Value2 = new ProducerBindingMonitoredValue<double>(variableName, _uaTypeInfo);
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
        if (Value1.Encoding.BuiltInType != BuiltInType.String)
          throw new ArgumentOutOfRangeException("Wrong type");
        if ("Value1" != name)
          throw new ArgumentOutOfRangeException("Wrong name");
        Value1.MonitoredValue = (string)value;
      }
      else if (name == "Value2")
      {
        if (Value2.Encoding.BuiltInType != BuiltInType.Double)
          throw new ArgumentOutOfRangeException("Wrong type");
        if ("Value2" != name)
          throw new ArgumentOutOfRangeException("Wrong name");
        Value2.MonitoredValue = (double)value;
      }
    }

    #endregion

  }

}
