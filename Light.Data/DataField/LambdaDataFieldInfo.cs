namespace Light.Data
{
	abstract class LambdaDataFieldInfo : DataFieldInfo
	{
		protected LambdaDataFieldInfo (DataEntityMapping mapping)
			: base (mapping)
		{
		}

		protected LambdaDataFieldInfo (DataEntityMapping mapping, bool customName, string name)
			: base (mapping, customName, name)
		{
		}

		//protected LambdaDataFieldInfo (DataEntityMapping mapping, string name)
		//	: base (mapping, name)
		//{
		//}


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

