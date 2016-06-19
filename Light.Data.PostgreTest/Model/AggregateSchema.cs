using System;
using Light.Data;

namespace Light.Data.PostgreTest
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

	class LevelIdAggX
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

		int sum;
		[AggregateField("Sum")]
		public int Sum {
			get {
				return sum;
			}
			set {
				sum = value;
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

		[AggregateField("Check_LevelType")]
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

	[AggregateTable]
	class RegDateAgg
	{
		DateTime regDate;

		[AggregateField("RegDate")]
		public DateTime RegDate {
			get {
				return regDate;
			}
			set {
				regDate = value;
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
	class RegTimeAgg
	{
		DateTime regTime;

		[AggregateField("regTime")]
		public DateTime RegTime {
			get {
				return regTime;
			}
			set {
				regTime = value;
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
	class StringDataAgg
	{
		string name;

		[AggregateField("Name")]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
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
	class NumDataAgg
	{
		int name;

		[AggregateField("Name")]
		public int Name {
			get {
				return name;
			}
			set {
				name = value;
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
	class WeekDataAgg
	{
		DayOfWeek name;

		[AggregateField("Name")]
		public DayOfWeek Name {
			get {
				return name;
			}
			set {
				name = value;
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

