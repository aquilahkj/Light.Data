using System;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[Serializable]
	[DataTable ("Te_UserLevel")]
	public partial class TeUserLevelWithUser : DataTableEntity
	{
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeUserLevel>.Create ("Id");

		static readonly DataFieldInfo levelNameField = DataFieldInfo<TeUserLevel>.Create ("LevelName");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUserLevel>.Create ("Status");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeUserLevel>.Create ("Remark");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo LevelNameField {
			get {
				return levelNameField;
			}
		}

		public static DataFieldInfo StatusField {
			get {
				return statusField;
			}
		}

		public static DataFieldInfo RemarkField {
			get {
				return remarkField;
			}
		}

		#endregion

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private string levelName;

		/// <summary>
		/// LevelName
		/// </summary>
		/// <value></value>
		[DataField ("LevelName")]
		public string LevelName {
			get { 
				return this.levelName; 
			}
			set { 
				this.levelName = value; 
			}
		}

		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField ("Status")]
		public int Status {
			get { 
				return this.status; 
			}
			set { 
				this.status = value; 
			}
		}

		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField ("Remark", IsNullable = true)]
		public string Remark {
			get { 
				return this.remark; 
			}
			set { 
				this.remark = value; 
			}
		}

		#endregion

		private LCollection<TeUser> users;

		[Relation ("Id", "LevelId")]
		public LCollection<TeUser> Users {
			get {
				return users;
			}
			set {
				users = value;
			}
		}

		private ICollection<TeUser> users2;

		[Relation ("Id", "LevelId")]
		public ICollection<TeUser> Users2 {
			get {
				return users2;
			}
			set {
				users2 = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_UserLevel")]
	public partial class TeUserLevelWithUser2 : TeUserLevel
	{
		private LCollection<TeUser> users;

		[Relation ("Id", "LevelId")]
		public LCollection<TeUser> Users {
			get {
				return users;
			}
			set {
				users = value;
			}
		}

		private ICollection<TeUser> users2;

		[Relation ("Id", "LevelId")]
		public ICollection<TeUser> Users2 {
			get {
				return users2;
			}
			set {
				users2 = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_UserLevel")]
	public partial class TeUserLevelWithUser3
	{
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeUserLevel>.Create ("Id");

		static readonly DataFieldInfo levelNameField = DataFieldInfo<TeUserLevel>.Create ("LevelName");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUserLevel>.Create ("Status");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeUserLevel>.Create ("Remark");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo LevelNameField {
			get {
				return levelNameField;
			}
		}

		public static DataFieldInfo StatusField {
			get {
				return statusField;
			}
		}

		public static DataFieldInfo RemarkField {
			get {
				return remarkField;
			}
		}

		#endregion

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private string levelName;

		/// <summary>
		/// LevelName
		/// </summary>
		/// <value></value>
		[DataField ("LevelName")]
		public string LevelName {
			get { 
				return this.levelName; 
			}
			set { 
				this.levelName = value; 
			}
		}

		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField ("Status")]
		public int Status {
			get { 
				return this.status; 
			}
			set { 
				this.status = value; 
			}
		}

		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField ("Remark", IsNullable = true)]
		public string Remark {
			get { 
				return this.remark; 
			}
			set { 
				this.remark = value; 
			}
		}

		#endregion

		private LCollection<TeUser> users;

		[Relation ("Id", "LevelId")]
		public LCollection<TeUser> Users {
			get {
				return users;
			}
			set {
				users = value;
			}
		}

		private ICollection<TeUser> users2;

		[Relation ("Id", "LevelId")]
		public ICollection<TeUser> Users2 {
			get {
				return users2;
			}
			set {
				users2 = value;
			}
		}
	}
}

