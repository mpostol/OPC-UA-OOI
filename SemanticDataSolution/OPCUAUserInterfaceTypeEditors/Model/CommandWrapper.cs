using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UAOOI.SemanticData.TypeEditors
{
	class CommandWrapper : ICommand
	{
		Action<object> execute;
		Predicate<object> canExecute;

		public CommandWrapper(Action<object> execute, Predicate<object> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			execute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }

			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}
