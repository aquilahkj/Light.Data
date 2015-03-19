using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Light.Data.Mappings;

namespace Light.Data
{
    class MssqlCommandFactory : CommandFactory
    {
        public MssqlCommandFactory(Database database)
            : base(database)
        {
            _canInnerPage = true;
        }

        public override string CreateDataFieldSql(string fieldName)
        {
            return string.Format("[{0}]", fieldName);
        }

        public override string CreateDataTableSql(string tableName)
        {
            return string.Format("[{0}]", tableName);
        }

        public override string GetHavingString(DataEntityMapping mapping, AggregateHavingExpression having, out DataParameter[] parameters, Dictionary<string, AggregateFunction> aggregateFunctionDictionary)
        {
            string havingString = null;
            parameters = null;
            if (having != null)
            {
                if (!having.IgnoreConsistency && !mapping.Equals(having.TableMapping))
                {
                    throw new LightDataException(RE.DataMappingIsNotMatchAggregationExpression);
                }
                havingString = string.Format("having {0}", having.CreateSqlString(this, out parameters, new GetAliasHandler(delegate(object obj)
                {
                    return null;
                })));
            }
            return havingString;
        }

        //public override IDbCommand CreateSelectSingleFieldCommand(DataFieldMapping fieldMapping, QueryExpression query, OrderExpression order, bool distinct, Region region)
        //{
        //    if (region == null)
        //    {
        //        return base.CreateSelectSingleFieldCommand(fieldMapping, query, order, distinct, region);
        //    }


        //    if (fieldMapping is PrimitiveFieldMapping || fieldMapping is EnumFieldMapping)
        //    {
        //        DataEntityMapping mapping = fieldMapping.EntityMapping;
        //        string select = CreateDataFieldSql(fieldMapping.Name);
        //        if (distinct)
        //        {
        //            if (region == null)
        //            {
        //                select = "distinct " + select;
        //                return base.CreateSelectBaseCommand(mapping, select, query, order, region);
        //            }
        //            else
        //            {
        //                return this.CreateSelectBaseCommand(mapping, select, query, order, region, distinct);
        //            }
        //        }
        //        else
        //        {
        //            return this.CreateSelectBaseCommand(mapping, select, query, order, region);
        //        }
        //    }
        //    else
        //    {
        //        throw new LightDataException(RE.OnlyPrimitiveFieldCanSelectSingle);
        //    }
        //}

        protected override IDbCommand CreateSelectBaseCommand(DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)
        {
            if (region == null)
            {
                return base.CreateSelectBaseCommand(mapping, customSelect, query, order, null);
            }

            StringBuilder sql = new StringBuilder();
            DataParameter[] parameters;
            string queryString = GetQueryString(mapping, query, out parameters);
            string orderString = GetOrderString(mapping, order);
            bool distinct = false;
            if (customSelect.StartsWith("distinct ", StringComparison.OrdinalIgnoreCase))
            {
                distinct = true;
                customSelect = customSelect.Substring(9);
            }
            if (region.Start == 0)
            {
                sql.AppendFormat("select {3}top {2} {0} from {1}", customSelect, CreateDataTableSql(mapping.TableName), region.Size, distinct ? "distinct " : string.Empty);
                if (!string.IsNullOrEmpty(queryString))
                {
                    sql.AppendFormat(" {0}", queryString);
                }
                if (!string.IsNullOrEmpty(orderString))
                {
                    sql.AppendFormat(" {0}", orderString);
                }
            }
            else
            {
                //select *
                //from (
                //select row_number()over(order by __tc__)__rn__,*
                //from (select top 开始位置+10 0 __tc__,* from Student where Age>18 order by Age)t
                //)tt
                //where __rn__>开始位置
                StringBuilder innerSQL = new StringBuilder();
                string tempCount = CreateCustomFiledName();
                string tempRowNumber = CreateCustomFiledName();
                innerSQL.AppendFormat("select {4}top {2} {0},0 {3} from {1}", customSelect, CreateDataTableSql(mapping.TableName), region.Start + region.Size, tempCount, distinct ? "distinct " : string.Empty);
                if (!string.IsNullOrEmpty(queryString))
                {
                    innerSQL.AppendFormat(" {0}", queryString);
                }
                if (!string.IsNullOrEmpty(orderString))
                {
                    innerSQL.AppendFormat(" {0}", orderString);
                }
                sql.AppendFormat("select {1} from (select a.*,row_number()over(order by {3}) {4} from ({0})a )b where {4}>{2}",
          innerSQL, customSelect, region.Start, tempCount, tempRowNumber);
            }
            IDbCommand command = BuildCommand(sql.ToString(), parameters);
            return command;
        }

        public override string CreateAvgSql(string fieldName, bool isDistinct)
        {
            return string.Format("avg({1}convert(float,{0}))", CreateDataFieldSql(fieldName), isDistinct ? "distinct " : "");
        }

        public override string CreateConditionAvgSql(string expressionSql, string fieldName, bool isDistinct)
        {
            return string.Format("avg({2}case when {0} then convert(float,{1}) else 0 end)", expressionSql, CreateDataFieldSql(fieldName), isDistinct ? "distinct " : "");
        }

