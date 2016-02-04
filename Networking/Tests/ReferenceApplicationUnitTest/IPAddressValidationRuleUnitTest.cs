
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using UAOOI.Networking.ReferenceApplication.Controls;

namespace UAOOI.Networking.ReferenceApplication.UnitTest
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
      ValidationResult _res = _vr.Validate("239.255.255.1", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsTrue(_res.IsValid);
      Assert.IsNull(_res.ErrorContent);
      _res = _vr.Validate("239.255.255.255", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsTrue(_res.IsValid);
      Assert.IsNull(_res.ErrorContent);
      _res = _vr.Validate("224.0.0.0", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsTrue(_res.IsValid);
      Assert.IsNull(_res.ErrorContent);
      _res = _vr.Validate("240.255.255.255", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsFalse(_res.IsValid);
      Assert.IsNotNull(_res.ErrorContent);
      _res = _vr.Validate("223.0.0.0", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsFalse(_res.IsValid);
      Assert.IsNotNull(_res.ErrorContent);
      _res = _vr.Validate("FF01:0:0:0:0:0:0:BAC0", System.Globalization.CultureInfo.InvariantCulture);
      Assert.IsFalse(_res.IsValid);
      Assert.IsNotNull(_res.ErrorContent);
      Assert.IsTrue(((string)_res.ErrorContent).Contains("The address family"));
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
