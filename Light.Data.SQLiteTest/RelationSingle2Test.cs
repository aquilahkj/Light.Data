using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class RelationSingle2Test:BaseTest
	{
		[Test ()]
		public void TestCase_Base ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

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

		[Test ()]
		public void TestCase_Inherit ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel5> list; 
			Dictionary<int,List<TeUserWithLevel5>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel5>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel5> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel5 lu = list.Find (x => x.Id == kvs.Key.Id);
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
			foreach (KeyValuePair<int,List<TeUserWithLevel5>> kvs in dict1) {
				List<TeUserWithLevel5> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreNotSame (ul, listlv [j].UserLevel);
						Assert.AreEqual (ul.Id, listlv [j].UserLevel.Id);
					}
				}
			}
		}

		[Test ()]
		public void TestCase_NoEntity ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel6> list; 
			Dictionary<int,List<TeUserWithLevel6>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel6>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel6> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel6 lu = list.Find (x => x.Id == kvs.Key.Id);
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
			foreach (KeyValuePair<int,List<TeUserWithLevel6>> kvs in dict1) {
				List<TeUserWithLevel6> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreNotSame (ul, listlv [j].UserLevel);
						Assert.AreEqual (ul.Id, listlv [j].UserLevel.Id);
					}
				}
			}
		}
	
		[Test ()]
		public void TestCase_Inherit_Multi ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);
			InitialAreaInfoTable (21);

			List<TeUser> users;
			List<TeUserLevel> levels;
			List<TeAreaInfo> areas;
			Dictionary<TeUser,TeUserLevel> dictLevel;
			Dictionary<TeUser,TeAreaInfo> dictArea;
			List<TeUserWithLeveAndArea> list; 
			Dictionary<int,List<TeUserWithLeveAndArea>> dict1;
			Dictionary<int,List<TeUserWithLeveAndArea>> dict2;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			areas = context.LQuery<TeAreaInfo> ().ToList ();
			dictLevel = new Dictionary<TeUser,TeUserLevel> ();
			dictArea = new Dictionary<TeUser, TeAreaInfo> ();
			dict1 = new Dictionary<int, List<TeUserWithLeveAndArea>> ();
			dict2 = new Dictionary<int, List<TeUserWithLeveAndArea>> ();
			foreach (TeUser user in users) {
				dictLevel [user] = levels.Find (x => x.Id == user.LevelId);
			}
			foreach (TeUser user in users) {
				dictArea [user] = areas.Find (x => x.Id == user.Area);
			}
			list = context.LQuery<TeUserWithLeveAndArea> ().ToList ();
			Assert.AreEqual (dictLevel.Count, list.Count);
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dictLevel) {
				TeUserWithLeveAndArea lu = list.Find (x => x.Id == kvs.Key.Id);
				Assert.NotNull (lu);
				if (levels.Exists (x => x.Id == lu.LevelId)) {
					Assert.AreEqual (lu.LevelId, lu.UserLevel.Id);
					Assert.AreEqual (kvs.Value.Id, lu.UserLevel.Id);
				}
				else {
					Assert.IsNull (lu.UserLevel);
				}
			}

			foreach (KeyValuePair<TeUser,TeAreaInfo> kvs in dictArea) {
				TeUserWithLeveAndArea lu = list.Find (x => x.Id == kvs.Key.Id);
				Assert.NotNull (lu);
				if (areas.Exists (x => x.Id == lu.Area)) {
					Assert.AreEqual (lu.Area, lu.AreaInfo.Id);
					Assert.AreEqual (kvs.Value.Id, lu.AreaInfo.Id);
				}
				else {
					Assert.IsNull (lu.Area);
				}
			}

			foreach (TeUserLevel level in levels) {
				dict1 [level.Id] = list.FindAll (x => x.LevelId == level.Id);
			}
			foreach (KeyValuePair<int,List<TeUserWithLeveAndArea>> kvs in dict1) {
				List<TeUserWithLeveAndArea> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeUserLevel ul = listlv [0].UserLevel;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreNotSame (ul, listlv [j].UserLevel);
						Assert.AreEqual (ul.Id, listlv [j].UserLevel.Id);
					}
				}
			}

			foreach (TeAreaInfo area in areas) {
				dict2 [area.Id] = list.FindAll (x => x.Area == area.Id);
			}
			foreach (KeyValuePair<int,List<TeUserWithLeveAndArea>> kvs in dict2) {
				List<TeUserWithLeveAndArea> listlv = kvs.Value;
				if (listlv.Count > 0) {
					TeAreaInfo ul = listlv [0].AreaInfo;
					for (int j = 1; j < listlv.Count; j++) {
						Assert.AreNotSame (ul, listlv [j].AreaInfo);
						Assert.AreEqual (ul.Id, listlv [j].AreaInfo.Id);
					}
				}
			}
		}

		[Test ()]
		public void TestCase_Inherit_OrderBy ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (6);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<TeUser,TeUserLevel> dict;
			List<TeUserWithLevel5> list; 
			Dictionary<int,List<TeUserWithLevel5>> dict1;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<TeUser,TeUserLevel> ();
			dict1 = new Dictionary<int, List<TeUserWithLevel5>> ();
			foreach (TeUser user in users) {
				dict [user] = levels.Find (x => x.Id == user.LevelId);
			}
			list = context.LQuery<TeUserWithLevel5> ().OrderBy (TeUser.IdField.OrderByDesc ()).ToList ();

			Assert.AreEqual (dict.Count, list.Count);

			for (int i = 1; i < list.Count; i++) {
				Assert.Greater (list [i - 1].Id, list [i].Id);
			}
			foreach (KeyValuePair<TeUser,TeUserLevel> kvs in dict) {
				TeUserWithLevel5 lu = list.Find (x => x.Id == kvs.Key.Id);
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
			foreach (KeyValuePair<int,List<TeUserWithLevel5>> kvs in dict1) {
				List<TeUserWithLevel5> listlv = kvs.Value;
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

