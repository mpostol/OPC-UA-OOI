//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UAOOI.SemanticData.UAModelDesignExport.XML
{
  [TestClass]
  public class UAResourcesUnitTest
  {
    [TestMethod]
    public void LoadUADefinedTypesTestMethod()
    {
      ModelDesign newInstance = UAResources.LoadUADefinedTypes();
      Assert.IsNotNull(newInstance);
      Assert.AreEqual<int>(557, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace).Count<NodeDesign>());
      Assert.AreEqual<int>(20, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 0).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 1).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 2).Count<NodeDesign>());
      Assert.AreEqual<int>(90, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 3).Count<NodeDesign>());
      Assert.AreEqual<int>(53, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 4).Count<NodeDesign>());
      Assert.AreEqual<int>(169, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 5).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 6).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 7).Count<NodeDesign>());
      Assert.AreEqual<int>(19, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 8).Count<NodeDesign>());
      Assert.AreEqual<int>(58, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 9).Count<NodeDesign>());
      Assert.AreEqual<int>(6, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 10).Count<NodeDesign>());
      Assert.AreEqual<int>(15, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 11).Count<NodeDesign>());
      Assert.AreEqual<int>(13, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 12).Count<NodeDesign>());
      Assert.AreEqual<int>(38, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 13).Count<NodeDesign>());
      Assert.AreEqual<int>(76, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 14).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 15).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 17).Count<NodeDesign>());
      Assert.AreEqual<int>(0, newInstance.Items.Where<NodeDesign>(x => !x.NotInAddressSpace && x.PartNo == 19).Count<NodeDesign>());
      Assert.AreEqual<int>(1, newInstance.Namespaces.Length);
      Assert.AreEqual<string>(@"http://opcfoundation.org/UA/", newInstance.TargetNamespace);
      Assert.AreEqual<string>(@"http://opcfoundation.org/UA/", newInstance.TargetXmlNamespace);
    }
  }
}