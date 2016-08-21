using System;
namespace Light.Data
{
	class LambdaNewArrayDataFieldInfo : LambdaDataFieldInfo
	{
		object [] _values;

		public LambdaNewArrayDataFieldInfo (DataEntityMapping mapping, params object [] values)
			: base (mapping)
		{
			if (values == null)
				throw new ArgumentNullException (nameof (values));
			this._values = values;
		}

		//public DataFieldInfo BaseDataFieldInfo {
		//	get {
		//		return BaseFieldInfo;
		//	}
		//}

		public object [] Values {
			get {
				return _values;
			}
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			throw new NotImplementedException ();
		}
	}
}

