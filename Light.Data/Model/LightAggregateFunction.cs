using System;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightAggregateFunction<T> : AggregateFunctionBase<T> where T : class
	{
		protected QueryExpression _query;

		public override QueryExpression QueryExpression {
			get {
				return _query;
			}
		}

		protected bool _distinct;

		public override bool Distinct {
			get {
				return _distinct;
			}
		}

		protected SafeLevel _level;

		public override SafeLevel Level {
			get {
				return _level;
			}
		}

		public LightAggregateFunction (DataContext context, QueryExpression query, bool distinct, SafeLevel level)
			: base (context)
		{
			_query = query;
			_distinct = distinct;
			_level = level;
		}

		public override IAggregateFunction<T> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		public override IAggregateFunction<T> SetDistinct (bool distinct)
		{
			_distinct = distinct;
			return this;
		}

		#region Avg Function

		public override double Avg (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, long>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, ulong>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, double>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override double Avg (Expression<Func<T, float>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level) ?? 0d);
		}

		public override decimal Avg (Expression<Func<T, decimal>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal)(context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.AVG, _query, _distinct, _level) ?? 0m);
		}

		public override double? Avg (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, long?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, ulong?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, double?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override double? Avg (Expression<Func<T, float?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.AVG, _query, _distinct, _level);
		}

		public override decimal? Avg (Expression<Func<T, decimal?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal?)context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.AVG, _query, _distinct, _level);
		}


		#endregion

		#region Count Function

		public override int Count (Expression<Func<T, string>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, bool>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, byte>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, sbyte>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, long>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, ulong>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, double>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, float>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, decimal>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, DateTime>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, bool?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, byte?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, sbyte?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, long?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, ulong?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, double?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, float?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, decimal?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		public override int Count (Expression<Func<T, DateTime?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.COUNT, _query, _distinct, _level) ?? 0);
		}

		#endregion

		#region Long Count Function

		public override long LongCount (Expression<Func<T, string>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, bool>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, byte>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, sbyte>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, long>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, ulong>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, float>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, double>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, decimal>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, DateTime>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, bool?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, byte?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, sbyte?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, long?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, ulong?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, float?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, double?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, decimal?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		public override long LongCount (Expression<Func<T, DateTime?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.COUNT, _query, _distinct, _level) ?? 0L);
		}

		#endregion

		#region Sum Function

		public override int Sum (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.SUM, _query, _distinct, _level) ?? 0);
		}

		public override uint Sum (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint)(context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.SUM, _query, _distinct, _level) ?? 0u);
		}

		public override int? Sum (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int?)context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.SUM, _query, _distinct, _level);
		}

		public override uint? Sum (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint?)context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.SUM, _query, _distinct, _level);
		}

		public override int Sum (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.SUM, _query, _distinct, _level) ?? 0);
		}

		public override uint Sum (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint)(context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.SUM, _query, _distinct, _level) ?? 0u);
		}

		public override int? Sum (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int?)context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.SUM, _query, _distinct, _level);
		}

		public override uint? Sum (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint?)context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.SUM, _query, _distinct, _level);
		}

		public override long Sum (Expression<Func<T, long>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.SUM, _query, _distinct, _level) ?? 0L);
		}

		public override ulong Sum (Expression<Func<T, ulong>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong)(context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.SUM, _query, _distinct, _level) ?? 0uL);
		}

		public override long? Sum (Expression<Func<T, long?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long?)context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.SUM, _query, _distinct, _level);
		}

		public override ulong? Sum (Expression<Func<T, ulong?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong?)context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.SUM, _query, _distinct, _level);
		}

		public override double Sum (Expression<Func<T, double>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.SUM, _query, _distinct, _level) ?? 0d);
		}

		public override double? Sum (Expression<Func<T, double?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.SUM, _query, _distinct, _level);
		}

		public override float Sum (Expression<Func<T, float>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (float)(context.Aggregate (fieldInfo, TypeCode.Single, AggregateType.SUM, _query, _distinct, _level) ?? 0f);
		}

		public override float? Sum (Expression<Func<T, float?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (float?)context.Aggregate (fieldInfo, TypeCode.Single, AggregateType.SUM, _query, _distinct, _level);
		}

		public override decimal Sum (Expression<Func<T, decimal>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal)(context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.SUM, _query, _distinct, _level) ?? 0m);
		}

		public override decimal? Sum (Expression<Func<T, decimal?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal?)context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.SUM, _query, _distinct, _level);
		}

		public override long LongSum (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.SUM, _query, _distinct, _level) ?? 0L);
		}

		public override ulong LongSum (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong)(context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.SUM, _query, _distinct, _level) ?? 0L);
		}

		public override long? LongSum (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long?)context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.SUM, _query, _distinct, _level);
		}

		public override ulong? LongSum (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong?)context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.SUM, _query, _distinct, _level);
		}

		public override long LongSum (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.SUM, _query, _distinct, _level) ?? 0L);
		}

		public override ulong LongSum (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong)(context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.SUM, _query, _distinct, _level) ?? 0uL);
		}

		public override long? LongSum (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long?)context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.SUM, _query, _distinct, _level);
		}

		public override ulong? LongSum (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong?)context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.SUM, _query, _distinct, _level);
		}

		#endregion

		#region Max Function

		public override short Max (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (short)(context.Aggregate (fieldInfo, TypeCode.Int16, AggregateType.MAX, _query, _distinct, _level) ?? 0);
		}

		public override ushort Max (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ushort)(context.Aggregate (fieldInfo, TypeCode.UInt16, AggregateType.MAX, _query, _distinct, _level) ?? 0);
		}

		public override short? Max (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (short?)context.Aggregate (fieldInfo, TypeCode.Int16, AggregateType.MAX, _query, _distinct, _level);
		}

		public override ushort? Max (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ushort?)context.Aggregate (fieldInfo, TypeCode.UInt16, AggregateType.MAX, _query, _distinct, _level);
		}

		public override int Max (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.MAX, _query, _distinct, _level) ?? 0);
		}

		public override uint Max (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint)(context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.MAX, _query, _distinct, _level) ?? 0);
		}

		public override int? Max (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int?)context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.MAX, _query, _distinct, _level);
		}

		public override uint? Max (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint?)context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.MAX, _query, _distinct, _level);
		}

		public override long Max (Expression<Func<T, long>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.MAX, _query, _distinct, _level) ?? 0L);
		}

		public override ulong Max (Expression<Func<T, ulong>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong)(context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.MAX, _query, _distinct, _level) ?? 0uL);
		}

		public override long? Max (Expression<Func<T, long?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long?)context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.MAX, _query, _distinct, _level);
		}

		public override ulong? Max (Expression<Func<T, ulong?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong?)context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.MAX, _query, _distinct, _level);
		}

		public override float Max (Expression<Func<T, float>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (float)(context.Aggregate (fieldInfo, TypeCode.Single, AggregateType.MAX, _query, _distinct, _level) ?? 0f);
		}

		public override float? Max (Expression<Func<T, float?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (float)context.Aggregate (fieldInfo, TypeCode.Single, AggregateType.MAX, _query, _distinct, _level);
		}

		public override double Max (Expression<Func<T, double>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.MAX, _query, _distinct, _level) ?? 0d);
		}

		public override double? Max (Expression<Func<T, double?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.MAX, _query, _distinct, _level);
		}

		public override decimal Max (Expression<Func<T, decimal>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal)(context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.MAX, _query, _distinct, _level) ?? 0m);
		}

		public override decimal? Max (Expression<Func<T, decimal?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal?)context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.MAX, _query, _distinct, _level);
		}

		public override DateTime Max (Expression<Func<T, DateTime>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (DateTime)(context.Aggregate (fieldInfo, TypeCode.DateTime, AggregateType.MAX, _query, _distinct, _level) ?? DateTime.MinValue);
		}

		public override DateTime? Max (Expression<Func<T, DateTime?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (DateTime?)context.Aggregate (fieldInfo, TypeCode.DateTime, AggregateType.MAX, _query, _distinct, _level);
		}

		#endregion

		#region Min Function
		public override short Min (Expression<Func<T, short>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (short)(context.Aggregate (fieldInfo, TypeCode.Int16, AggregateType.MIN, _query, _distinct, _level) ?? 0);
		}

		public override ushort Min (Expression<Func<T, ushort>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ushort)(context.Aggregate (fieldInfo, TypeCode.UInt16, AggregateType.MIN, _query, _distinct, _level) ?? 0);
		}

		public override short? Min (Expression<Func<T, short?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (short?)context.Aggregate (fieldInfo, TypeCode.Int16, AggregateType.MIN, _query, _distinct, _level);
		}

		public override ushort? Min (Expression<Func<T, ushort?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ushort?)context.Aggregate (fieldInfo, TypeCode.UInt16, AggregateType.MIN, _query, _distinct, _level);
		}

		public override int Min (Expression<Func<T, int>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int)(context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.MIN, _query, _distinct, _level) ?? 0);
		}

		public override uint Min (Expression<Func<T, uint>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint)(context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.MIN, _query, _distinct, _level) ?? 0);
		}

		public override int? Min (Expression<Func<T, int?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (int?)context.Aggregate (fieldInfo, TypeCode.Int32, AggregateType.MIN, _query, _distinct, _level);
		}

		public override uint? Min (Expression<Func<T, uint?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (uint?)context.Aggregate (fieldInfo, TypeCode.UInt32, AggregateType.MIN, _query, _distinct, _level);
		}

		public override long Min (Expression<Func<T, long>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long)(context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.MIN, _query, _distinct, _level) ?? 0L);
		}

		public override ulong Min (Expression<Func<T, ulong>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong)(context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.MIN, _query, _distinct, _level) ?? 0uL);
		}

		public override long? Min (Expression<Func<T, long?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (long?)context.Aggregate (fieldInfo, TypeCode.Int64, AggregateType.MIN, _query, _distinct, _level);
		}

		public override ulong? Min (Expression<Func<T, ulong?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (ulong?)context.Aggregate (fieldInfo, TypeCode.UInt64, AggregateType.MIN, _query, _distinct, _level);
		}

		public override float Min (Expression<Func<T, float>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (float)(context.Aggregate (fieldInfo, TypeCode.Single, AggregateType.MIN, _query, _distinct, _level) ?? 0f);
		}

		public override float? Min (Expression<Func<T, float?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (float)context.Aggregate (fieldInfo, TypeCode.Single, AggregateType.MIN, _query, _distinct, _level);
		}

		public override double Min (Expression<Func<T, double>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double)(context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.MIN, _query, _distinct, _level) ?? 0d);
		}

		public override double? Min (Expression<Func<T, double?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (double?)context.Aggregate (fieldInfo, TypeCode.Double, AggregateType.MIN, _query, _distinct, _level);
		}

		public override decimal Min (Expression<Func<T, decimal>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal)(context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.MIN, _query, _distinct, _level) ?? 0m);
		}

		public override decimal? Min (Expression<Func<T, decimal?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (decimal?)context.Aggregate (fieldInfo, TypeCode.Decimal, AggregateType.MIN, _query, _distinct, _level);
		}

		public override DateTime Min (Expression<Func<T, DateTime>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (DateTime)(context.Aggregate (fieldInfo, TypeCode.DateTime, AggregateType.MIN, _query, _distinct, _level) ?? DateTime.MinValue);
		}

		public override DateTime? Min (Expression<Func<T, DateTime?>> field)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (field);
			return (DateTime?)context.Aggregate (fieldInfo, TypeCode.DateTime, AggregateType.MIN, _query, _distinct, _level);
		}

		#endregion
	}
}
