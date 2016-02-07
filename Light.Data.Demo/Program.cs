using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Light.Data.Demo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			DataContext context = DataContext.Default;
			CommandOutput output = new CommandOutput ();
			output.OutputFullCommand = true;
			output.UseConsoleOutput = true;
			context.SetCommanfOutput (output);

			Console.ReadLine ();
		}
	}
}
