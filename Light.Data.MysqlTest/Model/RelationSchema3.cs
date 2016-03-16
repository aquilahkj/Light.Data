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
	public partial class TeUserLevelWithUserRefer:TeUserLevel
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
}

