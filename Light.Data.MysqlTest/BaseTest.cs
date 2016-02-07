using System;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	public abstract class BaseTest
	{
		readonly protected DataContext context = null;

		protected BaseTest ()
		{
			context = DataContext.Create ("mysql");
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

		protected List<TeUser> InitialUserTable (int count, bool insert = true)
		{
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
			return lists;
		}

		protected List<TeUserLevel> InitialUserLevelTable (int count, bool insert = true)
		{
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
			return lists;
		}

		protected bool EqualUser (TeUser user1, TeUser user2, bool checkId = true)
		{
			bool ret =
				user1.Account == user2.Account &&
				user1.Address == user2.Address &&
				user1.Birthday == user2.Birthday &&
				user1.Email == user2.Email &&
				user1.Gender == user2.Gender &&
				user1.LevelId == user2.LevelId &&
				user1.NickName == user2.NickName &&
				user1.Password == user2.Password &&
				user1.RegTime == user2.RegTime &&
				user1.Status == user2.Status &&
				user1.Telephone == user2.Telephone &&
				user1.LastLoginTime == user2.LastLoginTime &&
				user1.CheckLevelType == user2.CheckLevelType &&
				user1.CheckPoint == user2.CheckPoint &&
				user1.CheckStatus == user2.CheckStatus &&
				user1.Area == user2.Area &&
				user1.DeleteFlag == user2.DeleteFlag &&
				user1.RefereeId == user2.RefereeId &&
				user1.LoginTimes == user2.LoginTimes &&
				user1.Mark == user2.Mark &&
				user1.HotRate == user2.HotRate;
			if (checkId) {
				ret = ret && (user1.Id == user2.Id);
			}
			return ret;
		}

		protected TeDataLog CreateTestLog (bool useContext)
		{
			TeDataLog log;
			if (useContext) {
				log = context.CreateNew<TeDataLog> ();
			}
			else {
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

		protected List<TeDataLog> InitialDataLogTable (int count, bool insert = true)
		{
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
			return lists;
		}

		protected bool EqualLog (TeDataLog log1, TeDataLogHistory log2, bool checkId = true)
		{
			bool ret =
				log1.UserId == log2.UserId &&
				log1.ArticleId == log2.ArticleId &&
				log1.Action == log2.Action &&
				log1.RecordTime == log2.RecordTime &&
				log1.RequestUrl == log2.RequestUrl &&
				log1.CheckPoint == log2.CheckPoint &&
				log1.CheckData == log2.CheckData &&
				log1.CheckTime == log2.CheckTime &&
				log1.CheckId == log2.CheckId &&
				log1.CheckLevelTypeInt == log2.CheckLevelTypeInt &&
				log1.CheckLevelTypeString == log2.CheckLevelTypeString;
			if (checkId) {
				ret = ret && (log1.Id == log2.Id);
			}
			return ret;
		}

		protected bool EqualLog (TeDataLog log1, TeDataLogHistory2 log2, bool checkId = true)
		{
			bool ret =
				log1.UserId == log2.UserId &&
				log1.ArticleId == log2.ArticleId &&
				log1.Action == log2.Action &&
				log1.RecordTime == log2.RecordTime &&
				log1.RequestUrl == log2.RequestUrl &&
				log1.CheckPoint == log2.CheckPoint &&
				log1.CheckData == log2.CheckData &&
				log1.CheckTime == log2.CheckTime &&
				log1.CheckId == log2.CheckId &&
				log1.CheckLevelTypeInt == log2.CheckLevelTypeInt &&
				log1.CheckLevelTypeString == log2.CheckLevelTypeString;
			if (checkId) {
				ret = ret && (log1.Id == log2.Id);
			}
			return ret;
		}

		protected TeAreaInfo CreateTestAreaInfo (bool useContext)
		{
			TeAreaInfo info;
			if (useContext) {
				info = context.CreateNew<TeAreaInfo> ();
			}
			else {
				info = new TeAreaInfo ();
			}
			info.Name = "area";
			info.V1 = 1;
			info.V2 = 2;
			info.V3 = 3;
			return info;
		}

		protected List<TeAreaInfo> InitialAreaInfoTable (int count, bool insert = true)
		{
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
			return lists;
		}

		protected bool EqualLog (TeAreaInfo info1, TeAreaInfo info2, bool checkId = true)
		{
			bool ret =
				info1.Name == info2.Name &&
				info1.V1 == info2.V1 &&
				info1.V2 == info2.V2 &&
				info1.V3 == info2.V3;
			if (checkId) {
				ret = ret && (info1.Id == info2.Id);
			}
			return ret;
		}

		protected DateTime GetNow ()
		{
			DateTime now = DateTime.Now;
			DateTime d = new DateTime (now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			return d;
		}

		protected static double FormatDouble (double d)
		{
			string cc = d.ToString ("##.####");
			double t;
			double.TryParse (cc, out t);
			return t;
		}

	}
}

