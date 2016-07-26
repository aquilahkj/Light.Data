using System;
namespace Light.Data
{
	abstract class LambdaDataFieldInfo : ExtendDataFieldInfo
	{
		public LambdaDataFieldInfo (DataFieldInfo info)
			: base (info)
		{
		}

		internal override string DBType {
			get {
				return string.Empty;
			}
		}

		internal override object ToParameter (object value)
		{
			return value;
		}

	}
}

