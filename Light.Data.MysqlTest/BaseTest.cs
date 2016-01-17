using System;

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

