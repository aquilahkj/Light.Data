using System;
namespace Light.Data
{
	class LambdaDatePartDataFieldInfo : LambdaDataFieldInfo
	{
		DatePart _part;

		internal LambdaDatePartDataFieldInfo (DataFieldInfo info, DatePart part)
			: base (info)
		{
			_part = part;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
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
			case DatePart.DayOfWeek:
				sql = factory.CreateWeekDaySql (field);
				break;
			case DatePart.DayOfYear:
				sql = factory.CreateYearDaySql (field);
				break;
			}
			return sql;
		}
	}
}

