using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;

namespace Light.Data.OracleTest
{
	public abstract class BaseTest
	{
		readonly protected DataContext context = null;

		readonly protected CommandOutput output = new CommandOutput ();

		public const double DELTA = 0.0001;

		protected BaseTest ()
		{
			context = DataContext.Create ("oracle");
			output.OutputFullCommand = true;
			output.UseConsoleOutput = true;
			context.SetCommanfOutput (output);
		}

		protected TeUser CreateTestUser (bool useContext)
		{
			TeUser user;
			if (useContext) {
				user = context.CreateNew<TeUser> ();
			}
			else {
				user = new TeUser ();
			}
			user.Account = "test";
			//user.Address = "testaddress";
			user.Birthday = new DateTime (2001, 10, 20);
			user.Email = "test@test.com";
			user.Gender = GenderType.Female;
			user.LevelId = 1;
			user.NickName = "nicktest";
			user.Password = "imtest";
			user.RegTime = new DateTime (2015, 12, 30, 18, 0, 0);
			user.Status = 1;
			user.Telephone = "12345678";
			user.HotRate = 1.0d;
			//user.Area = 1;
			return user;
		}

		protected TeUser2 CreateTestUser2 ()
		{

			TeUser2 user = new TeUser2 ();

			user.Account = "test";
			//user.Address = "testaddress";
			user.Birthday = new DateTime (2001, 10, 20);
			user.Email = "test@test.com";
			user.Gender = GenderType.Female;
			user.LevelId = 1;
			user.NickName = "nicktest";
			user.Password = "imtest";
			user.RegTime = new DateTime (2015, 12, 30, 18, 0, 0);
			user.Status = 1;
			user.Telephone = "12345678";
			user.HotRate = 1.0d;
			//user.Area = 1;
			return user;
		}

		protected TeDataLog CreateTestLog (bool useContext)
		{
			TeDataLog log;
			if (useContext) {
				log = context.CreateNew<TeDataLog> ();
			} else {
				log = new TeDataLog ();
			}
			log.UserId = 1;
			log.ArticleId = 10;
			log.RecordTime = new DateTime (2001, 10, 20);
			log.Status = 1;
			log.Action = 1;
			log.RequestUrl = "http://light.data/test";

			return log;
		}

		protected TeAreaInfo CreateTestAreaInfo (bool useContext)
		{
			TeAreaInfo info;
			if (useContext) {
				info = context.CreateNew<TeAreaInfo> ();
			} else {
				info = new TeAreaInfo ();
			}
			info.Name = "area";
			info.V1 = 1;
			info.V2 = 2;
			info.V3 = 3;
			return info;
		}

		protected List<TeUser> InitialUserTable (int count, bool insert = true)
		{
			output.UseConsoleOutput = false;
			context.TruncateTable<TeUser> ();
			List<TeUser> lists = new List<TeUser> ();
			for (int i = 1; i <= count; i++) {
				TeUser userInsert = CreateTestUser (false);
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddMinutes (i * 300);
				userInsert.Gender = i % 2 == 0 ? GenderType.Male : GenderType.Female;
				userInsert.LevelId = i % 10 == 0 ? 10 : i % 10;
				userInsert.HotRate = 1 + i * 0.01d;
				userInsert.DeleteFlag = i % 2 == 0;
				userInsert.LoginTimes = i % 6 == 0 ? 6 : i % 6;
				userInsert.Address = i % 2 == 0 ? "addr" + userInsert.Account : null;

				if (i % 2 == 0) {
					userInsert.LastLoginTime = userInsert.RegTime.AddMinutes (i);
					userInsert.Area = i % 10 == 0 ? 10 : i % 10;
					userInsert.RefereeId = i % 6;
					userInsert.CheckPoint = i * 0.01d;
					userInsert.DeleteFlag = true;
					userInsert.CheckStatus = true;
					userInsert.CheckLevelType = CheckLevelType.Low;
					userInsert.Mark = i % 5;
				}
				if (i % 3 == 0) {
					userInsert.CheckLevelType = CheckLevelType.Normal;
					userInsert.Mark = i % 5 * -1;
				}
				if (i % 5 == 0) {
					userInsert.CheckLevelType = CheckLevelType.High;
				}
				lists.Add (userInsert);
			}
			if (insert) {
				context.BulkInsert (lists.ToArray ());
			}
			output.UseConsoleOutput = true;
			return lists;
		}

