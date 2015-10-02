using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class BindingUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void TestMethod1()
    {
      Binding<int> _nb = new Binding<int>(x => { });
      Assert.IsNotNull(_nb);
    }
    private class DataRepository : IBindingFactory
    {

      public IBinding GetDataBroker(string variableName)
      {
        if (variableName != VariableName)
          throw new ArgumentOutOfRangeException("variableName");
        return new Binding<string>(x => m_buffer = x);
      }
      private string m_buffer = null;

    }
    private const string VariableName = "VariableName";
  } //BindingUnitTest
}
