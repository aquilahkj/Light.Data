using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MssqlTest
{
	[TestFixture ()]
	public class LAggregateDataFieldTest:BaseTest
	{
		/// <summary>
		/// 获取该年中是第几周
		/// </summary>
		/// <param name="day">日期</param>
		/// <returns></returns>
		private static int WeekOfYear (DateTime day)
		{
			int weeknum;
			DateTime fDt = DateTime.Parse (day.Year.ToString () + "-01-01");
			int k = Convert.ToInt32 (fDt.DayOfWeek);//得到该年的第一天是周几 
			if (k == 0) {
				k = 7;
			}
			int l = Convert.ToInt32 (day.DayOfYear);//得到当天是该年的第几天 
			l = l - (7 - k + 1);
			if (l <= 0) {
				weeknum = 1;
			}
			else {
				if (l % 7 == 0) {
					weeknum = l / 7 + 1;
				}
				else {
					weeknum = l / 7 + 2;//不能整除的时候要加上前面的一周和后面的一周 
				}
			}
			return weeknum;
		}

		[Test ()]
		public void TestCase_DataField_Date ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<RegDateAgg> listEx;
			List<RegDateAgg> listAc;
			Dictionary<DateTime,RegDateAgg> dict;

			listEx = new List<RegDateAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformDate (), "RegDate")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<RegDateAgg> ();
			dict = new Dictionary<DateTime, RegDateAgg> ();

			foreach (TeUser user in list) {
				RegDateAgg i;
				if (!dict.TryGetValue (user.RegTime.Date, out i)) {
					i = new RegDateAgg ();
					i.RegDate = user.RegTime.Date;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Date] = i;
			}

			foreach (KeyValuePair<DateTime,RegDateAgg> kv in dict) {
				listEx.Add (kv.Value);
			}

			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.RegDate == x.RegDate && y.Data == x.Data)));
		}

		[Test ()]
		public void TestCase_DataField_DateTime ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<RegTimeAgg> listEx;
			List<RegTimeAgg> listAc;
			Dictionary<DateTime,RegTimeAgg> dict;

			listEx = new List<RegTimeAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformDate ())
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<RegTimeAgg> ();
			dict = new Dictionary<DateTime, RegTimeAgg> ();

			foreach (TeUser user in list) {
				RegTimeAgg i;
				if (!dict.TryGetValue (user.RegTime.Date, out i)) {
					i = new RegTimeAgg ();
					i.RegTime = user.RegTime.Date;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Date] = i;
			}

			foreach (KeyValuePair<DateTime,RegTimeAgg> kv in dict) {
				listEx.Add (kv.Value);
			}

			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.RegTime == x.RegTime && y.Data == x.Data)));
		}


		[Test ()]
		public void TestCase_DataField_DateString ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<StringDataAgg> listEx;
			List<StringDataAgg> listAc;
			Dictionary<string,StringDataAgg> dict;

			listEx = new List<StringDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformDateString (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<StringDataAgg> ();
			dict = new Dictionary<string, StringDataAgg> ();

			foreach (TeUser user in list) {
				StringDataAgg i;
				if (!dict.TryGetValue (user.RegTime.ToString ("yyyy-MM-dd"), out i)) {
					i = new StringDataAgg ();
					i.Name = user.RegTime.ToString ("yyyy-MM-dd");
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.ToString ("yyyy-MM-dd")] = i;
			}
			foreach (KeyValuePair<string,StringDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));


			Dictionary<string,string> formats = new Dictionary<string, string> ();
			//YMD|YM|Y-M-D|Y-M|D-M-Y|M-D-Y|Y/M/D|Y/M|D/M/Y|M/D/Y
			formats.Add ("YMD", "yyyyMMdd");
			formats.Add ("YM", "yyyyMM");
			formats.Add ("Y-M-D", "yyyy-MM-dd");
			formats.Add ("Y-M", "yyyy-MM");
			formats.Add ("D-M-Y", "dd-MM-yyyy");
			formats.Add ("M-D-Y", "MM-dd-yyyy");
			formats.Add ("Y/M/D", "yyyy/MM/dd");
			formats.Add ("Y/M", "yyyy/MM");
			formats.Add ("D/M/Y", "dd/MM/yyyy");
			formats.Add ("M/D/Y", "MM/dd/yyyy");
			foreach (KeyValuePair<string,string> format in formats) {
				listEx = new List<StringDataAgg> ();
				listAc = context.LAggregate<TeUser> ()
					.GroupBy (TeUser.RegTimeField.TransformDateString (format.Key), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<StringDataAgg> ();
				dict = new Dictionary<string, StringDataAgg> ();

				foreach (TeUser user in list) {
					StringDataAgg i;
					if (!dict.TryGetValue (user.RegTime.ToString (format.Value), out i)) {
						i = new StringDataAgg ();
						i.Name = user.RegTime.ToString (format.Value);
						i.Data = 0;
					}
					i.Data++;
					dict [user.RegTime.ToString (format.Value)] = i;
				}
				foreach (KeyValuePair<string,StringDataAgg> kv in dict) {
					listEx.Add (kv.Value);
				}
				Assert.AreEqual (listEx.Count, listAc.Count);
				Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));
			}

		}

		[Test ()]
		public void TestCase_DataField_DateWeek ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<WeekDataAgg> listEx;
			List<WeekDataAgg> listAc;
			Dictionary<DayOfWeek,WeekDataAgg> dict;

			listEx = new List<WeekDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformWeekDay (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<WeekDataAgg> ();
			dict = new Dictionary<DayOfWeek, WeekDataAgg> ();

			foreach (TeUser user in list) {
				WeekDataAgg i;
				DayOfWeek v = user.RegTime.DayOfWeek;
				if (!dict.TryGetValue (v, out i)) {
					i = new WeekDataAgg ();
					i.Name = v;
					i.Data = 0;
				}
				i.Data++;
				dict [v] = i;
			}
			foreach (KeyValuePair<DayOfWeek,WeekDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));
		}


		[Test ()]
		public void TestCase_DataField_DatePart ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<NumDataAgg> listEx;
			List<NumDataAgg> listAc;
			Dictionary<int,NumDataAgg> dict;

			listEx = new List<NumDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformYear (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<NumDataAgg> ();
			dict = new Dictionary<int, NumDataAgg> ();

			foreach (TeUser user in list) {
				NumDataAgg i;
				if (!dict.TryGetValue (user.RegTime.Year, out i)) {
					i = new NumDataAgg ();
					i.Name = user.RegTime.Year;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Year] = i;
			}
			foreach (KeyValuePair<int,NumDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));
		
			listEx = new List<NumDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformDay (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<NumDataAgg> ();
			dict = new Dictionary<int, NumDataAgg> ();

			foreach (TeUser user in list) {
				NumDataAgg i;
				if (!dict.TryGetValue (user.RegTime.Day, out i)) {
					i = new NumDataAgg ();
					i.Name = user.RegTime.Day;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Day] = i;
			}
			foreach (KeyValuePair<int,NumDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));

			listEx = new List<NumDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformHour (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<NumDataAgg> ();
			dict = new Dictionary<int, NumDataAgg> ();

			foreach (TeUser user in list) {
				NumDataAgg i;
				if (!dict.TryGetValue (user.RegTime.Hour, out i)) {
					i = new NumDataAgg ();
					i.Name = user.RegTime.Hour;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Hour] = i;
			}
			foreach (KeyValuePair<int,NumDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));


			listEx = new List<NumDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformMinute (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<NumDataAgg> ();
			dict = new Dictionary<int, NumDataAgg> ();

			foreach (TeUser user in list) {
				NumDataAgg i;
				if (!dict.TryGetValue (user.RegTime.Minute, out i)) {
					i = new NumDataAgg ();
					i.Name = user.RegTime.Minute;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Minute] = i;
			}
			foreach (KeyValuePair<int,NumDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));


			listEx = new List<NumDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.RegTimeField.TransformSecond (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<NumDataAgg> ();
			dict = new Dictionary<int, NumDataAgg> ();

			foreach (TeUser user in list) {
				NumDataAgg i;
				if (!dict.TryGetValue (user.RegTime.Second, out i)) {
					i = new NumDataAgg ();
					i.Name = user.RegTime.Second;
					i.Data = 0;
				}
				i.Data++;
				dict [user.RegTime.Second] = i;
			}
			foreach (KeyValuePair<int,NumDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));

		}


		[Test ()]
		public void TestCase_DataField_SubString ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<StringDataAgg> listEx;
			List<StringDataAgg> listAc;
			Dictionary<string,StringDataAgg> dict;

			listEx = new List<StringDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.AccountField.TransformSubString (0, 5), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<StringDataAgg> ();
			dict = new Dictionary<string, StringDataAgg> ();

			foreach (TeUser user in list) {
				StringDataAgg i;
				if (!dict.TryGetValue (user.Account.Substring (0, 5), out i)) {
					i = new StringDataAgg ();
					i.Name = user.Account.Substring (0, 5);
					i.Data = 0;
				}
				i.Data++;
				dict [user.Account.Substring (0, 5)] = i;
			}
			foreach (KeyValuePair<string,StringDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));
		}


		[Test ()]
		public void TestCase_DataField_StringLength ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<NumDataAgg> listEx;
			List<NumDataAgg> listAc;
			Dictionary<int,NumDataAgg> dict;

			listEx = new List<NumDataAgg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.AccountField.TransformLength (), "Name")
				.Aggregate (AggregateFunction.Count (), "Data")
				.GetObjectList<NumDataAgg> ();
			dict = new Dictionary<int, NumDataAgg> ();

			foreach (TeUser user in list) {
				NumDataAgg i;
				if (!dict.TryGetValue (user.Account.Length, out i)) {
					i = new NumDataAgg ();
					i.Name = user.Account.Length;
					i.Data = 0;
				}
				i.Data++;
				dict [user.Account.Length] = i;
			}
			foreach (KeyValuePair<int,NumDataAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.Name == x.Name && y.Data == x.Data)));
		}
	}
}

