using System;
namespace Light.Data.MysqlTest
{
	[Serializable]
	[DataTable ("Te_DataLog")]
	public partial class TeDataLogWithUser : TeDataLog
	{
		private TeUser user;

		[RelationField ("UserId", "Id")]
		public TeUser User {
			get {
				return user;
			}
			set {
				user = value;
			}
		}
	}

	public partial class TeDataLogAgg
	{
		private int userId;

		public int UserId {
			get {
				return userId;
			}

			set {
				userId = value;
			}
		}


		private int count;

		public int Count {
			get {
				return count;
			}

			set {
				count = value;
			}
		}

		private DateTime lastTime;

		public DateTime LastTime {
			get {
				return lastTime;
			}

			set {
				lastTime = value;
			}
		}
	}

	public partial class TeUserLogAgg
	{
		private int userId;

		public int UserId {
			get {
				return userId;
			}

			set {
				userId = value;
			}
		}

		private TeUser user;

		public TeUser User {
			get {
				return user;
			}

			set {
				user = value;
			}
		}

		private TeDataLogAgg logAgg;

		public TeDataLogAgg LogAgg {
			get {
				return logAgg;
			}

			set {
				logAgg = value;
			}
		}


	}

	public partial class TeUserSimpleLogAgg
	{
		private int userId;

		public int UserId {
			get {
				return userId;
			}

			set {
				userId = value;
			}
		}

		private TeUserSimple user;

		public TeUserSimple User {
			get {
				return user;
			}

			set {
				user = value;
			}
		}

		private TeDataLogAgg logAgg;

		public TeDataLogAgg LogAgg {
			get {
				return logAgg;
			}

			set {
				logAgg = value;
			}
		}
	}

	public partial class TeUserSimple
	{
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		public int Id {
			get {
				return this.id;
			}
			set {
				this.id = value;
			}
		}
		private string account;

		/// <summary>
		/// Account
		/// </summary>
		/// <value></value>
		public string Account {
			get {
				return this.account;
			}
			set {
				this.account = value;
			}
		}
		private int levelId;

		/// <summary>
		/// LevelId
		/// </summary>
		/// <value></value>
		public int LevelId {
			get {
				return this.levelId;
			}
			set {
				this.levelId = value;
			}
		}
		private DateTime regTime;

		/// <summary>
		/// RegTime
		/// </summary>
		/// <value></value>
		public DateTime RegTime {
			get {
				return this.regTime;
			}
			set {
				this.regTime = value;
			}
		}
	}
}

