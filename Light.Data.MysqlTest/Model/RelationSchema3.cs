using System;

namespace Light.Data.MysqlTest
{
	[Serializable]
	[DataTable ("Te_User")]
	public partial class TeUserWithLevelRefer : TeUser
	{
		private TeUserLevelWithUserRefer userLevel;

		[Relation ("LevelId", "Id")]
		public TeUserLevelWithUserRefer UserLevel {
			get {
				return userLevel;
			}
			set {
				userLevel = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_UserLevel")]
	public partial class TeUserLevelWithUserRefer : TeUserLevel
	{
		private LCollection<TeUserWithLevelRefer> users;

		[Relation ("Id", "LevelId")]
		public LCollection<TeUserWithLevelRefer> Users {
			get {
				return users;
			}
			set {
				users = value;
			}
		}
	}




	[Serializable]
	[DataTable ("Te_User")]
	public partial class TeUserWithExtendRefer : TeUser
	{
		private TeUserExtendWithUserRefer userExtend;

		[Relation ("Id", "UserId")]
		public TeUserExtendWithUserRefer UserExtend {
			get {
				return userExtend;
			}
			set {
				userExtend = value;
			}
		}
	}


	[Serializable]
	[DataTable ("Te_UserExtend")]
	public partial class TeUserExtendWithUserRefer :  TeUserExtend
	{
		private TeUserWithExtendRefer user;

		[Relation ("UserId", "Id")]
		public TeUserWithExtendRefer User {
			get {
				return user;
			}
			set {
				user = value;
			}
		}
	}





	[Serializable]
	[DataTable ("Te_UserExtend")]
	public partial class TeUserExtendWithUserRefer1 :  TeUserExtend
	{
		private TeUserWithExtendRefer1 user;

		[Relation ("UserId", "Id")]
		public TeUserWithExtendRefer1 User {
			get {
				return user;
			}
			set {
				user = value;
			}
		}

		private TeUser user1;

		[Relation ("UserId", "Id")]
		public TeUser User1 {
			get {
				return user1;
			}
			set {
				user1 = value;
			}
		}
	}


	[Serializable]
	[DataTable ("Te_User")]
	public partial class TeUserWithExtendRefer1 : TeUser
	{
		private TeUserExtendWithUserRefer1 userExtend;

		[Relation ("Id", "UserId")]
		public TeUserExtendWithUserRefer1 UserExtend {
			get {
				return userExtend;
			}
			set {
				userExtend = value;
			}
		}

		private TeUserExtendWithUserRefer1 userExtend1;

		[Relation ("Id", "UserId")]
		public TeUserExtendWithUserRefer1 UserExtend1 {
			get {
				return userExtend1;
			}
			set {
				userExtend1 = value;
			}
		}

		private TeUserExtendWithUserRefer1 userExtend2;

		[Relation ("LevelId", "UserId")]
		public TeUserExtendWithUserRefer1 UserExtend2 {
			get {
				return userExtend2;
			}
			set {
				userExtend2 = value;
			}
		}
	}




	[Serializable]
	[DataTable ("Te_UserExtend")]
	public partial class TeUserExtendWithUserRefer2 :  TeUserExtend
	{
		private TeUserWithExtendRefer2 user;

		[Relation ("UserId", "Id")]
		public TeUserWithExtendRefer2 User {
			get {
				return user;
			}
			set {
				user = value;
			}
		}

		private TeAreaInfoWithUserExtendrRefer2 areaInfo;

		[Relation ("ExtendAreaId", "Id")]
		public TeAreaInfoWithUserExtendrRefer2 AreaInfo {
			get {
				return areaInfo;
			}
			set {
				areaInfo = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_AreaInfo")]
	public partial class TeAreaInfoWithUserExtendrRefer2 :  TeAreaInfo
	{
		private TeUserExtendWithUserRefer2 userExtend;

		[Relation ("Id", "ExtendAreaId")]
		public TeUserExtendWithUserRefer2 UserExtend {
			get {
				return userExtend;
			}
			set {
				userExtend = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_User")]
	public partial class TeUserWithExtendRefer2 : TeUser
	{
		private TeUserExtendWithUserRefer2 userExtend;

		[Relation ("Id", "UserId")]
		public TeUserExtendWithUserRefer2 UserExtend {
			get {
				return userExtend;
			}
			set {
				userExtend = value;
			}
		}
	}
}

