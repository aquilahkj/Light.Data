using System;

namespace Light.Data
{
	class DataSelectFieldInfo : SelectFieldInfo
	{
		readonly DataFieldInfo info;

		public DataSelectFieldInfo (DataFieldInfo info)
		{
			if (Object.Equals (info, null))
				throw new ArgumentNullException (nameof (info));
			this.info = info;
		}

		#region implemented abstract members of SelectFieldInfo

		//internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter dataParameter)
		//{
		//	dataParameter = null;
		//	return info.CreateDataFieldSql (factory, false);
		//}

		internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter[] dataParameters)
		{
			return info.CreateDataFieldSql (factory, false, out dataParameters);
		}

		internal override DataEntityMapping TableMapping {
			get {
				return info.TableMapping;
			}
		}

		#endregion
	}
}

