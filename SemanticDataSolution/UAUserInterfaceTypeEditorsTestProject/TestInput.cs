using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace UAUserInterfaceTypeEditorsTestProject
{
	class TestInput
	{
		public float Number { get; set; }
		public string Text { get; set; }
		public Stopwatch Watch { get; set; }

		public TestInput()
		{
			Number = 15;
			Text = "Puszek";
			Watch = new Stopwatch();
			Watch.Start();
		}

		public override string ToString()
		{
			return "[" + Number + ", " + Text + "]";
		}
	}
}
