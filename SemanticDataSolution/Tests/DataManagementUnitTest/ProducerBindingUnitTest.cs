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
      Assert.AreEqual<string>(_testValue, (string)_bn.GetNewValue());
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
      Assert.AreEqual<string>(_testValue, (string)_bn.GetNewValue());
      Assert.IsFalse(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetNewValue());
      Assert.IsFalse(_bn.NewValue);
    }

    private class ProducerBindingFactor : IBindingFactory
    {
      private ValueClas<string> _value = new ValueClas<string>();

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        var _ret = new ProducerBinding<string>(() => _value.Value);
        _value.PropertyChanged += _ret.OnNewValue;
        return _ret;
      }
      private class ProducerBinding<type> : IProducerBinding
      {
        public ProducerBinding()
        {

        }
        public ProducerBinding(Func<type> getValue)
        {
          m_GetValue = getValue;
        }
        bool IProducerBinding.NewValue
        {
          get
          {
            return b_NewValue;
          }
        }
        object IProducerBinding.GetNewValue()
        {
          b_NewValue = false;
          return m_GetValue();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        internal void OnNewValue(object sender, PropertyChangedEventArgs e)
        {
          PropertyChanged.RaiseHandler<bool>(true, ref b_NewValue, "NewValue", this);
        }
        private Func<type> m_GetValue;
        private bool b_NewValue;
      }
      internal class ValueClas<type> : INotifyPropertyChanged
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
