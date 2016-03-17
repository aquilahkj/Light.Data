
namespace Light.Data
{
	class MinFunction : AggregateFunction
	{
		DataFieldInfo _fieldinfo;

		internal MinFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo)
			: base (mapping)
		{
			_fieldinfo = fieldInfo;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			dataParameters = null;
			return factory.CreateMinSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName));
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if (base.EqualsDetail (function)) {
				MinFunction target = function as MinFunction;
				return this._fieldinfo.Equals (target._fieldinfo);
			}
			else {
				return false;
			}
		}
	}
}
