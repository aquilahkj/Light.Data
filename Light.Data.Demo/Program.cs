using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.IO;

namespace Light.Data.Demo
{
	class MainClass
	{
		class DemoData
		{
			DateTime? date;

			int count;

			public DateTime? Date {
				get {
					return date;
				}

				set {
					date = value;
				}
			}

			public int Count {
				get {
					return count;
				}

				set {
					count = value;
				}
			}
		}

		[Flags]
		public enum TESTENUM
		{
			TEST1 = 1,
			TEST2 = 2,
			TEST3 = 4
		}

		public static void Main (string [] args)
		{
			DataContext context = DataContext.Create ("mysql");
			CommandOutput output = new CommandOutput ();
			output.UseConsoleOutput = true;
			output.OutputFullCommand = true;

			context.SetCommanfOutput (output);



			//var mycount = context.Query<TeUser> ().AggregateFunction ().Count (x => x.Account);
			//var jgy = context.Query<TeUser> ().AggregateFunction ().Count (x => x.LastLoginTime);

			//var dsum = context.Query<TeUser> ().Where (x => x.Area == null).AggregateFunction ().Sum (x => x.Area);
			//var gsum = context.Query<TeUser> ().Where (x => x.Area == null).AggregateFunction ().Sum (x => x.Area.Value);
			//var dgg = context.Query<TeUser> ().QueryFieldList (x => x.CheckPoint);

			//var dggs = context.Query<TeUser> ().SetDistinct (true).QueryFieldList (x => x.LevelId);

			//var ter = context.Query<TeUser> ()
			//				 .Where (x => x.Account.Length == 5)
			//				 .SetDistinct (true)
			//				 .Select (x => new {
			//					 x.Id,
			//					 Accounts = x.Account,
			//					 x.LevelId
			//				 })
			//				 .Join<TeUserLevel> ((x, y) => x.LevelId == y.Id)
			//				 .Where ((x, y) => x.Accounts.EndsWith ("1"))
			//				 .SetDistinct (true)
			//				 .Select ((x, y) => new {
			//					 x.Id,
			//					 x.Accounts,
			//					 x.LevelId,
			//					 y.LevelName,
			//					 Remark = y.Remark + "eee",
			//				 })
			//.ToList ();

			//var ter1 = context.Query<TeUser> ()
			//				 .Where (x => x.Account.Length == 5)
			//				 .SetDistinct (true)
			//				 .Select (x => new {
			//					 x.Id,
			//					 Accounts = x.Account,
			//					 x.LevelId
			//				 })
			//				 .Join<TeUserLevel> ((x, y) => x.LevelId == y.Id)
			//				 .Where ((x, y) => x.Accounts.EndsWith ("1"))
			//				 .SetDistinct (true)
			//				 .SelectInsert<TeUser> ((x, y) => new TeUser () {
			//					  Account = x.Accounts,
			//					  LevelId = x.LevelId,
			//					  Email = y.LevelName,
			//					  RegTime = DateTime.Now
			//				  });


			//var logdate = context.Query<TeUser> ().GroupBy (x => new DemoData {
			//	Date = x.LastLoginTime.Value.Date,
			//	Count = Function.CountAll ()
			//}).ToList ();


			//var user = context.Query<TeUser> ().Where (x => x.Id == 1)
			//							.Select (x => new {
			//								x.Id,
			//								x.HotRate
			//							}).First ();

			//List<TeUser> users = context.Query<TeUser> ().Where (x => x.Address + 1 == "dd" && ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();

			//List<TeUser> users1 = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();

			List<TeUser> users1 = context.Query<TeUser> ().Where (x => !ExtendQuery.In<TeUserLevel, int> (x.LevelId, u => u.Id, u => u.Id > 10)).ToList ();

			List<TeUser> users2 = context.Query<TeUser> ().Where (x => ExtendQuery.GtAll<TeUserLevel, int> (x.LevelId, u => u.Id, u => u.Id > 10)).ToList ();

			//var uddf = context.Query<TeUser> ().Where (x => x.Id > 10).Select (x => x.Id);


			//var users12 = context.Query<TeUser> ().Where (x => x.Id > 2 && x.Gender == GenderType.Male)
			//				.LeftJoin<TeUserLevel> (x => x.Status == 1, (x, y) => x.LevelId == y.Id)
			//				.Join<TeUserExtend> ((x, y, z) => x.Id == z.UserId)
			//				.Where ((x, y, z) => !ExtendQuery.Exists<TeUserLevel> (u => u.Id == x.LevelId) && ExtendQuery.IsNull (y.Id) && !ExtendQuery.IsNull (z.Id) && x.CheckPoint.HasValue)
			//				.Select ((x, y, z) => new SGroub {
			//					Date = x.RegTime.Date,
			//					Count = 10
			//				}).ToList ();

			var users22 = context.Query<TeUser> ().Where (x => x.Id > 2 && x.Gender == GenderType.Male)
							.LeftJoin<TeUserLevel> (x => x.Status == 1, (x, y) => x.LevelId == y.Id)
							.Join<TeUserExtend> ((x, y, z) => x.Id == z.UserId)
								 .Where ((x, y, z) => ExtendQuery.In<TeUserLevel, int> (x.LevelId, u => u.Id, u => u.Id > x.Id))
							.Select ((x, y, z) => new SGroub {
								Date = x.RegTime.Date,
								Count = 10
							}).ToList ();

			//string [] arr = new [] { "1", "2", "3" };
			//List<string> dd = new List<string> ();
			//dd.AddRange (arr);
			//DateTime dt = DateTime.Now;

			//List<TeUserLevelWithUserRefer> lus = context.Query<TeUserLevelWithUserRefer> ().ToList ();
			//var ff = lus [0].Users;

			//foreach (var f in ff) {
			//	Console.WriteLine (f);
			//}

			//TeUserWithLevel2 s = new TeUserWithLevel2 ();

			//var ll = context.Query<TeUserWithLevel2> ().Where (x => x.UserLevel.Remark == s.Address & (x.Area > 1 ? x.Account : x.Address).Length > 10 & (x.Email.Length > 0 ?
			//												   x.DeleteFlag :
			//												   x.Area > 0 & x.Email.Length > 0
			//														 ? x.Account.Length > 0
			//														 ? x.DeleteFlag :
			//											 !x.DeleteFlag : x.CheckPoint > 0)).OrderBy (x => x.Address)
			//			  .Select (x => new {
			//				  x.Id,
			//				  x.Account,
			//				  AC = x.UserLevel != null ? x.UserLevel.LevelName : string.Empty,
			//				  CD = x.UserLevel2.Remark
			//			  }).ToList ();

			//var vc = context.Query<TeUser> ().Where (x => x.Id > 2 && x.Gender == GenderType.Male)
			//				.LeftJoin<TeUserLevel> (x => x.Status == 1, (x, y) => x.LevelId == y.Id)
			//				.Where ((x, y) => y.Remark == "")
			//				.Join<TeUserExtend> ((x, y, z) => x.Id == z.UserId)
			//				.WhereWithAnd ((x, y, z) => x.CheckPoint > 0 && y.LevelName != null)
			//				//.Select ((x, y, z) => new SGroub {
			//				//	Date = x.RegTime.Date,
			//				//	Count = 10
			//				//}).ToList ();
			//				.Select ((x, y, z) => new {
			//					User = x,
			//					LevelName = y.LevelName,
			//					E1 = z.Extend1,
			//					E2 = z.Extend2
			//				}).ToList ();


			//var vc3 = context.Query<TeUser> ().Where (x => x.Id > 2 && x.Gender == GenderType.Male)
			//	.LeftJoin<TeUserLevel> (x => x.Status == 1, (x, y) => x.LevelId == y.Id)
			//	.Where ((x, y) => y.Remark == "")
			//	.Join<TeUserExtend> ((x, y, z) => x.Id == z.UserId)
			//	.WhereWithAnd ((x, y, z) => x.CheckPoint > 0 && y.LevelName != null)
			//	.Select ((x, y, z) => x).ToList ();
			//int? r = 1;

			//var dfxx = context.Query<TeUser> ().GroupBy (x => new {
			//	Count = Function.Sum (x.LoginTimes)
			//}).First ().Count;


			//var df = context.Query<TeUser> ().GroupBy (x => new {
			//	Date = x.RegTime.Date,
			//	Count = Function.Count (),
			//	CountID = Function.DistinctCount (x.Gender == GenderType.Female ? x.Id as int? : null)
			//}).Having (x => x.Count + 10 > 11).OrderBy (x => x.Count).ToList ();

			//var dg1 = context.Query<TeDataLog> ().Insert<TeDataLogHistory> ();

			//var dg2 = context.Query<TeDataLog> ()
			//				.Where (x => x.Id > 10)
			//				.SelectInsert (x => new TeDataLogHistory {
			//					Id = x.Id,
			//					UserId = x.UserId,
			//					ArticleId = x.ArticleId,
			//					RequestUrl = string.Concat ("[", x.RequestUrl, "]"),
			//					Action = x.Action,
			//					CheckId = x.CheckId,
			//					RecordTime = DateTime.Now,
			//					Status = 0
			//				});
			//var dg3 = context.Query<TeUserWithLevel2> ()
			//				 .Where (x => x.Id > 10 && x.Address != null)
			//				 .SelectInsert (x => new TeDataLogHistory {
			//					 Id = x.Id,
			//					 UserId = x.Id,
			//					 ArticleId = x.UserLevel.Id,
			//					 RequestUrl = x.Address,
			//					 Action = x.Mark,
			//					 RecordTime = DateTime.Now,
			//					 Status = 0
			//				 });
			//var up1 = context.Query<TeUser> ().Where (x => x.Id > 10).Update (x => new TeUser {
			//	Area = null,
			//	Address = "address:" + x.Address,
			//	DeleteFlag = false
			//});

			//var fg1 = context.Query<TeUserWithLevel2> ().SetDistinct (true).QuerySingleFieldList (x => x.Area);

			//var vc0 = context.Query<TeUser> ().GroupBy (x => new {
			//	Lid = x.LevelId,
			//	Count = Function.CountAll ()
			//});

			//var vc0c = context.Query<TeUser> ().GroupBy (x => new {
			//	Lid = x.LevelId,
			//	LoginTimes = Function.Sum (x.LoginTimes)
			//});

			//var vc1 = context.Query<TeUserLevel> ()
			//				.LeftJoin (vc0, (x, y) => x.Id == y.Lid)
			//				.Where ((x, y) => y.Count >= 1)
			//				.Select ((x, y) => new {
			//					Id = x.Id,
			//					Name = x.LevelName,
			//					Count = y.Count,
			//				}).ToList ();

			//var vc2 = vc0.LeftJoin<TeUserLevel> (x => x.Id > 1, (x, y) => x.Lid == y.Id)
			//		   .Select ((x, y) => new {
			//			   Id = y.Id,
			//			   Name = y.LevelName,
			//			   Count = x.Count,
			//		   }).ToList ();

			//var vc3 = vc0.LeftJoin (vc0c, (x, y) => x.Lid == y.Lid).
			//		   Select ((x, y) => new {
			//			   Id = x.Lid,
			//			   Count = x.Count,
			//			   Sum = y.LoginTimes
			//		   }).ToList ();


			//var vg2 = context.Query<TeUser> ().LeftJoin<TeUserLevel> (x => x.Id > 1, (x, y) => x.LevelId == y.Id)
			//				 .Where ((x, y) => x.Id > 1 && x.Id < 10)
			//				 .SelectInsert ((x, y) => new TeDataLogHistory {
			//					 Id = x.Id,
			//					 UserId = x.Id,
			//					 ArticleId = y.Id,
			//					 RequestUrl = x.Address != null ? x.Address : string.Empty,
			//					 Action = x.Mark,
			//					 RecordTime = DateTime.Now,
			//					 Status = 0
			//				 });


			//var vg1 = context.Query<TeUserLevel> ()
			//				.LeftJoin (vc0, (x, y) => x.Id == y.Lid)
			//				.Where ((x, y) => y.Count >= 1)
			//				.SelectInsert ((x, y) => new TeDataLogHistory {
			//					Id = x.Id,
			//					UserId = x.Id,
			//					ArticleId = y.Lid,
			//					RequestUrl = x.LevelName,
			//					Action = y.Count,
			//					RecordTime = DateTime.Now,
			//					Status = 0
			//				});

			//var vg3 = vc0.LeftJoin (vc0c, (x, y) => x.Lid == y.Lid).
			//		   Select ((x, y) => new {
			//			   Id = x.Lid,
			//			   Count = x.Count,
			//			   Sum = y.LoginTimes
			//		   }).ToList ();

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

		class SGroub1
		{
			public int Lid {
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
