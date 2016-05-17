using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class RelationCollectionTest:BaseTest
	{
		[Test ()]
		public void TestCase_Base ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int,List<TeUser>> dict;
			List<TeUserLevelWithUser> list; 


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUser> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int,List<TeUser>> kvs in dict) {
				TeUserLevelWithUser lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUser> us = new List<TeUser> ();
				us.AddRange (lu.Users);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					Assert.IsTrue (EqualUser (kvs.Value [i], us [i]));
				}

			}
		}
			
		[Test ()]
		public void TestCase_Base2 ()
		{
			InitialUserTable (35);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int,List<TeUser>> dict;
			List<TeUserLevelWithUser> list; 


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUser> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int,List<TeUser>> kvs in dict) {
				TeUserLevelWithUser lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUser> us = new List<TeUser> ();
				us.AddRange (lu.Users2);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					Assert.IsTrue (EqualUser (kvs.Value [i], us [i]));
				}

			}
		}

		[Test ()]
		public void TestCase_Inherit1 ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int,List<TeUser>> dict;
			List<TeUserLevelWithUser2> list; 


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUser2> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int,List<TeUser>> kvs in dict) {
				TeUserLevelWithUser2 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUser> us = new List<TeUser> ();
				us.AddRange (lu.Users);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					Assert.IsTrue (EqualUser (kvs.Value [i], us [i]));
				}

			}
		}
			
		[Test ()]
		public void TestCase_Inherit2 ()
		{
			InitialUserTable (35);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int,List<TeUser>> dict;
			List<TeUserLevelWithUser2> list; 


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUser2> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int,List<TeUser>> kvs in dict) {
				TeUserLevelWithUser2 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUser> us = new List<TeUser> ();
				us.AddRange (lu.Users2);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					Assert.IsTrue (EqualUser (kvs.Value [i], us [i]));
				}

			}
		}

		[Test ()]
		public void TestCase_NoEntity1 ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int,List<TeUser>> dict;
			List<TeUserLevelWithUser3> list; 


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUser3> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int,List<TeUser>> kvs in dict) {
				TeUserLevelWithUser3 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUser> us = new List<TeUser> ();
				us.AddRange (lu.Users);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					Assert.IsTrue (EqualUser (kvs.Value [i], us [i]));
				}

			}
		}

		[Test ()]
		public void TestCase_NoEntity2 ()
		{
			InitialUserTable (35);
			InitialUserLevelTable (10);

			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int,List<TeUser>> dict;
			List<TeUserLevelWithUser3> list; 


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUser3> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int,List<TeUser>> kvs in dict) {
				TeUserLevelWithUser3 lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUser> us = new List<TeUser> ();
				us.AddRange (lu.Users2);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					Assert.IsTrue (EqualUser (kvs.Value [i], us [i]));
				}

			}
		}
	}
}

