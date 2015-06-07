using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace UAOOI.SemanticData.TypeEditors
{
	public static class Reflector
	{
		private delegate void SetValue<T>(T value);

		public static IEnumerable<Data> ReflectObjectProperties(object obj, int reflectionLevel)
		{
			LinkedList<Data> root = new LinkedList<Data>();
			root.AddLast(new Data(obj, obj.GetType().GetTypeInfo().Name, obj, true));
			root.Last.Value.Children = FindProperties(obj, reflectionLevel);
			return root;
		}

		private static LinkedList<Data> FindProperties(object obj, int level)
		{
			if(level < 1 || obj == null)
			{
				return new LinkedList<Data>();
			}

			Type type = obj.GetType();
			PropertyInfo[] propInfo = type.GetProperties();

			LinkedList<Data> reflected = new LinkedList<Data>();
			foreach(PropertyInfo prop in propInfo)
			{
				if(prop.GetIndexParameters().Length > 0)
				{
					Data temp = new Data(obj, prop.Name , "<indexed>", true);
					reflected.AddLast(temp);
				}
				else
				{
					Data temp = new Data(obj, prop.Name, prop.GetValue(obj, null), !prop.CanWrite);
					if(level > 0)
					{
						temp.Children = FindProperties(prop.GetValue(obj, null), level - 1);
					}
					reflected.AddLast(temp);
				}
			}

			return reflected;
		}
	}
}
