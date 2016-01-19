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

		public TeUser CreateTestUser (bool useContext)
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
			user.RegTime = new DateTime (2016, 1, 1, 18, 0, 0);
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
				userInsert.RegTime = userInsert.RegTime.AddMinutes (i);
				userInsert.Gender = i % 2 == 0 ? GenderType.Male : GenderType.Female;
				userInsert.LevelId = i % 10 == 0 ? 10 : i % 10;
				userInsert.HotRate = 1 + i * 0.01d;
				userInsert.DeleteFlag = i % 2 == 0;

				userInsert.Address = i % 2 == 0 ? "addr" + userInsert.Account : null;

				if (i % 2 == 0) {
					userInsert.LastLoginTime = userInsert.RegTime.AddMinutes (i);
					userInsert.Area = i;
					userInsert.RefereeId = i % 6;
					userInsert.CheckPoint = i * 0.01d;
					userInsert.DeleteFlag = true;
					userInsert.CheckStatus = true;
					userInsert.CheckLevelType = CheckLevelType.Low;
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
				if (i % 2 == 0) {
					level.Status = 0;
				}
				else {
					level.Status = 1;
				}
				lists.Add (level);
			}
			if (insert) {
				context.BulkInsert (lists.ToArray ());	
			}
			return lists;
		}

		public bool EqualUser (TeUser user1, TeUser user2, bool checkId = true)
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
				user1.HotRate == user2.HotRate;
			if (checkId) {
				ret = ret && (user1.Id == user2.Id);
			}
			return ret;
		}

		protected DateTime GetNow ()
		{
			DateTime now = DateTime.Now;
			DateTime d = new DateTime (now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			return d;
		}

	}
}

