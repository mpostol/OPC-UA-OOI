
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerBindingUnitTest
  {
    #region tests
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void CreatorTestMethod1()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName");
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void GetNewValueTestMethod()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName");
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
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBinding", "variableName");
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
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void CreatorTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName");
      Assert.IsNotNull(_bn);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void GetNewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName");
      Assert.IsNotNull(_bn);
      string _testValue = "1231221431423421";
      _pr.Modify(_testValue);
      Assert.IsTrue(_bn.NewValue);
      Assert.AreEqual<string>(_testValue, (string)_bn.GetFromRepository());
      Assert.IsFalse(_bn.NewValue);
    }
    [TestMethod]
    [TestCategory("DataManagement_IProducerBinding")]
    public void NewValueTestMethod2()
    {
      ProducerBindingFactory _pr = new ProducerBindingFactory();
      Assert.IsNotNull(_pr);
      IProducerBinding _bn = _pr.GetProducerBinding("ProducerBindingMonitoredValue", "variableName");
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

    #endregion
    private class ProducerBindingFactory : IBindingFactory
    {
      private ValueClass<string> _value = new ValueClass<string>();
      private ProducerBindingMonitoredValue<string> _monitoredValue = new ProducerBindingMonitoredValue<string>();

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        if (repositoryGroup == "ProducerBinding")
        {
          ProducerBinding<string> _ret = new ProducerBinding<string>(() => _value.Value);
          _value.PropertyChanged += (x, y) => _ret.OnNewValue();
          return _ret;
        }
        else if (repositoryGroup == "ProducerBindingMonitoredValue")
          return _monitoredValue;
        throw new ArgumentOutOfRangeException("repositoryGroup");
      }
      /// <summary>
      /// Class ProducerBinding - provides a basic implementation of the <see cref="IProducerBinding"/> interface.
      /// It is used by the producer to get data from data repository.
      /// </summary>
      /// <typeparam name="type">The type of the object in the repository.</typeparam>
      public class ProducerBinding<type> : Binding<type>, IProducerBinding
      {

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerBinding{type}"/> class.
        /// </summary>
        /// <remarks>
        /// The <see cref="ProducerBinding{type}.GetReadValueDelegate"/> that captures a delegate used to assign new value to local variable in the data repository.
        /// </remarks>
        protected ProducerBinding()
        {
          GetReadValueDelegate = () => default(type);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerBinding{type}"/> class.
        /// </summary>
        /// <param name="getValue">Captures a delegate used to assign new value to local resources.</param>
        public ProducerBinding(Func<type> getValue)
        {
          GetReadValueDelegate = getValue;
        }
        #endregion

        #region IProducerBinding
        /// <summary>
        /// Gets a value indicating whether the new value is available in the repository.
        /// </summary>
        /// <value><c>true</c> if the new value is available in repository; otherwise, <c>false</c>.</value>
        bool IProducerBinding.NewValue
        {
          get
          {
            return b_NewValue;
          }
        }
        /// <summary>
        /// Gets the new value and resets the flag <see cref="IProducerBinding.NewValue" />.
        /// </summary>
        /// <returns>Current value in the repository <see cref="System.Object" />.</returns>
        object IProducerBinding.GetFromRepository()
        {
          b_NewValue = false;
          if (this.m_Converter == null)
            return GetReadValueDelegate();
          else
            return (type)m_Converter.Convert(GetReadValueDelegate(), m_TargetType, m_Parameter, m_Culture);
        }
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region internal API
        /// <summary>
        /// Called when the new value is available in the repository.
        /// </summary>
        internal void OnNewValue()
        {
          PropertyChanged.RaiseHandler<bool>(true, ref b_NewValue, "NewValue", this);
        }

        #endregion

        /// <summary>
        /// Gets the read value from repository delegate.
        /// </summary>
        /// <value>The <see cref="Func{type}"/> delegate used to read value from repository.</value>
        protected virtual Func<type> GetReadValueDelegate { private set; get; }
        private bool b_NewValue;

      }
      public class ProducerBindingMonitoredValue<type> : ProducerBinding<type>
      {
        public ProducerBindingMonitoredValue()
          : base()
        { }
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
        protected override Func<type> GetReadValueDelegate
        {
          get
          {
            return () => MonitoredValue;
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
        _monitoredValue.MonitoredValue = value;
      }

    }

  }
  /// <summary>
  /// Class Extensions - provides a set of static helper methods for this library.
  /// </summary>
  internal static class Extensions
  {

    /// <summary>
    /// Extension method that sets a new value in a variable and then executes the event handler if the new value
    /// differs from the old one.  Used to easily implement <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <typeparam name="T">The type of values being handled by the property.</typeparam>
    /// <param name="handler">The event handler to execute in the event of actual value change.</param>
    /// <param name="newValue">The new value to set.</param>
    /// <param name="oldValue">The old value to replace (and the value holder).</param>
    /// <param name="propertyName">The property's name as required by <see cref="PropertyChangedEventArgs"/>.</param>
    /// <param name="sender">The object to be appointed as the executioner of the handler.</param>
    /// <returns>A boolean value that indicates if the new value was truly different from the old value according to <see cref="Object.Equals"/>.</returns>
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
