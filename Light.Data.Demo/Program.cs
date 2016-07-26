using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Light.Data.Demo
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			Test ();

			DataContext context = DataContext.Create ("sqlite");
			CommandOutput output = new CommandOutput ();
			output.UseConsoleOutput = true;
			output.OutputFullCommand = true;

			context.SetCommanfOutput (output);


			//			MqDeviceInfo info = context.CreateNew<MqDeviceInfo> ();
			//			info.Imei = "00";
			//			info.Flag = "ww";
			//			info.RegisterTime = DateTime.Now;
			//			info.Status = 0;
			//			info.Valid = 1;
			//			info.Save ();

			//List<TeUser> users = context.LQuery<TeUser> ().Where (x => x.Address + 1 == "dd").ToList ();

			//string d = string.Concat (null);

			//List<TeUser> users = context.LQuery<TeUser> ().Where (x => x.Gender == GenderType.Female && Math.Abs (x.Id) * 2 + TestInt () + 2 + x.Id + 1 >= x.Id + 2 && x.CheckPoint < 8 && x.Address + 1 == "dd" && x.DeleteFlag && x.Email.StartsWith ("aaa") && x.NickName.Substring (1, 3) == "rr").ToList ();
			string [] arr = new [] { "1", "2", "3" };
			List<string> dd = new List<string> ();
			dd.AddRange (arr);
			List<TeUser> users2 = context.LQuery<TeUser> ()
 			 .Where (x => dd.Contains (x.Account) && !dd.Contains (x.Address) && string.Concat (x.RegTime.Year, x.Account, "00", 11) == "" && x.Address.IndexOf ('a') == 2).ToList ();

			Console.ReadLine ();
		}

		public static int TestInt ()
		{
			return 10;
		}

		public static void Test ()
		{
			//ConstantExpression _consNum = System.Linq.Expressions.Expression.Constant (5, typeof (int));
			//UnaryExpression _unaryPlus = System.Linq.Expressions.Expression.Decrement (_consNum);
			//Expression<Func<int>> _unaryLam = System.Linq.Expressions.Expression.Lambda<Func<int>> (_unaryPlus);
			//Console.WriteLine ("表达式：  " + _unaryLam);
			//Console.WriteLine (_unaryLam.Compile () ());



		}
	}
}
