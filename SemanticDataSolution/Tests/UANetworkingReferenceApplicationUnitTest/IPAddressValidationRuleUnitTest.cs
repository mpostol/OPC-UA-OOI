
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANetworking.ReferenceApplication.Controls;
using System.Windows.Controls;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.UnitTest
{
  [TestClass]
  public class IPAddressValidationRuleUnitTest
  {
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void CreatorTestMethod()
    {
      IPAddressValidationRule _vr = new Controls.IPAddressValidationRule();
      Assert.IsNotNull(_vr);
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void NullStringTestMethod()
    {
      IPAddressValidationRule _vr = new Controls.IPAddressValidationRule();
      Assert.IsNotNull(_vr);
      ValidationResult _res = _vr.Validate(null, System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsFalse(_res.IsValid);
      Assert.IsNotNull(_res.ErrorContent);
      Assert.IsFalse(string.IsNullOrEmpty(_res.ErrorContent as string));
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void NotStringTestMethod()
    {
      IPAddressValidationRule _vr = new Controls.IPAddressValidationRule();
      Assert.IsNotNull(_vr);
      ValidationResult _res = _vr.Validate(123, System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsFalse(_res.IsValid);
      Assert.IsNotNull(_res.ErrorContent);
      Assert.IsFalse(string.IsNullOrEmpty(_res.ErrorContent as string));
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void ValidStringTestMethod()
    {
      IPAddressValidationRule _vr = new Controls.IPAddressValidationRule();
      Assert.IsNotNull(_vr);
      ValidationResult _res = _vr.Validate("139.255.255.1", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsTrue(_res.IsValid);
      Assert.IsNull(_res.ErrorContent);
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_IPAddressValidationRuleUnitTest")]
    public void NotValidIPTestMethod()
    {
      IPAddressValidationRule _vr = new Controls.IPAddressValidationRule();
      Assert.IsNotNull(_vr);
      ValidationResult _res = _vr.Validate("139.255.255.999", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsFalse(_res.IsValid);
      Assert.IsNotNull(_res.ErrorContent);
      Assert.IsFalse(string.IsNullOrEmpty(_res.ErrorContent as string));
    }

  }
}
