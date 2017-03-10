using System;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class AggregateFunctionBase<T> : IAggregateFunction<T> where T : class
	{
		public abstract QueryExpression QueryExpression {
			get;
		}

		public abstract bool Distinct {
			get;
		}

		public abstract SafeLevel Level {
			get;
		}

		protected readonly DataContext context;

		protected AggregateFunctionBase (DataContext context)
		{
			this.context = context;
		}

		public abstract IAggregateFunction<T> SafeMode (SafeLevel level);

		public abstract IAggregateFunction<T> SetDistinct (bool distinct);

		public abstract double? Avg (Expression<Func<T, short?>> field);

		public abstract double? Avg (Expression<Func<T, int?>> field);

		public abstract double Avg (Expression<Func<T, double>> field);

		public abstract double? Avg (Expression<Func<T, long?>> field);

		public abstract double Avg (Expression<Func<T, long>> field);

		public abstract double Avg (Expression<Func<T, int>> field);

		public abstract decimal? Avg (Expression<Func<T, decimal?>> field);

		public abstract decimal Avg (Expression<Func<T, decimal>> field);

		public abstract double? Avg (Expression<Func<T, float?>> field);

		public abstract double Avg (Expression<Func<T, float>> field);

		public abstract double? Avg (Expression<Func<T, double?>> field);

		public abstract double Avg (Expression<Func<T, short>> field);

		public abstract int Count (Expression<Func<T, byte>> field);

		public abstract int Count (Expression<Func<T, sbyte>> field);

		public abstract int Count (Expression<Func<T, short>> field);

		public abstract int Count (Expression<Func<T, int>> field);

		public abstract int Count (Expression<Func<T, long>> field);

		public abstract int Count (Expression<Func<T, double>> field);

		public abstract int Count (Expression<Func<T, float>> field);

		public abstract int Count (Expression<Func<T, decimal>> field);

		public abstract int Count (Expression<Func<T, DateTime>> field);

		public abstract int Count (Expression<Func<T, DateTime?>> field);

		public abstract int Count (Expression<Func<T, decimal?>> field);

		public abstract int Count (Expression<Func<T, float?>> field);

		public abstract int Count (Expression<Func<T, double?>> field);

		public abstract int Count (Expression<Func<T, long?>> field);

		public abstract int Count (Expression<Func<T, int?>> field);

		public abstract int Count (Expression<Func<T, short?>> field);

		public abstract int Count (Expression<Func<T, sbyte?>> field);

		public abstract int Count (Expression<Func<T, byte?>> field);

		public abstract int Count (Expression<Func<T, bool?>> field);

		public abstract int Count (Expression<Func<T, bool>> field);

		public abstract int Count (Expression<Func<T, string>> field);

		public abstract long LongCount (Expression<Func<T, short?>> field);

		public abstract long LongCount (Expression<Func<T, int?>> field);

		public abstract long LongCount (Expression<Func<T, long>> field);

		public abstract long LongCount (Expression<Func<T, long?>> field);

		public abstract long LongCount (Expression<Func<T, double?>> field);

		public abstract long LongCount (Expression<Func<T, float>> field);

		public abstract long LongCount (Expression<Func<T, float?>> field);

		public abstract long LongCount (Expression<Func<T, decimal?>> field);

		public abstract long LongCount (Expression<Func<T, DateTime>> field);

		public abstract long LongCount (Expression<Func<T, DateTime?>> field);

		public abstract long LongCount (Expression<Func<T, decimal>> field);

		public abstract long LongCount (Expression<Func<T, double>> field);

		public abstract long LongCount (Expression<Func<T, int>> field);

		public abstract long LongCount (Expression<Func<T, byte?>> field);

		public abstract long LongCount (Expression<Func<T, sbyte>> field);

		public abstract long LongCount (Expression<Func<T, sbyte?>> field);

		public abstract long LongCount (Expression<Func<T, short>> field);

		public abstract long LongCount (Expression<Func<T, bool?>> field);

		public abstract long LongCount (Expression<Func<T, byte>> field);

		public abstract long LongCount (Expression<Func<T, bool>> field);

		public abstract long LongCount (Expression<Func<T, string>> field);

		public abstract long? LongSum (Expression<Func<T, int?>> field);

		public abstract long LongSum (Expression<Func<T, int>> field);

		public abstract long? LongSum (Expression<Func<T, short?>> field);

		public abstract long LongSum (Expression<Func<T, short>> field);

		public abstract float? Max (Expression<Func<T, float?>> field);

		public abstract decimal Max (Expression<Func<T, decimal>> field);

		public abstract decimal? Max (Expression<Func<T, decimal?>> field);

		public abstract DateTime? Max (Expression<Func<T, DateTime?>> field);

		public abstract DateTime Max (Expression<Func<T, DateTime>> field);

		public abstract float Max (Expression<Func<T, float>> field);

		public abstract int? Max (Expression<Func<T, int?>> field);

		public abstract long Max (Expression<Func<T, long>> field);

		public abstract long? Max (Expression<Func<T, long?>> field);

		public abstract double Max (Expression<Func<T, double>> field);

		public abstract double? Max (Expression<Func<T, double?>> field);

		public abstract short? Max (Expression<Func<T, short?>> field);

		public abstract int Max (Expression<Func<T, int>> field);

		public abstract short Max (Expression<Func<T, short>> field);

		public abstract DateTime? Min (Expression<Func<T, DateTime?>> field);

		public abstract double Min (Expression<Func<T, double>> field);

		public abstract double? Min (Expression<Func<T, double?>> field);

		public abstract float Min (Expression<Func<T, float>> field);

		public abstract float? Min (Expression<Func<T, float?>> field);

		public abstract decimal Min (Expression<Func<T, decimal>> field);

		public abstract int? Min (Expression<Func<T, int?>> field);

		public abstract long Min (Expression<Func<T, long>> field);

		public abstract long? Min (Expression<Func<T, long?>> field);

		public abstract int Min (Expression<Func<T, int>> field);

		public abstract short? Min (Expression<Func<T, short?>> field);

		public abstract decimal? Min (Expression<Func<T, decimal?>> field);

		public abstract DateTime Min (Expression<Func<T, DateTime>> field);

		public abstract short Min (Expression<Func<T, short>> field);

		public abstract double? Sum (Expression<Func<T, double?>> field);

		public abstract float? Sum (Expression<Func<T, float?>> field);

		public abstract decimal Sum (Expression<Func<T, decimal>> field);

		public abstract decimal? Sum (Expression<Func<T, decimal?>> field);

		public abstract float Sum (Expression<Func<T, float>> field);

		public abstract long Sum (Expression<Func<T, long>> field);

		public abstract long? Sum (Expression<Func<T, long?>> field);

		public abstract double Sum (Expression<Func<T, double>> field);

		public abstract int Sum (Expression<Func<T, int>> field);

		public abstract int? Sum (Expression<Func<T, int?>> field);

		public abstract int? Sum (Expression<Func<T, short?>> field);

		public abstract int Sum (Expression<Func<T, short>> field);

		public abstract int Count (Expression<Func<T, ushort>> field);

		public abstract int Count (Expression<Func<T, ushort?>> field);

		public abstract int Count (Expression<Func<T, uint>> field);

		public abstract int Count (Expression<Func<T, uint?>> field);

		public abstract int Count (Expression<Func<T, ulong>> field);

		public abstract int Count (Expression<Func<T, ulong?>> field);

		public abstract long LongCount (Expression<Func<T, ushort>> field);

		public abstract long LongCount (Expression<Func<T, ushort?>> field);

		public abstract long LongCount (Expression<Func<T, uint>> field);

		public abstract long LongCount (Expression<Func<T, uint?>> field);

		public abstract long LongCount (Expression<Func<T, ulong>> field);

		public abstract long LongCount (Expression<Func<T, ulong?>> field);

		public abstract uint Sum (Expression<Func<T, ushort>> field);

		public abstract uint? Sum (Expression<Func<T, ushort?>> field);

		public abstract ulong LongSum (Expression<Func<T, ushort>> field);

		public abstract ulong? LongSum (Expression<Func<T, ushort?>> field);

		public abstract uint Sum (Expression<Func<T, uint>> field);

		public abstract uint? Sum (Expression<Func<T, uint?>> field);

		public abstract ulong LongSum (Expression<Func<T, uint>> field);

		public abstract ulong? LongSum (Expression<Func<T, uint?>> field);

		public abstract ulong Sum (Expression<Func<T, ulong>> field);

		public abstract ulong? Sum (Expression<Func<T, ulong?>> field);

		public abstract double Avg (Expression<Func<T, ushort>> field);

		public abstract double? Avg (Expression<Func<T, ushort?>> field);

		public abstract double Avg (Expression<Func<T, ulong>> field);

		public abstract double? Avg (Expression<Func<T, ulong?>> field);

		public abstract ushort Max (Expression<Func<T, ushort>> field);

		public abstract ushort? Max (Expression<Func<T, ushort?>> field);

		public abstract uint Max (Expression<Func<T, uint>> field);

		public abstract uint? Max (Expression<Func<T, uint?>> field);

		public abstract ulong Max (Expression<Func<T, ulong>> field);

		public abstract ulong? Max (Expression<Func<T, ulong?>> field);

		public abstract ushort Min (Expression<Func<T, ushort>> field);

		public abstract ushort? Min (Expression<Func<T, ushort?>> field);

		public abstract uint Min (Expression<Func<T, uint>> field);

		public abstract uint? Min (Expression<Func<T, uint?>> field);

		public abstract ulong Min (Expression<Func<T, ulong>> field);

		public abstract ulong? Min (Expression<Func<T, ulong?>> field);

		public abstract double Avg (Expression<Func<T, uint>> field);

		public abstract double? Avg (Expression<Func<T, uint?>> field);
	}
}
