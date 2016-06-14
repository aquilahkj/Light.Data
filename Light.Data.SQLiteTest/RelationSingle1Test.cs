using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class RelationSingle1Test:BaseTest
	{
		[Test ()]
		public void TestCase_Base ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel> list; 
			Dictionary<int,List<TeUserWithLevel>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel lu = list.Find (x => x.Id == kvs.Key.Id);
				Assert.NotNull (lu);
				if (levels.Exists (x => x.Id == lu.LevelId)) {
					Assert.AreEqual (lu.LevelId, lu.UserLevel.Id);
					Assert.AreEqual (kvs.Value.Id, lu.UserLevel.Id);
				}
				else {
					Assert.IsNull (lu.UserLevel);
				}
			}

			foreach (TeUserLevel level in levels) {
				dict1 [level.Id] = list.FindAll (x => x.LevelId == level.Id);
			}
			foreach (KeyValuePair<int,List<TeUserWithLevel>> kvs in dict1) {
				List<TeUserWithLevel> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreSame (ul, listlv [j].UserLevel);
					}
				}
			}
		}
			
		[Test ()]
		public void TestCase_TestCase_Inherit ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel2> list; 
			Dictionary<int,List<TeUserWithLevel2>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel2>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel2> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel2 lu = list.Find (x => x.Id == kvs.Key.Id);
				Assert.NotNull (lu);
				if (levels.Exists (x => x.Id == lu.LevelId)) {
					Assert.AreEqual (lu.LevelId, lu.UserLevel.Id);
					Assert.AreEqual (kvs.Value.Id, lu.UserLevel.Id);
				}
				else {
					Assert.IsNull (lu.UserLevel);
				}
			}

			foreach (TeUserLevel level in levels) {
				dict1 [level.Id] = list.FindAll (x => x.LevelId == level.Id);
			}
			foreach (KeyValuePair<int,List<TeUserWithLevel2>> kvs in dict1) {
				List<TeUserWithLevel2> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreSame (ul, listlv [j].UserLevel);
					}
				}
			}
		}

		[Test ()]
		public void TestCase_TestCase_NoEntity ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel3> list; 
			Dictionary<int,List<TeUserWithLevel3>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel3>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel3> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel3 lu = list.Find (x => x.Id == kvs.Key.Id);
				Assert.NotNull (lu);
				if (levels.Exists (x => x.Id == lu.LevelId)) {
					Assert.AreEqual (lu.LevelId, lu.UserLevel.Id);
					Assert.AreEqual (kvs.Value.Id, lu.UserLevel.Id);
				}
				else {
					Assert.IsNull (lu.UserLevel);
				}
			}

			foreach (TeUserLevel level in levels) {
				dict1 [level.Id] = list.FindAll (x => x.LevelId == level.Id);
			}
			foreach (KeyValuePair<int,List<TeUserWithLevel3>> kvs in dict1) {
				List<TeUserWithLevel3> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreSame (ul, listlv [j].UserLevel);
					}
				}
			}
		}
	}
}

