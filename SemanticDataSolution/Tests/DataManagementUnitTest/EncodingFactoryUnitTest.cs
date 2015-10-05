using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class EncodingFactoryUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_IEncodingFactory")]
    [ExpectedException(typeof(NotImplementedException))]
    public void UpdateValueConverterTestMethod1()
    {
      IEF _ief = new IEF();
      Assert.IsNotNull(_ief);
      _ief.UpdateValueConverter(null, String.Empty, String.Empty);
    }
    [TestMethod]
    [TestCategory("DataManagement_IEncodingFactory")]
    public void UpdateValueConverterTestMethod2()
    {
      IEF _ief = new IEF();
      Assert.IsNotNull(_ief);
      IBF _ibf = new IBF();
      _ief.UpdateValueConverter(_ibf, String.Empty, String.Empty);
      _ibf.Assign2Repository(null);
    }
    //private
    private class IEF : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        if (converter == null)
          throw new NotImplementedException();
        converter.Parameter = "Conversion parameter";
        converter.Converter = new IVC();
        converter.Culture = System.Globalization.CultureInfo.InvariantCulture;
      }
    }
    private class IVC : IValueConverter
    {

      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }
    private class IBF : IBinding
    {
      public System.Windows.Data.IValueConverter Converter
      {
        set;
        private get;
      }
      public Type TargetType
      {
        get { throw new NotImplementedException(); }
      }
      public object Parameter
      {
        set;
        private get;
      }
      public System.Globalization.CultureInfo Culture
      {
        set;
        private get;
      }
      public void Assign2Repository(object value)
      {
        Assert.IsNotNull(Converter);
        Assert.IsNotNull(Parameter);
        Assert.IsNotNull(Culture);
      }
      public void OnEnabling()
      {
        throw new NotImplementedException();
      }

      public void OnDisabling()
      {
        throw new NotImplementedException();
      }
    }
  }
}
