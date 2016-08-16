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

			DataContext context = DataContext.Create ("mysql");
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
			//DateTimeFormater formater = new DateTimeFormater ();
			//formater.YearFormat = "%y";
			//formater.MonthFormat = "%M";
			//formater.DayFormat = "%d";
			//formater.HourFormat = "%H";
			//formater.MinuteFormat = "%m";
			//formater.SecondFormat = "%s";
			//string result = formater.FormatData ("MM/yyyy/dd HH:mm:ss");

			string [] arr = new [] { "1", "2", "3" };
			List<string> dd = new List<string> ();
			dd.AddRange (arr);
			DateTime dt = DateTime.Now;

			//double i = 1.2;
			//dt.ToString("
			//List<TeUser> users2 = context.LQuery<TeUser> ()
			//.Where (x =>  x.RegTime.ToString () == "" && dd.Contains (x.Account) && !dd.Contains (x.Address) && string.Concat (x.RegTime.Year, x.Account, "00", 11) == "" && x.Address.IndexOf ('a') == 2).ToList ();

			//List<TeUserWithLevel2> users2 = context.LQueryable<TeUserWithLevel2> ()
			//									   .Where (x => x.UserLevel.Status == 1 && x.Id == 1)
			//									   .OrderBy (x => x.Address).ToList ();

			//var dd1 = context.LQuery<TeUser> ().Select (x => new { Id = x.Id, AId = x.Account });
			//var dd2 = context.LQuery<TeUser> ().Select (x => new { Id = x.Id, AIds = x.Account});
			//var dd3 = context.LQuery<TeUser> ().Select (x => new { Id = x.Id, AIds = x.Account });
			//var dd4 = context.LQuery<TeUser> ().Select (x => new { Id = x.Id, AIds = x.Email });

			TeUserWithLevel2 s = new TeUserWithLevel2 ();

			var ll = context.Query<TeUserWithLevel2> ().Where (x => x.UserLevel.Remark == s.Address & (x.Area > 1 ? x.Account : x.Address).Length > 10 & x.Email.Length > 0 ? x.DeleteFlag : x.Area > 0 & x.Email.Length > 0 ? x.Account.Length > 0 ? x.DeleteFlag : !x.DeleteFlag : x.CheckPoint > 0).OrderBy (x => x.Address)
						  .Select (x => new {
							  x.Id,
							  x.Account,
							  AC = x.UserLevel != null ? x.UserLevel.LevelName : string.Empty,
							  CD = x.UserLevel2
						  }).ToList ();

			var vc = context.Query<TeUser> ().Where (x => x.Id > 5)
							.LeftJoin<TeUserLevel> (x => x.Status == 1, (x, y) => x.LevelId == y.Id)
							.Where ((x, y) => y.Remark == "")
							.Join<TeUserExtend> ((x, y, z) => x.Id == z.UserId)
							.WhereWithAnd ((x, y, z) => x.CheckPoint > 0 && y.LevelName != null)
							//.Select ((x, y, z) => new SGroub {
							//	Date = x.RegTime.Date,
							//	Count = 10
							//}).ToList ();
							.Select ((x, y, z) => new {
								User = x,
								LevelName = y.LevelName,
								E1 = z.Extend1,
								E2 = z.Extend2
							}).ToList ();

			var df = context.Query<TeUser> ().GroupBy (x => new SGroub {
				Date = x.RegTime.Date,
				Count = Function.Count (x.Address, x.DeleteFlag)
			}).ToList ();

			Console.ReadLine ();
		}

		class SGroub
		{
			public DateTime Date {
				get;
				set;
			}
			public int Count {
				get;
				set;
			}
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