        public override string CreateMatchSql(string field, bool left, bool right)
        {
            StringBuilder sb = new StringBuilder();
            if (left)
            {
                sb.AppendFormat("'{0}'+", _wildcards);
            }
            sb.Append(field);
            if (right)
            {
                sb.AppendFormat("+'{0}'", _wildcards);
            }
            return sb.ToString();

            //StringBuilder sb = new StringBuilder();
            //if (!string.IsNullOrEmpty(leftString))
            //{
            //    sb.AppendFormat("'%'+", leftString);
            //}
            //sb.Append(field);
            //if (!string.IsNullOrEmpty(rightString))
            //{
            //    sb.AppendFormat("+'{0}'", rightString);
            //}
            //return sb.ToString();
        }

        public override string CreateDateSql(string field, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return string.Format("cast({0} as date)", field);
            }
            else
            {
                string format1 = format.ToUpper();
                string sqlformat = null;
                switch (format1)
                {
                    case "YMD":
                        sqlformat = "convert(char(8),{0},112)";
                        break;
                    case "YM":
                        sqlformat = "convert(char(6),{0},112)";
                        break;
                    case "Y-M-D":
                        sqlformat = "convert(char(10),{0},23)";
                        break;
                    case "Y-M":
                        sqlformat = "convert(char(7),{0},23)";
                        break;
                    case "M-D-Y":
                        sqlformat = "convert(char(10),{0},110)";
                        break;
                    case "D-M-Y":
                        sqlformat = "convert(char(10),{0},105)";
                        break;
                    case "Y/M/D":
                        sqlformat = "convert(char(10),{0},111)";
                        break;
                    case "Y/M":
                        sqlformat = "convert(char(7),{0},111)";
                        break;
                    case "M/D/Y":
                        sqlformat = "convert(char(10),{0},101)";
                        break;
                    case "D/M/Y":
                        sqlformat = "convert(char(10),{0},103)";
                        break;
                    default:
                        throw new LightDataException(string.Format(RE.UnsupportDateFormat, format));
                }
                return string.Format(sqlformat, field);
            }
        }

        public override string CreateYearSql(string field)
        {
            return string.Format("datepart(year,{0})", field);
        }

        public override string CreateMonthSql(string field)
        {
            return string.Format("datepart(month,{0})", field);
        }

        public override string CreateDaySql(string field)
        {
            return string.Format("datepart(day,{0})", field);
        }

        public override string CreateHourSql(string field)
        {
            return string.Format("datepart(hour,{0})", field);
        }

        public override string CreateMinuteSql(string field)
        {
            return string.Format("datepart(minute,{0})", field);
        }

        public override string CreateSecondSql(string field)
        {
            return string.Format("datepart(second,{0})", field);
        }

        public override string CreateWeekSql(string field)
        {
            return string.Format("datepart(week,{0})", field);
        }

        public override string CreateWeekDaySql(string field)
        {
            return string.Format("datepart(weekday,{0})", field);
        }

        public override string CreateLengthSql(string field)
        {
            return string.Format("len({0})", field);
        }

        public override string CreateSubStringSql(string field, int start, int size)
        {
            if (size == 0)
            {
                return string.Format("substring({0},{1},len({0})-{1}+1)", field, start);
            }
            else
            {
                return string.Format("substring({0},{1},{2})", field, start, size);
            }
        }

        public override string CreateDividedSql(string field, object value)
        {
            return string.Format("convert(float,{0})/{1}", field, value);
        }

        public override string CreatePowerSql(string field, object value)
        {
            return string.Format("power({0},{1})", field, value);
        }

    }
}


//private IDbCommand CreateSelectBaseCommand(DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region, bool distinct)
//{
//    if (region == null)
//    {
//        return base.CreateSelectBaseCommand(mapping, customSelect, query, order, null);
//    }

//    StringBuilder sql = new StringBuilder();
//    DataParameter[] parameters;
//    string queryString = GetQueryString(mapping, query, out parameters);
//    string orderString = GetOrderString(mapping, order);

//    if (region.Start == 0)
//    {
//        sql.AppendFormat("select {3}top {2} {0} from {1}", customSelect, CreateDataTableSql(mapping.TableName), region.Size, distinct ? "distinct " : string.Empty);
//        if (!string.IsNullOrEmpty(queryString))
//        {
//            sql.AppendFormat(" {0}", queryString);
//        }
//        if (!string.IsNullOrEmpty(orderString))
//        {
//            sql.AppendFormat(" {0}", orderString);
//        }
//    }
//    else
//    {
//        //select *
//        //from (
//        //select row_number()over(order by __tc__)__rn__,*
//        //from (select top 开始位置+10 0 __tc__,* from Student where Age>18 order by Age)t
//        //)tt
//        //where __rn__>开始位置
//        StringBuilder innerSQL = new StringBuilder();
//        string tempCount = CreateTempName();
//        string tempRowNumber = CreateTempName();
//        innerSQL.AppendFormat("select {4}top {2} {0},0 {3} from {1}", customSelect, CreateDataTableSql(mapping.TableName), region.Start + region.Size, tempCount, distinct ? "distinct " : string.Empty);
//        if (!string.IsNullOrEmpty(queryString))
//        {
//            innerSQL.AppendFormat(" {0}", queryString);
//        }
//        if (!string.IsNullOrEmpty(orderString))
//        {
//            innerSQL.AppendFormat(" {0}", orderString);
//        }
//        sql.AppendFormat("select a.* from (select a.*,row_number()over(order by {2}) {3} from ({0})a )b where {3}>{1}",
//  innerSQL, region.Start, tempCount, tempRowNumber);
//    }
//    IDataParameter[] dataParameters = TransferSqlStringParams(sql, parameters);
//    IDbCommand command = CreateCommand(sql.ToString(), dataParameters);
//    return command;
//}

