using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{

  internal class CustomNodesManager
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
      throw new ArgumentOutOfRangeException("variableName");
    }

    private DataRepository.ProducerBindingMonitoredValue<string> Value1 { get; set; }
    private DataRepository.ProducerBindingMonitoredValue<double> Value2 { get; set; }

  }
}