		protected List<TeUserExtend> InitialUserExtendTable (int count, bool insert = true)
		{
			output.UseConsoleOutput = false;
			context.TruncateTable<TeUserExtend> ();
			List<TeUserExtend> lists = new List<TeUserExtend> ();
			for (int i = 1; i <= count; i++) {
				TeUserExtend userInsert = new TeUserExtend ();

				userInsert.UserId = i;

				if (i % 2 == 0) {
					userInsert.Extend1 = (i % 2 == 0 ? 2 : i % 2).ToString ();
				}
				if (i % 3 == 0) {
					userInsert.Extend1 = (i % 3 == 0 ? 3 : i % 3).ToString ();
				}
				if (i % 5 == 0) {
					userInsert.Extend1 = (i % 5 == 0 ? 5 : i % 5).ToString ();
				}
				userInsert.ExtendAreaId = i;
				lists.Add (userInsert);
			}
			if (insert) {
				context.BulkInsert (lists.ToArray ());
			}
			output.UseConsoleOutput = true;
			return lists;
		}

		protected List<TeUserLevel> InitialUserLevelTable (int count, bool insert = true)
		{
			output.UseConsoleOutput = false;
			context.TruncateTable<TeUserLevel> ();
			List<TeUserLevel> lists = new List<TeUserLevel> ();
			for (int i = 1; i <= count; i++) {
				TeUserLevel level = new TeUserLevel ();
				level.Id = i;
				level.LevelName = "level" + i;
				//				if (i % 2 == 0) {
				//					level.Status = 0;
				//				}
				//				else {
				//					level.Status = 1;
				//				}
				level.Status = i % 6 == 0 ? 6 : i % 6;
				lists.Add (level);
			}
			if (insert) {
				context.BulkInsert (lists.ToArray ());	
			}
			output.UseConsoleOutput = true;
			return lists;
		}

		protected List<TeDataLog> InitialDataLogTable (int count, bool insert = true)
		{
			output.UseConsoleOutput = false;
			context.TruncateTable<TeDataLog> ();
			List<TeDataLog> lists = new List<TeDataLog> ();
			for (int i = 1; i <= count; i++) {
				TeDataLog logInsert = CreateTestLog (false);
				logInsert.UserId = i % 20 == 0 ? 20 : i % 20;
				logInsert.RecordTime = logInsert.RecordTime.AddMinutes (i * 300);
				logInsert.Action = i % 10 == 0 ? 10 : i % 10;
				logInsert.RequestUrl += ("?id=" + i);

				if (i % 2 == 0) {
					logInsert.CheckTime = logInsert.RecordTime.AddMinutes (i);
					logInsert.Status = 1;
					logInsert.CheckPoint = i * 0.01d;
					logInsert.CheckData = "eeeee";
				}
				if (i % 3 == 0) {
					logInsert.CheckId = i % 7 == 0 ? 7 : i % 7;
				}

				if (i % 5 == 0) {
					logInsert.CheckData = "bbb";
				}

				lists.Add (logInsert);
			}
			if (insert) {
				context.BulkInsert (lists.ToArray ());
			}
			output.UseConsoleOutput = true;
			return lists;
		}

