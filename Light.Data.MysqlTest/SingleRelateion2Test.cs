using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class SingleRelateion2Test:BaseTest
	{
		[Test ()]
		public void TestCase_Base ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel4> list; 
			Dictionary<int,List<TeUserWithLevel4>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel4>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel4> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel4 lu = list.Find (x => x.Id == kvs.Key.Id);
				Assert.NotNull (lu);
				Assert.AreEqual (lu.LevelId, lu.UserLevel.Id);
				Assert.AreEqual (kvs.Value.Id, lu.UserLevel.Id);
			}

			foreach (TeUserLevel level in levels) {
				dict1 [level.Id] = list.FindAll (x => x.LevelId == level.Id);
			}
			foreach (KeyValuePair<int,List<TeUserWithLevel4>> kvs in dict1) {
				List<TeUserWithLevel4> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreNotSame (ul, listlv [j].UserLevel);
						Assert.AreEqual (ul.Id, listlv [j].UserLevel.Id);
					}
				}
			}
		}

	}
}

