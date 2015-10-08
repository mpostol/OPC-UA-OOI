using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerBindingUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void CreatorTestMethod1()
    {
      ProducerBindingFactor _pr = new ProducerBindingFactor();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("repositoryGroup", "variableName");
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void GetNewValueTestMethod()
    {
      ProducerBindingFactor _pr = new ProducerBindingFactor();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("repositoryGroup", "variableName");
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void NewValueTestMethod()
    {
      ProducerBindingFactor _pr = new ProducerBindingFactor();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("repositoryGroup", "variableName");
      Assert.IsNotNull(_bn);
      Assert.IsFalse(_bn.NewValue);
      _pr.Modify("654321");
      Assert.IsTrue(_bn.NewValue);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }

    private class ProducerBindingFactor : IBindingFactory
    {
      private ValueClass<string> _value = new ValueClass<string>();

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        var _ret = new ProducerBinding<string>(() => _value.Value);
        _value.PropertyChanged += (x, y) => _ret.OnNewValue();
        return _ret;
      }
      public class ProducerBinding<type> : Binding<type>, IProducerBinding
      {

        #region constructor
        protected ProducerBinding()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerBinding{type}"/> class.
        /// </summary>
        /// <param name="getValue">Captures a delegate used to assign new value to local resources.</param>
        public ProducerBinding(Func<type> getValue)
        {
          m_GetValue = getValue;
        }
        #endregion

        #region IProducerBinding
        bool IProducerBinding.NewValue
        {
          get
          {
            return b_NewValue;
          }
        }
        object IProducerBinding.GetFromRepository()
        {
          b_NewValue = false;
          if (this.m_Converter == null)
            return m_GetValue();
          else
            return (type)m_Converter.Convert(m_GetValue(), m_TargetType, m_Parameter, m_Culture);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        internal void OnNewValue()
        {
          PropertyChanged.RaiseHandler<bool>(true, ref b_NewValue, "NewValue", this);
        }

        protected Func<type> m_GetValue;
        private bool b_NewValue;

      }
      public class ProducerBindingMonitoredValue<type> : ProducerBinding<type>
      {
        public ProducerBindingMonitoredValue()
          : base()
        {
          m_GetValue = () => MonitoredValue;
        }
        public type MonitoredValue
        {
          get
          {
            return b_MyProperty;
          }
          set
          {
            if (Object.Equals(b_MyProperty, value))
              return;
            b_MyProperty = value;
            OnNewValue();
          }
        }
        private type b_MyProperty;

      }
      internal class ValueClass<type> : INotifyPropertyChanged
      {
        public type Value
        {
          get
          {
            return b_Value;
          }
          set
          {
            PropertyChanged.RaiseHandler<type>(value, ref b_Value, "Value", this);
          }
        }
        private type b_Value;
        public event PropertyChangedEventHandler PropertyChanged;
      }
      internal void Modify(string value)
      {
        _value.Value = value;
      }

    }

  }
  internal static class MyClass
  {

    /// <summary>
    /// Extension method that sets a new value in a variable and then executes the event handler if the new value
    /// differs from the old one.  Used to easily implement INotifyPropeprtyChanged.
    /// </summary>
    /// <typeparam name="T">The type of values being handled (usually the type of the property).</typeparam>
    /// <param name="handler">The event handler to execute in the event of actual value change.</param>
    /// <param name="newValue">The new value to set.</param>
    /// <param name="oldValue">The old value to replace (and the value holder).</param>
    /// <param name="propertyName">The property's name as required by <typeparamref name="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
    /// <param name="sender">The object to be appointed as the executioner of the handler.</param>
    /// <returns>A boolean value that indicates if the new value was truly different from the old value according to <code>object.Equals()</code>.</returns>
    public static bool RaiseHandler<T>(this PropertyChangedEventHandler handler, T newValue, ref T oldValue, string propertyName, object sender)
    {
      bool changed = !Object.Equals(oldValue, newValue);
      if (changed)
      {
        //Save the new value. 
        oldValue = newValue;
        //Raise the event 
        if (handler != null)
        {
          handler(sender, new PropertyChangedEventArgs(propertyName));
        }
      }
      //Signal what happened. 
      return changed;
    }
  }
}
