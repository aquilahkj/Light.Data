using System;

namespace Light.Data.MssqlTest
{
[Serializable]
	[DataTable ("Te_RelateA")]
	public partial class TeRelateA_BE : TeRelateA
	{
		private TeRelateB_CD relateB;

		[RelationField ("Id", "RelateAId")]
		public TeRelateB_CD RelateB {
			get {
				return relateB;
			}
			set {
				relateB = value;
			}
		}

		//private TeRelateB_CD relateB1;

		//[RelationField ("Id", "RelateCId")]
		//public TeRelateB_CD RelateB1 {
		//	get {
		//		return relateB1;
		//	}
		//	set {
		//		relateB1 = value;
		//	}
		//}

		private TeRelateE_A relateE;

		[RelationField ("Id", "RelateAId")]
		public TeRelateE_A RelateE {
			get {
				return relateE;
			}
			set {
				relateE = value;
			}
		}

		//private TeRelateE_A relateE1;

		//[RelationField ("RelateCId", "RelateAId")]
		//public TeRelateE_A RelateE1 {
		//	get {
		//		return relateE1;
		//	}
		//	set {
		//		relateE1 = value;
		//	}
		//}
	}


	[Serializable]
	[DataTable ("Te_RelateB")]
	public partial class TeRelateB_CD : TeRelateB
	{
		private TeRelateC_A relateC;

		[RelationField ("RelateAId", "RelateBId")]
		public TeRelateC_A RelateC {
			get {
				return relateC;
			}
			set {
				relateC = value;
			}
		}

		private TeRelateD relateD;

		[RelationField ("Id", "RelateBId")]
		public TeRelateD RelateD {
			get {
				return relateD;
			}
			set {
				relateD = value;
			}
		}

		private TeRelateE_A relateE;

		[RelationField ("RelateAId", "RelateAId")]
		public TeRelateE_A RelateE {
			get {
				return relateE;
			}
			set {
				relateE = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_RelateC")]
	public partial class TeRelateC_A : TeRelateC
	{
		private TeRelateA_BE relateA;

		[RelationField ("RelateBId", "Id")]
		public TeRelateA_BE RelateA {
			get {
				return relateA;
			}
			set {
				relateA = value;
			}
		}
	}

	[Serializable]
	[DataTable ("Te_RelateE")]
	public partial class TeRelateE_A : TeRelateE
	{
		private TeRelateA_BE relateA;

		[RelationField ("RelateAId", "Id")]
		public TeRelateA_BE RelateA {
			get {
				return relateA;
			}
			set {
				relateA = value;
			}
		}
	}
}

