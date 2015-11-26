using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data.Demo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			DataContext context = DataContextConfiguration.Default;
			context.LQuery<MainClass> ();
			context.BulkInsert (null, batchCount: 30);
		}

		static void ReadXml ()
		{
			XmlDocument doc = new XmlDocument ();
			StringBuilder sb = new StringBuilder ();
			doc.Load ("Re.xml");
			XmlNode root = doc.SelectNodes ("root") [0];
			foreach (XmlNode node in root.ChildNodes) {
				string name = node.Attributes ["name"].Value;
				string value = node.ChildNodes [1].InnerText;
				sb.AppendFormat ("/// <summary>\n\t\t/// {0}\n\t\t/// </summary>\n\t\t", value);
				string newValue = Regex.Replace (name, "[A-Z][a-z]", x => {
					return " " + x.Value.ToLower ();
				}, RegexOptions.Compiled);
				sb.AppendFormat ("public const string {0} = \"{1}\";\n\t\t", name, newValue);
			}
			string result = sb.ToString ();
			Console.WriteLine (result);
			Console.ReadLine ();
		}
	}
}
