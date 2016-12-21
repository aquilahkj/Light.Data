using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_RelationCollectionTest : BaseTest
	{
		[Test ()]
		public void TestCase_Base ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUser> list;


			users = context.Query<TeUser> ().ToList ();
			levels = context.Query<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.Query<TeUserLevelWithUser> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUser lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				AssertExtend.AreObjectEqual (kvs.Value, lu.Users);
			}
		}

		[Test ()]
		public void TestCase_Base2 ()
		{
			InitialUserTable (35);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUser> list;


			users = context.Query<TeUser> ().ToList ();
			levels = context.Query<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.Query<TeUserLevelWithUser> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUser lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				AssertExtend.AreObjectEqual (kvs.Value, lu.Users2);
			}
		}

		[Test ()]
		public void TestCase_Inherit1 ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUser2> list;


			users = context.Query<TeUser> ().ToList ();
			levels = context.Query<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.Query<TeUserLevelWithUser2> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUser2 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				AssertExtend.AreObjectEqual (kvs.Value, lu.Users);
			}
		}

		[Test ()]
		public void TestCase_Inherit2 ()
		{
			InitialUserTable (35);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUser2> list;


			users = context.Query<TeUser> ().ToList ();
			levels = context.Query<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.Query<TeUserLevelWithUser2> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUser2 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				AssertExtend.AreObjectEqual (kvs.Value, lu.Users2);
			}
		}

		[Test ()]
		public void TestCase_NoEntity1 ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUser3> list;


			users = context.Query<TeUser> ().ToList ();
			levels = context.Query<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.Query<TeUserLevelWithUser3> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUser3 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				AssertExtend.AreObjectEqual (kvs.Value, lu.Users);
			}
		}

		[Test ()]
		public void TestCase_NoEntity2 ()
		{
			InitialUserTable (35);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUser3> list;


			users = context.Query<TeUser> ().ToList ();
			levels = context.Query<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.Query<TeUserLevelWithUser3> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUser3 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				AssertExtend.AreObjectEqual (kvs.Value, lu.Users2);
			}
		}
	}
}

