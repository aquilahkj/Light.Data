using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.DataField
{
	class DatePartDataFieldInfo : ExtendDataFieldInfo
	{
		DatePart _part;

		internal DatePartDataFieldInfo (DataFieldInfo info, DatePart part)
			: base (info)
		{
			_part = part;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			string sql = null;
			switch (_part) {
				case DatePart.Year:
					sql = factory.CreateYearSql (field);
					break;
				case DatePart.Month:
					sql = factory.CreateMonthSql (field);
					break;
				case DatePart.Day:
					sql = factory.CreateDaySql (field);
					break;
				case DatePart.Hour:
					sql = factory.CreateHourSql (field);
					break;
				case DatePart.Minute:
					sql = factory.CreateMinuteSql (field);
					break;
				case DatePart.Second:
					sql = factory.CreateSecondSql (field);
					break;
				case DatePart.Week:
					sql = factory.CreateWeekSql (field);
					break;
				case DatePart.WeekDay:
					sql = factory.CreateWeekDaySql (field);
					break;
			}
			return sql;
		}

		internal override string DBType {
			get {
				return string.Empty;
			}
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				DatePartDataFieldInfo target = info as DatePartDataFieldInfo;
				return this._part == target._part;
			}
			else {
				return false;
			}
		}
	}
}