		protected List<TeAreaInfo> InitialAreaInfoTable (int count, bool insert = true)
		{
			output.UseConsoleOutput = false;
			context.TruncateTable<TeAreaInfo> ();
			List<TeAreaInfo> lists = new List<TeAreaInfo> ();
			for (int i = 1; i <= count; i++) {
				TeAreaInfo infoInsert = CreateTestAreaInfo (false);
				infoInsert.Name += i;

				if (i % 2 == 0) {
					infoInsert.V1 = i % 8 == 0 ? 8 : i % 8;
				}
				if (i % 3 == 0) {
					infoInsert.V2 = i % 7 == 0 ? 7 : i % 7;
				}

				if (i % 5 == 0) {
					infoInsert.V3 = i % 5 == 0 ? 5 : i % 5;
				}

				lists.Add (infoInsert);
			}
			if (insert) {
				context.BulkInsert (lists.ToArray ());
			}
			output.UseConsoleOutput = true;
			return lists;
		}

		protected void InitialRelateTable (int count)
		{
			output.UseConsoleOutput = false;
			context.TruncateTable<TeRelateA> ();
			context.TruncateTable<TeRelateB> ();
			context.TruncateTable<TeRelateC> ();
			context.TruncateTable<TeRelateD> ();
			context.TruncateTable<TeRelateE> ();
			context.TruncateTable<TeRelateF> ();

			List<TeRelateA> lista = new List<TeRelateA> ();
			List<TeRelateB> listb = new List<TeRelateB> ();
			List<TeRelateC> listc = new List<TeRelateC> ();
			List<TeRelateD> listd = new List<TeRelateD> ();
			List<TeRelateE> liste = new List<TeRelateE> ();
			List<TeRelateF> listf = new List<TeRelateF> ();

			for (int i = 1; i <= count; i++) {
				TeRelateA itema = new TeRelateA ();
				itema.RelateBId = i;
				itema.RelateCId = i;
				itema.RelateDId = i;
				itema.RelateEId = i;
				itema.RelateFId = i;
				itema.Content = "A" + i;
				lista.Add (itema);

				TeRelateB itemb = new TeRelateB ();
				itemb.RelateAId = i;
				itemb.RelateCId = i;
				itemb.RelateDId = i;
				itemb.RelateEId = i;
				itemb.RelateFId = i;
				itemb.Content = "B" + i;
				listb.Add (itemb);

				TeRelateC itemc = new TeRelateC ();
				itemc.RelateBId = i;
				itemc.RelateAId = i;
				itemc.RelateDId = i;
				itemc.RelateEId = i;
				itemc.RelateFId = i;
				itemc.Content = "C" + i;
				listc.Add (itemc);

				TeRelateD itemd = new TeRelateD ();
				itemd.RelateBId = i;
				itemd.RelateCId = i;
				itemd.RelateAId = i;
				itemd.RelateEId = i;
				itemd.RelateFId = i;
				itemd.Content = "D" + i;
				listd.Add (itemd);

				TeRelateE iteme = new TeRelateE ();
				iteme.RelateBId = i;
				iteme.RelateCId = i;
				iteme.RelateDId = i;
				iteme.RelateAId = i;
				iteme.RelateFId = i;
				iteme.Content = "A" + i;
				liste.Add (iteme);

				TeRelateF itemf = new TeRelateF ();
				itemf.RelateBId = i;
				itemf.RelateCId = i;
				itemf.RelateDId = i;
				itemf.RelateEId = i;
				itemf.RelateAId = i;
				itemf.Content = "F" + i;
				listf.Add (itemf);
			}

			context.BulkInsert (lista.ToArray ());
			context.BulkInsert (listb.ToArray ());
			context.BulkInsert (listc.ToArray ());
			context.BulkInsert (listd.ToArray ());
			context.BulkInsert (liste.ToArray ());
			context.BulkInsert (listf.ToArray ());
			output.UseConsoleOutput = true;
		}

		protected DateTime GetNow ()
		{
			DateTime now = DateTime.Now;
			DateTime d = new DateTime (now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			return d;
		}
	}
}