//public override IDbCommand CreateSelectCommand(DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region)
//{
//    if (region == null)
//    {
//        return base.CreateSelectCommand(mapping, query, order, null);
//    }
//    else
//    {
//        StringBuilder select = new StringBuilder();
//        string[] names = mapping.GetFieldNames();
//        foreach (string name in names)
//        {
//            select.AppendFormat("{0},", CreateDataFieldSql(name));
//        }
//        select.Remove(select.Length - 1, 1);
//        return this.CreateSelectCommand(mapping, select.ToString(), query, order, region);
//    }

//    //StringBuilder select = new StringBuilder();
//    //StringBuilder sql = new StringBuilder();
//    //string[] names = mapping.GetFieldNames();
//    //foreach (string name in names)
//    //{
//    //    select.AppendFormat("{0},", CreateDataFieldSql(name));
//    //}
//    //select.Remove(select.Length - 1, 1);

//    //string queryString = null;
//    //DataParameter[] parameters = null;
//    //if (query != null)
//    //{
//    //    if (!query.IgnoreConsistency && !mapping.Equals(query.TableMapping))
//    //    {
//    //        throw new LightDataException(RE.DataMappingIsNotMatchQueryExpression);
//    //    }
//    //    queryString = string.Format("where {0}", query.CreateSqlString(this, out parameters));
//    //}

//    //string orderString = null;
//    //if (order != null)
//    //{
//    //    if (!order.IgnoreConsistency && !mapping.Equals(order.TableMapping))
//    //    {
//    //        throw new LightDataException(RE.DataMappingIsNotMatchOrderExpression);
//    //    }
//    //    orderString = string.Format("order by {0}", order.CreateSqlString(this));
//    //}
//    //else//使用内分页必需指定order by
//    //{
//    //    if (region.Start > 0)
//    //    {
//    //        PrimitiveFieldMapping[] primaryKeyMappings = null;
//    //        DataTableEntityMapping dte = mapping as DataTableEntityMapping;
//    //        if (dte != null)
//    //        {
//    //            primaryKeyMappings = dte.PrimaryKeyFields;
//    //        }
//    //        if (primaryKeyMappings != null && primaryKeyMappings.Length > 0)
//    //        {
//    //            StringBuilder orderBuilder = new StringBuilder();
//    //            foreach (PrimitiveFieldMapping pfm in primaryKeyMappings)
//    //            {
//    //                orderBuilder.AppendFormat("{0},", CreateDataFieldSql(pfm.Name));
//    //            }
//    //            orderBuilder.Remove(orderBuilder.Length - 1, 1);
//    //            orderString = string.Format("order by {0}", orderBuilder);
//    //        }
//    //        else
//    //        {
//    //            FieldMapping orderMapping = null;
//    //            foreach (FieldMapping fm in mapping.GetFieldMappings())
//    //            {
//    //                orderMapping = fm;
//    //                break;
//    //            }
//    //            orderString = string.Format("order by {0}", CreateDataFieldSql(orderMapping.Name));
//    //        }
//    //    }
//    //}
//    //if (region.Start > 0)
//    //{
//    //    if (queryString != null)
//    //    {
//    //        queryString = " " + queryString;
//    //    }
//    //    sql.AppendFormat("select {0} from (select row_number() over ({3}) as rn,{0} from {1}{2}) temp where rn between {4} and {5}",
//    //        select, CreateDataTableSql(mapping.TableName), queryString, orderString, region.Start + 1, region.Start + region.Size);
//    //}
//    //else
//    //{
//    //    sql.AppendFormat("select top {2} {0} from {1}", select, CreateDataTableSql(mapping.TableName), region.Size);
//    //    if (!string.IsNullOrEmpty(queryString))
//    //    {
//    //        sql.AppendFormat(" {0}", queryString);
//    //    }
//    //    if (!string.IsNullOrEmpty(orderString))
//    //    {
//    //        sql.AppendFormat(" {0}", orderString);
//    //    }
//    //}
//    ////sql.Append(";");
//    //IDataParameter[] dataParameters = DealSqlStringParams(sql, parameters);
//    //IDbCommand command = _database.CreateCommand(sql.ToString());
//    //command.CommandType = CommandType.Text;
//    //foreach (IDataParameter param in dataParameters)
//    //{
//    //    command.Parameters.Add(param);
//    //}
//    //return command;
//}
