
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;

namespace UAOOI.Networking.ReferenceApplication.Core.MvvmLight
{
  /// <summary>
  /// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
  /// </summary>
  public abstract class DelegateCommandBase : ICommand
  {

    #region constructors
    /// <summary>
    /// Creates a new instance of a <see cref="DelegateCommandBase"/>, specifying both the execute action and the can execute function.
    /// </summary>
    /// <param name="executeMethod">The <see cref="Action"/> to execute when <see cref="ICommand.Execute"/> is invoked.</param>
    /// <param name="canExecuteMethod">The <see cref="Func{Object,Bool}"/> to invoked when <see cref="ICommand.CanExecute"/> is invoked.</param>
    protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
    {
      if (executeMethod == null || canExecuteMethod == null)
        throw new ArgumentNullException(nameof(executeMethod), Resources.DelegateCommandDelegatesCannotBeNull);
      m_ExecuteMethod = (arg) => { executeMethod(arg); return Task.Delay(0); };
      m_CanExecuteMethod = canExecuteMethod;
      m_SynchronizationContext = SynchronizationContext.Current;
    }
    /// <summary>
    /// Creates a new instance of a <see cref="DelegateCommandBase"/>, specifying both the Execute action as an awaitable Task and the CanExecute function.
    /// </summary>
    /// <param name="executeMethod">The <see cref="Func{Object,Task}"/> to execute when <see cref="ICommand.Execute"/> is invoked.</param>
    /// <param name="canExecuteMethod">The <see cref="Func{Object,Bool}"/> to invoked when <see cref="ICommand.CanExecute"/> is invoked.</param>
    protected DelegateCommandBase(Func<object, Task> executeMethod, Func<object, bool> canExecuteMethod)
    {
      if (executeMethod == null || canExecuteMethod == null)
        throw new ArgumentNullException(nameof(executeMethod), Resources.DelegateCommandDelegatesCannotBeNull);
      m_ExecuteMethod = executeMethod;
      m_CanExecuteMethod = canExecuteMethod;
      m_SynchronizationContext = SynchronizationContext.Current;
    }
    #endregion

    #region ICommand
    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public virtual event EventHandler CanExecuteChanged;
    /// <summary>
    /// Raises <see cref="DelegateCommandBase.CanExecuteChanged"/> so every command invoker can requery to check if the command can execute.
    /// <remarks>Note that this will trigger the execution of <see cref="DelegateCommandBase.CanExecute"/> once for each invoker.</remarks>
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
      OnCanExecuteChanged();
    }
    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    async void ICommand.Execute(object parameter)
    {
      await Execute(parameter);
    }
    bool ICommand.CanExecute(object parameter)
    {
      return CanExecute(parameter);
    }
    #endregion

    #region private
    protected readonly Func<object, Task> m_ExecuteMethod;
    protected Func<object, bool> m_CanExecuteMethod;
    /// <summary>
    /// Executes the command with the provided parameter by invoking the <see cref="Action{Object}"/> supplied during construction.
    /// </summary>
    /// <param name="parameter"></param>
    protected virtual async Task Execute(object parameter)
    {
      await m_ExecuteMethod(parameter);
    }
    /// <summary>
    /// Determines if the command can execute with the provided parameter by invoking the <see cref="Func{Object,Bool}"/> supplied during construction.
    /// </summary>
    /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
    /// <returns>Returns <see langword="true"/> if the command can execute.  <see langword="False"/> otherwise.</returns>
    protected virtual bool CanExecute(object parameter)
    {
      return m_CanExecuteMethod(parameter);
    }
    /// <summary>
    /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
    /// </summary>
    /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
    /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
    /// <returns>The current instance of DelegateCommand</returns>
    /// <summary>
    /// Raises <see cref="ICommand.CanExecuteChanged"/> so every command invoker can requery <see cref="ICommand.CanExecute"/>.
    /// </summary>
    protected virtual void OnCanExecuteChanged()
    {
      EventHandler handler = CanExecuteChanged;
      if (handler != null)
      {
        if (m_SynchronizationContext != null && m_SynchronizationContext != SynchronizationContext.Current)
          m_SynchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
        else
          handler.Invoke(this, EventArgs.Empty);
      }
    }
    protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
    {
      AddPropertyToObserve(PropertySupport.ExtractPropertyName(propertyExpression));
      HookInpc(propertyExpression.Body as MemberExpression);
    }
    /// <summary>
    /// Observes a property that is used to determine if this command can execute, and if it implements INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
    /// </summary>
    /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute((o) => PropertyName).</param>
    /// <returns>The current instance of DelegateCommand</returns>
    protected internal void ObservesCanExecuteInternal(Expression<Func<object, bool>> canExecuteExpression)
    {
      m_CanExecuteMethod = canExecuteExpression.Compile();
      AddPropertyToObserve(PropertySupport.ExtractPropertyNameFromLambda(canExecuteExpression));
      HookInpc(canExecuteExpression.Body as MemberExpression);
    }
    protected void HookInpc(MemberExpression expression)
    {
      if (expression == null)
        return;
      if (m_NotifyPropertyChangedInstance == null)
      {
        ConstantExpression constantExpression = expression.Expression as ConstantExpression;
        if (constantExpression != null)
        {
          m_NotifyPropertyChangedInstance = constantExpression.Value as INotifyPropertyChanged;
          if (m_NotifyPropertyChangedInstance != null)
            m_NotifyPropertyChangedInstance.PropertyChanged += Inpc_PropertyChanged;
        }
      }
    }
    protected void AddPropertyToObserve(string property)
    {
      if (m_PropertiesToObserve.Contains(property))
        throw new ArgumentException(String.Format("{0} is already being observed.", property));
      m_PropertiesToObserve.Add(property);
    }
    private void Inpc_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (m_PropertiesToObserve.Contains(e.PropertyName))
        RaiseCanExecuteChanged();
    }
    private SynchronizationContext m_SynchronizationContext = null;
    readonly HashSet<string> m_PropertiesToObserve = new HashSet<string>();
    private INotifyPropertyChanged m_NotifyPropertyChangedInstance = null;
    #endregion

  }
}