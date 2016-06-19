using System;

namespace Light.Data.OracleTest
{

	[Serializable]
	[DataTable ("Te_User")]
	public partial class TeUserWithLeveAndArea : TeUser
	{
		private TeUserLevel userLevel;

		[RelationField ("LevelId", "Id")]
		public TeUserLevel UserLevel {
			get {
				return userLevel;
			}
			set {
				userLevel = value;
			}
		}

		private TeAreaInfo areaInfo;

		[RelationField ("Area", "Id")]
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

