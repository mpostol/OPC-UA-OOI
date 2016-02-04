using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest
{
  // Set the Name and Namespace properties to new values.
  [DataContract(Name = "CSharpJsonEncoding", Namespace = "http://www.commsvr.com/UAOOI/DataBindings/UnitTest")]
  internal class CSharpSelectedTypesEncoding : IExtensibleDataObject
  {
    public CSharpSelectedTypesEncoding() { }
    // To implement the IExtensibleDataObject interface, you must also implement the ExtensionData property.
    private ExtensionDataObject extensionDataObjectValue;
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return extensionDataObjectValue;
      }
      set
      {
        extensionDataObjectValue = value;
      }
    }

    [DataMember(Name = "BooleanType")]
    internal bool BooleanType = false;
    [DataMember(Name = "StringType")]
    internal string StringType = "StringType";
    [DataMember(Name = "floatType")]
    internal float floatType = 1.12345678e-3F;
    [DataMember(Name = "intType")]
    internal int intType = 98765;
    [DataMember(Name = "DateTimeType")]
    internal DateTime DateTimeType = DateTime.Today;
    [DataMember(Name = "GuidType")]
    internal Guid GuidType = Guid.NewGuid();
    [DataMember(Name = "ByteStringType")]
    internal byte[] ByteStringType = new byte[] { 0x1, 0x55, 0xCF, 0xFF };

    internal void AreEqual(CSharpSelectedTypesEncoding p2)
    {
      Assert.AreEqual<bool>(this.BooleanType, p2.BooleanType);
      Assert.AreEqual<string>(this.StringType, p2.StringType);
      Assert.AreEqual<float>(this.floatType, p2.floatType);
      Assert.AreEqual<int>(this.intType, p2.intType);
      Assert.AreEqual<DateTime>(this.DateTimeType, p2.DateTimeType);
      Assert.AreEqual<Guid>(this.GuidType, p2.GuidType);
      CollectionAssert.AreEqual(this.ByteStringType, p2.ByteStringType);
    }

  }

}
