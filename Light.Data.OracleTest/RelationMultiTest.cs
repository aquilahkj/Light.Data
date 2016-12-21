using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.OracleTest
{
	[TestFixture ()]
	public class RelationMultiTest : BaseTest
	{
		[Test ()]
		public void TestCase_CollectionAndSingle ()
		{
			InitialUserTable (40);
			InitialUserLevelTable (10);


			List<TeUser> users;
			List<TeUserLevel> levels;
			Dictionary<int, List<TeUser>> dict;
			List<TeUserLevelWithUserRefer> list;


			users = context.LQuery<TeUser> ().ToList ();
			levels = context.LQuery<TeUserLevel> ().ToList ();
			dict = new Dictionary<int, List<TeUser>> ();
			foreach (TeUserLevel level in levels) {
				dict [level.Id] = users.FindAll (x => x.LevelId == level.Id);
			}
			list = context.LQuery<TeUserLevelWithUserRefer> ().ToList ();
			Assert.AreEqual (dict.Count, list.Count);
			foreach (KeyValuePair<int, List<TeUser>> kvs in dict) {
				TeUserLevelWithUserRefer lu = list.Find (x => x.Id == kvs.Key);
				Assert.NotNull (lu);
				List<TeUserWithLevelRefer> us = new List<TeUserWithLevelRefer> ();
				us.AddRange (lu.Users);
				Assert.AreEqual (kvs.Value.Count, us.Count);
				for (int i = 0; i < us.Count; i++) {
					AssertExtend.AreTypeEqual<TeUser> (kvs.Value [i], us [i]);
					Assert.NotNull (us [i].UserLevel);
					Assert.AreEqual (lu, us [i].UserLevel);
				}

			}
		}

		[Test ()]
		public void TestCase_SingleAndSingle ()
		{
			InitialUserTable (40);
			InitialUserExtendTable (30);


			List<TeUser> users;
			List<TeUserExtend> extends;
			List<TeUserWithExtendRefer> list;


			users = context.LQuery<TeUser> ().ToList ();
			extends = context.LQuery<TeUserExtend> ().ToList ();

			list = context.LQuery<TeUserWithExtendRefer> ().ToList ();
			Assert.AreEqual (users.Count, list.Count);
			foreach (TeUser user in users) {
				TeUserExtend extend = extends.Find (x => x.UserId == user.Id);
				TeUserWithExtendRefer refer = list.Find (x => x.Id == user.Id);
				AssertExtend.AreTypeEqual<TeUser> (user, refer);
				if (extend == null) {
					Assert.IsNull (refer.UserExtend);
				}
				else {
					AssertExtend.AreTypeEqual<TeUserExtend> (extend, refer.UserExtend);
					Assert.AreEqual (refer.UserExtend.User, refer);
				}
			}
		}

		[Test ()]
		public void TestCase_SingleAndSingle1 ()
		{
			InitialUserTable (40);
			InitialUserExtendTable (30);


			List<TeUser> users;
			List<TeUserExtend> extends;
			List<TeUserWithExtendRefer1> list;


			users = context.LQuery<TeUser> ().ToList ();
			extends = context.LQuery<TeUserExtend> ().ToList ();

			list = context.LQuery<TeUserWithExtendRefer1> ().ToList ();
			Assert.AreEqual (users.Count, list.Count);
			foreach (TeUser user in users) {
				TeUserExtend extend = extends.Find (x => x.UserId == user.Id);
				TeUserWithExtendRefer1 refer = list.Find (x => x.Id == user.Id);
				AssertExtend.AreTypeEqual<TeUser> (user, refer);
				if (extend == null) {
					Assert.IsNull (refer.UserExtend);
					Assert.IsNull (refer.UserExtend1);
				}
				else {
					AssertExtend.AreTypeEqual<TeUserExtend> (extend, refer.UserExtend);
					Assert.AreNotEqual (refer.UserExtend, refer.UserExtend1);
					Assert.AreEqual (refer.UserExtend.User, refer);
					Assert.IsNotNull (refer.UserExtend.User1);
					AssertExtend.AreTypeEqual<TeUser> (user, refer.UserExtend.User1);
				}
				Assert.IsNotNull (refer.UserExtend2);
			}
		}

		[Test ()]
		public void TestCase_SingleAndSingle2 ()
		{
			InitialUserTable (40);
			InitialUserExtendTable (30);
			InitialAreaInfoTable (40);

			List<TeUser> users;
			List<TeUserExtend> extends;
			List<TeAreaInfo> areaInfos;
			List<TeUserWithExtendRefer2> list;


			users = context.LQuery<TeUser> ().ToList ();
			extends = context.LQuery<TeUserExtend> ().ToList ();
			areaInfos = context.LQuery<TeAreaInfo> ().ToList ();
			list = context.LQuery<TeUserWithExtendRefer2> ().ToList ();

			Assert.AreEqual (users.Count, list.Count);
			foreach (TeUser user in users) {
				TeUserExtend extend = extends.Find (x => x.UserId == user.Id);

				TeUserWithExtendRefer2 refer = list.Find (x => x.Id == user.Id);
				AssertExtend.AreTypeEqual<TeUser> (user, refer);
				if (extend == null) {
					Assert.IsNull (refer.UserExtend);
				}
				else {
					AssertExtend.AreTypeEqual<TeUserExtend> (extend, refer.UserExtend);
					Assert.AreEqual (refer.UserExtend.User, refer);
					TeAreaInfo areaInfo = areaInfos.Find (x => x.Id == extend.ExtendAreaId);
					if (areaInfo == null) {
						Assert.IsNull (refer.UserExtend.AreaInfo);
					}
					else {
						AssertExtend.AreTypeEqual<TeAreaInfo> (areaInfo, refer.UserExtend.AreaInfo);
						Assert.AreEqual (refer.UserExtend, refer.UserExtend.AreaInfo.UserExtend);
					}
				}
			}
		}

		[Test ()]
		public void TestCase_SingleAndSingle_r1 ()
		{
			InitialRelateTable (35);
			List<TeRelateA_BE> relateList = context.LQuery<TeRelateA_BE> ().ToList ();
			foreach (TeRelateA_BE relate in relateList) {
				//Assert.IsNull (relate.RelateB1);
				//Assert.IsNull (relate.RelateE1);
				Assert.AreEqual (relate, relate.RelateB.RelateC.RelateA);
				Assert.AreEqual (relate, relate.RelateE.RelateA);
				Assert.AreNotEqual (relate.RelateE, relate.RelateB.RelateE);
				AssertExtend.AreObjectEqual (relate.RelateE, relate.RelateB.RelateE);
			}
		}
	}
}