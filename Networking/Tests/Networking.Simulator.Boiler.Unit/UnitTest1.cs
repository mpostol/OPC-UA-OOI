using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest
{
  [TestClass]
  public class DataGeneratorUnitTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      Assert.Inconclusive("Under development");
      using (DataGenerator _generator = new DataGenerator(new BoilersSetFixture()))
      {
        IBindingFactory _bindingFactory = _generator;
        IProducerBinding _binding = _bindingFactory.GetProducerBinding("repositoryGroup", "processValueName", new UATypeInfo(BuiltInType.Boolean));
      }
    }
    private class BoilersSetFixture : IBoilersSet
    {
      public void Dispose()
      {
        throw new NotImplementedException();
      }
      public void GetSemanticDataSetSources(Action<string, string, IVariable> registerSemanticData)
      {
        throw new NotImplementedException();
      }
    }
  }
}
