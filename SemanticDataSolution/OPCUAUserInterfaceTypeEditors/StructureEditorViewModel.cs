using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UAOOI.SemanticData.TypeEditors
{
	public class StructureEditorViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		//-------------------------------------------------------------------------------

		public static object ReflectedObject { get; set; }

		//-------------------------------------------------------------------------------

		public int ReflectionLevel { get; set; }

		//-------------------------------------------------------------------------------

		public IEnumerable<Data> TreeItemsSource { get; private set; }

		//-------------------------------------------------------------------------------

		public ICommand ReflectCommand { get; private set; }

		//-------------------------------------------------------------------------------

		public StructureEditorViewModel()
		{
			ReflectionLevel = 3;

			ReflectCommand = new CommandWrapper(param =>
			{
				TreeItemsSource = Reflector.ReflectObjectProperties(ReflectedObject, ReflectionLevel);
				PropertyChanged(this, new PropertyChangedEventArgs("TreeItemsSource"));

			}, param => true);
		}
	}
}
