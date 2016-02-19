using System;

namespace Light.Data.MysqlTest
{

	[Serializable]
	[DataTable ("Te_User")]
	public partial class TeUserWithLeveAndArea : TeUser
	{
		private TeUserLevel userLevel;

		[Relation ("LevelId", "Id")]
		public TeUserLevel UserLevel {
			get {
				return userLevel;
			}
			set {
				userLevel = value;
			}
		}

		private TeAreaInfo areaInfo;

		[Relation ("Area", "Id")]
		public TeAreaInfo AreaInfo {
			get {
				return areaInfo;
			}
			set {
				areaInfo = value;
			}
		}
	}
}

