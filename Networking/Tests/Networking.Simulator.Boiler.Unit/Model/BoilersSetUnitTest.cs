//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using Boilers = Commsvr.UA.Examples.BoilersSet;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{
  [TestClass]
  public class BoilersSetUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      using (BoilersSet _set = BoilersSet.Factory)
      {
        Assert.IsNotNull(_set);
        Assert.AreEqual<string>(Boilers.BrowseNames.BoilersArea, _set.BrowseName.Name);
        Assert.AreEqual<NodeClass>(NodeClass.Object_1, _set.NodeClass);
        Assert.IsNull(_set.Parent);
      }
    }
    [TestMethod]
    public void GetSemanticDataSourcesTest()
    {
      using (ISemanticDataSource _set = BoilersSet.Factory)
      {
        Assert.IsNotNull(_set);
        _set.GetSemanticDataSources((semanticDataSetRootBrowsePath, variableRelativeBrowsePath, variable) => AddressSpace.Add($"{semanticDataSetRootBrowsePath}:{variableRelativeBrowsePath}", variable));
        Assert.AreEqual<int>(80, AddressSpace.Count);
        foreach (KeyValuePair<string, IVariable> _var in AddressSpace)
          Debug.WriteLine($"{_var.Key}:{_var.Value.ValueType}");

      }
      Assert.IsTrue(AddressSpace.ContainsKey("BoilersArea_Boiler #1:CCX001_ControlOut"));
      Assert.IsTrue(AddressSpace.ContainsKey("BoilersArea_Boiler #2:CCX001_ControlOut"));
      Assert.IsTrue(AddressSpace.ContainsKey("BoilersArea_Boiler #3:CCX001_ControlOut"));
      Assert.IsTrue(AddressSpace.ContainsKey("BoilersArea_Boiler #4:CCX001_ControlOut"));
    }
    private Dictionary<string, IVariable> AddressSpace = new Dictionary<string, IVariable>();

  }
}
