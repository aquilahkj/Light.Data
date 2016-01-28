using System;
using Light.Data;

namespace Light.Data.MysqlTest
{
	[AggregateTable]
	class LevelIdAgg
	{
		int levelId;

		[AggregateField("LevelId")]
		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int data;
		[AggregateField("Data")]
		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}


	}

	[AggregateTable]
	class LevelIdAggAvg
	{
		int levelId;

		[AggregateField("LevelId")]
		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		double? data;
		[AggregateField("Data")]
		public double? Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}


	}

	[AggregateTable]
	class LevelIdAggMul
	{
		int levelId;

		[AggregateField("LevelId")]
		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int count;
		[AggregateField("Count")]
		public int Count {
			get {
				return count;
			}
			set {
				count = value;
			}
		}

		double avg;
		[AggregateField("Avg")]
		public double Avg {
			get {
				return avg;
			}
			set {
				avg = value;
			}
		}
	}

	[AggregateTable]
	class LevelIdAggEnum
	{
		GenderType gender;

		[AggregateField("Gender")]
		public GenderType Gender {
			get {
				return gender;
			}
			set {
				gender = value;
			}
		}

		int data;
		[AggregateField("Data")]
		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}

	[AggregateTable]
	class LevelIdAggEnumNull
	{
		CheckLevelType? checkLevel;

		[AggregateField("CheckLevelType")]
		public CheckLevelType? CheckLevelType {
			get {
				return checkLevel;
			}
			set {
				checkLevel = value;
			}
		}

		int data;
		[AggregateField("Data")]
		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}
}

