
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows.Data;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

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
      EncodingFactory _ief = new EncodingFactory();
      Assert.IsNotNull(_ief);
      _ief.UpdateValueConverter(null, String.Empty,  BuiltInType.Null);
    }
    [TestMethod]
    [TestCategory("DataManagement_IEncodingFactory")]
    public void UpdateValueConverterTestMethod2()
    {
      EncodingFactory _ief = new EncodingFactory();
      Assert.IsNotNull(_ief);
      MyBinding _ibf = new MyBinding();
      _ief.UpdateValueConverter(_ibf, String.Empty, BuiltInType.Null);
      _ibf.Assign2Repository(null);
    }
    //private
    private class EncodingFactory : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, BuiltInType sourceEncoding)
      {
        if (converter == null)
          throw new NotImplementedException();
        converter.Parameter = "Conversion parameter";
        converter.Converter = new IVC();
        converter.Culture = CultureInfo.InvariantCulture;
      }
      public IUADecoder UADecoder
      {
        get
        {
          return m_UADecoder;
        }
      }
      private readonly IUADecoder m_UADecoder = new Helpers.UABinaryDecoderImplementation();
    }
    private class IVC : IValueConverter
    {

      /// <summary>
      /// Converts the specified value.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="targetType">Type of the target.</param>
      /// <param name="parameter">The parameter.</param>
      /// <param name="culture">The culture.</param>
      /// <returns>System.Object.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }
    private class MyBinding : IBinding
    {
      public System.Windows.Data.IValueConverter Converter
      {
        set;
        private get;
      }
      public BuiltInType Encoding
      {
        get { throw new NotImplementedException(); }
      }
      public object Parameter
      {
        set;
        get;
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
