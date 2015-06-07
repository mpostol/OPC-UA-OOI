using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UAOOI.SemanticData.TypeEditors
{
	public class Data
	{
		private object instance;
		public LinkedList<Data> Children { get; set; }
		public bool ReadOnly { get; private set; }
		public string Name { get; set; }
		public object Value { get; set; }
		public ICommand ModifyCommand { get; private set; }
		
		public Data(object instance, string name, object value, bool readOnly)
		{
			this.instance = instance;
			Children = new LinkedList<Data>();
			Name = name;
			Value = value;
			ReadOnly = readOnly;

			ModifyCommand = new CommandWrapper(param =>
			{
				PropertyInfo prop = instance.GetType().GetProperty(Name);
				try
				{
					prop.SetValue(instance, Convert.ChangeType(Value, prop.PropertyType));
				}
				catch(FormatException e)
				{
					MessageBox.Show(e.Message, e.HelpLink);
				}
				Keyboard.ClearFocus();

			}, param => !ReadOnly);
		}

		public Data Add(Data child)
		{
			Children.AddLast(child);
			return this;
		}
	}
}
